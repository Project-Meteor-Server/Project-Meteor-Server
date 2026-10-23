--[[

BattleNpc Stats Script

Functions:

onCalculateBaseStats(mob)
	Desc: Called by BattleNpc.CalculateBaseStats. HP and MP left at 0 in server_battlenpc_groups
		  come from the level and genus.

--]]

require("global");
require("battle/stats");

function onCalculateBaseStats(mob)
	stats.applyMobBaseStats(mob);
end
