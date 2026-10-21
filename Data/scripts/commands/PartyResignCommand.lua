--[[

PartyResignCommand Script

Handles leaving a party

--]]

function onEventStarted(player, actor, eventType, eventName)
	player:PartyLeave(name);	
	player:EndEvent();
end