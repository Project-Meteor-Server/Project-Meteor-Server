require ("global")
require ("tutorial")
require ("quests/man/man0g0")

--processTtrBtl001: Active Mode Tutorial - DoW only
--processTtrBtl002: Targetting Tutorial (After active mode done)

function init()
	return "/Director/Quest/QuestDirectorMan0g001";
end

function onCreateContentArea(players, director, contentArea, contentGroup)

    papalymo = contentArea:SpawnActor(2290005, "papalymo", 365.89, 4.0943, -706.72, -0.718);
    yda = contentArea:SpawnActor(2290006, "yda", 365.266, 4.122, -700.73, 1.5659);

    mob1 = contentArea:SpawnActor(2201407, "mob1", 374.427, 4.4, -698.711, -1.942);
    mob2 = contentArea:SpawnActor(2201407, "mob2", 375.377, 4.4, -700.247, -1.992);
    mob3 = contentArea:SpawnActor(2201407, "mob3", 375.125, 4.4, -703.591, -1.54);
    
    openingStoper = contentArea:SpawnActor(1090384, "openingstoper", 356.09, 3.74, -701.62, -1.41);
	
	for _, player in pairs(players) do
        contentGroup:AddMember(player);
    end;

    

	contentGroup:AddMember(director);
    contentGroup:AddMember(yda);
    contentGroup:AddMember(papalymo);
	contentGroup:AddMember(mob1);
	contentGroup:AddMember(mob2);
	contentGroup:AddMember(mob3);

end

function onEventStarted(player, actor, triggerName)

	man0g0Quest = player:GetQuest("Man0g0");
	startTutorialMode(player);


	--Combat portion of tutorial
	
	if player:IsDiscipleOfWar() then
    
        callClientFunction(player, "delegateEvent", player, man0g0Quest, "processTtrBtl001");
        player:EndEvent();
        waitForSignal("playerActive");
        wait(1); --If this isn't here, the scripts bugs out. TODO: Find a better alternative.
        kickEventContinue(player, actor, "noticeEvent", "noticeEvent");	
        
        callClientFunction(player, "delegateEvent", player, man0g0Quest, "processTtrBtl002", nil, nil, nil);
        player:EndEvent();        

		--waitForSignal("playerAttack");
		closeTutorialWidget(player);
		showTutorialSuccessWidget(player, 9055); --Open TutorialSuccessWidget for attacking enemy
		openTutorialWidget(player, CONTROLLER_KEYBOARD, TUTORIAL_TP);

        wait(3);
		closeTutorialWidget(player);
		openTutorialWidget(player, CONTROLLER_KEYBOARD, TUTORIAL_WEAPONSKILLS);
		--waitForSignal("weaponskillUsed");
        wait(3);
		--player:SetMod(modifiersGlobal.MinimumTpLock, 0);
		closeTutorialWidget(player);
		showTutorialSuccessWidget(player, 9065); --Open TutorialSuccessWidget for weapon skill
        
	elseif player:IsDiscipleOfMagic() then
    
        callClientFunction(player, "delegateEvent", player, man0g0Quest, "processTtrBtlMagic001");  
        player:EndEvent();
        wait(1);
        kickEventContinue(player, actor, "noticeEvent", "noticeEvent");
        closeTutorialWidget(player);
        player:SendMessage(0x20, "", "Sending success");
        showTutorialSuccessWidget(player, 9050); --Open TutorialSuccessWidget for magic skill
        wait(1)
		openTutorialWidget(player, CONTROLLER_KEYBOARD, TUTORIAL_DEFEATENEMY);
        wait(3);
        closeTutorialWidget(player);
 

		--waitForSignal("spellUsed");  9050

	elseif player:IsDiscipleOfHand() then
		--waitForSignal("abilityUsed");
	elseif player:IsDiscipleOfLand() then
		--waitForSignal("abilityUsed");
	end
	
    wait(3);
	worldMaster = GetWorldMaster();
	player:SendDataPacket("attention", worldMaster, "", 51073, 2);
	wait(7);
	player:ChangeMusic(7);
	player:ChangeState(0); 

	--kickEventContinue(player, actor, "noticeEvent", "noticeEvent");    
	callClientFunction(player, "delegateEvent", player, man0g0Quest, "processEvent020_1");

    man0g0Quest:StartSequence(10);
	
	player:EndEvent();	
    player.CurrentArea:ContentFinished();
	GetWorldManager():DoZoneChange(player, 155, "PrivateAreaMasterPast", 1, 15, 175.38, -1.21, -1156.51, -2.1);
	player:EndEvent();	
end

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
	--]]