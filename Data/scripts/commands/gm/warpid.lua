require("global");

properties = {
    permissions = 0,
    parameters = "s",
    description = "Teleports to Actor uniqueId's position",
}

function onTrigger(player, argc, uID)
	local messageID = MESSAGE_TYPE_SYSTEM_ERROR;
    local sender = "[warpid] ";
    local message = "unable to find actor";
    local worldManager = GetWorldManager();
 
    if not player then
        printf("[Command] [warpid] Player not found!");
        return;
    end; 
    
    actor = GetWorldManager():GetActorInWorldByUniqueId(uID);        
    actorId = actor:GetActorClassId();
    if (actor ~= nil) then
        local actorPos = actor:GetPos();
        local playerPos = player:GetPos();

        if actorPos[5] == playerPos[5] then
            worldManager:DoPlayerMoveInZone(player, actorPos[1], actorPos[2], actorPos[3], actorPos[4], 0x00);
        else
            worldManager:DoZoneChange(player, actorPos[5], nil, 0, 0x02, actorPos[1], actorPos[2], actorPos[3], actorPos[4]);  
        end
        
        message =  string.format("Moving to %s 's coordinates @ zone %s, %.3f %.3f %.3f ", uID, actorPos[5], actorPos[1], actorPos[2], actorPos[3]);
	end	;

    player:SendMessage(messageID, sender, message);
    player:SendMessage(0x20, "", "Actor Class Id: "..actorId);

end

    