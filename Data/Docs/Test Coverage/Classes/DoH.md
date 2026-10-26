# Disciples of the Hand

Eight crafting classes: Carpenter, Blacksmith, Armorer, Goldsmith, Leatherworker, Weaver, Alchemist, Culinarian. No jobs, no traits. Equip a class's tool to become that class.

⬜ not tested · ✅ works · ⚠️ partly works · ❌ broken · ➖ not in 1.23b

## Basics

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-01 | Become a crafter | Equip a crafting tool in main hand; class changes. | ⬜ |  |
| DOH-02 | Off-hand tool | Optional second tool; synthesis can use either hand. | ⬜ |  |
| DOH-03 | No class change in combat | Can't swap tools while engaged or in an event. | ⬜ |  |
| DOH-04 | Synthesize command | Appears in Interactions with a crafting tool equipped. | ⬜ |  |
| DOH-05 | Material slots | Eight slots; shards and crystals taken when synthesis starts. | ⬜ | Bug: nothing is consumed |
| DOH-06 | Not enough shards | Synthesis can't start. | ⬜ | Missing |
| DOH-07 | Pick hand and recipe | Lists every item those materials make with that tool. | ⬜ | Missing: no recipe data |
| DOH-08 | No match | "You cannot synthesize with those materials." | ⬜ |  |
| DOH-09 | Recipe confirm | Shows result, crystals, recommended level and facility. | ⬜ | Bug: recommendations blank |
| DOH-10 | Recipes list | Last 8 recipes per class plus 8 from leves; fills slots. | ⬜ |  |
| DOH-11 | Lowest quality first | Recipes list fills slots with normal-quality materials first. | ⬜ |  |
| DOH-12 | Main vs off hand | Hands can give different quality, quantity or item. | ⬜ | Missing |
| DOH-13 | Treatises | Some recipes need a book or durability drops fast. | ⬜ | Missing |
| DOH-14 | Setting abilities | Set synthesis abilities in Actions & Traits; other crafters' too. | ⬜ | Missing; slot count unknown |
| DOH-15 | No traits | Crafters have no traits. | ⬜ |  |
| DOH-16 | Crafting stats | Craftsmanship, Magic Craftsmanship and Control affect results. | ⬜ | Formulas unknown |
| DOH-17 | Crafting EXP | EXP per synthesis; more for higher quality. Level cap 50. | ⬜ | Missing |
| DOH-18 | Recipes above 50 | Recipe lists go to level 60. | ⬜ | Rules unknown |
| DOH-19 | Facilities | Pay gil at a guild for a buff that raises success. | ⬜ | Bug: no buff given |
| DOH-20 | Repair your items | Repair unequipped gear as its class with dark matter. Back to 100%. | ⬜ | Missing |
| DOH-21 | Ask others to repair | Request repair: 100-500 gil plus dark matter grade 1-5. | ⬜ | Missing |
| DOH-22 | Repair for others | Target someone with the hammer icon; repair; get paid. | ⬜ | Missing |
| DOH-23 | NPC repair | Repair NPCs fix gear for gil, not to 100%. | ⬜ | Bug: fixed item and price |
| DOH-24 | Gear wear | Icon under 10% durability; at 0% gear does nothing. | ⬜ | Missing |
| DOH-25 | Spiritbond | Crafting raises tool spiritbond; slower on off-level recipes. | ⬜ | Missing |
| DOH-26 | Make materia | 100% spiritbond item turns into materia; item is lost. | ⬜ | Missing |
| DOH-27 | Meld materia | As the item's repair class, meld materia with a catalyst. | ⬜ | Missing |
| DOH-28 | Meld more materia | Extra melds can fail; failure destroys item, materia, catalyst. | ⬜ | Missing |
| DOH-29 | Meld for others | Request or fulfil a meld for a gil fee. | ⬜ | Missing |
| DOH-30 | Remove materia | Goblin NPC removes all materia for gil; materia destroyed. | ⬜ | Missing |
| DOH-31 | Local leve counter | Pick crafting leves by class and rank; one allowance each; hold 8. | ⬜ | Bug: shows test popups only |
| DOH-32 | Leve types | Materials handed over, or talk to the client first. | ⬜ | Missing |
| DOH-33 | Requested Items | Pick a leve or class quest; its materials fill the slots. | ⬜ | Bug: leves refused |
| DOH-34 | Leve items | Leve materials and results stay out of your inventory. | ⬜ | Missing |
| DOH-35 | Leve continue prompt | After each leve craft: shows made, needed, left; continue? | ⬜ | Missing |
| DOH-36 | Leve difficulty | Four tiers with different amounts and rewards. | ⬜ | Missing |
| DOH-37 | Leve rewards | Pay by quality: 1 item, +1 at 100, chance more at 300. | ⬜ | Missing |
| DOH-38 | Guild marks | No longer earned from leves; guild shop exchanges leftovers. | ⬜ |  |
| DOH-39 | Hasty Hand | Any crafter, Lv15: finish instantly on success; worse failure. | ⬜ | Missing |

## Mechanics

| ID | Mechanic | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-40 | Gauges | Progress, durability, quality change each step. 100% progress before durability 0. | ⬜ | Bug: random numbers |
| DOH-41 | Standard Synthesis | Best success; moderate durability loss; small quality gain. | ⬜ | Bug: all commands act the same |
| DOH-42 | Rapid Synthesis | Big progress; heavy durability loss on failure; no quality. | ⬜ | Bug: all commands act the same |
| DOH-43 | Careful Synthesis | Big quality gain; more durability loss. | ⬜ | Bug: all commands act the same |
| DOH-44 | Wait | Hold a step; only durability drops. 3 s recast. | ⬜ | Missing |
| DOH-45 | No time limit | Synthesis never times out. | ⬜ |  |
| DOH-46 | Stability glow | White best; yellow, red, flashing colours worse but better quality. | ⬜ | Missing |
| DOH-47 | Glow log message | Each glow change also prints a log line. | ⬜ | Missing |
| DOH-48 | Elemental instability | Failures can destabilise the recipe's elements; progress halved meanwhile. | ⬜ | Missing |
| DOH-49 | Failure | Durability 0 fails the synthesis; "botched" message. | ⬜ | Missing; material loss unknown |
| DOH-50 | Finish / Double Down | At 100%: finish, or gamble for HQ and risk losing it. | ⬜ | Missing |
| DOH-51 | High quality result | Quality and HQ materials raise HQ odds; grades +1 to +3. | ⬜ | Missing |
| DOH-52 | Inscribe | Inscribe the finished item. | ⬜ | Missing; when offered unknown |
| DOH-53 | Quit / Cancel | End or cancel the synthesis. | ⬜ | Missing |
| DOH-54 | Result message | "You create ..." with item, quality, quantity. | ⬜ | Bug: other players not told |
| DOH-55 | Full inventory | Result goes to the loot list. | ⬜ | Missing |
| DOH-56 | Crafting music | Crafting music plays during synthesis. | ⬜ |  |
| DOH-57 | Ability limits | Synthesis abilities cost nothing; per-synthesis limits unknown. | ⬜ | Limits unknown |
| DOH-58 | Class commands | Each class gets its own Standard/Rapid/Careful set. | ⬜ | Bug: always sends Carpenter's |
| DOH-59 | Step effects | "Next step" and "3 steps" effects end on time. | ⬜ | Missing |

## Carpenter

Woodworker. Guild in Gridania.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-60 | Tool | Equip a carpenter's tool to become Carpenter. | ⬜ |  |
| DOH-61 | Class quests | Lv20 The Mouths of Babes, Lv30 Hide and Seek Shenanigans, Lv36 Spanning the Spectrum. | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOH-62 | Standard / Rapid / Careful Synthesis | 1 | Synthesis | Basic synthesis commands for this class. | ⬜ |  |
| DOH-63 | Tender Touch | 10 | Synthesis | Durability loss -25% for 3 steps. | ⬜ | Bug: does nothing |
| DOH-64 | Improvise | 20 | Synthesis | Quality +200% for 3 steps. Unstable only. | ⬜ | Missing |
| DOH-65 | Unerring Hand | 30 | Synthesis | Advances progress. | ⬜ | Missing |
| DOH-66 | Grandmastery | 30 | Synthesis | Next step always succeeds. | ⬜ | Missing |
| DOH-67 | Cloudkin Eluder | 30 | Ability | Cloudkin ignore you for a while. 10 min recast. | ⬜ | Missing; duration unknown |
| DOH-68 | Perfection | 36 | Synthesis | Next Careful Synthesis is a great success. | ⬜ | Missing |
| DOH-69 | Byregot's Blessing | 45 | Synthesis | Quality +100% for 3 steps. | ⬜ | Missing |

## Blacksmith

Metal weapons and tools. Guild in Limsa Lominsa.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-70 | Tool | Equip a blacksmith's tool to become Blacksmith. | ⬜ |  |
| DOH-71 | Class quests | Lv20 An Ear for Quality, Lv30 Song of the Sirens, Lv36 The Sound of Silence. | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOH-72 | Standard / Rapid / Careful Synthesis | 1 | Synthesis | Basic synthesis commands for this class. | ⬜ | Bug: shows Carpenter's set |
| DOH-73 | Maker's Muse | 10 | Synthesis | Standard Synthesis always succeeds for 3 steps. | ⬜ | Missing |
| DOH-74 | Brand of Fire | 20 | Synthesis | Ends instability, raises quality. Best on fire. | ⬜ | Missing |
| DOH-75 | Ingenuity | 30 | Synthesis | Recipe counts as your level for 3 steps. | ⬜ | Bug: fake effect |
| DOH-76 | Sublimation | 30 | Synthesis | Quality +50% for 3 steps. | ⬜ | Missing |
| DOH-77 | Soulkin Eluder | 30 | Ability | Soulkin ignore you for a while. 10 min recast. | ⬜ | Missing; duration unknown |
| DOH-78 | High Return | 36 | Synthesis | Next step great success. Unstable only. | ⬜ | Missing |
| DOH-79 | Greater Calling | 45 | Synthesis | All synthesis succeeds for 3 steps. | ⬜ | Missing |

## Armorer

Metal armor. Guild in Limsa Lominsa, shared with Blacksmith.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-80 | Tool | Equip a armorer's tool to become Armorer. | ⬜ |  |
| DOH-81 | Class quests | Uses the Blacksmith quests (Lv20, 30, 36). | ⬜ | Missing; shared quests unconfirmed |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOH-82 | Standard / Rapid / Careful Synthesis | 1 | Synthesis | Basic synthesis commands for this class. | ⬜ | Bug: shows Carpenter's set |
| DOH-83 | Sure and Steady | 10 | Synthesis | Next Standard Synthesis is a great success. | ⬜ | Missing |
| DOH-84 | Brand of Ice | 20 | Synthesis | Ends instability, raises quality. Best on ice. | ⬜ | Missing |
| DOH-85 | Great Strides | 30 | Synthesis | Progress +50% for 3 steps. | ⬜ | Missing |
| DOH-86 | Resolution | 30 | Synthesis | Quality +50% for 3 steps. | ⬜ | Missing |
| DOH-87 | Spoken Eluder | 30 | Ability | Spoken ignore you for a while. 10 min recast. | ⬜ | Missing; duration unknown |
| DOH-88 | Eye for Detail | 36 | Synthesis | More quality, slightly less progress. | ⬜ | Missing |
| DOH-89 | Piece by Piece | 45 | Synthesis | Big progress. | ⬜ | Missing |

## Goldsmith

Jewellery. Guild in Ul'dah.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-90 | Tool | Equip a goldsmith's tool to become Goldsmith. | ⬜ |  |
| DOH-91 | Class quests | Lv20 She Walks in Beauty, Lv30 F'lhaminn's Flower, Lv36 Struck Through the Heart. | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOH-92 | Standard / Rapid / Careful Synthesis | 1 | Synthesis | Basic synthesis commands for this class. | ⬜ | Bug: shows Carpenter's set |
| DOH-93 | Pride of Labor | 10 | Synthesis | Quality +50% next step. | ⬜ | Missing |
| DOH-94 | Brand of Wind | 20 | Synthesis | Ends instability, raises quality. Best on wind. | ⬜ | Missing |
| DOH-95 | Meticulous Mind | 30 | Synthesis | Durability loss -50% for 3 steps. | ⬜ | Missing |
| DOH-96 | Manipulation | 30 | Synthesis | Restores durability. | ⬜ | Missing |
| DOH-97 | Voidsent Eluder | 30 | Ability | Voidsent ignore you for a while. 10 min recast. | ⬜ | Missing; duration unknown |
| DOH-98 | Inspiration | 36 | Synthesis | Quality +50% for 3 steps. | ⬜ | Missing |
| DOH-99 | By the Book | 45 | Synthesis | Big progress. | ⬜ | Missing |

## Leatherworker

Leather goods. Guild in Gridania.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-100 | Tool | Equip a leatherworker's tool to become Leatherworker. | ⬜ |  |
| DOH-101 | Class quests | Lv20 The Silent Partners, Lv30 Design Imposters, Lv36 Head of the Class. | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOH-102 | Standard / Rapid / Careful Synthesis | 1 | Synthesis | Basic synthesis commands for this class. | ⬜ | Bug: shows Carpenter's set |
| DOH-103 | Fulfillment | 10 | Synthesis | Next Standard Synthesis is a great success. | ⬜ | Missing |
| DOH-104 | Brand of Earth | 20 | Synthesis | Ends instability, raises quality. Best on earth. | ⬜ | Missing |
| DOH-105 | Bold Endeavor | 30 | Synthesis | Next step always succeeds. | ⬜ | Missing |
| DOH-106 | Inner Quiet | 30 | Synthesis | Quality +100% next step. | ⬜ | Missing |
| DOH-107 | Scalekin Eluder | 30 | Ability | Scalekin ignore you for a while. 10 min recast. | ⬜ | Missing; duration unknown |
| DOH-108 | Clean Slate | 36 | Synthesis | Resets progress, quality and durability. | ⬜ | Missing |
| DOH-109 | Determination | 45 | Synthesis | Progress +150% next step. | ⬜ | Bug: fake effect |

## Weaver

Cloth. Guild in Ul'dah.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-110 | Tool | Equip a weaver's tool to become Weaver. | ⬜ |  |
| DOH-111 | Class quests | Lv20 Hoodwinked, Lv30 Dance the Night Away, Lv36 A Fruitful Murder. | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOH-112 | Standard / Rapid / Careful Synthesis | 1 | Synthesis | Basic synthesis commands for this class. | ⬜ | Bug: shows Carpenter's set |
| DOH-113 | Masterpiece | 10 | Synthesis | Next Careful Synthesis succeeds. | ⬜ | Missing |
| DOH-114 | Brand of Lightning | 20 | Synthesis | Ends instability, raises quality. Best on lightning. | ⬜ | Missing |
| DOH-115 | Grand Design | 30 | Synthesis | Advances progress. | ⬜ | Missing |
| DOH-116 | Good Measure | 30 | Synthesis | Quality +100% next step. | ⬜ | Missing |
| DOH-117 | Ashkin Eluder | 30 | Ability | Ashkin ignore you for a while. 10 min recast. | ⬜ | Missing; duration unknown |
| DOH-118 | Flawlessness | 36 | Synthesis | No durability loss next step. | ⬜ | Missing |
| DOH-119 | Fit for Kings | 36 | Synthesis | Quality +50% for 3 steps. | ⬜ | Missing |
| DOH-120 | Culmination | 45 | Synthesis | Next step great success. | ⬜ | Missing |

## Alchemist

Potions and crystals. Guild in Ul'dah.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-121 | Tool | Equip a alchemist's tool to become Alchemist. | ⬜ |  |
| DOH-122 | Class quests | Lv20 Sleep, Cousin of Death, Lv30 The Boy and the Dragon Gay, Lv36 Dream On, Dream Away. | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOH-123 | Standard / Rapid / Careful Synthesis | 1 | Synthesis | Basic synthesis commands for this class. | ⬜ | Bug: shows Carpenter's set |
| DOH-124 | Aspect Balance | 10 | Synthesis | Keeps the white glow for 3 steps. | ⬜ | Missing |
| DOH-125 | Creative Urge | 20 | Synthesis | Next step always succeeds. | ⬜ | Missing |
| DOH-126 | Tricks of the Trade | 30 | Synthesis | Advances progress. | ⬜ | Missing |
| DOH-127 | Innovation | 30 | Synthesis | Quality +50% for 3 steps. | ⬜ | Missing |
| DOH-128 | Forgekin Eluder | 30 | Ability | Forgekin ignore you for a while. 10 min recast. | ⬜ | Missing; duration unknown |
| DOH-129 | Comfort Zone | 36 | Synthesis | No durability loss for 3 steps; no great successes. | ⬜ | Bug: fake effect |
| DOH-130 | Purification | 36 | Synthesis | Quality +125% next step. | ⬜ | Missing |
| DOH-131 | Master's Mend | 45 | Synthesis | Restores a lot of durability. | ⬜ | Missing |

## Culinarian

Food. Guild in Limsa Lominsa.

| ID | Feature | What it should do | Status | Notes |
|---|---|---|---|---|
| DOH-132 | Tool | Equip a culinarian's tool to become Culinarian. | ⬜ |  |
| DOH-133 | Class quests | Lv20 Showdown, Lv30 Mystery of the Gastronome Gone Home, Lv36 Something in the Soup. | ⬜ | Missing |

| ID | Action | Lv | Type | What it does | Status | Notes |
|---|---|---|---|---|---|---|
| DOH-134 | Standard / Rapid / Careful Synthesis | 1 | Synthesis | Basic synthesis commands for this class. | ⬜ | Bug: shows Carpenter's set |
| DOH-135 | Make the Most | 10 | Synthesis | Restores a little durability. | ⬜ | Missing |
| DOH-136 | Brand of Water | 20 | Synthesis | Ends instability, raises quality. Best on water. | ⬜ | Missing |
| DOH-137 | Life's Purpose | 30 | Synthesis | Next step always succeeds. | ⬜ | Missing |
| DOH-138 | Waste Not | 30 | Synthesis | No durability loss next step. | ⬜ | Bug: fake effect |
| DOH-139 | Vilekin Eluder | 30 | Ability | Vilekin ignore you for a while. 10 min recast. | ⬜ | Missing; duration unknown |
| DOH-140 | Elemental Appeal | 36 | Synthesis | No instability, keeps white glow, 3 steps. | ⬜ | Missing |
| DOH-141 | Heart and Soul | 36 | Synthesis | Quality +125% next step. | ⬜ | Missing |
| DOH-142 | Magnum Opus | 45 | Synthesis | Quality +150% next step. Rainbow glow only. | ⬜ | Missing |

## Unknowns

- Limits on synthesis abilities per synthesis (client shows no cost or recast).
- How many synthesis abilities can be set at once.
- Whether step effects show as status icons or only in the synthesis window.
- Whether "rainbow" (prismatic) glow is the flashing-colours state.
- Formulas for success, progress, durability and quality.
- Which materials are lost on failure, Quit or Cancel.
- Eluder duration, what breaks it, and the patch that added it.
- What the Synthesis Support and Crafter's Grace buffs do and who gives them.
- When Inscribe is offered and what it writes.
- Whether Armorer really shared the Blacksmith class quests.
- Whether HQ grades +1 to +3 still existed, and their thresholds.
- Facility prices, duration and success bonus.
- Whether any recipe book existed beyond the Recipes list.
- Whether treatises were still needed after the guild mark changes.
- Rules for recipes above level 50.
- Which Standard/Rapid/Careful set is main hand and which off hand.

[Back to overview](../README.md)
