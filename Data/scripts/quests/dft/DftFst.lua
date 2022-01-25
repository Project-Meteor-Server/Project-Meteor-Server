require ("global")

--[[

Quest Script

Name: 	Small Talk
Code: 	DftFst
Id: 	110542

Contains all default lines for talkable npcs in the Forest Region (aka Black Shroud).
* NOTE: This quest is active for all players at all times.
]]

-- [ActorClassId] = "client_function_name"
local defaultTalkFst = {	
	[1000821] = "defaultTalkWithNellaure_001",				-- Nellaure
	[1001081] = "defaultTalkWithKhumamoshroca_001",			-- Khuma Moshroca
	[1001103] = "defaultTalkWithMestonnaux_001", 			-- Mestonnaux
	[1001396] = "defaultTalkWithLefwyne_001",				-- Lefwyne
	[1001430] = "defaultTalkWithKinnison_001",				-- Kinnison
	[1001437] = "defaultTalkWithSybell_001", 				-- Sybell	
}

function onTalk(player, quest, npc, eventName)
	local clientFunc = defaultTalkFst[npc:GetActorClassId()];
	callClientFunction(player, "delegateEvent", player, quest, clientFunc);
	player:EndEvent();
end

function IsQuestENPC(player, quest, npc)
	return defaultTalkFst[npc:GetActorClassId()] ~= nil;
end