require ("global")

--[[

Quest Script

Name: 	Small Talk
Code: 	DftWil
Id: 	110543

Contains all default lines for talkable npcs in the Wilderness Region (aka Thanalan).
* NOTE: This quest is active for all players at all times.
]]

-- [ActorClassId] = "client_function_name"
local defaultTalkWil = {
	[1000841] = "defaultTalkWithMomodi_001", -- Momodi
	[1000994] = "defaultTalkWithLefchild_001" -- Lefchild
}

function onTalk(player, quest, npc, eventName)
	local clientFunc = defaultTalkWil[npc:GetActorClassId()];
	callClientFunction(player, "delegateEvent", player, quest, clientFunc);
	player:EndEvent();
end

function IsQuestENPC(player, quest, npc)
	return defaultTalkWil[npc:GetActorClassId()] ~= nil;
end