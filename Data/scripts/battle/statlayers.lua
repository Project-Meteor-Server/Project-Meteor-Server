--[[
battle/statlayers.lua - percentage stat statuses as idempotent stat layers.

A status that changes a stat by a percentage (Weakness, Brink of Death, Comradery, Strength in
Numbers) owns one named layer (Character.SetStatLayer). Setting a layer replaces its old value, so
re-applying never compounds. Max HP/MP are never moved by deltas. The layer is a share of the
character's *base* value (the "base" layer from battle/stats.lua applyPlayerBaseStats), and
hpMax/mpMax are rewritten from Modifier.Hp/Mp (base + every layer), the same copy
Player.CalculateBaseStats makes.

Base HP/MP is 0 until onCalculateBaseStats has run, for example for a status loaded from the DB
at login before the stats exist. In that case the layer stays 0, and player.lua
onCalculateBaseStats re-applies every active percentage status (statlayers.refresh) once the base
is there. The old code fell back to the DB's hpMax and nudged hpMax by deltas. A Weakness loaded
at login then took 25% of the stale DB value (1000 -> 750), and its expiry later added the same
amount back on top of the recomputed max (-> 1000).
--]]
require("modifiers")

statlayers = statlayers or {}

local M = modifiersGlobal

local function isPool(mod)
    return mod == M.Hp or mod == M.Mp
end

-- Sets layer's share of mod to floor(base * pct) (pct may be negative; 0 clears it).
-- HP/MP use the base layer only. Other stats fall back to the current value without this layer
-- when they have no base layer (e.g. elemental resistances).
function statlayers.set(owner, layer, mod, pct)
    owner:SetStatLayer(layer, mod, 0)
    if (pct == 0) then return end

    local base = owner:GetBaseStat(mod)
    if (base <= 0 and not isPool(mod)) then base = owner:GetMod(mod) end
    if (base <= 0) then return end

    owner:SetStatLayer(layer, mod, math.floor(math.abs(base * pct)) * (pct < 0 and -1 or 1))
end

-- hpMax/mpMax := Modifier.Hp/Mp, clamping current HP/MP. Only for characters whose HP/MP come from
-- the base layer (players after onCalculateBaseStats); others are left alone.
function statlayers.syncPools(owner)
    if (owner:GetBaseStat(M.Hp) > 0) then
        owner:SetMaxHP(math.max(1, math.floor(owner:GetMod(M.Hp))))
        if (owner:GetHP() > owner:GetMaxHP()) then owner:SetHP(owner:GetMaxHP()) end
    end
    if (owner:GetBaseStat(M.Mp) > 0) then
        owner:SetMaxMP(math.max(0, math.floor(owner:GetMod(M.Mp))))
        if (owner:GetMP() > owner:GetMaxMP()) then owner:SetMP(owner:GetMaxMP()) end
    end
end

-- Statuses re-applied by refresh: id -> function(owner, on).
statlayers.refreshers = statlayers.refreshers or {}

function statlayers.register(statusId, apply)
    statlayers.refreshers[statusId] = apply
end

-- Called from onCalculateBaseStats after the base layer is rebuilt. Re-applies each registered
-- status on (if active) or off (so a status lost without its onLose leaves no layer behind).
function statlayers.refresh(owner)
    for id, apply in pairs(statlayers.refreshers) do
        apply(owner, owner.statusEffects:HasStatusEffect(id), true)
    end
end
