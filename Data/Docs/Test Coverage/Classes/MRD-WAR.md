# Marauder / Warrior

Tank and damage dealer with a great axe, no shield; defends by parrying. Warrior needs Marauder 30 and Gladiator 15, the job quest, and the soul crystal.

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b

## Basics
| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| MRD-01 | Become a Marauder | Equip a great axe; class changes to Marauder. | ⬜ | |
| MRD-02 | Class quests | Marauders' Guild, Limsa: quests at Lv 20, 30, 36. | ⬜ | Missing |
| MRD-03 | Warrior unlock | Marauder 30 + Gladiator 15 opens the first job quest. | ⬜ | Missing |
| MRD-04 | Soul crystal | Quest gives Soul of the Warrior; equip it to become Warrior. | ⬜ | |
| MRD-05 | Job quests | Quests at 35, 40, 45, 50 teach the job actions. | ⬜ | Missing |
| MRD-06 | Warrior gear | Fighter's armour from the Lv 45 quest; Warrior only. | ⬜ | Missing |
| MRD-07 | Warrior action bar | Job actions set automatically; 3–5 slots for Gladiator or Pugilist actions. | ⬜ | Missing |
| MRD-08 | Lending actions | Other classes can borrow some actions; exclusive ones stay Marauder-only. | ⬜ | Missing |
| MRD-09 | No shield | Can't equip a shield; never blocks. | ⬜ | |
| MRD-10 | Auto-attack | Melee swings at the axe's delay (about 4 s). | ⬜ | |
| MRD-11 | High HP | More max HP than a Gladiator of the same level. | ⬜ | |

## Class actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| MRD-12 | Heavy Swing | 1 | Weaponskill | Hit; more accurate from the front. 1000 TP, 10 s. | ⬜ | Bug: no front bonus |
| MRD-13 | Foresight | 2 | Ability | Parry up until you parry, max 30 s. 60 s recast. | ⬜ | Parry bonus unknown |
| MRD-14 | Brutal Swing | 4 | Weaponskill | Hit; more damage from the side. 1500 TP, 20 s. | ⬜ | |
| MRD-15 | Bloodbath | 6 | Ability | Next weaponskill drains HP. Lasts 30 s, 60 s recast. | ⬜ | Bug: never used up |
| MRD-16 | Skull Sunder | 10 | Weaponskill | Hit; extra enmity after Heavy Swing. 1500 TP, 30 s. | ⬜ | |
| MRD-17 | Provoke | 14 | Ability | Pulls the enemy's attention to you. 30 s recast. | ⬜ | |
| MRD-18 | Fracture | 18 | Weaponskill | After a parry: hit, may stop enemy weaponskills 8 s. 500 TP. | ⬜ | |
| MRD-19 | Berserk | 22 | Ability | Toggle: each hit adds attack, lowers defense; resets when hit. | ⬜ | Bug: defense keeps dropping |
| MRD-20 | Maim | 26 | Weaponskill | Hit; more accurate after Brutal Swing. 1500 TP, 30 s. | ⬜ | Bug: may do no damage |
| MRD-21 | Overpower | 30 | Weaponskill | After a parry: hits everyone in front. 250 TP, 5 s. | ⬜ | |
| MRD-22 | Rampage | 34 | Ability | Toggle: hits taken speed you up, raise parry; crits heal. | ⬜ | |
| MRD-23 | Path of the Storm | 38 | Weaponskill | Hit; may slow the enemy from behind. 1500 TP, 30 s. | ⬜ | Slow duration unknown |
| MRD-24 | Enduring March | 42 | Ability | Slower walk, resist all debuffs 20 s. 180 s recast. | ⬜ | Bug: debuffs not resisted |
| MRD-25 | Whirlwind | 46 | Weaponskill | Hits all around you, 10 yalms. 3000 TP, 80 s. | ⬜ | Wrong: area 8 yalms |
| MRD-26 | Godsbane | 50 | Weaponskill | Three hits; ends Berserk for accuracy. 3000 TP, 60 s. | ⬜ | Bug: Berserk bonus never works |

## Job actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| MRD-27 | Vengeance | 30 | Ability | Sends physical damage back at attackers 15 s. 150 s recast. | ⬜ | Bug: errors with Fighter's body armour |
| MRD-28 | Antagonize | 35 | Ability | All your enmity ×1.5 for 15 s. 120 s recast. | ⬜ | Wrong: gives ×2.5 |
| MRD-29 | Collusion | 40 | Ability | Takes some enmity from an ally's next attack. 90 s recast. | ⬜ | Wrong: lasts 15 s, should be 30 s |
| MRD-30 | Steel Cyclone | 45 | Weaponskill | Hits all around you; may stun your attacker. 1000 TP, 30 s. | ⬜ | Wrong: costs 2000 TP |
| MRD-31 | Mighty Strikes | 50 | Ability | Every axe hit crits for 15 s. 15 min recast. | ⬜ | Bug: only Steel Cyclone crits |

## Traits
| ID | Trait | Lv | What it does | Status | Notes |
|---|---|---|---|---|---|
| MRD-32 | Attack up | 8 | Attack +8. | ⬜ | |
| MRD-33 | Defense up | 12 | Defense +8. | ⬜ | |
| MRD-34 | Swift Bloodbath | 16 | Bloodbath recast 45 s. | ⬜ | |
| MRD-35 | Swift Foresight | 20 | Foresight recast 45 s. | ⬜ | |
| MRD-36 | Parry up | 24 | Parry +8. | ⬜ | |
| MRD-37 | Enhanced Provoke | 28 | Provoke also lowers enemy attack 30 s. | ⬜ | Bug: attack-down never applied |
| MRD-38 | Crit evasion up | 32 | Enemies crit you less (+10). | ⬜ | |
| MRD-39 | Enhanced Berserk | 36 | Berserk 20% stronger. | ⬜ | |
| MRD-40 | Defense up II | 40 | Defense +10. | ⬜ | |
| MRD-41 | Enhanced Rampage | 44 | Rampage crit healing doubled. | ⬜ | Missing |
| MRD-42 | Enhanced Enduring March | 48 | Walk 20% faster during Enduring March. | ⬜ | |

## Status effects
| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| MRD-43 | Foresight | Foresight | Parry up; ends on your next parry. 30 s. | ⬜ | |
| MRD-44 | Bloodbath | Bloodbath | Next weaponskill drains HP. 30 s. | ⬜ | |
| MRD-45 | Attack Down | Provoke (Lv 28+) | Enemy hits weaker. 30 s. | ⬜ | Missing |
| MRD-46 | Pacification | Fracture | Enemy can't use weaponskills. 8 s. | ⬜ | Bug: may not apply |
| MRD-47 | Berserk | Berserk | Attack up, defense down per hit. | ⬜ | Size and max stacks unknown |
| MRD-48 | Rampage | Rampage | Faster swings, more parry; fades over time. | ⬜ | Size and fade rate unknown |
| MRD-49 | Heavy | Path of the Storm | Enemy moves slower. | ⬜ | Duration unknown |
| MRD-50 | Enduring March | Enduring March | Slow walk, no debuffs, Rampage doesn't fade. 20 s. | ⬜ | |
| MRD-51 | Vengeance | Vengeance | Reflects physical damage. 15 s. | ⬜ | Reflect amount unknown |
| MRD-52 | Antagonize | Antagonize | More enmity from everything. 15 s. | ⬜ | |
| MRD-53 | Collusion | Collusion | Ally's next attack sends you its enmity. 30 s. | ⬜ | |
| MRD-54 | Stun | Steel Cyclone | Enemy can't act. | ⬜ | Bug: may not apply; duration unknown |
| MRD-55 | Mighty Strikes | Mighty Strikes | All axe hits crit. 15 s. | ⬜ | |

## Mechanics
| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| MRD-56 | Combo 1 | Heavy Swing → Skull Sunder → Steel Cyclone (Warrior only). | ⬜ | |
| MRD-57 | Combo 2 | Brutal Swing → Maim → Godsbane. | ⬜ | |
| MRD-58 | Combo 3 | Path of the Storm → Whirlwind; uses up Berserk for damage. | ⬜ | Bug: Berserk bonus never works |
| MRD-59 | Free combo steps | A combo step costs no TP. | ⬜ | |
| MRD-60 | Combo timer | Next step must come soon after the last. | ⬜ | Exact window unknown (6–10 s) |
| MRD-61 | Combo rules | Positional must be met; borrowed weaponskills don't combo. | ⬜ | |
| MRD-62 | Parry window | Fracture and Overpower usable for a few seconds after a parry. | ⬜ | Exact window unknown |
| MRD-63 | Ready blink | Icons blink when a combo or parry action is ready. | ⬜ | |
| MRD-64 | One stance | Berserk and Rampage cancel each other; reuse turns off. | ⬜ | |
| MRD-65 | Stance payoffs | Steel Cyclone ends Rampage for damage. | ⬜ | |
| MRD-66 | Multi-hit drain | Bloodbath drains on the first hit only. | ⬜ | |
| MRD-67 | Area attacks | Hit about 8 enemies at most. | ⬜ | |
| MRD-68 | Debuff fatigue | Repeat stun, slow, weaponskill-lock get shorter, then immune. | ⬜ | Missing |
| MRD-69 | Warrior gear bonuses | Fighter's armour: Vengeance reflects magic, Antagonize ×1.65. | ⬜ | |
| MRD-70 | Job actions locked | Job actions only usable as Warrior. | ⬜ | |

## Unknowns
- Berserk and Rampage: size per stack, max stacks, Rampage fade speed.
- Exact combo window and parry window.
- How long Path of the Storm's slow and Steel Cyclone's stun last.
- How much enmity Collusion moves; how much Vengeance reflects.
- Foresight's parry bonus and Attack Down's size.
- Whether Enhanced Enduring March means faster than normal or just less slow.
- Whether Marauder class quests above Lv 36 existed.

[Back to overview](../README.md)
