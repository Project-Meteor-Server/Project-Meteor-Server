# Disciples of the Land: Miner / Botanist / Fisher

Gathering classes. The main-hand tool sets the class. No gathering jobs in 1.23b.

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b

## Basics
| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOL-01 | Tool sets class | Pickaxe = Miner, hatchet = Botanist, fishing rod = Fisher. | ⬜ | |
| DOL-02 | Off-hand tool | Sledgehammer, scythe or gig adds a second gathering command. | ⬜ | Missing |
| DOL-03 | Gathering points | Graded nodes in the world you can target and gather. | ⬜ | Only 2 test points |
| DOL-04 | Point lights up | Walking near a point readies the gathering command. | ⬜ | Missing |
| DOL-05 | Gathering stat | Higher Gathering drains the node gauge slower. | ⬜ | Missing |
| DOL-06 | Output stat | More attempts per node; shorter wait for a bite. | ⬜ | Missing |
| DOL-07 | Perception stat | Higher chance of high-quality items. | ⬜ | Missing |
| DOL-08 | EXP and level-up | Each gather gives EXP; own level-up message. | ⬜ | Missing |
| DOL-09 | Fieldcraft leves | Regional gathering leves, Lv1–40, count points gathered. | ⬜ | Missing |
| DOL-10 | Local gathering leves | Small gathering jobs from local NPCs. | ⬜ | Unsure they existed |
| DOL-11 | Guild support | Each guild sells a gathering support buff. | ⬜ | Effect unknown |
| DOL-12 | Gathering jobs | No Miner, Botanist or Fisher job or soul crystal. | ➖ | |

## Shared actions
| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOL-13 | Stone Throw | 1 | Ability | Throw a rock at an enemy. Recast 5 s. | ⬜ | Missing |
| DOL-14 | Stealth I–IV | 8/18/28/38 | Ability | Move slowly, unseen by enemies up to Lv20/30/40/50. | ⬜ | Missing |
| DOL-15 | Wrist Flick | 10 | Ability | Next thrown stone stuns. Recast 30 s. | ⬜ | Missing |
| DOL-16 | Fingerprints of the Gods | 18 | Ability | Sometimes find catalysts at nodes. | ⬜ | Missing |

## Mechanics
| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| DOL-17 | Start gathering | Using a point opens the gathering window, grade shown. | ⬜ | Bug: always opens mining |
| DOL-18 | Aim | Pick a spot on the node's bar, then Begin. | ⬜ | |
| DOL-19 | Strike | Each hit hints higher or lower toward the sweet spot. | ⬜ | Bug: every hit succeeds |
| DOL-20 | Node gauge | Each hit drains the gauge; attempt ends at empty. | ⬜ | Wrong: flat drain |
| DOL-21 | Attempts | Limited attempts per node; message shows how many left. | ⬜ | Wrong: always 2 |
| DOL-22 | Attempts vs rank | Low rank on a high-grade node gives fewer attempts. | ⬜ | Missing |
| DOL-23 | Node respawn | A spent node refuses, then returns after a while. | ⬜ | Respawn time unknown |
| DOL-24 | Items and HQ | Hits give items; count and quality vary. | ⬜ | Bug: no items given |
| DOL-25 | Shards and catalysts | Gathering can also give shards and catalysts. | ⬜ | Missing |
| DOL-26 | Tutorial button | Explains the gathering window in chat. | ⬜ | |
| DOL-27 | Close window | Finish or cancel closes the window; you can move again. | ⬜ | |
| DOL-28 | Attacked while gathering | Some points spawn monsters; a hit stops gathering. | ⬜ | Missing |

## Miner

Guild in Ul'dah. Pickaxe main hand, sledgehammer off-hand.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOL-29 | Class quests | A Piece of History (20), Little Saboteurs (30), Runaway Little Girl (36). | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOL-30 | Mine | 1 | Gathering | Mine a mineral deposit. | ⬜ | Bug: no items given |
| DOL-31 | Quarry | 1 | Gathering | Quarry with the sledgehammer. | ⬜ | Missing |
| DOL-32 | Begin / Strike / Cancel | 1 | Gathering | Aim, swing the pickaxe, or stop (uses an attempt). | ⬜ | |
| DOL-33 | Lay of the Land I–VI | 1–48 | Ability | Show deposits of one grade. Recast 15 s. | ⬜ | Missing |
| DOL-34 | Prospect I–VI | 3–50 | Ability | Count and direction of deposits; faster run when far. Recast 30 s. | ⬜ | Missing |
| DOL-35 | Sharp Vision | 10 | Ability | Wider hit range when aiming. | ⬜ | Missing |
| DOL-36 | Earthen Favor | 15 | Ability | Better chance of rare items. | ⬜ | Missing |
| DOL-37 | Nald'thal's Ward | 20 | Ability | More fire shards for a while. | ⬜ | Missing |
| DOL-38 | Menphina's Ward | 20 | Ability | More ice shards for a while. | ⬜ | Missing |
| DOL-39 | Deep Vigor | 20 | Ability | Raises Vitality for a while. | ⬜ | Missing |
| DOL-40 | Solid Reason | 20 | Ability | Raises Mind for a while. | ⬜ | Missing |
| DOL-41 | King's Yield | 25 | Ability | Several items from one hit. | ⬜ | Missing |
| DOL-42 | Stone Resolve | 30 | Ability | Node gauge drains slower. | ⬜ | Missing |
| DOL-43 | Truth of Mountains | 30 | Ability | More accurate aim. | ⬜ | Missing |
| DOL-44 | Beastkin Eluder | 30 | Ability | Beastkin ignore you for a while. Recast 10 min. | ⬜ | Missing |
| DOL-45 | Master of Rock | 36 | Ability | Better chance of high-quality items. | ⬜ | Missing |
| DOL-46 | Survey tiers VII–X | 58–90 | Ability | Above the level 50 cap. | ➖ | |

| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| DOL-47 | Expert Mining | Master of Rock (likely) | Better high-quality chance while mining. | ⬜ | Source and duration unknown |
| DOL-48 | Miner's Support | Guild | Gathering support buff. | ⬜ | Effect unknown |

## Botanist

Guild in Gridania. Hatchet main hand, scythe off-hand.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOL-49 | Class quests | Gridanian Roots (20), The Grass is Always Greener (30), A Moogle Bouquet (36). | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOL-50 | Log | 1 | Gathering | Log a mature tree. | ⬜ | Bug: opens mining window |
| DOL-51 | Harvest | 1 | Gathering | Harvest plants with the scythe. | ⬜ | Bug: opens mining window |
| DOL-52 | Begin / Chop / Cancel | 1 | Gathering | Cut a notch, swing the hatchet, or stop. | ⬜ | Missing |
| DOL-53 | Arbor Call I–VI | 1–48 | Ability | Show trees of one grade. Recast 15 s. | ⬜ | Missing |
| DOL-54 | Triangulate I–VI | 3–50 | Ability | Count and direction of trees; faster run when far. Recast 30 s. | ⬜ | Missing |
| DOL-55 | Field Mastery | 10 | Ability | Wider hit range when aiming. | ⬜ | Missing |
| DOL-56 | Stroke of Luck | 15 | Ability | Better chance of rare items. | ⬜ | Missing |
| DOL-57 | Nophica's Ward | 20 | Ability | More earth shards for a while. | ⬜ | Missing |
| DOL-58 | Byregot's Ward | 20 | Ability | More lightning shards for a while. | ⬜ | Missing |
| DOL-59 | Brunt Force | 20 | Ability | Raises Strength for a while. | ⬜ | Missing |
| DOL-60 | Ageless Words | 20 | Ability | Raises Intelligence for a while. | ⬜ | Missing |
| DOL-61 | Sweat of Brow | 25 | Ability | More EXP from gathering. | ⬜ | Missing |
| DOL-62 | Green Mind | 30 | Ability | Node gauge drains slower. | ⬜ | Missing |
| DOL-63 | Truth of Forests | 30 | Ability | Wider aim. | ⬜ | Missing |
| DOL-64 | Seedkin Eluder | 30 | Ability | Seedkin ignore you for a while. Recast 10 min. | ⬜ | Missing |
| DOL-65 | Master of Timber | 36 | Ability | Better chance of high-quality items. | ⬜ | Missing |
| DOL-66 | Survey tiers VII–X | 58–90 | Ability | Above the level 50 cap. | ➖ | |

| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| DOL-67 | Expert Logging / Expert Harvesting | Master of Timber (likely) | Better high-quality chance while logging or harvesting. | ⬜ | Source and duration unknown |
| DOL-68 | Botanist's Support | Guild | Gathering support buff. | ⬜ | Effect unknown |

## Fisher

Guild in Limsa Lominsa (N'nmulika). Fishing rod main hand, gig off-hand.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOL-69 | Class quests | To Fight a Fishback (20), The Beast of the Barrel (30), Polishing the Mast (36). | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOL-70 | Fish | 1 | Gathering | Cast your line. Uses bait. | ⬜ | Bug: does nothing |
| DOL-71 | Spearfish | 1 | Gathering | Spearfish with the gig. | ⬜ | Missing |
| DOL-72 | Begin / Wait / Jig / Cancel | 1 | Gathering | Pick a depth, wait 15 s for a bite, jig, or reel in. | ⬜ | Missing |
| DOL-73 | Gulleye I–VI | 1–48 | Ability | Show schools of fish of one grade. Recast 15 s. | ⬜ | Missing |
| DOL-74 | Dowse I–VI | 3–50 | Ability | Count and direction of schools; faster run when far. Recast 30 s. | ⬜ | Missing |
| DOL-75 | Veteran Trade | 10 | Ability | Wider hit range when aiming. | ⬜ | Missing |
| DOL-76 | Nature's Bounty | 15 | Ability | Better chance of rare catches. | ⬜ | Missing |
| DOL-77 | Llymlaen's Ward | 20 | Ability | More wind shards for a while. | ⬜ | Missing |
| DOL-78 | Thaliak's Ward | 20 | Ability | More water shards for a while. | ⬜ | Missing |
| DOL-79 | Deft Grasp | 20 | Ability | Raises Dexterity for a while. | ⬜ | Missing |
| DOL-80 | Tidal Faith | 20 | Ability | Raises Piety for a while. | ⬜ | Missing |
| DOL-81 | Way of Plenty | 25 | Ability | More left to catch at a spot. | ⬜ | Missing |
| DOL-82 | Clever Arc | 30 | Ability | Spot drains slower. | ⬜ | Missing |
| DOL-83 | Truth of Seas | 30 | Ability | Every catch gives a shard. | ⬜ | Missing |
| DOL-84 | Wavekin Eluder | 30 | Ability | Wavekin ignore you for a while. Recast 10 min. | ⬜ | Missing |
| DOL-85 | Master of Fish | 36 | Ability | Better chance of high-quality catches. | ⬜ | Missing |
| DOL-86 | Survey tiers VII–X | 58–90 | Ability | Above the level 50 cap. | ➖ | |

| ID | Effect | From | What it does | Status | Notes |
|---|---|---|---|---|---|
| DOL-87 | Expert Fishing / Expert Spearfishing | Master of Fish (likely) | Better high-quality chance while fishing or spearfishing. | ⬜ | Source and duration unknown |
| DOL-88 | Fisher's Support | Guild | Gathering support buff. | ⬜ | Effect unknown |

| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| DOL-89 | Fishing spots | Cast only at water; schools of fish are graded. | ⬜ | Missing |
| DOL-90 | Bait | Each cast uses bait; fresh and sea bait catch different fish. | ⬜ | How bait is picked unknown |
| DOL-91 | Depth | Cast depth changes what bites. | ⬜ | Missing |
| DOL-92 | Hook and reel | Hooked fish has an HP bar; it can get away. | ⬜ | Line tension unknown |
| DOL-93 | Catch | Fish may come in multiples, high quality, or with shards. | ⬜ | Missing |

## Unknowns
- What buff icon and duration each gathering ability shows.
- Which ability gives Gatherer's Grace.
- Whether Quarry, Harvest and Spearfish use the full aim-and-strike window.
- How bait is picked, and whether line tension or fish size existed.
- Whether grade 6 nodes existed.
- Whether gatherers had local leves.
- Whether gatherers had any traits.
- How guild support buffs are bought and what they do.
- Node respawn time, attempts per grade, gauge drain formula.
- Whether Herd, Heightened Senses and Caution did anything.
- Which points spawn monsters while you gather.

[Back to overview](../README.md)
