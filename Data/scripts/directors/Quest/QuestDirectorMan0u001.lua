require ("global")
require ("tutorial")
require ("quests/man/man0u0")

--processTtrBtl001: Active Mode Tutorial
--processTtrBtl002: Targetting Tutorial (After active mode done)
--processTtrBtl003: Auto Attack Done
--processTtrBtl004: Tutorial Complete

function init()
	return "/Director/Quest/QuestDirectorMan0u001";
end


function onCreateContentArea(players, director, contentArea, contentGroup)
	
	niellefresne = contentArea:SpawnActor(2290003, "niellefresne", -11.86, 192, 35.06, -0.8);
	thancred = contentArea:SpawnActor(2290004, "thancred", -26.41, 192, 39.52, 1.2);
	thancred:ChangeState(2);
	
	mob1 = contentArea:SpawnActor(2203301, "mob1", -6.193, 192, 47.658, -2.224);

	openingStoper = contentArea:SpawnActor(1090385, "openingstoper", -24.34, 192, 34.22, 0);
	
	for _, player in pairs(players) do
        contentGroup:AddMember(player);
    end;
    
	contentGroup:AddMember(director);
	contentGroup:AddMember(niellefresne);
	contentGroup:AddMember(thancred);
	contentGroup:AddMember(mob1);
end

function onEventStarted(player, director, triggerName)	

	man0u0Quest = player:GetQuest("Man0u0");
	startTutorialMode(player);
	callClientFunction(player, "delegateEvent", player, man0u0Quest, "processTtrBtl001", nil, nil, nil);
	player:EndEvent();
	waitForSignal("playerActive");
	wait(1); --If this isn't here, the scripts bugs out. TODO: Find a better alternative.
	kickEventContinue(player, director, "noticeEvent", "noticeEvent");	
    
	callClientFunction(player, "delegateEvent", player, man0u0Quest, "processTtrBtl002", nil, nil, nil);
	player:EndEvent();
	wait(4);
	closeTutorialWidget(player);
	showTutorialSuccessWidget(player, 9055); --Open TutorialSuccessWidget for attacking enemy
	wait(3);
	openTutorialWidget(player, CONTROLLER_KEYBOARD, TUTORIAL_TP);
	wait(5);
	closeTutorialWidget(player);
	openTutorialWidget(player, CONTROLLER_KEYBOARD, TUTORIAL_WEAPONSKILLS);
	wait(4); --Should be wait for weaponskillUsed signal
	closeTutorialWidget(player);
	showTutorialSuccessWidget(player, 9065); --Open TutorialSuccessWidget for weapon skill
	
	wait(6); --Should be wait for mobkill
	worldMaster = GetWorldMaster();
	player:SendDataPacket("attention", worldMaster, "", 51073, 3);
	wait(7);
	player:ChangeMusic(7);
	player:ChangeState(0); 
	kickEventContinue(player, director, "noticeEvent", "noticeEvent");
	callClientFunction(player, "delegateEvent", player, man0u0Quest, "processEvent020", nil, nil, nil);	
	
	--[[
	IF DoW:
		OpenWidget (TP)
		IF TP REACHED:
			CloseWidget
			OpenWidget (WS)
		IF WS USED:
			Success
			CloseWidget
	ELSE MAGIC:
		OpenWidget (DEFEAT ENEMY)			
	]]
	
	man0u0Quest:StartSequence(10);		
	player.CurrentArea:ContentFinished();
	GetWorldManager():DoZoneChange(player, 175, "PrivateAreaMasterPast", 3, 15, -22.81, 196, 87.82, 2.98);
    player:EndEvent();
end
