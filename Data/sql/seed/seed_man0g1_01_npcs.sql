-- Man0g1 "Souls Gone Wild": the private areas, actor classes and spawns man0g1.lua uses.
-- Restart the map server after applying. A class spawned with an empty classPath crashes the client.

-- ---- (1) private areas ---------------------------------------------------------------------------

INSERT INTO server_zones_privateareas (parentZoneId, className, privateAreaName, privateAreaType, canExitArea, music)
SELECT v.parentZoneId, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', v.privateAreaType, 0, v.music
FROM (SELECT 155 AS parentZoneId, 0 AS privateAreaType, 51 AS music
      UNION ALL SELECT 155, 3, 51
      UNION ALL SELECT 155, 4, 51
      UNION ALL SELECT 150, 0, 52
      UNION ALL SELECT 150, 1, 52) v
WHERE NOT EXISTS (SELECT 1 FROM server_zones_privateareas p
                  WHERE p.parentZoneId = v.parentZoneId AND p.privateAreaName = 'PrivateAreaMasterPast'
                    AND p.privateAreaType = v.privateAreaType);

-- ---- (2) populace class fills --------------------------------------------------------------------

UPDATE gamedata_actor_class SET
    classPath = '/Chara/Npc/Populace/PopulaceStandard',
    propertyFlags = 19,
    eventConditions = '{"talkEventConditions":[{"unknown1":4,"unknown2":0,"conditionName":"talkDefault"}],"noticeEventConditions":[{"unknown1":0,"unknown2":1,"conditionName":"noticeEvent"}]}'
WHERE id IN (
    1000033,  -- O-App-Pesi (2000010), Stillglade Echo
    1000372,  -- Ingram (1000141), Stillglade Echo
    1000956,  -- Challinie (1300032), Stillglade Echo
    1000237,  -- Fufucha (1500021), Growery scenes
    1000015,  -- T'kebbe (1900026), Lancers' Guild scenes
    1000741,  -- Jijimaya (1400016), Lancers' Guild scenes
    1000733,  -- Turstin (1000244), Lancers' Guild scene
    1000734,  -- Langloisiert (1200134), Lancers' Guild scene
    1000735,  -- Helbhanth (1600094), Lancers' Guild scene
    1000738   -- Pasdevillet (1200140), Lancers' Guild scene
) AND classPath = '';

-- ---- (3) emote conditions: SEQ_050 dance practice ------------------------------------------------
-- Emote id = 100 + the DoEmote id man0g1 expects from that kid; without it the emote never reaches the quest.

UPDATE gamedata_actor_class SET eventConditions = CONCAT(
    '{"talkEventConditions":[{"unknown1":4,"unknown2":0,"conditionName":"talkDefault"}],',
    '"noticeEventConditions":[{"unknown1":0,"unknown2":1,"conditionName":"noticeEvent"}],',
    '"emoteEventConditions":[{"unknown1":4,"unknown2":0,"emoteId":',
    CASE id
        WHEN 1000410 THEN '108'   -- Aunillie: Beckon
        WHEN 1000409 THEN '107'   -- Nicollaux: Clap
        WHEN 1000239 THEN '105'   -- Sansa: Bow
        WHEN 1000238 THEN '106'   -- Powle: Cheer
        WHEN 1000412 THEN '101'   -- Ryd: Surprised
        WHEN 1000411 THEN '122'   -- Elyn: Lookout
    END,
    ',"conditionName":"emoteDefault1"}]}')
WHERE id IN (1000410, 1000409, 1000239, 1000238, 1000412, 1000411)
  AND classPath <> '' AND (eventConditions IS NULL OR eventConditions NOT LIKE '%emoteEventConditions%');

-- ---- (4) push triggers ---------------------------------------------------------------------------

UPDATE gamedata_actor_class SET
    classPath = '/Chara/Npc/Populace/PopulaceStandard',
    propertyFlags = 1,
    eventConditions = CONCAT('{"noticeEventConditions":[{"unknown1":0,"unknown2":1,"conditionName":"noticeEvent"}],',
        '"pushWithCircleEventConditions":[{"isEnabled":"false","radius":"',
        CASE id WHEN 1090200 THEN '12.0' WHEN 1090201 THEN '8.0' WHEN 1090202 THEN '25.0'
                WHEN 1090203 THEN '12.0' WHEN 1090204 THEN '15.0' END,
        '","outwards":"', IF(id = 1090204, 'true', 'false'),
        '","silent":"false","conditionName":"pushDefault"}]}')
WHERE id IN (
    1090200,  -- CNJ_TRIG, SEQ_015 Stillglade Fane
    1090201,  -- KIDS_TRIGGER, SEQ_055 leaving the Growery
    1090202,  -- GATE_TRIGGER, SEQ_060 White Wolf Gate (duty ask)
    1090203,  -- STUMP_TRIGGER, SEQ_070
    1090204   -- STUMP_EXIT_TRIGGER, SEQ_071 (fires on walking out of the circle)
) AND classPath = '';

-- BTN_TRIGGER (1090046) only had talk and notice; man0g1 uses it as a push.
UPDATE gamedata_actor_class SET eventConditions =
'{"talkEventConditions":[{"unknown1":4,"unknown2":0,"conditionName":"talkDefault"}],"noticeEventConditions":[{"unknown1":0,"unknown2":1,"conditionName":"noticeEvent"}],"pushWithCircleEventConditions":[{"isEnabled":"false","radius":"12.0","outwards":"false","silent":"false","conditionName":"pushDefault"}]}'
WHERE id = 1090046 AND (eventConditions IS NULL OR eventConditions NOT LIKE '%pushWithCircle%');

-- ---- (5) spawns ----------------------------------------------------------------------------------

DELETE FROM server_eventnpc_spawn_locations WHERE uniqueId LIKE 'm0g1\_%';

INSERT INTO server_eventnpc_spawn_locations
    (actorClassId, uniqueId, zoneId, privateAreaName, privateAreaLevel, positionX, positionY, positionZ, rotation, motionPack)
VALUES
    (1090200, 'm0g1_cnj_trig',          206, '', 0, -342.60, 8.0,  -1686.51, 0,     0),
    -- SEQ_015: Stillglade Echo, 155 PA 0 (warp-in -353.05, 6.25, -1697.39)
    (1000680, 'm0g1_echo_swethyna',     155, 'PrivateAreaMasterPast', 0, -352.029, 6.265, -1693.978, 0.762, 1015),
    (1000009, 'm0g1_echo_yda',          155, 'PrivateAreaMasterPast', 0, -350.461, 6.265, -1693.581, -2.334, 0),
    (1000010, 'm0g1_echo_papalymo',     155, 'PrivateAreaMasterPast', 0, -351.118, 6.265, -1693.024, -2.529, 0),
    (1000033, 'm0g1_echo_oapppesi',     155, 'PrivateAreaMasterPast', 0, -351.517, 6.265, -1694.592, 0.807, 1015),
    (1000460, 'm0g1_echo_hetzkin',      155, 'PrivateAreaMasterPast', 0, -329.120, 8.0,   -1683.098, -0.016, 1015),
    (1000737, 'm0g1_echo_biddy',        155, 'PrivateAreaMasterPast', 0, -342.728, 8.0,   -1674.651, -2.175, 1015),
    (1000513, 'm0g1_echo_gugula',       155, 'PrivateAreaMasterPast', 0, -357.663, 6.821, -1686.486, 2.611, 1015),
    (1000372, 'm0g1_echo_ingram',       155, 'PrivateAreaMasterPast', 0, -338.209, 8.2,   -1668.174, -3.116, 1015),
    (1000956, 'm0g1_echo_challinie',    155, 'PrivateAreaMasterPast', 0, -325.708, 8.0,   -1676.921, -1.615, 1015),
    -- SEQ_050: the dance drill, 155 PA 1
    (1000237, 'm0g1_pa1_fufucha',       155, 'PrivateAreaMasterPast', 1, -233.019, 12.0,   -1499.633, 1.478,  1015),
    (1000410, 'm0g1_pa1_aunille',       155, 'PrivateAreaMasterPast', 1, -218.000, 12.001, -1497.562, -1.489, 0),
    (1000238, 'm0g1_pa1_powle',         155, 'PrivateAreaMasterPast', 1, -219.901, 12.001, -1495.523, -1.838, 0),
    (1000239, 'm0g1_pa1_sansa',         155, 'PrivateAreaMasterPast', 1, -219.450, 12.001, -1497.016, -1.371, 0),
    (1000409, 'm0g1_pa1_nicollaux',     155, 'PrivateAreaMasterPast', 1, -220.748, 12.001, -1494.328, -1.814, 0),
    (1000412, 'm0g1_pa1_ryd',           155, 'PrivateAreaMasterPast', 1, -220.277, 12.001, -1499.770, -1.589, 0),
    (1000411, 'm0g1_pa1_elyn',          155, 'PrivateAreaMasterPast', 1, -214.778, 12.001, -1491.678, -2.092, 0),
    -- SEQ_055: the huddle, 155 PA 2; the push is the Growery exit at marker 610 (the BTN trigger's spot)
    (1000237, 'm0g1_pa2_fufucha',       155, 'PrivateAreaMasterPast', 2, -227.999, 12.0,   -1498.164, 1.266,  1015),
    (1000410, 'm0g1_pa2_aunille',       155, 'PrivateAreaMasterPast', 2, -215.066, 12.080, -1494.933, -1.797, 0),
    (1000238, 'm0g1_pa2_powle',         155, 'PrivateAreaMasterPast', 2, -217.700, 12.005, -1494.779, 2.033,  0),
    (1000239, 'm0g1_pa2_sansa',         155, 'PrivateAreaMasterPast', 2, -217.044, 12.0,   -1496.906, 0.610,  0),
    (1000409, 'm0g1_pa2_nicollaux',     155, 'PrivateAreaMasterPast', 2, -215.278, 12.102, -1496.170, -1.014, 0),
    (1000412, 'm0g1_pa2_ryd',           155, 'PrivateAreaMasterPast', 2, -216.017, 12.073, -1496.706, -0.442, 0),
    (1000411, 'm0g1_pa2_elyn',          155, 'PrivateAreaMasterPast', 2, -216.278, 12.005, -1493.923, -3.036, 0),
    (1090201, 'm0g1_kids_trig',         155, 'PrivateAreaMasterPast', 2, -202.91, 18.09, -1477.51, 0,   0),
    (1090202, 'm0g1_gate_trig',         155, '', 0, -194.73, 3.54, -1021.33, -1.642, 0),
    -- SEQ_070: Lifemend Stump walk-up, 150 PA 0 at marker 613.
    (1090203, 'm0g1_stump_trig',        150, 'PrivateAreaMasterPast', 0, -794.81, 21,   -1060.32, 0,  0),
    -- SEQ_071: leave the stump, 150 PA 1; the circle's centre is the warp-in.
    (1090204, 'm0g1_stump_exit_trig',   150, 'PrivateAreaMasterPast', 1, -756.77, 22.77, -1092.33, 0, 0),
    -- SEQ_090: Lancers' Guild, 155 PA 3 (warp-in 176.13, 27.5, -1581.84): the crew around Burchard.
    (1000243, 'm0g1_pa3_burchard',      155, 'PrivateAreaMasterPast', 3, 176.737, 27.5, -1582.575, -1.569, 1041),
    (1000015, 'm0g1_pa3_tkebbe',        155, 'PrivateAreaMasterPast', 3, 174.936, 27.5, -1583.036, 1.563,  1014),
    (1000017, 'm0g1_pa3_farrimond',     155, 'PrivateAreaMasterPast', 3, 174.468, 27.5, -1580.628, 0.601,  1001),
    (1000681, 'm0g1_pa3_nuala',         155, 'PrivateAreaMasterPast', 3, 176.353, 27.5, -1580.889, -1.320, 1052),
    (1000682, 'm0g1_pa3_mansel',        155, 'PrivateAreaMasterPast', 3, 174.140, 27.5, -1578.116, 1.513,  1052),
    (1000683, 'm0g1_pa3_cecilia',       155, 'PrivateAreaMasterPast', 3, 175.076, 27.5, -1577.582, -0.793, 1007),
    (1000733, 'm0g1_pa3_turstin',       155, 'PrivateAreaMasterPast', 3, 176.059, 27.5, -1579.488, 2.236,  1008),
    (1000734, 'm0g1_pa3_langloisiert',  155, 'PrivateAreaMasterPast', 3, 178.249, 27.5, -1583.473, 1.574,  1000),
    (1000735, 'm0g1_pa3_helbhanth',     155, 'PrivateAreaMasterPast', 3, 176.820, 27.5, -1584.781, -2.388, 1000),
    (1000738, 'm0g1_pa3_pasdevillet',   155, 'PrivateAreaMasterPast', 3, 167.379, 27.5, -1585.992, 1.572,  0),
    (1000741, 'm0g1_pa3_jijimaya',      155, 'PrivateAreaMasterPast', 3, 177.482, 27.5, -1585.254, -0.688, 1052),
    -- SEQ_095: the same room after Burchard's talk, 155 PA 4 (warp-in 177.0, 27.5, -1581.0)
    (1000243, 'm0g1_pa4_burchard',      155, 'PrivateAreaMasterPast', 4, 194.4514, 27.90045, -1577.0281, 3.1395, 1041),
    (1000681, 'm0g1_pa4_nuala',         155, 'PrivateAreaMasterPast', 4, 195.69795, 27.90045, -1577.4851, -2.445, 1043),
    (1000015, 'm0g1_pa4_tkebbe',        155, 'PrivateAreaMasterPast', 4, 208.0318, 29.5, -1582.1663, -0.0134, 1052),
    (1000017, 'm0g1_pa4_farrimond',     155, 'PrivateAreaMasterPast', 4, 208.5445, 29.5, -1581.1256, -0.5696, 1001),
    (1000683, 'm0g1_pa4_cecilia',       155, 'PrivateAreaMasterPast', 4, 208.7333, 29.5, -1578.9501, -0.0058, 1007),
    (1000682, 'm0g1_pa4_mansel',        155, 'PrivateAreaMasterPast', 4, 208.4712, 29.5, -1577.7032, 3.1318, 1052),
    (1000741, 'm0g1_pa4_jijimaya',      155, 'PrivateAreaMasterPast', 4, 183.7241, 27.5, -1594.3449, 0.8122, 1014);
