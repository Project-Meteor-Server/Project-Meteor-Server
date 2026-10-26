require ("global")
require ("modifiers")
require ("quests/man/man0u1")

TEXT_BOUND_BY_DUTY = 50011;

POOL_TOURNEY_GLADIATOR = 8280157;
GLADIATOR_LEVEL        = 5;
-- No round timer is known; five minutes keeps a stalled match from running forever.
ROUND_SECONDS          = 300;
GLADIATOR_POST         = { -170.794, 174.984, 160.326, -1.58 };

function init()
	return "/Director/Quest/QuestDirectorMan0u101";
end

-- The first-round match can't be won: both sides keep 1 HP, and the player pinned at 1 HP, the round
-- running out or the gladiator gone all end it as a loss.
function onEventStarted(player, director, triggerName)
	player:EndEvent();
	local quest = player:GetQuest("Man0u1");
	if (quest == nil) then
		return;
	end

	local area = waitForContentArea(player, "SimpleContentMan0u101");
	if (area == nil) then
		return;
	end

	local duty = director:CreateDuty(area);
	duty:AddPlayer(player);
	duty:SetWinOnClear(false);
	local p = GLADIATOR_POST;
	local gladiator = duty:SpawnEnemy(POOL_TOURNEY_GLADIATOR, GLADIATOR_LEVEL, p[1], p[2], p[3], p[4]);
	if (gladiator ~= nil) then
		gladiator:SetMod(modifiersGlobal.MinimumHpLock, 1);
	end
	player:SetMod(modifiersGlobal.MinimumHpLock, 1);
	player:SendGameMessage(GetWorldMaster(), TEXT_BOUND_BY_DUTY, 0x20);
	duty:Start();
	if (gladiator ~= nil) then
		gladiator.hateContainer:AddBaseHate(player);
		gladiator:Engage(player);
	end

	local elapsed = 0;
	while (duty:IsRunning() and player.CurrentArea == area) do
		wait(1);
		elapsed = elapsed + 1;
		if (player:GetHP() <= 1 or elapsed >= ROUND_SECONDS or gladiator == nil or gladiator:IsDead()) then
			break;
		end
	end
	if (gladiator ~= nil and not gladiator:IsDead()) then
		gladiator:Disengage(0);
		gladiator:ChangeState(0);
	end

	if (player.CurrentArea ~= area) then
		player:SetMod(modifiersGlobal.MinimumHpLock, 0);
		duty:End();
		return;
	end

	-- The client's own KO handling sends this line, and a pinned loss never reaches it.
	player:SendMessage(MESSAGE_TYPE_SYSTEM, "", "You are defeated.");
	player:SetHP(player:GetMaxHP());
	player:Disengage(0);
	player:ChangeState(0);
	wait(2);
	kickEventContinue(player, director, "noticeEvent", "noticeEvent");
	quest = player:GetQuest("Man0u1");
	if (quest == nil or quest:GetSequence() ~= SEQ_015) then
		player:SetMod(modifiersGlobal.MinimumHpLock, 0);
		player:EndEvent();
		duty:End();
		return;
	end
	finishColiseumMatch(player, quest, duty, area);
end

function main()
end

function waitForContentArea(player, contentName)
	for i = 1, 20 do
		local area = player.CurrentArea;
		if (area ~= nil and area:GetPrivateAreaName() == contentName) then
			return area;
		end
		wait(0.5);
	end
	return nil;
end
