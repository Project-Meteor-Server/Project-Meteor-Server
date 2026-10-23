# Conjurer / White Mage

Healer with wand or staff. White Mage needs Conjurer 30 and Gladiator 15, the job quests, and the soul crystal.

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b

## Basics
| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| CNJ-01 | Become a Conjurer | Wand or staff in main hand makes you a Conjurer. | ⬜ | |
| CNJ-02 | Conjurers' Guild | Guild at Stillglade Fane, Gridania, with its guildmaster. | ⬜ | Missing |
| CNJ-03 | Class quests | Conjurer quests from the guild at Lv20, 30 and later. | ⬜ | Missing. Later quest levels unknown |
| CNJ-04 | Job quest unlock | First job quest offered at Conjurer 30 with Gladiator 15. | ⬜ | Missing |
| CNJ-05 | Job quest chain | Six quests. First gives the soul crystal. Each teaches a job action. | ⬜ | Missing. Levels of quests 3–6 unknown |
| CNJ-06 | Soul crystal | Equip it (or /job) to become White Mage. Remove it to go back. | ⬜ | |
| CNJ-07 | Job actions on the bar | Job actions set automatically. Only usable as White Mage. | ⬜ | Bug: learned by level, not quest |
| CNJ-08 | Other-class action slots | Conjurer: 6–10 slots, any class. White Mage: 3–5, Gladiator/Pugilist only. | ⬜ | Job class limit not enforced |
| CNJ-09 | Shared level | White Mage uses the Conjurer level and EXP bar. | ⬜ | |
| CNJ-10 | Job gear | White Mage can wear Conjurer gear and the Healer's set. | ⬜ | Not enforced |
| CNJ-11 | Auto-attack | Melee swings with the weapon while facing the target. | ⬜ | |
| CNJ-12 | Stats | MND raises healing. PIE raises MP. VIT raises buff strength. | ⬜ | |

## Class actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| CNJ-13 | Stone | 1 | Spell | Earth damage. May lower magic evasion. 2 s cast, 6 s recast. | ⬜ | Bug: flat damage |
| CNJ-14 | Cure | 2 | Spell | Heals one target. 2 s cast, 5 s recast. | ⬜ | |
| CNJ-15 | Aero | 4 | Spell | Wind damage. May add a bleed. 3 s cast, 6 s recast. | ⬜ | Bug: bleed never ends |
| CNJ-16 | Protect | 6 | Spell | Defense up for party within 8 yalms. 5 min. 30 s recast. | ⬜ | Wrong: area 20, should be 8 |
| CNJ-17 | Cleric Stance | 10 | Ability | Attack spells +20%, heals −20%. Use again to end. 30 s recast. | ⬜ | Bug: attack spells not boosted |
| CNJ-18 | Blissful Mind | 14 | Ability | Costs 1/4 HP, stores MP. Use again to collect. 30 s recast. | ⬜ | Wrong: uses current HP, not max |
| CNJ-19 | Raise | 18 | Spell | Revives a KO'd ally. 10 s cast, 5 min recast. | ⬜ | Bug: does nothing |
| CNJ-20 | Stonera | 22 | Spell | Earth damage around target. May slow. 3 s cast, 30 s recast. | ⬜ | |
| CNJ-21 | Stoneskin | 26 | Spell | Barrier absorbs damage. 5 min. 30 s recast. | ⬜ | |
| CNJ-22 | Cura | 30 | Spell | Big heal on one target. 2 s cast, 5 s recast. | ⬜ | |
| CNJ-23 | Sacred Prism | 34 | Ability | Next heal or buff hits an area, casts slower. 90 s recast. | ⬜ | Cast and area unknown |
| CNJ-24 | Shroud of Saints | 38 | Ability | Halves your enmity, restores MP. 20 s. 3 min recast. | ⬜ | Bug: icon only, no effect |
| CNJ-25 | Aerora | 42 | Spell | Wind damage around target. Removes one buff each. 4 s cast, 20 s recast. | ⬜ | |
| CNJ-26 | Curaga | 46 | Spell | Shared heal for party within 8 yalms, lowest HP first. 10 s recast. | ⬜ | Bug: flat heal, area 15 |
| CNJ-27 | Repose | 50 | Spell | Puts target to sleep. 3 s cast, no recast. | ⬜ | |

## Job actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| CNJ-28 | Presence of Mind | 30 | Ability | Next spell casts near-instantly, no recast. 5 min recast. | ⬜ | Missing |
| CNJ-29 | Regen | 35 | Spell | Heals every 3 s for 45 s. 2 s cast, 5 s recast. | ⬜ | |
| CNJ-30 | Esuna | 40 | Spell | Removes one debuff. 2 s cast, 10 s recast. | ⬜ | |
| CNJ-31 | Holy | 45 | Spell | Uses all MP. Damage around you, may bind. 5 min recast. | ⬜ | Bug: doesn't use all MP |
| CNJ-32 | Benediction | 50 | Ability | Fully heals party within 20 yalms. 15 min recast. | ⬜ | Missing |

## Traits
| ID | Trait | Lv | What it does | Status | Notes |
|---|---|---|---|---|---|
| CNJ-33 | Greater Healing | 8 | Healing +8. | ⬜ | |
| CNJ-34 | Greater Enhancing Magic | 12 | Buff strength +8. | ⬜ | |
| CNJ-35 | Enhanced Magic Accuracy | 16 | Magic accuracy +8. | ⬜ | |
| CNJ-36 | Auto-refresh | 20 | +3 MP every 3 s. | ⬜ | |
| CNJ-37 | Enhanced Protect | 24 | Protect also raises magic defense. | ⬜ | Bug: no effect |
| CNJ-38 | Greater Healing II | 28 | Healing +10. | ⬜ | Wrong: Lv18, should be 28 |
| CNJ-39 | Enhanced Blissful Mind | 32 | Blissful Mind stores MP faster. | ⬜ | Rate unknown |
| CNJ-40 | Enhanced Stoneskin | 36 | Stoneskin absorbs 25% more. | ⬜ | Wrong: gives 46% more |
| CNJ-41 | Swift Sacred Prism | 40 | Sacred Prism recast 60 s. | ⬜ | |
| CNJ-42 | Swift Shroud of Saints | 44 | Shroud of Saints recast 2 min. | ⬜ | |
| CNJ-43 | Enhanced Raise | 48 | Raise no longer causes Weakness. | ⬜ | Raise broken |

## Status effects
| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| CNJ-44 | Magic Evasion Down | Stone | Target dodges spells less. | ⬜ | Duration unknown |
| CNJ-45 | Bleed | Aero | Damage every 3 s for 21 s. | ⬜ | Bug: never ends |
| CNJ-46 | Protect | Protect | Defense up for 5 min. Replaces Shell and vice versa. | ⬜ | Wrong: defense far too high |
| CNJ-47 | Cleric Stance | Cleric Stance | Stance icon until used again. | ⬜ | |
| CNJ-48 | Blissful Mind / Fully Blissful Mind | Blissful Mind | Fills with MP, changes icon when full. | ⬜ | |
| CNJ-49 | Weakness | Raise, Return | Max HP and MP −25% for 3 min. | ⬜ | Missing |
| CNJ-50 | Brink of Death | Revived while Weak | HP, MP, attack, defense, magic −50% for 3 min. | ⬜ | Missing |
| CNJ-51 | Heavy | Stonera | Target moves slower. | ⬜ | Duration unknown |
| CNJ-52 | Stoneskin | Stoneskin | Absorbs damage until used up or 5 min. | ⬜ | |
| CNJ-53 | Sacred Prism | Sacred Prism | Waits 60 s for the next heal or buff. | ⬜ | |
| CNJ-54 | Shroud of Saints | Shroud of Saints | Lower enmity, MP regen for 20 s. | ⬜ | Bug: no effect |
| CNJ-55 | Sleep | Repose | Asleep 60 s. Damage wakes it. | ⬜ | |
| CNJ-56 | Presence of Mind | Presence of Mind | Waits 30 s for the next spell. Lost on class change. | ⬜ | Missing |
| CNJ-57 | Regen | Regen | Heals every 3 s for 45 s. Stacks with Paladin's Divine Veil. | ⬜ | |
| CNJ-58 | Bind | Holy | Target can't move. | ⬜ | Duration unknown |

## Mechanics
| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| CNJ-59 | Stone → Stonera combo | Stonera after Stone hits one target harder. | ⬜ | |
| CNJ-60 | Aero → Aerora combo | Aerora after Aero hits one target, more accurate. | ⬜ | Wrong: adds damage, not accuracy |
| CNJ-61 | Resisted combo start | A resisted Stone or Aero breaks the combo. | ⬜ | |
| CNJ-62 | Extra effects landing | Higher PIE lands slows, bleeds and binds more often. | ⬜ | |
| CNJ-63 | Sleep, slow, bind wear off faster | Same effect again: half, quarter, then immune for 3 min. | ⬜ | Missing |
| CNJ-64 | Heal enmity | Heals draw some enmity. Healing a full-HP target draws none. | ⬜ | |
| CNJ-65 | Heal spread and crits | Heals vary a little; sometimes crit for about 22% more. | ⬜ | |
| CNJ-66 | KO | HP 0, buffs lost, TP kept, enemies forget you. | ⬜ | |
| CNJ-67 | Raise offer | Target gets a prompt; accepting revives with full HP. | ⬜ | Missing |
| CNJ-68 | After being raised | No actions for 7 s. | ⬜ | Missing |
| CNJ-69 | Return | KO'd player goes home with Weakness and gear damage. | ⬜ | Missing |
| CNJ-70 | Raised while weak | Weakness turns into Brink of Death. | ⬜ | Missing |
| CNJ-71 | Presence of Mind + Raise | Raise casts in about 1 s. | ⬜ | Missing |
| CNJ-72 | MP cost by level | Spells cost more MP at higher level. | ⬜ | |
| CNJ-73 | Resting MP | Standing still in passive restores MP faster each tick. | ⬜ | |
| CNJ-74 | Healer's gear set | Robe boosts Cure, Culottes cut Presence of Mind recast, Boots boost Regen. | ⬜ | Bug: Regen bonus on the Culottes |
| CNJ-75 | Spells in passive mode | Whether spells can be cast in passive mode. | ⬜ | Retail rule unknown |

## Unknowns
- Untraited Raise: HP on revive, and whether it gives Weakness.
- Return: HP and MP on revive, and how much gear damage.
- Levels of White Mage quests 3–6 and later Conjurer class quests.
- Whether Presence of Mind fully removes cast time for every spell.
- How long Stone's magic evasion down, Stonera's slow and Holy's bind last.
- Shroud of Saints MP per tick and Blissful Mind fill speed.
- Sacred Prism's slower cast and area size.
- Whether Holy's damage depends on the MP it uses.
- Whether Cleric Stance affects Regen and Benediction, and survives KO.
- Whether spells could be cast in passive mode.
- Which Healer's piece boosts Regen: Culottes or Boots.

[Back to overview](../README.md)
