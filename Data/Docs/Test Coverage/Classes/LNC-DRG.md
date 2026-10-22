# Lancer / Dragoon

Melee damage with a polearm. Dragoon needs Lancer 30 and Pugilist 15, the job quest, and the soul crystal.

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b

## Basics
| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| LNC-01 | Become Lancer | Equipping a polearm switches you to Lancer. | ⬜ | |
| LNC-02 | Lancers' Guild | Guild in Gridania gives Lancer class quests. | ⬜ | Missing |
| LNC-03 | Class quests | A Wailing Welcome (20), Culture Shock (30), Necessary Evils (36). | ⬜ | Missing |
| LNC-04 | Job quest gate | First Dragoon quest needs Lancer 30 and Pugilist 15. | ⬜ | Missing |
| LNC-05 | Dragoon job quests | Eye of the Dragon (30) through Into the Dragon's Maw (50). Six quests. | ⬜ | Missing |
| LNC-06 | Soul crystal | First quest gives Soul of the Dragoon. Equip it to become Dragoon. | ⬜ | |
| LNC-07 | Leave the job | Unequip the crystal to go back to Lancer. Level and EXP shared. | ⬜ | |
| LNC-08 | Job keeps class actions | Dragoon can use every Lancer action. | ⬜ | |
| LNC-09 | Job actions only as Dragoon | Dragoon actions auto-set when learned. Lancer can't use them. | ⬜ | |
| LNC-10 | Job cross-class limit | Dragoon sets only Pugilist and Archer actions, 3-5 slots by level. | ⬜ | Missing: no limit |
| LNC-11 | Class cross-class rules | Lancer weaponskills: warrior classes only. Some actions are Lancer-only. | ⬜ | Missing |
| LNC-12 | Gear | Dragoon wears Lancer gear and Dragoon gear. Drachen set is Dragoon-only. | ⬜ | |
| LNC-13 | Drachen armor bonuses | Mail: more Invigorate TP. Breeches: Jump recast 6 s shorter. | ⬜ | Bug: no Breeches bonus |
| LNC-14 | Auto-attack | Swings every weapon delay (about 3 s). About 326 TP per hit at 50. | ⬜ | |

## Class actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| LNC-15 | True Thrust | 1 | Weaponskill | Hit. More accurate from the front. 1000 TP. 10 s recast. | ⬜ | |
| LNC-16 | Vorpal Thrust | 2 | Weaponskill | Hit. More crits from behind. 1500 TP. 20 s recast. | ⬜ | |
| LNC-17 | Feint | 4 | Weaponskill | Accurate hit, only right after you miss. 250 TP. | ⬜ | |
| LNC-18 | Blood for Blood | 6 | Ability | Costs HP. Next weaponskill hits harder. 60 s recast. | ⬜ | HP cost and bonus unknown |
| LNC-19 | Heavy Thrust | 10 | Weaponskill | Hit. May stun 4 s if target attacks you. 1500 TP. | ⬜ | Bug: stuns even if not targeted |
| LNC-20 | Invigorate | 14 | Ability | Gives TP over 30 s. No TP loss out of combat. 90 s recast. | ⬜ | |
| LNC-21 | Impulse Drive | 18 | Weaponskill | Hit. More damage from the side. 1500 TP. 30 s recast. | ⬜ | |
| LNC-22 | Life Surge | 22 | Ability | Auto-attacks drain HP. Stacks 3 times. 3 min. 250 TP. | ⬜ | Drain amount unknown |
| LNC-23 | Keen Flurry | 26 | Ability | Costs HP. Next weaponskill recasts faster. 90 s recast. | ⬜ | Bug: costs no HP |
| LNC-24 | Leg Sweep | 30 | Weaponskill | Hits enemies around you (8 yalms). 1000 TP. 30 s recast. | ⬜ | Wrong animation |
| LNC-25 | Power Surge | 34 | Ability | Attack up, defense down. Grows to 3 stages. 10 s recast. | ⬜ | Wrong: stage size and growth rate |
| LNC-26 | Full Thrust | 38 | Weaponskill | Accurate hit that gives TP, only after you miss. 250 TP. | ⬜ | TP gained unknown |
| LNC-27 | Dread Spike | 42 | Ability | Next physical hit taken heals you instead. 2 min recast. | ⬜ | Amount and duration unknown |
| LNC-28 | Doom Spike | 46 | Weaponskill | Hits everything in a line ahead (8 yalms). 3000 TP. | ⬜ | Bug: always deals 5000 |
| LNC-29 | Chaos Thrust | 50 | Weaponskill | Six hits, bigger crits. 3000 TP. 80 s recast. | ⬜ | Bug: no crit damage bonus |

## Job actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| LNC-30 | Jump | 30 | Ability | Leap onto target, hit, return. Dodges hits mid-air. 60 s recast. | ⬜ | Bug: no dodge |
| LNC-31 | Disembowel | 35 | Weaponskill | Hit. May paralyze. 750 TP. 30 s recast. | ⬜ | Duration unknown |
| LNC-32 | Elusive Jump | 40 | Ability | Leap far back, dodge hits, lose enmity. 3 min recast. | ⬜ | Bug: does nothing |
| LNC-33 | Ring of Talons | 45 | Weaponskill | Hits enemies around you (8 yalms). 2000 TP. 60 s recast. | ⬜ | |
| LNC-34 | Dragonfire Dive | 50 | Ability | Leaping fire attack on target and nearby foes. 15 min recast. | ⬜ | Wrong animation |

## Traits
| ID | Trait | Lv | What it does | Status | Notes |
|---|---|---|---|---|---|
| LNC-35 | Attack Power I / II / III | 8, 20, 40 | Attack +8, +10, +12. | ⬜ | |
| LNC-36 | Store TP | 12 | 5% more TP from attacks. | ⬜ | |
| LNC-37 | Swift Blood for Blood | 16 | Blood for Blood recast 15 s shorter. | ⬜ | |
| LNC-38 | Crit Accuracy | 24 | Physical crit +10. | ⬜ | |
| LNC-39 | Enhanced Invigorate | 28 | Invigorate lasts 15 s longer. | ⬜ | |
| LNC-40 | Enhanced Life Surge | 32 | Life Surge drains 20% more. | ⬜ | Wrong: adds 20 points, not 20% |
| LNC-41 | Enhanced Keen Flurry | 36 | Keen Flurry cuts recast 50% more. | ⬜ | Wrong: cuts recast to zero |
| LNC-42 | Enhanced Power Surge | 44 | Power Surge 10% stronger. | ⬜ | |
| LNC-43 | Enhanced Blood for Blood | 48 | Blood for Blood 25% stronger. | ⬜ | |

## Status effects
| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| LNC-44 | Blood for Blood | Blood for Blood | Next weaponskill hits harder, then ends. | ⬜ | |
| LNC-45 | Invigorate | Invigorate | TP every tick for 30 s (45 with trait). | ⬜ | |
| LNC-46 | Life Surge I-III | Life Surge | Auto-attacks drain HP. Icon shows stack. 3 min. | ⬜ | |
| LNC-47 | Keen Flurry | Keen Flurry | Next weaponskill recasts faster, then ends. | ⬜ | Duration unknown |
| LNC-48 | Power Surge I-III | Power Surge | Attack up, defense down. 60 s, refreshed by weaponskills. | ⬜ | |
| LNC-49 | Dread Spike | Dread Spike | Next physical hit heals you, then ends. | ⬜ | Duration unknown |
| LNC-50 | Stun | Heavy Thrust, Leg Sweep | Target can't act. 4 s / 8 s. | ⬜ | |
| LNC-51 | Paralysis | Disembowel | Target sometimes fails to act. | ⬜ | Duration unknown |
| LNC-52 | Combo | Any combo starter | Shows the next combo step is ready. | ⬜ | |

## Mechanics
| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| LNC-53 | Combo window | Next combo step must land within about 12 s. | ⬜ | Wrong: 13 s |
| LNC-54 | Combo: True Thrust | Then Heavy Thrust (longer stun) or Leg Sweep (stun chance). | ⬜ | |
| LNC-55 | Combo: Leg Sweep | Then Doom Spike is more accurate. | ⬜ | |
| LNC-56 | Combo: Vorpal Thrust | Then Impulse Drive, then Chaos Thrust or Ring of Talons. More crits each step. | ⬜ | |
| LNC-57 | Combo: Feint | Then Disembowel paralyzes longer (Dragoon). | ⬜ | |
| LNC-58 | Combo break | Any other weaponskill mid-chain ends the combo. | ⬜ | |
| LNC-59 | After-a-miss actions | Feint and Full Thrust light up only right after a miss. | ⬜ | Window length unknown |
| LNC-60 | Positions | Front, rear, side bonuses apply only from that side. | ⬜ | Bonus sizes unknown |
| LNC-61 | Surge exclusivity | Life Surge and Power Surge cancel each other. Reusing Power Surge ends it. | ⬜ | |
| LNC-62 | Power Surge growth | Each weaponskill or Jump refreshes it. 10 refreshes per stage. | ⬜ | Wrong: grows after 1 |
| LNC-63 | Dread Spike uses surges | Eats an active Surge to heal more. | ⬜ | |
| LNC-64 | Multi-hit and buffs | Next-weaponskill buffs boost only the first hit. Throw doesn't use them. | ⬜ | |
| LNC-65 | Stun and paralysis wear-off | Repeats on one target get shorter, then immune. | ⬜ | Missing |
| LNC-66 | TP loss | No TP loss in combat. Lose 90 TP per 3 s out of combat. | ⬜ | |
| LNC-67 | Damage and weapon | Weaponskills follow weapon DPS. Auto-attack and Jump follow weapon damage. | ⬜ | |

## Unknowns
- Power Surge per-stage attack and Life Surge drain amount.
- Blood for Blood and Keen Flurry HP cost, bonus and duration.
- Which Dragoon quest teaches which action (six quests, five actions).
- Jump and Elusive Jump dodge length, Elusive Jump distance and enmity cut.
- Durations of Disembowel paralysis, combo Heavy Thrust stun, Dread Spike.
- How long Feint and Full Thrust stay usable after a miss; Full Thrust TP gain.
- Melee reach in yalms.
- Bonuses of the other Drachen pieces; any Lancer class quests below 20 or above 36.

[Back to overview](../README.md)
