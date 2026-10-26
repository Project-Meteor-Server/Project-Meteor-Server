# Gladiator / Paladin

Tank with sword and shield. Paladin needs Gladiator 30 and Conjurer 15, the job quest, and the soul crystal.

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b

## Basics
| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| GLA-01 | Become Gladiator | Equip a one-handed sword in the main hand. | ⬜ | |
| GLA-02 | Shield block | With a shield you block; without one you parry. | ⬜ | |
| GLA-03 | Guild | Gladiators' Guild at the Coliseum, Ul'dah. | ⬜ | Missing |
| GLA-04 | Class quest Lv20 | "All Bark and No Bite". | ⬜ | Missing |
| GLA-05 | Class quest Lv30 | "Unalienable Rights". | ⬜ | Missing |
| GLA-06 | Class quest Lv36 | "Thrill of the Fight". | ⬜ | Missing |
| GLA-07 | Class quest Lv40 | Never released. | ➖ | |
| GLA-08 | Job quest unlock | "Paladin's Pledge" needs Gladiator 30 and Conjurer 15. | ⬜ | Missing |
| GLA-09 | Job quest 1 | "Paladin's Pledge": gives soul crystal and Cover. | ⬜ | Missing |
| GLA-10 | Job quests 2-6 | Honor Lost, Power Struggles, Poisoned Hearts, Parley on High Ground, Keeping the Oath. | ⬜ | Missing |
| GLA-11 | Switch to Paladin | Equip soul crystal or type /job to switch. | ⬜ | |
| GLA-12 | Paladin keeps class actions | All Gladiator actions stay on the bar. | ⬜ | |
| GLA-13 | Job actions | Usable only as Paladin; set on the bar automatically. | ⬜ | Bug: no job check |
| GLA-14 | Other-class actions as Paladin | Marauder and Conjurer only; 3 slots, 4 at Lv40, 5 at Lv50. | ⬜ | Bug: no limit enforced |
| GLA-15 | Other-class actions as Gladiator | 6 slots at Lv30, one more every 5 levels. | ⬜ | Bug: no limit enforced |
| GLA-16 | Stats on job change | Stats switch to Paladin's; spent points carry over. | ⬜ | |
| GLA-17 | Job gear | Paladin wears Gladiator gear plus the Gallant set. | ⬜ | |
| GLA-18 | Shared level | Paladin levels and EXP are the Gladiator's. | ⬜ | |
| GLA-19 | Auto-attack | Swings at weapon speed; each hit gives TP. | ⬜ | |
| GLA-20 | TP | Up to 3000 TP; weaponskills spend it. | ⬜ | |

## Class actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| GLA-21 | Fast Blade | 1 | Weaponskill | Hit; stronger from the front. 1000 TP. | ⬜ | Front bonus size unknown |
| GLA-22 | Rampart | 2 | Ability | Defense up, big enmity. 60 s. Recast 2 min. | ⬜ | Defense amount unknown |
| GLA-23 | Phalanx | 4 | Weaponskill | Hit, only right after a block. 250 TP. | ⬜ | |
| GLA-24 | Aegis Boon | 6 | Ability | Blocks next hit, heals some HP. Recast 60 s. | ⬜ | |
| GLA-25 | Savage Blade | 10 | Weaponskill | Hit; stronger after Fast Blade. 1000 TP. | ⬜ | |
| GLA-26 | Flash | 14 | Ability | Big enmity on target. Recast 30 s. | ⬜ | |
| GLA-27 | Shield Bash | 18 | Weaponskill | Shield hit; stuns 5 s. 250 TP. | ⬜ | Wrong: stun should always land |
| GLA-28 | Sentinel | 22 | Ability | Take 30% less damage. 15 s. Recast 90 s. | ⬜ | |
| GLA-29 | Flat Blade | 26 | Weaponskill | Hit; big enmity after Fast Blade. 1500 TP. | ⬜ | |
| GLA-30 | Riot Blade | 30 | Weaponskill | Ranged hit; Defense Down from behind. 2000 TP. | ⬜ | |
| GLA-31 | Outmaneuver | 34 | Ability | Block more; TP on each block. 30 s. | ⬜ | Amounts unknown |
| GLA-32 | War Drum | 38 | Weaponskill | Hits enemies around you after a block. 500 TP. | ⬜ | |
| GLA-33 | Tempered Will | 42 | Ability | Removes Bind/Heavy; no knockback. 20 s. Recast 3 min. | ⬜ | Bug: no draw-in immunity |
| GLA-34 | Rage of Halone | 46 | Weaponskill | Five weak-accuracy hits; better after Riot Blade. 1500 TP. | ⬜ | |
| GLA-35 | Goring Blade | 50 | Weaponskill | Hit; bleed from behind 30 s. 3000 TP. | ⬜ | Bleed damage unknown |

## Job actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| GLA-36 | Cover | 30 | Ability | Take melee hits for a party member within 8 y. 15 s. | ⬜ | Bug: redirects nothing |
| GLA-37 | Divine Veil | 35 | Ability | Block all front hits 20 s; others' Cures heal party. | ⬜ |  |
| GLA-38 | Holy Succor | 40 | Spell | Heal; you also heal half when healing others. 2 s cast. | ⬜ |  |
| GLA-39 | Spirits Within | 45 | Weaponskill | Accurate hit; stronger at high HP. 3000 TP. | ⬜ | Bug: damage far too high |
| GLA-40 | Hallowed Ground | 50 | Ability | Immune to most attacks 20 s. Recast 15 min. | ⬜ | Bug: effect missing, can't apply |

## Traits
| ID | Trait | Lv | What it does | Status | Notes |
|---|---|---|---|---|---|
| GLA-41 | Enhanced Physical Defense | 8 | Defense +8. | ⬜ | Wrong: gives +10, should be +8 |
| GLA-42 | Enhanced Rampart | 12 | Rampart also covers party within 8 y. | ⬜ | |
| GLA-43 | Enhanced Block Rate | 16 | Block rate +10. | ⬜ | Bug: raises the wrong stat |
| GLA-44 | Swift Aegis Boon | 20 | Aegis Boon recast 45 s. | ⬜ | |
| GLA-45 | Enhanced Physical Defense II | 24 | Defense +10. | ⬜ | |
| GLA-46 | Enhanced Flash | 28 | Flash also blinds. | ⬜ | |
| GLA-47 | Enhanced Physical Crit Resilience | 32 | Take less crit damage. | ⬜ | |
| GLA-48 | Enhanced Sentinel | 36 | Sentinel cuts 50% damage. | ⬜ | |
| GLA-49 | Enhanced Physical Defense III | 40 | Defense +12. | ⬜ | |
| GLA-50 | Enhanced Outmaneuver | 44 | Blocks under Outmaneuver restore MP. | ⬜ | Bug: starts at Lv20 |
| GLA-51 | Enhanced Flash II | 48 | Flash also hits enemies near the target. | ⬜ | Bug: no area effect |

## Status effects
| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| GLA-52 | Rampart | Rampart | Defense up 60 s. | ⬜ | |
| GLA-53 | Aegis Boon | Aegis Boon | Next hit blocked, HP back. 30 s. | ⬜ | |
| GLA-54 | Blind | Flash | Accuracy down. | ⬜ | Duration unknown |
| GLA-55 | Stun | Shield Bash | Can't act 5 s; shorter if repeated. | ⬜ | |
| GLA-56 | Sentinel | Sentinel | Less damage taken, more enmity. 15 s. | ⬜ | |
| GLA-57 | Defense Down | Riot Blade | Target's defense down 30 s. | ⬜ | Amount unknown |
| GLA-58 | Outmaneuver | Outmaneuver | Block up, TP per block. 30 s. | ⬜ | |
| GLA-59 | Tempered Will | Tempered Will | No knockback or draw-in. 20 s. | ⬜ | |
| GLA-60 | Goring Blade | Goring Blade | Bleed damage over 30 s. | ⬜ | Tick size unknown |
| GLA-61 | Cover / Covered | Cover | Marks Paladin and protected member. 15 s. | ⬜ | Bug: redirects nothing |
| GLA-62 | Divine Veil | Divine Veil | Blocks all front physical hits. 20 s. | ⬜ |  |
| GLA-63 | Divine Regen | Divine Veil | Party heal over time; stacks with Regen. | ⬜ | Duration unknown |
| GLA-64 | Hallowed Ground | Hallowed Ground | Immune to most attacks 20 s. | ⬜ | Missing |

## Mechanics
| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| GLA-65 | Combos | Fast > Flat; Fast > Savage > Goring; Riot > Rage; Phalanx > Spirits Within. | ⬜ | |
| GLA-66 | Combo TP | Combo follow-ups cost no TP. | ⬜ | |
| GLA-67 | Combo window | About 10 s between combo steps. | ⬜ | Wrong: 13 s, should be ~10 s |
| GLA-68 | Combo needs own class | Other classes using Fast Blade get no combo. | ⬜ | |
| GLA-69 | Combo icons blink | Next combo action's icon blinks. | ⬜ | |
| GLA-70 | Block procs | Phalanx and War Drum icons blink after a block. | ⬜ | Wrong: window too short |
| GLA-71 | Positionals | Fast Blade bonus from front; Riot/Goring effects from behind. | ⬜ | |
| GLA-72 | No rear crit bonus | Crit rate same from front and behind. | ⬜ | |
| GLA-73 | Block damage | Blocks cut damage; better shields cut more. | ⬜ | |
| GLA-74 | Enmity sizes | Flash, Rampart, War Drum, Hallowed Ground give set enmity, growing with level. | ⬜ | Bug: doesn't grow with level |
| GLA-75 | Gallant Surcoat | Cover returns a quarter of damage as MP. | ⬜ | |
| GLA-76 | Gallant Cuisses | Divine Regen heals more. | ⬜ | |
| GLA-77 | Gallant Gauntlets | Holy Succor heals 10% more. | ⬜ | |
| GLA-78 | Shared actions | Other classes can set Rampart, Flash, Shield Bash, Sentinel, Outmaneuver. | ⬜ | Bug: no class check |
| GLA-79 | Battle Regimen | Removed before 1.23b. | ➖ | |

## Unknowns
- How long the combo window and block proc really last.
- Whether Fast Blade must hit from the front to start a combo.
- Sizes: Fast Blade front bonus, Rampart, Outmaneuver, Defense Down, Goring Blade bleed, Spirits Within.
- How long Flash's Blind and Divine Regen last.
- Which job quest teaches which Paladin action.
- Whether Shield Bash needs a shield when other classes use it.
- Which attacks Cover takes, and which attacks get through Hallowed Ground.
- Whether the other Gallant pieces enhance actions.

[Back to overview](../README.md)
