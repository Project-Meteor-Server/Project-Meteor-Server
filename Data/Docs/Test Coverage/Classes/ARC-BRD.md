# Archer / Bard

Ranged bow damage dealer with slows, binds and party songs. Bard needs Archer 30 and Conjurer 15, the job quest, and the soul crystal.

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b

## Basics
| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| ARC-01 | Class by weapon | Equipping a bow makes you an Archer. | ⬜ | |
| ARC-02 | Bow and quiver | Bow is two-handed; quiver shows on the back hand; no shield. | ⬜ | |
| ARC-03 | Arrows | Arrows go in the ammo slot, stack to 999. | ⬜ | Missing: arrows never used up |
| ARC-04 | Auto-attack | Archers swing bare-handed at melee range, fixed speed. | ⬜ | Swing delay unknown |
| ARC-05 | Archers' Guild | Guild in Gridania. Class quests at 20, 30, 36. | ⬜ | Missing: no class quests |
| ARC-06 | Bard job quests | Archer 30 + Conjurer 15 unlocks six quests, Lv30 to 50. | ⬜ | Missing: no job quests |
| ARC-07 | Soul crystal | Equip Soul of the Bard or type /job to switch. | ⬜ | |
| ARC-08 | Learning job actions | First with the crystal, rest from quests; placed on bar. | ⬜ | Bug: not tied to quests |
| ARC-09 | Shared level | Bard uses the Archer level and EXP. | ⬜ | |
| ARC-10 | Borrowed actions as Archer | Up to 10 other-class actions at Lv50. | ⬜ | Missing: no slot limits |
| ARC-11 | Borrowed actions as Bard | Only Conjurer and Thaumaturge actions; 3 to 5 slots. | ⬜ | Missing: no slot limits |
| ARC-12 | Lending Archer actions | Others may use Heavy Shot, Decoy, Hawk's Eye, etc.; rest Archer-only. | ⬜ | Missing: no checks |
| ARC-13 | Job gear | Bard can't wear Archer-only gear; can wear Bard gear. | ⬜ | Missing |

## Class actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| ARC-14 | Heavy Shot | 1 | Weaponskill | Bow attack. Starts combos from 8 y away. 1000 TP. | ⬜ | Bug: flat damage |
| ARC-15 | Light Shot | 1 | Ability | Quick bow shot. Recast equals bow delay. | ⬜ | Bug: no recast, flat damage, no TP |
| ARC-16 | Refill | 1 | Ability | Refills quiver with arrows from your bags. | ⬜ | Missing |
| ARC-17 | Decoy | 2 | Ability | Dodge one ranged or magic attack. 60 s, 90 s recast. | ⬜ | Bug: errors when it should trigger |
| ARC-18 | Piercing Arrow | 4 | Weaponskill | Bow attack. Starts combos. 1000 TP, 20 s recast. | ⬜ | |
| ARC-19 | Hawk's Eye | 6 | Ability | Accuracy up 15 s. 90 s recast. | ⬜ | Accuracy amount unknown |
| ARC-20 | Leaden Arrow | 10 | Weaponskill | Bow attack, may slow target 30 s. 1500 TP. | ⬜ | |
| ARC-21 | Raging Strike | 14 | Ability | On/off. Light Shot hits stack attack up to 3. | ⬜ | Bug: no attack bonus. Recast 10 or 60 s? |
| ARC-22 | Shadowbind | 18 | Weaponskill | Bow attack, may root target 30 s. 250 TP. | ⬜ | Wrong: lasts 15 s, should be 30 |
| ARC-23 | Quelling Strike | 22 | Ability | Next attack: less enmity, more TP. 60 s recast. | ⬜ | Bug: buff never used up |
| ARC-24 | Swiftsong | 26 | Spell | Party runs faster 3 min. Ends on combat. | ⬜ | Wrong: +25%, should be +20%; doesn't end |
| ARC-25 | Gloom Arrow | 30 | Weaponskill | Bow attack. Blinds if comboed. 1000 TP. | ⬜ | |
| ARC-26 | Barrage | 34 | Ability | Next Light Shot fires several arrows, less accurate. | ⬜ | Hit count unsure: 3 or 4 |
| ARC-27 | Quick Nock | 38 | Weaponskill | Cone attack. Combo: five hits on one target. 3 min recast. | ⬜ | |
| ARC-28 | Chameleon | 42 | Ability | Drops your enmity. 3 min recast. | ⬜ | Bug: does nothing |
| ARC-29 | Bloodletter | 46 | Weaponskill | Bow attack, may bleed 30 s. 1500 TP. | ⬜ | |
| ARC-30 | Wide Volley | 50 | Weaponskill | Hits target and nearby enemies, low accuracy. 2000 TP. | ⬜ | |

## Job actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| ARC-31 | Ballad of Magi | 30 | Spell | Party regains MP 3 min. 20 MP per tick. | ⬜ | Wrong: reaches 20 y, should be 8 |
| ARC-32 | Minuet of Rigor | 35 | Spell | Party accuracy +35 for 3 min. | ⬜ | Bug: AF gloves give +25, should be +8 |
| ARC-33 | Paeon of War | 40 | Spell | Spends 1000 TP; party gains 50 TP per tick. | ⬜ | |
| ARC-34 | Rain of Death | 45 | Weaponskill | Hits target and nearby enemies. Combo may stun. 3000 TP. | ⬜ | Stun length unknown |
| ARC-35 | Battle Voice | 50 | Ability | Party max HP +25%; doubles songs sung meanwhile. 15 min recast. | ⬜ | Bug: only the bard gets it |

## Traits
| ID | Trait | Lv | What it does | Status | Notes |
|---|---|---|---|---|---|
| ARC-36 | Enhanced Physical Accuracy | 8 | Accuracy +8. | ⬜ | |
| ARC-37 | Enhanced Physical Evasion | 12 | Evasion +8. | ⬜ | |
| ARC-38 | Enhanced Decoy | 16 | Decoy also dodges melee. | ⬜ | |
| ARC-39 | Enhanced Physical Crit Evasion | 20 | Critical evasion +10. | ⬜ | |
| ARC-40 | Enhanced Physical Accuracy II | 24 | Accuracy +10. | ⬜ | |
| ARC-41 | Enhanced Hawk's Eye | 28 | Hawk's Eye gives 50% more accuracy. | ⬜ | |
| ARC-42 | Enhanced Quelling Strike | 32 | Quelling Strike gives 50% more TP. | ⬜ | |
| ARC-43 | Enhanced Raging Strike | 36 | Raging Strike 50% stronger. | ⬜ | Bug: never applies |
| ARC-44 | Enhanced Physical Crit Accuracy | 40 | Critical rate +10. | ⬜ | |
| ARC-45 | Enhanced Barrage | 44 | Barrage fires one more arrow. | ⬜ | |
| ARC-46 | Swift Chameleon | 48 | Chameleon recast 2 min. | ⬜ | |

## Status effects
| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| ARC-47 | Decoy | Decoy | Dodges the next ranged or magic hit. | ⬜ | |
| ARC-48 | Hawk's Eye | Hawk's Eye | Accuracy up 15 s. | ⬜ | |
| ARC-49 | Heavy | Leaden Arrow | Target moves slowly 30 s; longer if comboed. | ⬜ | Combo length unknown |
| ARC-50 | Raging Strike | Raging Strike | Stacks 1 to 3; resets on a miss. | ⬜ | Bug: no miss reset |
| ARC-51 | Bind | Shadowbind | Target can't move 30 s; longer if comboed. | ⬜ | Combo length unknown |
| ARC-52 | Quelling Strike | Quelling Strike | Lasts 30 s or until your next attack. | ⬜ | |
| ARC-53 | Swiftsong | Swiftsong | Faster running; ends on combat or new song. | ⬜ | |
| ARC-54 | Blind | Gloom Arrow combo | Target misses more, 30 s. | ⬜ | |
| ARC-55 | Barrage | Barrage | Waits 60 s for your next Light Shot. | ⬜ | |
| ARC-56 | Bleed | Bloodletter | Damage over 30 s; combo adds a burst at end. | ⬜ | Burst size unknown |
| ARC-57 | Ballad of Magi | Ballad of Magi | MP every tick for 3 min. | ⬜ | |
| ARC-58 | Minuet of Rigor | Minuet of Rigor | Accuracy and magic accuracy up 3 min. | ⬜ | |
| ARC-59 | Paeon of War | Paeon of War | TP every tick for 3 min. | ⬜ | |
| ARC-60 | Stun | Rain of Death combo | Target can't act. | ⬜ | Duration unknown |
| ARC-61 | Battle Voice | Battle Voice | Songs sung now are doubled. | ⬜ | Lasts 30 or 60 s? |
| ARC-62 | HP Boost | Battle Voice | Max HP +25%. | ⬜ | Duration unknown |

## Mechanics
| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| ARC-63 | Heavy Shot combos | Leaden Arrow then Wide Volley; or Quick Nock then Rain of Death. | ⬜ | |
| ARC-64 | Piercing Arrow combos | Gloom Arrow then Bloodletter; or Shadowbind. | ⬜ | Bug: Shadowbind combo adds nothing |
| ARC-65 | 8-yalm rule | Openers only start a combo from 8 y or farther. | ⬜ | Missing |
| ARC-66 | Combo window | About 10 s to use the next step. | ⬜ | Exact window unknown |
| ARC-67 | Light Shot keeps combos | Light Shot doesn't break a combo or use up buffs. | ⬜ | |
| ARC-68 | Quelling ends Raging | Quelling Strike ends Raging Strike for extra TP per stack. | ⬜ | Bug: TP ignores stacks |
| ARC-69 | Running out of arrows | Bow attacks stop at 0 arrows until Refill. | ⬜ | Missing |
| ARC-70 | One song per bard | A new song replaces your last; two bards' songs stack. | ⬜ | |
| ARC-71 | Song area | Songs reach party within 8 y, last 3 min. | ⬜ | Wrong: reaches 20 y |
| ARC-72 | Song casting | Bard songs take 3 s; Swiftsong is instant; moving interrupts. | ⬜ | |
| ARC-73 | Bard gear and songs | Choral body, gloves, legs each boost one song. | ⬜ | |
| ARC-74 | Crowd-control fatigue | Repeat slows, binds, blinds get shorter, then fail. | ⬜ | Missing |
| ARC-75 | Body-part hits | Bloodletter hits head, Leaden Arrow legs, Gloom Arrow special. | ⬜ | Missing |
| ARC-76 | Bow strength | Weaponskills scale with bow and arrow; PIE and DEX add damage. | ⬜ | Bug: weaponskills do flat damage |
| ARC-77 | Job mode | Bard keeps all Archer actions; songs only as Bard. | ⬜ | |

## Unknowns
- Raging Strike recast: 10 s or 60 s.
- Battle Voice length: 30 s or 60 s; HP Boost length.
- Barrage arrow count: 3 base, or 4.
- How many arrows each action uses; quiver size; what Refill moves.
- Combo bonus sizes: slow and root length, blind and stun chance, Wide Volley accuracy, bleed burst.
- Light Shot and Bloodletter range: 8 y or 15 y.
- Archer auto-attack swing delay.
- Swiftsong speed and song tick timing rest on forum posts.
- 1.x Archers' Guild master's name.

[Back to overview](../README.md)
