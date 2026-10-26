# Pugilist / Monk

Melee fighter with fist weapons. Monk needs Pugilist 30 and Lancer 15, the job quest, and the soul crystal.

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b

## Basics
| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| PGL-01 | Become a Pugilist | Equipping fist weapons switches you to Pugilist. | ✅ | |
| PGL-02 | Pugilists' Guild | Guild in Ul'dah; class quests start with Gagaruna. | ⬜ | |
| PGL-03 | Class quests | Quests at Lv20, 30, 36 and 40. | ⬜ | Missing: only Lv20 quest exists |
| PGL-04 | Monk unlock | Job quest offered at Pugilist 30 with Lancer 15. | ⬜ | Missing: no job quests |
| PGL-05 | Monk job quests | Six quests; first gives the crystal, later ones teach job actions. | ⬜ | Missing. Which quest teaches what unknown |
| PGL-06 | Switch to Monk | Equip the Soul of the Monk, or /job on and off. | ⬜ | |
| PGL-07 | Shared level | Monk uses the Pugilist level and EXP. | ⬜ | |
| PGL-08 | Other-class actions as Monk | Only Lancer and Archer actions; 3 slots at 30, 4 at 40, 5 at 50. | ⬜ | Wrong: fixed slots, no class check |
| PGL-09 | Other-class actions as Pugilist | Slots grow from 1 at Lv1 to 10 at Lv50. | ⬜ | Wrong: fixed slots |
| PGL-10 | Gear as Monk | Can wear Pugilist and Monk gear; not other class-only gear. | ⬜ | Missing |
| PGL-11 | Monk artifact armor | Body cuts Fists MP cost; boots speed up Fists of Wind more. | ⬜ | Missing. Other pieces unknown |
| PGL-12 | Auto-attack | Two fist hits per swing, one swing per weapon delay. | ⬜ | |
| PGL-13 | Main stats | Strength raises damage most, Intelligence second. | ⬜ | Bug: weaponskills do flat damage |
| PGL-14 | Melee reach | Weaponskills work only at melee range. | ⬜ | Exact reach unknown |

## Class actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| PGL-15 | Pummel | 1 | Weaponskill | Hit, stronger from the front. 1000 TP, 10 s. | ⚠️ | Confirm visual effect |
| PGL-16 | Featherfoot | 2 | Ability | Big evasion up; MP back on first evade. 30 s, 60 s recast. | ⬜ | Wrong: MP should be about 140 |
| PGL-17 | Pounce | 4 | Weaponskill | Hit; stuns 10 s from behind. 1500 TP, 20 s. | ⬜ | Bug: flat damage |
| PGL-18 | Second Wind | 6 | Ability | Heal self, about 580 at Lv50, more with INT. 45 s. | ⬜ | Wrong: heals too much at Lv50 |
| PGL-19 | Concussive Blow | 10 | Weaponskill | Hit; blinds 30 s from the side. 1500 TP, 30 s. | ⬜ | Bug: flat damage |
| PGL-20 | Blindside | 14 | Ability | Next weaponskill from behind crits. 60 s. | ⬜ | |
| PGL-21 | Haymaker | 18 | Weaponskill | Only right after evading; may stun. 250 TP, 5 s. | ⬜ | Bug: flat damage. Stun length unknown |
| PGL-22 | Fists of Earth | 22 | Ability | Earth attacks, defense up; drains MP until toggled off. | ⬜ | Missing: does nothing |
| PGL-23 | Sucker Punch | 26 | Weaponskill | Hit; restores MP from the side. 1000 TP, 15 s. | ⬜ | Bug: flat damage |
| PGL-24 | Demolish | 30 | Weaponskill | Hit; after Pounce removes one enemy effect. 1500 TP, 30 s. | ⬜ | Bug: flat damage |
| PGL-25 | Fists of Fire | 34 | Ability | Fire attacks, attack up; drains MP until toggled off. | ⬜ | Missing: does nothing |
| PGL-26 | Aura Pulse | 38 | Weaponskill | Hits enemies around you; after Pummel may slow. 1500 TP, 40 s. | ⬜ | Bug: flat damage |
| PGL-27 | Taunt | 42 | Ability | Enemy attacks only you for 5 s. 60 s. | ⬜ | Missing: does nothing |
| PGL-28 | Howling Fist | 46 | Weaponskill | Big hit, more accurate from the side. 3000 TP, 80 s. | ⬜ | Bug: flat damage |
| PGL-29 | Simian Thrash | 50 | Weaponskill | Nine light hits. 2000 TP, 80 s. | ⬜ | Bug: flat damage |

## Job actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| PGL-30 | Shoulder Tackle | 30 | Ability | Charging hit; stuns if the enemy isn't fighting you. 60 s. | ⬜ | Bug: still builds enmity |
| PGL-31 | Spinning Heel | 35 | Ability | Extra kick each swing, weaker hits. 20 s, 120 s recast. | ⬜ | Bug: hits not weaker |
| PGL-32 | Fists of Wind | 40 | Ability | Wind attacks, faster run, weaponskill recast -20%; drains MP. | ⬜ | Missing: does nothing |
| PGL-33 | Dragon Kick | 45 | Weaponskill | Two hits; after Sucker Punch may block enemy weaponskills. 2000 TP, 60 s. | ⬜ | Bug: one hit, combo broken |
| PGL-34 | Hundred Fists | 50 | Ability | Faster auto-attacks for a while. 15 min recast. | ⬜ | Duration and speed unknown |

## Traits
| ID | Trait | Lv | What it does | Status | Notes |
|---|---|---|---|---|---|
| PGL-35 | Enhanced Physical Attack | 8 | Attack +8. | ⬜ | |
| PGL-36 | Enhanced Evasion | 12 | Evasion +8. | ⬜ | |
| PGL-37 | Enhanced Physical Accuracy | 16 | Accuracy +8. | ⬜ | |
| PGL-38 | Enhanced Second Wind | 20 | Second Wind heals 25% more. | ⬜ | |
| PGL-39 | Enhanced Blindside | 24 | Blindside also adds attack based on INT. | ⬜ | Missing. Amount unknown |
| PGL-40 | Enhanced Featherfoot | 28 | Featherfoot restores 25% more MP. | ⬜ | |
| PGL-41 | Enhanced Physical Crit Damage | 32 | Critical hits +10 stronger. | ⬜ | |
| PGL-42 | Enhanced Fists of Earth | 36 | Fists of Earth defense +5% more. | ⬜ | Missing |
| PGL-43 | Enhanced Physical Attack II | 40 | Attack +10. | ⬜ | |
| PGL-44 | Enhanced Fists of Fire | 44 | Fists of Fire attack +5% more. | ⬜ | Missing |
| PGL-45 | Swift Taunt | 48 | Taunt recast 45 s instead of 60. | ⬜ | Bug: applies from Lv24 |

## Status effects
| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| PGL-46 | Featherfoot | Featherfoot | Evasion up; ends on first evade or 30 s. | ⬜ | Evasion amount unknown |
| PGL-47 | Stun | Pounce, Haymaker, Shoulder Tackle | Enemy can't act. 10 s from Pounce, about 8 s from Tackle. | ⬜ | Haymaker stun length unknown |
| PGL-48 | Blind | Concussive Blow | Enemy misses more. 30 s. | ⬜ | Bug: no effect on monsters |
| PGL-49 | Blindside | Blindside | Next rear weaponskill crits; ends on use or 60 s. | ⬜ | |
| PGL-50 | Fists of Earth | Fists of Earth | Defense +20% (+25% at 36). MP drain. | ⬜ | Missing |
| PGL-51 | Fists of Fire | Fists of Fire | Attack +20% (+25% at 44). MP drain. | ⬜ | Missing |
| PGL-52 | Fists of Wind | Fists of Wind | Faster run, shorter weaponskill recast. MP drain. | ⬜ | Missing. Speed amount unknown |
| PGL-53 | Slow | Aura Pulse | Enemy attacks slower. 30 s. | ⬜ | Amount unknown |
| PGL-54 | Taunted | Taunt | Enemy locked onto you. 5 s. | ⬜ | Missing |
| PGL-55 | Spinning Heel | Spinning Heel | Three hits per swing, each weaker. 20 s. | ⬜ | |
| PGL-56 | Pacification | Dragon Kick | Enemy can't use weaponskills. | ⬜ | Duration unknown |
| PGL-57 | Hundred Fists | Hundred Fists | Faster auto-attacks. | ⬜ | Duration unknown |
| PGL-58 | Combo | Any combo step | Shows the combo is live; next step is free. About 12 s. | ⬜ | Wrong: 13 s, should be 12 s |

## Mechanics
| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| PGL-59 | Combo: Pummel chain | Pummel, then Concussive Blow (more damage), then Simian Thrash (more damage). | ⬜ | |
| PGL-60 | Combo: Pummel to Aura Pulse | Aura Pulse after Pummel may slow. | ⬜ | |
| PGL-61 | Combo: Pounce to Sucker Punch | Sucker Punch restores more MP, then Dragon Kick (Monk). | ⬜ | Bug: Dragon Kick step broken |
| PGL-62 | Combo: Pounce to Demolish | Demolish removes an effect, then Howling Fist hits harder. | ⬜ | |
| PGL-63 | Free combo steps | Only the first step costs TP. | ⬜ | |
| PGL-64 | Combo breaks | A miss or evade ends the chain. | ⬜ | |
| PGL-65 | Positions | Wrong position loses the bonus, not the combo. | ⬜ | Bug: wrong position breaks combo |
| PGL-66 | Haymaker timing | Lights up only briefly after you evade; parry doesn't count. | ⬜ | Bug: never times out. Window unknown |
| PGL-67 | Blindside on multi-hit | Only the first hit of a multi-hit weaponskill crits. | ⬜ | Missing |
| PGL-68 | One Fists at a time | Using a Fists replaces the other; reuse turns it off. | ⬜ | Missing |
| PGL-69 | Fists MP cost | 28 MP every tick while a Fists is on. | ⬜ | Missing. Out-of-MP result unknown |
| PGL-70 | Fists element | Attacks use the stance's element against enemy weakness. | ⬜ | Missing |
| PGL-71 | Spinning Heel or Hundred Fists | The two can't be on together. | ⬜ | Missing |
| PGL-72 | Stun and blind resistance | Repeats in a short time last half, a quarter, then fail. | ⬜ | Missing |
| PGL-73 | Shoulder Tackle TP | Gives TP like one swing. | ⬜ | Missing |
| PGL-74 | Body part damage | Concussive Blow hits head, Demolish legs, Dragon Kick rear. | ⬜ | Missing |
| PGL-75 | Weaponskill damage | Scales with weapon damage per second, Strength and Intelligence. | ⬜ | Bug: always flat damage |
| PGL-76 | Other classes using Pugilist actions | War classes get Pummel, Concussive, Sucker Punch, Demolish; all get Featherfoot, Second Wind, Blindside. | ⬜ | |
| PGL-77 | Second Wind INT bonus | Extra heal from INT only for Pugilist and Monk. | ⬜ | |
| PGL-78 | Battle Regimen | Not in 1.23b. | ➖ | |

## Unknowns
- Hundred Fists duration and speed.
- Haymaker stun and Dragon Kick pacify durations; every stun/blind/slow chance.
- Fists of Wind run speed and MP cost; what happens to a Fists at 0 MP.
- Size of position and combo bonuses; Sucker Punch MP formula.
- Haymaker window after an evade; Featherfoot evasion amount; Enhanced Blindside amount.
- Which Monk quest teaches which action; Monk artifact armor bonuses beyond body and boots.
- Exact melee reach for weaponskills.

[Back to overview](../README.md)
