--[[
battle/stats.lua - attribute system for players and monsters (FFXIV 1.23b).

formulas in functions, and callers (player.lua onCalculateBaseStats, mob hooks,
battle/formulas.lua) stay thin.

Evidence tiers in tags: [CAP] retail 1.23b capture, [CL] client sheet, [OFF]
official notes, [TEST] player test with data, [WIKI], [CLAIM], [SPEC] inference.
DESIGN = no source covers it; we chose a value. Revisit those first.
Sources are the battle research artifacts (Research Notes, Battle Data); see
for the full write-up.

What is solid:
  * 1 VIT = 1 max HP, 1 PIE = 1 max MP, 4 INT = 1 attack magic potency,
    4 MND = 1 healing magic potency and 1 magic accuracy, 4 PIE = 1 enfeebling
    potency and 1 magic evasion, 4 VIT = 1 enhancing potency, 3 STR = 2 attack
    (hidden) [TEST BG 107403 / kanican; OFF dev post 2011-10-22; 03_community_en §A]
  * Bonus attribute points: 5 at level 10, +1 per level (45 at 50) [WIKI 2nWIKI, 05_community_jp §B]
  * Race only changes the starting offsets [OFF-JP 05_community_jp l.600]; the clan values
    themselves come from the client (stats.TRIBE) [CL]
  * HP anchors: Lv26 MRD 758 HP / VIT 102, Lv31 GLA 1016 HP / VIT 110, Lv27 GLA 750,
    Lv50 MNK 2634 [CAP player-hp-anchors, 09_gap_fill §0]
  * MP anchors: Lv26 MRD 249, Lv27 GLA 405, Lv50 MNK 1914 [CAP]; Lv50 THM ~2900 [TEST 37858]
  * Naked Lv50 attributes: PGL STR 246 / INT 242, DRG STR 255 / PIE 244, PLD MND 232 [TEST]
  * PGL Lv8 STR 35, Lv9 37 [TEST JP]
What is DESIGN: the per-level curves between anchors, class multipliers, the
automatic bonus-point split (no allocation UI yet), monster stat curves.
--]]

require("modifiers")

stats = stats or {}

-- Class ids (global.lua CLASSID_*); jobs map onto their base class.
stats.CLASS = { PGL = 2, GLA = 3, MRD = 4, ARC = 7, LNC = 8, THM = 22, CNJ = 23 }
stats.JOB_TO_CLASS = { [15] = 2, [16] = 3, [17] = 4, [18] = 7, [19] = 8, [26] = 22, [27] = 23 }

local M = modifiersGlobal

-- Piecewise-linear interpolation over {level, value} anchor pairs (sorted by level).
local function interp(anchors, level)
    if level <= anchors[1][1] then return anchors[1][2] end
    for i = 2, #anchors do
        local a, b = anchors[i - 1], anchors[i]
        if level <= b[1] then
            return a[2] + (b[2] - a[2]) * (level - a[1]) / (b[1] - a[1])
        end
    end
    -- Extrapolate past the last anchor with the last slope (monsters go above 50).
    local a, b = anchors[#anchors - 1], anchors[#anchors]
    return b[2] + (b[2] - a[2]) * (level - b[1]) / (b[1] - a[1])
end
stats.interp = interp

-- Attribute growth for a 1.0 multiplier, before bonus points.
-- Anchors: Lv8 35 / Lv9 37 (PGL STR, race offset included) [TEST JP];
-- Lv50 ~205 so that class multipliers + 45 bonus points land on the naked
-- Lv50 values 232-255 [TEST]. Everything between is DESIGN (1.20 notes say the
-- growth curve rises faster at higher levels [OFF-JP]).
stats.ATTR_CURVE = { {1, 21}, {8, 34}, {9, 36}, {20, 62}, {30, 98}, {40, 148}, {50, 205} }

-- Class attribute multipliers. Which two attributes are high follows the 1.20
-- weapon-damage pairs [OFF 1.20 notes]: PGL STR+INT, GLA STR+MND, MRD STR+VIT,
-- ARC PIE+DEX, LNC STR+PIE, THM/CNJ MND+PIE (we give THM INT as its top stat
-- because INT drives attack magic). Sizes are DESIGN.
-- primary/secondary = where automatic bonus points go.
stats.CLASS_PROFILE = {
    [2]  = { str = 1.08, vit = 0.98, dex = 1.00, int = 1.06, mnd = 0.92, pie = 0.92, primary = M.Strength,     secondary = M.Intelligence, hp = 0.98, mp = 1.00 },
    [3]  = { str = 1.06, vit = 1.04, dex = 1.00, int = 0.90, mnd = 1.06, pie = 0.92, primary = M.Strength,     secondary = M.Mind,         hp = 1.00, mp = 1.60 },
    [4]  = { str = 1.08, vit = 1.08, dex = 0.98, int = 0.90, mnd = 0.90, pie = 0.90, primary = M.Strength,     secondary = M.Vitality,     hp = 1.03, mp = 1.00 },
    [7]  = { str = 1.00, vit = 0.95, dex = 1.08, int = 0.92, mnd = 0.95, pie = 1.08, primary = M.Piety,        secondary = M.Dexterity,    hp = 0.92, mp = 1.20 },
    [8]  = { str = 1.08, vit = 0.98, dex = 1.02, int = 0.90, mnd = 0.92, pie = 1.06, primary = M.Strength,     secondary = M.Piety,        hp = 0.97, mp = 1.00 },
    [22] = { str = 0.90, vit = 0.92, dex = 0.95, int = 1.10, mnd = 1.00, pie = 1.06, primary = M.Intelligence, secondary = M.Piety,        hp = 0.82, mp = 1.60 },
    [23] = { str = 0.90, vit = 0.95, dex = 0.95, int = 1.00, mnd = 1.10, pie = 1.06, primary = M.Mind,         secondary = M.Piety,        hp = 0.85, mp = 1.60 },
}
local DEFAULT_PROFILE = { str = 1, vit = 1, dex = 1, int = 1, mnd = 1, pie = 1, primary = M.Strength, secondary = M.Vitality, hp = 1, mp = 1 }

-- Base HP for a 1.0 multiplier, VIT not included (HP = base * classHp + VIT).
-- Lv26 656 = 758 - VIT 102 (MRD) and Lv31 906 = 1016 - VIT 110 (GLA) [CAP].
-- Lv50 2100 keeps a Lv50 MNK at ~2350 before HP gear (captured 2634 with gear) [CAP, soft].
-- Lv1/10/20/40 are DESIGN.
stats.HP_CURVE = { {1, 100}, {10, 250}, {20, 470}, {26, 656}, {31, 906}, {40, 1400}, {50, 2100} }

-- Base MP for a 1.0 multiplier, PIE not included (MP = base * classMp + PIE).
-- MRD Lv26 249 - PIE ~77 -> ~172 [CAP]; GLA Lv27 405 gives GLA x1.6 [CAP];
-- Lv50 MNK 1914 [CAP]; THM Lv50 ~2900 [TEST t-37858]. "MP grows slowly early and
-- faster from ~Lv35" [OFF-JP 1.20]. Low-level anchors are DESIGN.
stats.MP_CURVE = { {1, 30}, {10, 70}, {20, 130}, {26, 172}, {30, 230}, {40, 700}, {50, 1650} }

-- Displayed level bases for derived stats (what a naked character shows).
-- Attack: PGL Lv8 74 / Lv9 77 naked, STR not shown in it [TEST JP]; Lv50 DESIGN.
stats.ATK_BASE_CURVE = { {1, 53}, {8, 74}, {9, 77}, {20, 115}, {30, 170}, {40, 250}, {50, 340} }
-- Healing magic potency base: a Lv27 GLA cross-class Cure healed 151-166 [CAP]; with
-- 1.10 HP per potency (non-CNJ) [TEST kanican] that needs ~130-150 potency at Lv27.
-- 4.8 per level (DESIGN fit) gives 130 + MND/4.
stats.MAGIC_BASE_PER_LEVEL = 4.8
-- Expected gear DEF for a level (starter set ~30 at Lv1 [server item data], ~1000 at
-- Lv50 [TEST kanican III / BG: "max gear DEF ~1050", GLA "1,000 Defense"]).
stats.GEAR_DEF_CURVE = { {1, 30}, {50, 1000} }

-- Bonus attribute points: 5 at Lv10, +1 per level -> 45 at 50 [WIKI 2nWIKI].
function stats.bonusPoints(level)
    if level < 10 then return 0 end
    return 5 + (level - 10)
end

-- Per-attribute allotment cap: 3 at Lv10, +1 per 2 levels -> 23 at Lv50 [OFF 1.20 dev1228 table].
function stats.bonusPointCap(level)
    if level < 10 then return 0 end
    return 3 + math.floor((level - 10) / 2)
end

-- Automatic allotment until players can allot (DESIGN): ceil/floor half to the class's primary and
-- secondary attribute, each clamped to the per-attribute cap; leftovers go to VIT then the next
-- stats. Returns { [modifier] = points }.
function stats.bonusAllocation(classId, level)
    local p = stats.profile(classId)
    local bp, cap = stats.bonusPoints(level), stats.bonusPointCap(level)
    local out = { [M.Strength] = 0, [M.Vitality] = 0, [M.Dexterity] = 0,
                  [M.Intelligence] = 0, [M.Mind] = 0, [M.Piety] = 0 }
    out[p.primary] = math.min(cap, math.ceil(bp / 2))
    out[p.secondary] = math.min(cap, bp - out[p.primary])
    local left = bp - out[p.primary] - out[p.secondary]
    for _, id in ipairs({ M.Vitality, M.Strength, M.Dexterity, M.Mind, M.Piety, M.Intelligence }) do
        if left <= 0 then break end
        local add = math.min(cap - out[id], left)
        if add > 0 then out[id] = out[id] + add; left = left - add end
    end
    return out
end

-- Clan base values from the 1.23b client's own table (the character creation tribe panel) [CL]:
-- STR VIT DEX INT MND PIE, then Fire Ice Wind Earth Lightning Water resistance. Keyed by
-- playerWork.tribe; male and female rows of a clan are identical. 1.19 made race only a matter of
-- starting values with the same growth for everyone [OFF], so attributes get (value - 15) on top
-- of the level curve, 15 being the clan average, and the resistance values are the base resistance
-- at every level [DESIGN].
local MIDLANDER  = { 16, 15, 14, 16, 13, 16,  16, 13, 15, 15, 15, 16 }
local WILDWOOD   = { 14, 13, 18, 17, 12, 16,  12, 14, 18, 17, 14, 15 }
local DUSKWIGHT  = { 15, 14, 15, 18, 15, 13,  14, 16, 12, 17, 15, 16 }
local PLAINSFOLK = { 13, 13, 17, 16, 15, 16,  14, 13, 15, 16, 17, 15 }
local DUNESFOLK  = { 12, 12, 15, 16, 17, 18,  17, 12, 16, 18, 15, 12 }
stats.TRIBE = {
    [1] = MIDLANDER, [2] = MIDLANDER,
    [3] = { 18, 17, 15, 13, 15, 12,  15, 16, 14, 14, 18, 13 },   -- Highlander
    [4] = WILDWOOD, [5] = WILDWOOD,
    [6] = DUSKWIGHT, [7] = DUSKWIGHT,
    [8] = PLAINSFOLK, [9] = PLAINSFOLK,
    [10] = DUNESFOLK, [11] = DUNESFOLK,
    [12] = { 16, 15, 17, 13, 14, 15,  18, 15, 13, 15, 12, 17 },  -- Seeker of the Sun
    [13] = { 13, 12, 16, 14, 18, 17,  13, 18, 15, 14, 16, 14 },  -- Keeper of the Moon
    [14] = { 17, 18, 13, 12, 16, 14,  13, 17, 17, 12, 13, 18 },  -- Sea Wolf
    [15] = { 15, 16, 12, 15, 17, 15,  18, 14, 16, 13, 14, 15 },  -- Hellsguard
}
stats.TRIBE_MEAN = 15
local TRIBE_RESISTS = { M.FireResistance, M.IceResistance, M.WindResistance, M.EarthResistance, M.LightningResistance, M.WaterResistance }

-- The player's clan row, or nil (monsters, or an unknown tribe id).
function stats.tribeOf(player)
    local tribe = nil
    pcall(function() tribe = player.playerWork.tribe end)
    return tribe and stats.TRIBE[tribe] or nil
end

function stats.classOf(chara)
    local c = chara:GetClass()
    return stats.JOB_TO_CLASS[c] or c
end

function stats.profile(classId)
    return stats.CLASS_PROFILE[classId] or DEFAULT_PROFILE
end

-- Attribute points for a player's base class: what the player allotted with BonusPointCommand
-- (characters_bonus_points). As in retail, points nobody allotted are unspent and add nothing.
-- Without a player (stats.expected, monsters) the automatic split stands in for a typical
-- character. Index order of GetBonusPoint: 0 STR, 1 VIT, 2 DEX, 3 INT, 4 MND, 5 PIE.
local BONUS_ORDER = { M.Strength, M.Vitality, M.Dexterity, M.Intelligence, M.Mind, M.Piety }
function stats.bonusFor(classId, level, player)
    if player == nil then
        return stats.bonusAllocation(classId, level)
    end
    local points = {}
    for i, id in ipairs(BONUS_ORDER) do points[id] = player:GetBonusPoint(classId, i - 1) end
    return points
end

-- Base attributes for a class/level plus attribute points (stats.bonusFor). player is optional:
-- without it (stats.expected, monsters) the automatic split is used, so "typical" stays fixed.
function stats.baseAttributes(classId, level, player)
    local p = stats.profile(classId)
    local a = interp(stats.ATTR_CURVE, level)
    local out = {
        [M.Strength]     = a * p.str,
        [M.Vitality]     = a * p.vit,
        [M.Dexterity]    = a * p.dex,
        [M.Intelligence] = a * p.int,
        [M.Mind]         = a * p.mnd,
        [M.Piety]        = a * p.pie,
    }
    for id, pts in pairs(stats.bonusFor(classId, level, player)) do out[id] = out[id] + (pts or 0) end
    local tribe = player ~= nil and stats.tribeOf(player) or nil
    if tribe ~= nil then
        for i, id in ipairs(BONUS_ORDER) do out[id] = out[id] + tribe[i] - stats.TRIBE_MEAN end
    end
    for k, v in pairs(out) do out[k] = math.floor(v + 0.5) end
    return out
end

function stats.baseHp(classId, level, vit)
    return math.floor(interp(stats.HP_CURVE, level) * stats.profile(classId).hp + vit)
end

function stats.baseMp(classId, level, pie)
    return math.floor(interp(stats.MP_CURVE, level) * stats.profile(classId).mp + pie)
end

-- What a class-typical character of this level has; formulas normalise against it so
-- the same coefficients work at Lv1 and Lv50.
function stats.expected(classId, level)
    local attrs = stats.baseAttributes(classId, level)
    local e = {
        str = attrs[M.Strength], vit = attrs[M.Vitality], dex = attrs[M.Dexterity],
        int = attrs[M.Intelligence], mnd = attrs[M.Mind], pie = attrs[M.Piety],
    }
    e.atk = interp(stats.ATK_BASE_CURVE, level) + e.str * 2 / 3
    e.gearDef = interp(stats.GEAR_DEF_CURVE, level)
    e.def = e.gearDef + 0.67 * e.vit
    e.magic = stats.MAGIC_BASE_PER_LEVEL * level
    return e
end

-- Typical weapon Damage value at a level (3.0 s reference weapon), from the item
-- sheet: L1 7 (Weathered Gladius), L25 ~26-48, L50 ~115-144 [CLI item data]. DESIGN fit.
function stats.referenceWeaponDamage(level)
    local l = math.max(level, 1) - 1
    return 7 + 1.0 * l + 0.012 * l * l
end

------------------------------------------------------------------------------
-- Equipment totals (C# does not apply gear stats yet; we read the item sheet here)
------------------------------------------------------------------------------

-- Item level penalty: the client's calculateLevelAdjust(userLevel, itemLevel)
-- (item/Normal/NormalItemBaseClass_common) [CL, Research Notes 01 §3.5 / t_item_level_adjust].
-- 1.0 when the item level is at or below the user's; otherwise by deficit d = min(7, itemLv - userLv).
-- Applied to every benefit the piece gives (DEF, evasion, attack, bonuses, weapon Damage); which
-- params the client scales is not documented, so "all of them" is DESIGN.
stats.LEVEL_ADJUST = {
    high = { 0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1 },     -- item level >= 31
    mid  = { 0.9, 0.8, 0.7, 0.6, 0.5, 0.4, 0.3 },     -- 11-30
    low  = { 0.9, 0.85, 0.8, 0.75, 0.7, 0.6, 0.5 },   -- <= 10
}
function stats.levelAdjust(userLevel, itemLevel)
    if itemLevel == nil or itemLevel <= userLevel then return 1.0 end
    local d = math.min(7, itemLevel - userLevel)
    if itemLevel >= 31 then return stats.LEVEL_ADJUST.high[d] end
    if itemLevel >= 11 then return stats.LEVEL_ADJUST.mid[d] end
    return stats.LEVEL_ADJUST.low[d]
end

-- How much of an equipped piece counts: 0 when its condition is 0% ("the item will become damaged
-- and offer no benefits" [OFF 1.19 dev1016]; C# Player.IsEquippedGearBroken), else the
-- level penalty above times the class's compatibility with the item.
function stats.itemFactor(chara, slot, d)
    local broken = false
    pcall(function() broken = chara:IsEquippedGearBroken(slot) end)
    if broken then return 0 end
    local lvl = 0
    pcall(function() lvl = d.level end)
    return stats.levelAdjust(chara:GetLevel(), lvl) * stats.compatibility(chara, d)
end

-- The client's item compatibility for the current class or job: compatibility sheet value / 100
-- (itemBaseClass_common getItemCompatibilityData) [CL]. 0 cannot equip, 1 full effect, 0.6 a
-- "Favors" item worn by another class. Scaling every benefit by it is DESIGN: the client's
-- getItemCompatibilityWithAdjust takes an adjust term we haven't traced.
function stats.compatibility(chara, d)
    local c = 1.0
    pcall(function() c = d:GetCompatibility(chara:GetClass()) end)
    return c
end

-- paramBonusType is a paramName id; 15001 + n lines up with modifier n.
-- 16xxx rows are "damage bonus by class" flags with value 0 and are skipped.
-- Each piece is scaled by stats.itemFactor (broken = nothing, over-level = client penalty).
function stats.gearTotals(chara)
    local t = { def = 0, eva = 0, atk = 0, acc = 0, matk = 0, macc = 0, crit = 0, parry = 0, mods = {} }
    if not chara:IsPlayer() then return t end
    local ok, eq = pcall(function() return chara:GetEquipment() end)
    if not ok or eq == nil then return t end
    for slot = 0, 22 do
        local item = eq:GetItemAtSlot(slot)
        if item ~= nil then
            local d = GetItemGamedata(item.itemId)
            local f = d ~= nil and stats.itemFactor(chara, slot, d) or 0
            if d ~= nil and f > 0 then
                pcall(function() t.def = t.def + f * d.defense; t.eva = t.eva + f * d.evasion end)
                pcall(function()
                    t.atk = t.atk + f * d.attack; t.acc = t.acc + f * d.rate
                    t.matk = t.matk + f * d.magicAttack; t.macc = t.macc + f * d.magicRate
                    t.crit = t.crit + f * d.critical; t.parry = t.parry + f * d.parry
                end)
                pcall(function()
                    for i = 1, 10 do
                        local ty = d["paramBonusType" .. i]
                        local v = d["paramBonusValue" .. i]
                        if ty ~= nil and ty >= 15001 and ty <= 15105 and v ~= 0 then
                            local id = ty - 15001
                            t.mods[id] = (t.mods[id] or 0) + f * v
                        end
                    end
                end)
            end
        end
    end
    return t
end

-- Equipment slots (Player.SLOT_*): main hand 0, throwing weapon 4.
stats.SLOT_MAINHAND = 0
stats.SLOT_THROWINGWEAPON = 4

-- A player's weapon in `slot` (default main hand): damage, delay (s), hit count, ammo, dps.
-- Unarmed/missing main hand = Lv-based fallback. For the throwing slot, w.missing is true when
-- nothing is equipped (callers refuse the action).
-- dps = (Damage + ammo) / Delay. ammo is the item sheet's ammoVirtualDamagePower, non-zero only on
-- bows: "DPS = (Damage + damage of the strongest NQ ammunition at the bow's level) / Delay" [OFF 1.22
-- notes via BG #504; 05_community_jp: Ifrit's Bow (102+56)/4.3 = 36.74]. It feeds weaponskills and
-- the WS stat caps only; auto-attack and Light Shot key off w.damage (bow Damage alone) [TEST BG].
function stats.weapon(chara, slot)
    slot = slot or stats.SLOT_MAINHAND
    local w = { damage = 0, delay = 3.0, hits = 1, dps = 0, ammo = 0 }
    if chara:IsPlayer() then
        local found = false
        pcall(function()
            local item = chara:GetEquipment():GetItemAtSlot(slot)
            if item ~= nil then
                local d = GetItemGamedata(item.itemId)
                -- Broken (0%) -> no Damage, so the main hand falls to the unarmed branch and a thrown
                -- weapon counts as missing; over-level -> client level penalty on Damage (DESIGN scope).
                local f = stats.itemFactor(chara, slot, d)
                w.damage = d.damagePower * f
                w.delay = d.damageInterval
                w.hits = math.max(1, d.frequency)
                found = f > 0
                pcall(function() w.ammo = math.max(0, d.ammoVirtualDamagePower or 0) * f end)
            end
        end)
        if slot ~= stats.SLOT_MAINHAND then
            w.missing = not found or w.damage <= 0
            w.dps = (w.damage + w.ammo) / math.max(w.delay, 0.5)
            return w
        end
        if w.damage <= 0 then w.damage = math.max(1, math.floor(stats.referenceWeaponDamage(chara:GetLevel()) * 0.5)) end
    else
        -- Monsters: no weapon. A level-based "weapon" calibrated so a Lv30 monster (4.2 s swing)
        -- hits a Lv27 GLA for ~60 like the retail capture (56-63) [CAP 09_gap_fill §6a]. DESIGN fit.
        -- server_battlenpc_pools.combatDmgMult arrives as mod 132 (CombatDmgMult); 0 = not set
        -- (GM spawn by class id) -> 1.
        local mult = chara:GetMod(132)
        if mult == nil or mult <= 0 then mult = 1 end
        w.damage = (7 + 1.0 * chara:GetLevel()) * mult
        local delay = chara:GetMod(M.Delay)
        w.delay = (delay ~= nil and delay > 0) and delay / 1000 or 4.2
    end
    w.dps = (w.damage + w.ammo) / w.delay
    return w
end

------------------------------------------------------------------------------
-- Players: onCalculateBaseStats target
------------------------------------------------------------------------------

-- SetBaseStat(modId, value) is the C# hook (idempotent: applies value minus the last
-- value it set). Before it exists we fall back to SetMod, which loses status/gear
-- stacking but lets us test live.
local function setBase(chara, id, value)
    value = math.floor(value + 0.5)
    local ok = pcall(function() chara:SetBaseStat(id, value) end)
    if not ok then chara:SetMod(id, value) end
end

-- Computes base attributes, gear totals and derived stats and stores them as mods.
-- Returns a table of what it set (for logging/tests).
function stats.applyPlayerBaseStats(player)
    local classId = stats.classOf(player)
    local level = player:GetLevel()
    local attrs = stats.baseAttributes(classId, level, player)
    local gear = stats.gearTotals(player)

    for id, v in pairs(attrs) do setBase(player, id, v + (gear.mods[id] or 0)) end
    local vit = attrs[M.Vitality] + (gear.mods[M.Vitality] or 0)
    local pie = attrs[M.Piety] + (gear.mods[M.Piety] or 0)
    local str = attrs[M.Strength] + (gear.mods[M.Strength] or 0)
    local dex = attrs[M.Dexterity] + (gear.mods[M.Dexterity] or 0)
    local int = attrs[M.Intelligence] + (gear.mods[M.Intelligence] or 0)
    local mnd = attrs[M.Mind] + (gear.mods[M.Mind] or 0)

    local hp = stats.baseHp(classId, level, vit) + (gear.mods[M.Hp] or 0)
    local mp = stats.baseMp(classId, level, pie) + (gear.mods[M.Mp] or 0)
    setBase(player, M.Hp, hp)
    setBase(player, M.Mp, mp)

    -- Derived stats as the character sheet shows them (conversions in the header).
    local magicBase = stats.MAGIC_BASE_PER_LEVEL * level
    local derived = {
        [M.Attack]                  = interp(stats.ATK_BASE_CURVE, level) + gear.atk + (gear.mods[M.Attack] or 0),
        [M.Accuracy]                = interp(stats.ATK_BASE_CURVE, level) + gear.acc + (gear.mods[M.Accuracy] or 0),
        [M.Defense]                 = gear.def + (gear.mods[M.Defense] or 0),
        [M.Evasion]                 = interp(stats.ATK_BASE_CURVE, level) + gear.eva + (gear.mods[M.Evasion] or 0),
        [M.AttackMagicPotency]      = magicBase + int / 4 + gear.matk + (gear.mods[M.AttackMagicPotency] or 0),
        [M.HealingMagicPotency]     = magicBase + mnd / 4 + (gear.mods[M.HealingMagicPotency] or 0),
        [M.EnhancementMagicPotency] = magicBase + vit / 4 + (gear.mods[M.EnhancementMagicPotency] or 0),
        [M.EnfeeblingMagicPotency]  = magicBase + pie / 4 + (gear.mods[M.EnfeeblingMagicPotency] or 0),
        [M.MagicAccuracy]           = magicBase + mnd / 4 + gear.macc + (gear.mods[M.MagicAccuracy] or 0),
        [M.MagicEvasion]            = magicBase + pie / 4 + (gear.mods[M.MagicEvasion] or 0),
        [M.CriticalHitRating]       = gear.crit + (gear.mods[M.CriticalHitRating] or 0),
        [M.Parry]                   = gear.parry + (gear.mods[M.Parry] or 0),
    }
    for id, v in pairs(derived) do setBase(player, id, v) end

    -- Elemental resistances: the clan's value plus gear.
    local tribe = stats.tribeOf(player)
    for i, id in ipairs(TRIBE_RESISTS) do
        setBase(player, id, (tribe and tribe[6 + i] or 0) + (gear.mods[id] or 0))
    end

    -- Natural regen bonuses on top of C#'s floor(1.5% of max)/tick base [CAP]: none by default.
    setBase(player, M.RestingHp, gear.mods[M.RestingHp] or 0)
    setBase(player, M.RestingMp, gear.mods[M.RestingMp] or 0)

    return { class = classId, level = level, hp = hp, mp = mp, attrs = attrs, gear = gear }
end

------------------------------------------------------------------------------
-- Monsters
------------------------------------------------------------------------------

-- Monster max HP and MP (refit 2026-10-05).
--
-- retail 1.23b pcap corpus (their migrations 20260918_000044 "captured shroud/thanalan enemies",
-- 57 rows, and 20260717_000006 "central shroud enemy restore", 16 rows; each row cites capture +
-- frame). The raw pcaps are restricted, so we could not re-read the frames: the values rest on
-- Independent cross-checks that match: Lv25
-- 679 HP (a Mirror Roselet here) and the ~437 HP field mob (a Lv21 opo-opo here) [CAP
-- 09_gap_fill §1b]; ~3400 HP near Lv45-50 from the monster TP k-fit [SPEC]. The Lv30 leve mobs
-- (550/770) are leve-scaled and not on this curve (leve rows author their HP).
--
-- HP = MOB_HP_BASE(level) x family multiplier. Four multipliers explain every row to within
-- ~1% (worst 1.2%, Lv12-13): 1.2 (rodent, funguar, mole, flytrap, ogre, amalj'aa), 0.92 (wolf,
-- bat, diremite, goblin), 0.85 (firefly), 1.0 everything else. The base grows ~11%/level to
-- Lv16, ~6.9%/level to at least Lv67, then ~4.5%/level (Lv76-88 rows), so it is interpolated
-- geometrically between the anchors below (each anchor = a captured value / its multiplier).
-- Single-row inferences: wolf 0.92 (one Lv32 row), diremite 0.92 (one Lv36 row; 1.0 breaks
-- the growth rate), goblin 0.92 (Lv67), amalj'aa 1.2 (Lv88). Lv41-67 has no row, so that span
-- is the Lv16-41 growth rate carried on to the goblin [inference]. Families not captured get
-- 1.0 (DESIGN default). Rows with an authored server_battlenpc_groups.hp > 0 are kept as is.
--
-- History: the 2026-10-03 curve (68/170/340/510/544/578/935/1530/2380 at Lv1/5/10/20/25/30/40/
-- 50/60) was DESIGN with a 15% playtest cut. The capture fit is lower at Lv1-20 (Lv10 175 vs
-- 340) and higher from Lv30 (Lv50 ~2950 vs 1530). MOB_HP_SCALE is the playtest knob if the
-- retail curve feels wrong against our damage formulas (the old cut was 0.85); keep it 1.0
-- unless a playtest says otherwise, and tune damage first if low-level fights end too fast.
-- {47, 2356}: toxic toad (Gigantoad, family 1.0 assumed) from a retail 1.20 capture in caps/
-- ; the 1.19a/1.20 rows there land on this curve at Lv25-41
-- to within 0.4%, so 1.20 HP is the 1.23b HP. It replaces the inferred Lv41-67 span near Lv47
-- (that span predicted 2414).
stats.MOB_HP_BASE = {
    {1, 67}, {5, 102}, {10, 175}, {16, 317}, {26, 603}, {32, 897}, {41, 1622}, {47, 2356},
    {67, 9101}, {76, 10693}, {78, 11703}, {88, 17918},
}
stats.MOB_HP_CURVE = stats.MOB_HP_BASE   -- old name, kept for callers and docs
stats.MOB_HP_SCALE = 1.0

-- Family HP multipliers by server_battlenpc_genus.genusId [CAP, see above].
stats.MOB_HP_FAMILY = {
    [11] = 1.2,    -- Mole (snapping shrew)
    [12] = 1.2,    -- Rodent (hare, marmot, squirrel, dormouse, glirulus)
    [14] = 1.2,    -- Funguar
    [15] = 1.2,    -- Flying-trap (roselets, toadtrap)
    [60] = 1.2,    -- Ogre (Lv80-84 rows)
    [23] = 1.2,    -- Amalj'aa (one Lv88 row) [inference]
    [42] = 1.2,    -- Dodo (one Lv17 row: 406 HP vs base 338) [CAP 1.23b caps/]
    [3]  = 0.92,   -- Wolf (one Lv32 row) [inference]
    [43] = 0.92,   -- Bat
    [35] = 0.92,   -- Diremite (one Lv36 row) [inference]
    [26] = 0.92,   -- Goblin (one Lv67 row) [inference]
    [1002] = 0.85, -- Firefly
}

-- Piecewise geometric interpolation (straight lines in log space) over {level, value} anchors.
local function interpGeo(anchors, level)
    if level <= anchors[1][1] then return anchors[1][2] end
    for i = 2, #anchors do
        local a, b = anchors[i - 1], anchors[i]
        if level <= b[1] then
            return a[2] * (b[2] / a[2]) ^ ((level - a[1]) / (b[1] - a[1]))
        end
    end
    local a, b = anchors[#anchors - 1], anchors[#anchors]
    return b[2] * (b[2] / a[2]) ^ ((level - b[1]) / (b[1] - a[1]))
end
stats.interpGeo = interpGeo

function stats.mobFamilyHpMult(genusId)
    return stats.MOB_HP_FAMILY[genusId or 0] or 1.0
end

-- mult: family multiplier (stats.mobFamilyHpMult); defaults to 1.0.
function stats.mobHp(level, mult)
    return math.floor(interpGeo(stats.MOB_HP_BASE, level) * (mult or 1) * stats.MOB_HP_SCALE)
end

-- Monster max MP [CAP, same 73 rows]. No family term: every non-caster row sits
-- exactly on this table, which is linear within each decade (10, 20, 40, 70, 130, 200, 320, 500
-- MP per level). Lv41 1630 is the only row in Lv40-50 and Lv67 7040 the only one in Lv50-70, so
-- the Lv50 (2800) and Lv60 (4800) knots are inferred from those two plus the Lv78-88 rows.
-- Casters (captured THM ghosts, CNJ bombs) show exactly 5x the table, capped at 32767 (the
-- Lv76/78 bombs read 32767). Mob spells cost 0 MP today, so MP is presentation for now.
stats.MOB_MP_BASE = {
    {1, 110}, {10, 200}, {20, 400}, {30, 800}, {40, 1500}, {50, 2800},
    {60, 4800}, {70, 8000}, {80, 13000}, {90, 18000},
}
stats.MOB_MP_CASTER_MULT = 5
stats.MOB_MP_MAX = 32767

-- Caster = a THM/CNJ (or BLM/WHM) pool class, as captured; or, our DESIGN stand-in because the
-- seed pools carry currentJob 0, any monster with a spell list.
function stats.mobIsCaster(mob)
    local c = mob:GetClass()
    if c == 22 or c == 23 or c == 26 or c == 27 then return true end
    local spells = 0
    pcall(function() spells = mob.spellListId or 0 end)
    return spells > 0
end

function stats.mobMp(level, caster)
    local mp = interp(stats.MOB_MP_BASE, level)
    if caster then mp = mp * stats.MOB_MP_CASTER_MULT end
    return math.min(stats.MOB_MP_MAX, math.floor(mp))
end

-- Monster attributes: same curve as a 1.0 player (DESIGN), so normalised formulas give
-- a "typical" monster a stat ratio of 1. Genus mods can push them later.
function stats.mobExpected(level)
    return stats.expected(0, level)
end

-- For a mob hook (onCalculateBaseStats / onSpawn): HP/MP from level (plus family / caster)
-- when the group row leaves hp/mp at 0, otherwise keep the authored value.
function stats.applyMobBaseStats(mob)
    -- Also runs once from the BattleNpc constructor, before the DB mods and level exist.
    local level = mob:GetLevel()
    if level == nil or level <= 0 then return 0 end
    -- Monsters use absolute SetMod: the DB loader SetMods over everything, so no layers.
    local hp = mob:GetMod(M.Hp)
    if hp == nil or hp <= 0 then
        local genus = 0
        pcall(function() genus = mob.genusId or 0 end)
        hp = stats.mobHp(level, stats.mobFamilyHpMult(genus))
        mob:SetMod(M.Hp, hp)
    end
    local a = math.floor(interp(stats.ATTR_CURVE, level) + 0.5)
    for _, id in ipairs({ M.Strength, M.Vitality, M.Dexterity, M.Intelligence, M.Mind, M.Piety }) do
        local cur = mob:GetMod(id)
        if cur == nil or cur <= 1 then mob:SetMod(id, a) end
    end
    local mp = mob:GetMod(M.Mp)
    if mp == nil or mp <= 0 then
        if mob:IsAlly() then
            -- Story allies (Ally: opening Yda/Papalymo, duty allies) cast player spells, so they
            -- get a player's MP for their class and level (DESIGN).
            mob:SetMod(M.Mp, stats.baseMp(mob:GetClass(), level, mob:GetMod(M.Piety) or 0))
        else
            mob:SetMod(M.Mp, stats.mobMp(level, stats.mobIsCaster(mob)))
        end
    end
    return hp
end

return stats
