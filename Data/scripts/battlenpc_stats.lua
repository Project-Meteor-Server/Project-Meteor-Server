require("global");
require("battle/stats");

--[[
Called by BattleNpc.CalculateBaseStats() (C#) after the DB/pool/genus mods are set and the
level is rolled, before Modifier.Hp/Mp become max HP/MP.

server_battlenpc_groups.hp: 0 = derive from the rolled level and the genus (battle/stats.lua
MOB_HP_BASE x MOB_HP_FAMILY, fit to retail 1.23b captures); > 0 = authored override (NMs, scripted
fights). groups.mp: 0 = derive from the level (MOB_MP_BASE, x5 for casters); > 0 = authored.
Attributes left at the genus default (1) get the level-typical value so formulas.lua sees a
"typical" monster.
--]]
function onCalculateBaseStats(mob)
	stats.applyMobBaseStats(mob);
end
