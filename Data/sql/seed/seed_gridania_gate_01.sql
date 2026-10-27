-- The Blue Badger Gate's door in public Gridania (zone 155): a map door keeps its closed collision
-- unless an actor binds its bg object, and class 5900003 spawns it open. Restart the map server after applying.

DELETE mo FROM server_eventnpc_mapobj mo
    JOIN server_eventnpc_spawn_locations s ON s.id = mo.id
    WHERE s.zoneId = 155 AND s.uniqueId = 'gate_blue_badger';
DELETE FROM server_eventnpc_spawn_locations WHERE zoneId = 155 AND uniqueId = 'gate_blue_badger';

INSERT INTO server_eventnpc_spawn_locations
    (actorClassId, uniqueId, zoneId, privateAreaName, privateAreaLevel, positionX, positionY, positionZ, rotation, motionPack)
VALUES (5900003, 'gate_blue_badger', 155, '', 0, 185, -1, -1154, 0, 0);

INSERT INTO server_eventnpc_mapobj (id, layoutId, instanceId)
    SELECT s.id, 321, 2937 FROM server_eventnpc_spawn_locations s
    WHERE s.zoneId = 155 AND s.uniqueId = 'gate_blue_badger';
