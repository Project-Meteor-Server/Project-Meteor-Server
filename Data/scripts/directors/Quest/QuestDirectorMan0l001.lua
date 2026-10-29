require ("global")
require ("modifiers")
require ("tutorial")
require ("quests/man/man0l0")

WEAPONSKILL_TP = 1000;

function init()
	return "/Director/Quest/QuestDirectorMan0l001";
end

local function isBattleOver(director)
	local duty = director:GetDuty();
	return duty == nil or duty:IsOver();
end

function onEventStarted(player, director, triggerName)
	local quest = player:GetQuest("Man0l0");
	if (quest == nil) then
		player:EndEvent();
		return;
	end

	startTutorialMode(player);
	local isMagic = player:IsDiscipleOfMagic();

	if (not isMagic) then
		callClientFunction(player, "delegateEvent", player, quest, "processTtrBtl001");
		player:EndEvent();
		waitForSignal("playerActive");
		wait(1);
		kickEventContinue(player, director, "noticeEvent", "noticeEvent");
		callClientFunction(player, "delegateEvent", player, quest, "processTtrBtl002", nil, nil, nil);
		player:EndEvent();
		closeTutorialWidget(player);
		showTutorialSuccessWidget(player, 9055);
		openTutorialWidget(player, CONTROLLER_KEYBOARD, TUTORIAL_TP);
		while (not isBattleOver(director) and player:GetTP() < WEAPONSKILL_TP) do
			wait(0.5);
		end
		closeTutorialWidget(player);
		if (not isBattleOver(director)) then
			openTutorialWidget(player, CONTROLLER_KEYBOARD, TUTORIAL_WEAPONSKILLS);
			while (not isBattleOver(director) and player:GetTP() >= WEAPONSKILL_TP) do
				wait(0.5);
			end
			closeTutorialWidget(player);
			showTutorialSuccessWidget(player, 9065);
		end
	else
		callClientFunction(player, "delegateEvent", player, quest, "processTtrBtlMagic001");
		player:EndEvent();
		wait(1);
		closeTutorialWidget(player);
		openTutorialWidget(player, CONTROLLER_KEYBOARD, TUTORIAL_DEFEATENEMY);
	end

	while (not isBattleOver(director)) do
		wait(1);
	end
	wait(3);
	closeTutorialWidget(player);
	if (isMagic) then
		showTutorialSuccessWidget(player, 9050);
	end

	player:SetMod(modifiersGlobal.MinimumHpLock, 0);
	if (player:GetHP() < player:GetMaxHP()) then
		player:SetHP(player:GetMaxHP());
	end
	attentionMessage(player, 51073, 1);
	wait(7);
	player:ChangeMusic(7);
	-- ChangeState alone leaves the player in active mode.
	player:Disengage(0);
	player:ChangeState(0);

	kickEventContinue(player, director, "noticeEvent", "noticeEvent");
	quest = player:GetQuest("Man0l0");
	if (quest == nil or quest:GetSequence() ~= SEQ_005) then
		endTutorialMode(player);
		player:EndEvent();
		return;
	end
	finishDeckBattle(player, quest, director:GetDuty(), player.CurrentArea);
end

function main()
end
