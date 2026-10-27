-- The Botanists' Guild door in the Man0g1 kids echo (zone 155 PrivateAreaMasterPast 2): a map door
-- stays shut unless an actor binds its bg object. Restart the map server after applying.
DELETE mo FROM server_eventnpc_mapobj mo JOIN server_eventnpc_spawn_locations s ON s.id = mo.id
    WHERE s.zoneId = 155 AND s.uniqueId = 'm0g1_echo_door_btn';
DELETE FROM server_eventnpc_spawn_locations WHERE zoneId = 155 AND uniqueId = 'm0g1_echo_door_btn';
INSERT INTO server_eventnpc_spawn_locations
    (actorClassId, uniqueId, zoneId, privateAreaName, privateAreaLevel, positionX, positionY, positionZ, rotation, motionPack)
SELECT actorClassId, 'm0g1_echo_door_btn', 155, 'PrivateAreaMasterPast', 2, positionX, positionY, positionZ, rotation, motionPack
    FROM server_eventnpc_spawn_locations WHERE zoneId = 206 AND uniqueId = 'guild_btn';
INSERT INTO server_eventnpc_mapobj (id, layoutId, instanceId)
    SELECT s.id, 321, 2941 FROM server_eventnpc_spawn_locations s WHERE s.zoneId = 155 AND s.uniqueId = 'm0g1_echo_door_btn';
