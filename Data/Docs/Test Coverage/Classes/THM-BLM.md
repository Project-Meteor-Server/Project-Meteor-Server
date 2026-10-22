# Thaumaturge / Black Mage

Attack mage: fire, ice and lightning spells, sleep and binds. Black Mage needs Thaumaturge 30 and Pugilist 15, the job quest, and the soul crystal.

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b

## Basics
| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| THM-01 | Become a Thaumaturge | Equip a Thaumaturge weapon; class and hotbar switch | ⬜ | |
| THM-02 | Thaumaturges' Guild | Guild in Ul'dah gives class quests | ⬜ | Missing |
| THM-03 | Class quests | Quests at Lv20, 30, 36 and 40 | ⬜ | Missing. Lower-level quests unknown |
| THM-04 | Job quests | Six quests, Lv30 to 50; each gives a job action | ⬜ | Missing |
| THM-05 | Job unlock | Job quest opens at Thaumaturge 30 and Pugilist 15 | ⬜ | Missing |
| THM-06 | Soul crystal | Equip it, or type /job, to become Black Mage | ⬜ | |
| THM-07 | Job hotbar | Keeps class actions; borrows only Pugilist/Archer actions, 3 to 5 | ⬜ | Bug: slot count fixed, any class allowed |
| THM-08 | Borrowed actions as Thaumaturge | 1 slot at Lv1, up to 10 at Lv50 | ⬜ | Bug: slot count fixed |
| THM-09 | Shared actions | Other classes can borrow Thunder, Fire, Fira and the self buffs | ⬜ | Bug: exclusive actions not blocked |
| THM-10 | Job gear | Class-only gear can't be worn as Black Mage | ⬜ | Missing |
| THM-11 | Shared level | Black Mage uses Thaumaturge level and EXP | ⬜ | |
| THM-12 | Auto-attack | Swings the weapon at its delay | ⬜ | Reach unknown |

## Class actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| THM-13 | Thunder | 1 | Spell | Lightning damage. 2 s cast, 6 s recast. 210 MP at Lv50 | ⬜ | Bug: damage is a flat number |
| THM-14 | Parsimony | 2 | Ability | Next attack spell costs half MP and refunds MP. 90 s recast | ⬜ | Wrong: refund too small |
| THM-15 | Blizzard | 4 | Spell | Ice damage, may slow (Heavy). 3 s cast, 10 s recast | ⬜ | Bug: damage is a flat number |
| THM-16 | Necrogenesis | 6 | Ability | Next attack spell heals you once. 90 s recast | ⬜ | Wrong: heals far too much |
| THM-17 | Fire | 10 | Spell | Fire damage to target and enemies near it. 3 s cast, 8 s recast | ⬜ | Bug: always deals 5000 |
| THM-18 | Dark Seal | 14 | Ability | Next spell more likely to land. 90 s recast | ⬜ | Bonus size unknown |
| THM-19 | Thundara | 18 | Spell | Instant lightning hit, may stun 4 s. 30 s recast | ⬜ | Bug: damage is a flat number |
| THM-20 | Resonance | 22 | Ability | Next spell reaches farther. 90 s recast | ⬜ | Range bonus unknown |
| THM-21 | Blizzara | 26 | Spell | Instant ice damage around you, may bind. 40 s recast | ⬜ | Wrong: aimed at target, should circle you |
| THM-22 | Sanguine Rite | 30 | Spell | 20 s: hits taken give MP, casts can't be interrupted | ⬜ | |
| THM-23 | Fira | 34 | Spell | Fire damage to target and nearby. 5 s cast, 16 s recast | ⬜ | Bug: damage is a flat number |
| THM-24 | Excruciate | 38 | Ability | Spell crit chance climbs until a crit. 30 s, 90 s recast | ⬜ | Climb rate unknown |
| THM-25 | Sleep | 42 | Spell | Puts target to sleep 60 s. 3 s cast, no recast | ⬜ | |
| THM-26 | Thundaga | 46 | Spell | Big lightning hit. 5 s cast, 45 s recast | ⬜ | Bug: damage is a flat number |
| THM-27 | Firaga | 50 | Spell | Big fire hit on target and nearby. 8 s cast, 7 s recast | ⬜ | Bug: damage is a flat number |

## Job actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| THM-28 | Convert | 30 | Ability | Swaps your HP and MP. 7.5 min recast | ⬜ | |
| THM-29 | Freeze | 35 | Spell | Ice damage that lowers your enmity. 5 s cast, 2 min recast | ⬜ | Bug: script errors |
| THM-30 | Flare | 40 | Spell | Fire around you, stronger up close, may burn. 8 s cast | ⬜ | Bug: no burn, no up-close bonus |
| THM-31 | Sleepga | 45 | Spell | Sleeps target and enemies near it. 4 s cast | ⬜ | Missing |
| THM-32 | Burst | 50 | Spell | Lightning hit. 4 s cast, 15 min recast | ⬜ | Bug: damage is a flat number |

## Traits
| ID | Trait | Lv | What it does | Status | Notes |
|---|---|---|---|---|---|
| THM-33 | Enhanced Magic Potency | 8 | Attack magic +8 | ⬜ | Bug: spells ignore it |
| THM-34 | Enhanced Enfeebling Magic | 12 | Enfeebling magic +8 | ⬜ | |
| THM-35 | Enhanced Parsimony | 16 | Parsimony refunds 25% more MP | ⬜ | |
| THM-36 | Auto-refresh | 20 | +3 MP every 3 s, even in combat | ⬜ | |
| THM-37 | Swift Necrogenesis | 24 | Necrogenesis recast 60 s | ⬜ | |
| THM-38 | Enhanced Magic Potency II | 28 | Attack magic +10 | ⬜ | Bug: spells ignore it |
| THM-39 | Enhanced Enfeebling Magic II | 32 | Enfeebling magic +10 | ⬜ | Wrong: learned at 28, should be 32 |
| THM-40 | Swift Dark Seal | 36 | Dark Seal recast 60 s | ⬜ | |
| THM-41 | Enhanced Magic Crit Potency | 40 | Spell crits hit harder (+10) | ⬜ | Unsure: crit damage or crit rate |
| THM-42 | Enhanced Sanguine Rite | 44 | Sanguine Rite also cuts damage taken 25% | ⬜ | Bug: never applies |
| THM-43 | Enhanced Excruciate | 48 | Excruciate climbs faster | ⬜ | Size unknown |

## Status effects
| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| THM-44 | Parsimony | Parsimony | Half MP and MP refund on next attack spell. 30 s | ⬜ | |
| THM-45 | Necrogenesis | Necrogenesis | Heal on next attack spell. 30 s | ⬜ | |
| THM-46 | Dark Seal | Dark Seal | Better hit chance on next spell. 30 s | ⬜ | |
| THM-47 | Resonance | Resonance | Longer range on next spell. 30 s | ⬜ | |
| THM-48 | Sanguine Rite | Sanguine Rite | 30% of each hit taken comes back as MP. 20 s | ⬜ | |
| THM-49 | Excruciate | Excruciate | Rising spell crit chance; ends on a crit. 30 s | ⬜ | |
| THM-50 | Heavy | Blizzard | Target moves slower. 30 s | ⬜ | Slow amount unknown |
| THM-51 | Bind | Blizzara | Target can't move. 30 s | ⬜ | |
| THM-52 | Stun | Thundara | Target can't act. 4 s | ⬜ | Bug: won't apply |
| THM-53 | Sleep | Sleep, Sleepga | Target can't act; wakes when hit. 60 s | ⬜ | |
| THM-54 | Flare burn | Flare | Damage over time | ⬜ | Missing. Duration unknown |
| THM-55 | Combo | Thunder, Thundara, Fire, Fira | Icon shows the next combo step is ready | ⬜ | Wrong: lasts 13 s, should be 12 s |

## Mechanics
| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| THM-56 | Thunder combo | Thundara after Thunder: more damage, shorter recast, no stun | ⬜ | Bonus size unknown |
| THM-57 | Thundaga combo | Thundaga after Thundara: crits hit harder | ⬜ | Bug: bonus does nothing |
| THM-58 | Burst combo | Burst after Thundara: more damage the lower your HP | ⬜ | Missing |
| THM-59 | Fire combo | Fira after Fire and Firaga after Fira cast faster | ⬜ | |
| THM-60 | Combo breaks | A miss, a resist, a wait over 12 s or another action breaks it | ⬜ | |
| THM-61 | Combo cost | Combo spells still cost full MP | ⬜ | Bug: combo spells are free |
| THM-62 | MP cost by level | Spell costs grow with level | ⬜ | |
| THM-63 | Casting interrupted | Moving stops a cast; getting hit can too | ⬜ | Missing: hits never interrupt |
| THM-64 | Spell damage | Damage rises with INT and attack magic; element matters | ⬜ | Bug: damage is a flat number |
| THM-65 | Partial resists | Spells can be partly resisted for less damage | ⬜ | |
| THM-66 | Area shapes | Fire spells hit around the target; Blizzara and Flare around you | ⬜ | Wrong: Blizzara and Flare aimed at target |
| THM-67 | Repeat crowd control | Same sleep, bind, slow or stun lasts shorter, then fails | ⬜ | Missing |
| THM-68 | Job-only actions | Convert, Freeze, Flare, Sleepga, Burst work only as Black Mage | ⬜ | |
| THM-69 | Recasts | Actions can't be reused before their recast ends | ⬜ | Bug: server doesn't check |
| THM-70 | Battle Regimen | Not in the game | ➖ | |

## Unknowns
- How much MP Parsimony returns (one sample only).
- How much Necrogenesis heals (one sample only).
- Bonus sizes for Dark Seal, Resonance and Excruciate.
- Combo sizes for Thundara, Thundaga and Burst; Flare's up-close bonus and burn.
- Whether the Lv40 trait raises crit damage or crit rate.
- Whether Blizzard's cast is 2 s or 3 s.
- Whether combo spells really cost full MP.
- How much Heavy slows, and how often spells add their effects.
- Whether getting hit interrupts casts.
- Class quests below Lv20; the guildmaster's name; which job quest gives which action.
- Auto-attack reach for Thaumaturge weapons.

[Back to overview](../README.md)
