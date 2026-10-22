--[[

BonusPointCommand Script

Client: operateUI(TOTAL, perStatMax, str, vit, dex, int, mnd, pie) (delegateCommand puts the player
first). The widget computes remaining = TOTAL - (str+...+pie) itself and shows per-attribute room as
perStatMax - stat. It returns (ok, str, vit, dex, int, mnd, pie): ok true on Complete, false on Cancel;
each stat is the new ABSOLUTE value (the widget only adds). (client proto2 +
BonusPointAssignWidget, static.)

Attribute points [OFF 1.20 dev1228]: 5 at Lv10, +1 per level (45 at Lv50), allotted 1:1 per class
(jobs use their base class's), per-attribute cap 3 + floor((L-10)/2) (23 at Lv50). Reset with the
Keeper's Hymn from 1.21a.

Stored per class in characters_bonus_points (Player.SetBonusAllotment, which re-checks the sum and the
cap, and refuses lowering an attribute). Points not yet allotted are unspent, as in retail: the
console tray icon shows and the screen opens with them all to spend.

--]]

require ("global")
require ("battle/stats")

function onEventStarted(player, actor, eventType, eventName)
	local classId = stats.classOf(player);
	local level = player:GetLevel();
	local total = stats.bonusPoints(level);
	local cap = stats.bonusPointCap(level);

	-- GetBonusPoint is 0 for a class with nothing allotted yet.
	local cur = { player:GetBonusPoint(classId, 0), player:GetBonusPoint(classId, 1), player:GetBonusPoint(classId, 2),
	              player:GetBonusPoint(classId, 3), player:GetBonusPoint(classId, 4), player:GetBonusPoint(classId, 5) };

	-- a1 is the TOTAL (the client subtracts what is already spent itself)
	local ok, str, vit, dex, int, mnd, pie = callClientFunction(player, "delegateCommand", actor, "operateUI", total, cap,
		cur[1], cur[2], cur[3], cur[4], cur[5], cur[6]);

	if (ok == true) then
		local saved = player:SetBonusAllotment(classId, level, tonumber(str) or cur[1], tonumber(vit) or cur[2],
			tonumber(dex) or cur[3], tonumber(int) or cur[4], tonumber(mnd) or cur[5], tonumber(pie) or cur[6]);
		if (not saved) then
			printf("[BonusPointCommand] refused allotment class=%s level=%s: %s %s %s %s %s %s", tostring(classId), tostring(level),
				tostring(str), tostring(vit), tostring(dex), tostring(int), tostring(mnd), tostring(pie));
		end
	end

	player:EndEvent();
end
