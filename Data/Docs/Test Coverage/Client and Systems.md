# Client and game systems

Menus, windows, HUD and everything outside class actions. Class actions are in [Classes](Classes/).

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b


## Launch, lobby, session

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| LOG-01 | Launcher sign-in | Launcher signs in to auth, gets session id, starts the client with it | ⬜ |  |
| LOG-02 | Server list | Lobby lists worlds with status | ✅ |  |
| LOG-03 | Character list | Lists characters with name, world, appearance preview, last zone, class and level | ✅ |  |
| LOG-04 | Character delete | Delete from lobby with confirmation; name freed | ✅ |  |
| LOG-05 | Character rename | Rename prompt when a name is flagged by the server | ⚠️ | forcing rename works, doesn't take affect ingame |
| LOG-06 | Retainer edit in lobby | Lobby retainer edit mode | ⬜ |  |
| LOG-07 | World select to zone-in | Pick character, world hands session to map; lands at saved position, zone, weather, time | ✅ |  |
| LOG-08 | Opening sequence | New character starts the city-state opening (Man0 quest, opening cutscene, tutorial) | ⬜ |  |
| LOG-09 | Logout | Logout command, 30 s countdown, cancelled by moving; return to title | ⚠️ | No timer implemented |
| LOG-10 | Exit game | Shutdown command exits after countdown | ⚠️ | No timer implemented |
| LOG-11 | Re-login restore | Position, HP/MP/TP, statuses, gear, bags, hotbar, quests, party restored | ✅ |  |
| LOG-12 | Disconnect handling | Lost connection shows an error and returns to title, no crash | ✅ |  |
| LOG-13 | Login while KO | Still KO'd after login, with message; Return works | ⚠️ | Return packet sequencing wrong |
| LOG-14 | Session timeout / duplicate login | Second login of same account kicks the first | ⚠️ |  |

## Character creation

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| CHR-01 | Race, clan, gender | 5 races, 10 clans, gender (Miqo'te female, Roegadyn male only in 1.x) | ✅ |  |
| CHR-02 | Appearance editor | Face, hair, colours, height/size sliders, voice, features | ✅ |  |
| CHR-03 | Guardian and nameday | Pick one of 12 guardians and an Eorzean nameday | ✅ |  |
| CHR-04 | City-state | Pick Limsa, Gridania or Ul'dah; sets opening and starting zone | ✅ |  |
| CHR-05 | Starting class | Pick any DoW/DoM/DoH/DoL class; starting weapon and gear given | ✅ |  |
| CHR-06 | Starting attributes | Base attributes per clan plus class bonus | ✅ |  |
| CHR-07 | Name validation | First + last name rules, duplicates and reserved names refused | ✅ |  |
| CHR-08 | Character limit | Per-world and per-account character slots | ⬜ | Not sure it was in 1.23b |

## HUD

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| HUD-01 | HP/MP/TP gauges | Own gauges update live on damage, heal, regen, TP gain | ✅ |  |
| HUD-02 | Target bar | Name, HP%, claim colour, level difficulty | ⚠️ | Doesn't handle different states properly yet |
| HUD-04 | Nameplates | Names above players/NPCs/mobs; claim and aggro colours; party colour | ✅ |  |
| HUD-05 | Status icons | Self, target and party status icons with timers; tooltip | ⚠️ | acquisition of statuses works |
| HUD-06 | Remove own buff | Right-click own buff to remove it | ⬜ |  |
| HUD-07 | Cast bar | Own and target cast bars, interrupt shown | ⬜ |  |
| HUD-08 | Flytext | Damage, heal, miss, evade, parry, block, crit numbers | ✅ |  |
| HUD-09 | Battle log | Combat lines with actor, action, amount | ✅ |  |
| HUD-10 | EXP bar | Class EXP bar; level-up effect and message | ✅ |  |
| HUD-11 | Eorzea clock | Eorzea time and earth time | ✅ |  |
| HUD-12 | Compass / minimap | Position, facing, aetheryte and NPC markers; party markers | ⚠️ | needs additional verification |
| HUD-13 | Party list HUD | Members, HP/MP/TP, class, level, statuses | ⚠️ | doesn't send all information regarding party properly |
| HUD-14 | Enmity indicator | Aggro/claim indicator on target | ❌ |  |
| HUD-15 | Battle stance UI | Active/passive mode, weapon drawn indicator | ✅ |  |
| HUD-16 | HUD layout | Move/scale/hide HUD parts; saved | ✅ |  |

## Action bar and actions

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| ACT-01 | Class hotbar | Bar shows actions of current class; changes with class | ✅ |  |
| ACT-02 | Action list | Actions window lists learned actions by class and level | ✅ |  |
| ACT-03 | Equip actions | Drag actions onto bar slots; saved server-side | ⬜ |  |
| ACT-04 | Cross-class actions | Actions from other classes equippable within slot limits and level rules | ⬜ |  |
| ACT-05 | Job mode bar | Job mode limits cross-class choices to the job's allowed set | ⬜ |  |
| ACT-06 | Bar paging and keys | Multiple bars, number keys | ⬜ |  |
| ACT-07 | Range and facing | Range, facing and line-of-sight checks with error text | ⬜ |  |
| ACT-08 | Target type check | Wrong target type refused with message | ⬜ |  |
| ACT-09 | Recast | Recast shown and enforced; shared recast groups | ⬜ |  |
| ACT-10 | Cost check | MP/TP deducted; not enough refused with message | ⬜ |  |
| ACT-11 | Combo indicator | Combo-ready highlight on next step | ⬜ |  |
| ACT-12 | Traits window | Learned traits listed per class | ⬜ |  |
| ACT-13 | Items on bar | Consumables placeable on bar | ⬜ |  |
| ACT-14 | Macros | Create/edit macros (text, /ac lines, waits); place on bar | ⬜ |  |

## Character and attributes

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| ATT-01 | Character window | Name, race, clan, guardian, nameday, city, GC and rank, physical level | ✅ |  |
| ATT-02 | Physical level | Physical level separate from class rank; gives attribute points | ✅ |  |
| ATT-03 | Class ranks | Every class rank shown (DoW, DoM, DoH, DoL) | ✅ |  |
| ATT-04 | Attributes | STR VIT DEX INT MND PIE base + gear | ✅ |  |
| ATT-05 | Elemental resistances | Fire Ice Wind Earth Lightning Water | ⬜ |  |
| ATT-06 | Derived stats | Attack, defense, accuracy, evasion, magic potency/accuracy, crafting and gathering stats | ⬜ |  |
| ATT-07 | Point allotment | Allocate bonus points to attributes and resistances; confirm | ✅ |  |
| ATT-08 | Point reset | Reset allotment (rules and limits per 1.23b) | ⬜ | Not sure it was in 1.23b |
| ATT-09 | Stat refresh | Stats update on gear change and level-up without relog | ⬜ |  |
| ATT-10 | HP/MP by class | Max HP/MP follow class and level | ✅ |  |
| ATT-11 | Titles | Titles from achievements selectable | ⬜ | Not sure it was in 1.23b |

## Equipment and armoury

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| EQP-01 | Equipment slots | Main/off hand, head, body, hands, legs, feet, waist, neck, ears, fingers, wrists, ammo/throwing, soul crystal | ⬜ |  |
| EQP-02 | Equip / unequip | From bag and armoury; model updates for self and others | ⬜ |  |
| EQP-03 | Equip rules | Level, class, race/gender restrictions with message | ⬜ |  |
| EQP-04 | Class change by weapon | Swapping main hand changes class: bar, stats, level, gauges | ⬜ |  |
| EQP-05 | Class change blocked | No class change in combat or while casting | ⬜ |  |
| EQP-06 | Soul crystal | Equipping soul crystal switches to job mode | ⬜ |  |
| EQP-07 | Gear sets | Save and equip gear sets | ⬜ | Not sure it was in 1.23b |
| EQP-08 | Durability | Durability shown; at 0% gear stops giving stats | ⬜ |  |
| EQP-09 | Examine | Tooltip on equipped gear (stats, durability, spiritbond, materia) | ⬜ |  |
| EQP-10 | Check another player | /check shows their gear and model | ⬜ |  |

## Inventory

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| INV-01 | Bags | Bag opens; capacity and counts right; icons and names | ⬜ |  |
| INV-02 | Armoury chest | Gear sorted by slot in its own storage | ⬜ |  |
| INV-03 | Key items | Key item list | ⬜ |  |
| INV-04 | Currency | Gil, crystals/shards/clusters, seals, beastcoins, anima where shown | ⬜ |  |
| INV-05 | Use item | Potions, ethers, food: effect, count down, recast | ⬜ |  |
| INV-06 | Food buff | Well Fed status with stat effect and duration | ⬜ |  |
| INV-07 | Split / merge / sort | Split stacks, merge, sort | ⬜ |  |
| INV-08 | Discard | Discard with confirmation; gone server-side | ⬜ |  |
| INV-09 | Full bag | Loot and purchases refused when full | ⬜ |  |
| INV-10 | Loot to bag | Drops land in bag with log line | ⬜ |  |
| INV-11 | Treasure pool | Party loot pool and lotting/pass | ⬜ | Not sure it was in 1.23b |
| INV-12 | Persistence | Bag survives relog and zone change | ⬜ |  |

## Journal, quests, map

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| JRN-01 | Journal | Active quests (story, class, side, GC) with step text | ⬜ |  |
| JRN-02 | History | Completed quest list | ⬜ |  |
| JRN-03 | Abandon | Abandon quest where allowed | ⬜ |  |
| JRN-04 | Quest markers | Quest icons over NPCs and on map | ⬜ |  |
| JRN-05 | Quest flow | Accept, progress, complete; rewards given | ⬜ |  |
| JRN-06 | Cutscenes | Quest cutscenes play and hand back control | ⬜ |  |
| JRN-07 | Cutscene replay | Replay seen cutscenes (inn) | ⬜ |  |
| JRN-08 | Map window | Zone, region, world maps; zoom; position; aetheryte icons | ⬜ |  |
| JRN-09 | Quest map | Map opens at quest objective | ⬜ |  |
| JRN-10 | Opening and story | Story quests per nation up to To Kill a Raven / Nael | ⬜ |  |

## Guildleves and regimens

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| LEV-01 | Leve allowance | Allowance count shown, used per leve, refills on timer | ⬜ |  |
| LEV-02 | Accept leve | From levemete; leve card in journal | ⬜ |  |
| LEV-03 | Battle leve | Start at aetherial node, timer, targets spawn, complete, reward | ⬜ |  |
| LEV-04 | Difficulty | Star rating picked at start | ⬜ |  |
| LEV-05 | Local leve | Crafting/gathering leves delivered to NPC | ⬜ |  |
| LEV-06 | Faction leve | Faction / GC leves | ⬜ |  |
| LEV-07 | Party leve | Party enters together, scaled | ⬜ |  |
| LEV-08 | Abandon leve | Abandon from journal | ⬜ |  |
| LEV-09 | Leve music | Leve music plays; fanfare on clear | ⬜ |  |
| LEV-10 | Guardian's favor | Guardian aspect boosts | ⬜ | Not sure it was in 1.23b |
| LEV-11 | Battle Regimen | Party regimen chains (order of actions builds effect) | ⬜ |  |
| LEV-12 | Behests | Timed area events in camps | ⬜ |  |

## Achievements

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| ACH-01 | Window | Categories and list | ⬜ |  |
| ACH-02 | Points | Total points | ⬜ |  |
| ACH-03 | Earn | Popup and log; saved after relog | ⬜ |  |
| ACH-04 | Progress | Counters on unfinished ones | ⬜ |  |
| ACH-05 | Rewards | Item/title rewards | ⬜ | Not sure it was in 1.23b |

## Party

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| PTY-01 | Invite | Invite by menu and /pcmd add; accept or decline | ⬜ |  |
| PTY-02 | Party list | Members, HP/MP/TP, class/level, leader | ⬜ |  |
| PTY-03 | Leave / kick / disband | All three work | ⬜ |  |
| PTY-04 | Leader | Pass leadership | ⬜ |  |
| PTY-05 | Party chat | /p channel | ⬜ |  |
| PTY-06 | Out of zone | Members in other zones shown as away | ⬜ |  |
| PTY-07 | Relog | Party kept over relog | ⬜ | Not sure it was in 1.23b |
| PTY-08 | Shared EXP | EXP shared by party rules | ⬜ | Not sure it was in 1.23b |
| PTY-09 | Party size | Limit 8 | ⬜ |  |
| PTY-10 | Target from list | Target member from party list; heals land | ⬜ |  |

## Linkshells

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| LS-01 | Create | Create at NPC (name, emblem); linkpearl given | ⬜ |  |
| LS-02 | Invite | Give linkpearl; accept/decline | ⬜ |  |
| LS-03 | Active shell | Equip/switch active linkshell | ⬜ |  |
| LS-04 | Member list | Members, online state, ranks | ⬜ |  |
| LS-05 | Ranks | Appoint/demote, transfer master | ⬜ |  |
| LS-06 | Kick / leave / disband | Each works | ⬜ |  |
| LS-07 | Chat and message | /l channel, shell message | ⬜ |  |
| LS-08 | Persistence | Kept over relog | ⬜ |  |
| LS-09 | NPC linkshell | Story NPC linkshell calls | ⬜ |  |

## Social and chat

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| SOC-01 | Channels | Say, shout, yell, tell, reply, party, linkshell | ⬜ |  |
| SOC-02 | Log tabs | Tabs, filters, timestamps | ⬜ |  |
| SOC-03 | Auto-translate | Auto-translate dictionary in chat | ⬜ |  |
| SOC-04 | Emotes | Menu and /commands; seen by others | ⬜ |  |
| SOC-05 | Sit | Sit emote and seated state | ⬜ |  |
| SOC-06 | Friend list | Add with request, remove, online state | ⬜ |  |
| SOC-07 | Blacklist | Add/remove; blocks tells | ⬜ |  |
| SOC-08 | Search | /sea by name, class, level, area | ⬜ |  |
| SOC-09 | Search comment | Set your search comment | ⬜ |  |
| SOC-10 | Dice | /random | ⬜ |  |
| SOC-11 | Check | /check on player | ⬜ |  |

## Trade, bazaar, retainers, markets

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| MKT-01 | Trade | Trade window: items and gil, both confirm, items move; cancel | ⬜ |  |
| MKT-02 | Bazaar set | Set items and prices for your bazaar | ⬜ |  |
| MKT-03 | Bazaar buy | Others browse and buy | ⬜ |  |
| MKT-04 | Bazaar seek | Seeking items (buy requests) | ⬜ |  |
| MKT-05 | Hire retainer | Hire at NPC; name and look | ⬜ |  |
| MKT-06 | Summon retainer | Bell or ward; dismiss | ⬜ |  |
| MKT-07 | Retainer storage | Entrust/withdraw items and gil | ⬜ |  |
| MKT-08 | Retainer bazaar | Retainer sells in Market Ward; sale log; gil credited | ⬜ |  |
| MKT-09 | Market Ward | Enter ward, browse retainers | ⬜ |  |
| MKT-10 | Item search | Search items for sale | ⬜ |  |
| MKT-11 | Retainer limit | Number of retainers allowed | ⬜ | Not sure it was in 1.23b |

## Travel

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| TRV-01 | Attune | Attune to aetherytes and gates | ⬜ |  |
| TRV-02 | Teleport | Teleport menu, anima cost, cast time, interrupt | ⬜ |  |
| TRV-03 | Return | Return to home point; set home point | ⬜ |  |
| TRV-04 | Anima | Regen over time; cap | ⬜ |  |
| TRV-05 | Zone lines | Seamless crossings and zone loads | ⬜ |  |
| TRV-06 | Airships | Airship routes between cities | ⬜ |  |
| TRV-07 | Ferries | Boats where 1.23b had them | ⬜ | Not sure it was in 1.23b |
| TRV-08 | Rental chocobo | Chocobo porter / rental | ⬜ |  |
| TRV-09 | Own chocobo | GC companion chocobo summon, ride, dismount | ⬜ |  |
| TRV-10 | Chocobo music | Chocobo music while riding | ⬜ |  |

## NPCs, shops, inns

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| NPC-01 | Talk | Dialogue, choices, menus | ⬜ |  |
| NPC-02 | Shop buy/sell | Buy and sell, quantity, gil change | ⬜ |  |
| NPC-03 | Guild shops | Guild and seal shops | ⬜ |  |
| NPC-04 | Repair NPC | NPC repair for gil | ⬜ |  |
| NPC-05 | Inn | Enter room, rest bonus | ⬜ |  |
| NPC-06 | Support desk | GM call / support tickets and FAQs | ⬜ |  |

## Grand Company

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| GC-01 | Enlist | Join a GC | ⬜ |  |
| GC-02 | Rank | Rank shown, promotions | ⬜ |  |
| GC-03 | Seals | Earn and spend seals | ⬜ |  |
| GC-04 | Company leves | GC leves | ⬜ |  |
| GC-05 | GC quests | GC story quests | ⬜ |  |

## Combat basics

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| CMB-01 | Targeting | Click, tab, lock-on | ⬜ |  |
| CMB-02 | Auto-attack | Starts in active mode, stops when sheathed | ⬜ |  |
| CMB-03 | Claim | First hit claims; others see claim colour | ⬜ |  |
| CMB-04 | Enmity and aggro | Aggro types, linking, enmity tables | ⬜ |  |
| CMB-05 | Mob death | EXP, drops, log lines, fade out | ⬜ |  |
| CMB-06 | Battle music | Battle music on claim; field music back after last kill | ⬜ |  |
| CMB-07 | Hit results | Hit, crit, miss, evade, parry, block, resist | ⬜ |  |
| CMB-08 | Damage formulas | Damage numbers match retail | ⬜ |  |
| CMB-09 | Regen | HP/MP/TP regen in and out of combat; TP decay | ⬜ |  |
| CMB-10 | Resting | Rest state | ⬜ | Not sure it was in 1.23b |
| CMB-11 | Status ailments | Poison, slow, sleep, bind, heavy, stun, etc. on players | ⬜ |  |
| CMB-12 | Death | KO state, Return to home point | ⬜ |  |
| CMB-13 | Raise | Raise from another player; accept prompt; weakness | ⬜ |  |
| CMB-14 | EXP chain | Chain bonus from kills | ⬜ |  |
| CMB-15 | Level sync | Leve/party level sync if in 1.23b | ⬜ | Not sure it was in 1.23b |

## Materia, repair, spiritbond

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| MAT-01 | Spiritbond | Gear gains spiritbond in use | ⬜ |  |
| MAT-02 | Materia | Convert at 100% to materia | ⬜ |  |
| MAT-03 | Meld | Meld materia (success, fail, item effects) | ⬜ |  |
| MAT-04 | Self repair | Repair with matching craft class and dark matter | ⬜ |  |
| MAT-05 | Repair levels | Repair limit by craft rank | ⬜ | Not sure it was in 1.23b |

## Instanced content

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| CNT-01 | Cutter's Cry | Dungeon: entry, mobs, boss, exit | ⬜ |  |
| CNT-02 | Dzemael Darkhold | Dungeon | ⬜ |  |
| CNT-03 | Aurum Vale | Dungeon | ⬜ |  |
| CNT-04 | Ifrit | Bowl of Embers (Normal/Hard/Extreme): phases, adds | ⬜ |  |
| CNT-05 | Garuda | Howling Eye | ⬜ |  |
| CNT-06 | Moogle | Good King Moggle Mog | ⬜ |  |
| CNT-07 | Story instances | Nation story fights to Nael | ⬜ |  |
| CNT-08 | Raid entry | Party entry, director, start event | ⬜ |  |

## Crafting and gathering UI

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| CG-01 | Synthesis window | Recipe pick, material check, crystals | ⬜ |  |
| CG-02 | Synth actions | Standard / rapid / bold, progress, durability, quality | ⬜ |  |
| CG-03 | Outcome | Success, fail, HQ, EXP, item in bag | ⬜ |  |
| CG-04 | Gathering node | Target node, minigame, yield | ⬜ |  |
| CG-05 | Fishing | Cast, bite, reel, catch | ⬜ |  |

## Config and misc

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| CFG-01 | Config | Graphics, sound, controls, UI, log; saved | ⬜ |  |
| CFG-02 | Keybinds | Rebind keys | ⬜ |  |
| CFG-03 | Help | Help and tutorial windows | ⬜ |  |
| CFG-04 | System messages | Log and error text correct | ⬜ |  |
| CFG-05 | Main menu | Every entry opens its window | ⬜ |  |
| CFG-06 | Slash commands | Common / commands work | ⬜ |  |
| CFG-07 | Weather | Weather per zone and time | ⬜ |  |
| CFG-08 | Day/night | Eorzea day cycle | ⬜ |  |

[Back to overview](README.md)
