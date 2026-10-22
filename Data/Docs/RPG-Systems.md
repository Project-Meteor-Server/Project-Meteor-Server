# RPG systems: sources

Where the numbers for race, stats, attribute points, levels and EXP come from.

**Trust**, best to worst:
- **Capture**: recorded from the real 1.23b server.
- **Game files**: read from the 1.23b client.
- **Official**: Square Enix patch notes or developer posts.
- **Player test**: a player's own measurement.
- **Fan wiki**: community pages, often unsourced.
- **Our guess**: nothing settles it, so we chose.

Forum threads are at `forum.square-enix.com/ffxiv/threads/<number>`. Public captures are in [XIVLegacy xivl-captures](https://github.com/XIVLegacy/xivl-captures) (`pcap-1.23b`).

## Race

| What | Source | Trust | Our part |
|---|---|---|---|
| Clan attributes and resistances (table below) | `ffxivgame.exe`, the character creation table | Game files | Which row is which tribe id |
| Race only changes starting values | Patch 1.19 notes (thread 24910) | Official | That growth is the same for everyone |
| Attributes get (clan value − 15) at every level; resistances are the clan value | — | — | Our guess |

| Tribe id | Clan | STR | VIT | DEX | INT | MND | PIE | Fire | Ice | Wind | Earth | Lightning | Water |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| 1, 2 | Midlander | 16 | 15 | 14 | 16 | 13 | 16 | 16 | 13 | 15 | 15 | 15 | 16 |
| 3 | Highlander | 18 | 17 | 15 | 13 | 15 | 12 | 15 | 16 | 14 | 14 | 18 | 13 |
| 4, 5 | Wildwood | 14 | 13 | 18 | 17 | 12 | 16 | 12 | 14 | 18 | 17 | 14 | 15 |
| 6, 7 | Duskwight | 15 | 14 | 15 | 18 | 15 | 13 | 14 | 16 | 12 | 17 | 15 | 16 |
| 8, 9 | Plainsfolk | 13 | 13 | 17 | 16 | 15 | 16 | 14 | 13 | 15 | 16 | 17 | 15 |
| 10, 11 | Dunesfolk | 12 | 12 | 15 | 16 | 17 | 18 | 17 | 12 | 16 | 18 | 15 | 12 |
| 12 | Seeker of the Sun | 16 | 15 | 17 | 13 | 14 | 15 | 18 | 15 | 13 | 15 | 12 | 17 |
| 13 | Keeper of the Moon | 13 | 12 | 16 | 14 | 18 | 17 | 13 | 18 | 15 | 14 | 16 | 14 |
| 14 | Sea Wolf | 17 | 18 | 13 | 12 | 16 | 14 | 13 | 17 | 17 | 12 | 13 | 18 |
| 15 | Hellsguard | 15 | 16 | 12 | 15 | 17 | 15 | 18 | 14 | 16 | 13 | 14 | 15 |

## Attributes and HP/MP

| What | Source | Trust | Our part |
|---|---|---|---|
| Lv8–9 Pugilist STR 35–37 | Japanese forum, 2012-02-23 | Player test | — |
| Lv50 attributes around 230–255 | Threads 36412, 50479; BlueGartr 107403 | Player test | Some "naked" values are assumed |
| HP and MP at Lv26, 27, 31 and 50 | Captures `login`, `party_battle_leve`, `gridania_to_coerthas`, `war_quest_update2` | Capture | — |
| Lv50 caster MP ~2885 | Japanese forum thread 32893 | Player test | — |
| Which two attributes each class favours | Patch 1.20 notes (thread 32606) | Official | How strongly; Thaumaturge gets INT as its top attribute |
| Growth between those points, class HP/MP factors | — | — | Our guess |

## Stats from attributes

| What | Source | Trust |
|---|---|---|
| 1 VIT = 1 HP, 1 PIE = 1 MP | Threads 28942, 26648 | Player test |
| 4 INT, MND or PIE = 1 point of a magic stat | Threads 28942, 26648 | Player test |
| 3 STR = 2 attack | kanican.livejournal.com/55915 | Player test |
| 4 VIT = 1 enhancing magic | Thread 41900 | Fan wiki (another post says 5) |

## Attribute points

| What | Source | Trust | Our part |
|---|---|---|---|
| 5 at level 10, then 1 more per level | Patch 1.20 notes (thread 32606) | Official | — |
| At most 3 + (level − 10) / 2 in one attribute | Same | Official | — |
| Per class; jobs use their class's points | Same; patch 1.21 notes (thread 39024) | Official | — |
| Unspent until the player allots them | How the retail screen works | Game files | — |

## Gear

| What | Source | Trust | Our part |
|---|---|---|---|
| Broken gear gives nothing | Patch 1.19 notes (thread 24910) | Official | — |
| Gear above your level counts for less | Client script, via [ffxivDecomp](https://github.com/Yokimitsuro/ffxivDecomp) | Game files (second-hand) | That it applies to every stat |

## Levels and EXP

| What | Source | Trust | Our part |
|---|---|---|---|
| EXP to next level | Client's own table | Game files | — |
| Base EXP per kill by level | Original server code only | None | Lv23–26 may be 10 too low |
| Bonus or penalty for level difference | Original server code only | None | — |
| Cap on the bonus (1.5× to 3×) | Patch 1.19 notes say there is one; worked out from a fan table | Fan wiki | The values |
| Party share (0.75, 0.67, 0.58) | Fan table (eLeMeN) | Fan wiki | 5+ members: 0.5 |
| Only members within 10 levels share | 2nWIKI | Fan wiki | 50 yalm range |
| Link and chain bonuses, chain timer | Patch 1.19 notes (thread 24910) | Official | — |
| Food +3% | Patch 1.21 notes; fan table for the 3% | Official + Fan wiki | — |
| Rested +50%, not used up at level 50 | Patch 1.21 notes (thread 39024) | Official | Pool size and fill time |
| Inn "Resting" status | Captures `return_to_inn`, `move_out_of_room` | Capture | — |

## Monsters

| What | Source | Trust | Our part |
|---|---|---|---|
| HP at a few levels | Public and private captures | Capture | — |
| The rest of the HP curve, family multipliers, MP | Another server's database, citing captures we can't open | Second-hand | Levels 41–67 |
| Attributes | — | — | Our guess |

## Open issues

- Base EXP per kill and the level-difference rule have no source we can check. Replace them first.
- Base EXP at levels 23–26 is probably 10 too low.
- Two comments in `stats.lua` cite the wrong source, and `Player.CalcPotencial` should be named after the client's `getPotencial`.
