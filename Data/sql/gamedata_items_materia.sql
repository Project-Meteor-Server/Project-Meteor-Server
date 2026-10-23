--
-- Table structure for table `gamedata_items_materia`
--

SET autocommit = 0;

DROP TABLE IF EXISTS `gamedata_items_materia`;

CREATE TABLE `gamedata_items_materia` (
    `id` smallint(5) NOT NULL,
    `tier1CatalogID` int(10) unsigned NOT NULL,
    `tier2CatalogID` int(10) unsigned NOT NULL,
    `tier3CatalogID` int(10) unsigned NOT NULL,
    `tier4CatalogID` int(10) unsigned NOT NULL,
    `bonus1Type` int(11) NOT NULL,
    `bonus1Grade0Value` smallint(6) NOT NULL,
    `bonus1Grade1Value` smallint(6) NOT NULL,
    `bonus1Grade2Value` smallint(6) NOT NULL,
    `bonus1Grade3Value` smallint(6) NOT NULL,
    `bonus1Grade4Value` smallint(6) NOT NULL,
    `bonus1Grade5Value` smallint(6) NOT NULL,
    `bonus1Grade6Value` smallint(6) NOT NULL,
    `bonus1Grade7Value` smallint(6) NOT NULL,
    `bonus1Grade8Value` smallint(6) NOT NULL,
    `bonus1Grade9Value` smallint(6) NOT NULL,
    `bonus1Grade10Value` smallint(6) NOT NULL,
    `bonus1Grade11Value` smallint(6) NOT NULL,
    `bonus1Grade12Value` smallint(6) NOT NULL,
    `bonus1Grade13Value` smallint(6) NOT NULL,
    `bonus1Grade14Value` smallint(6) NOT NULL,
    `bonus1Grade15Value` smallint(6) NOT NULL,
    `bonus2Type` int(11) NOT NULL,
    `bonus2Grade0Value` smallint(6) NOT NULL,
    `bonus2Grade1Value` smallint(6) NOT NULL,
    `bonus2Grade2Value` smallint(6) NOT NULL,
    `bonus2Grade3Value` smallint(6) NOT NULL,
    `bonus2Grade4Value` smallint(6) NOT NULL,
    `bonus2Grade5Value` smallint(6) NOT NULL,
    `bonus2Grade6Value` smallint(6) NOT NULL,
    `bonus2Grade7Value` smallint(6) NOT NULL,
    `bonus2Grade8Value` smallint(6) NOT NULL,
    `bonus2Grade9Value` smallint(6) NOT NULL,
    `bonus2Grade10Value` smallint(6) NOT NULL,
    `bonus2Grade11Value` smallint(6) NOT NULL,
    `bonus2Grade12Value` smallint(6) NOT NULL,
    `bonus2Grade13Value` smallint(6) NOT NULL,
    `bonus2Grade14Value` smallint(6) NOT NULL,
    `bonus2Grade15Value` smallint(6) NOT NULL,
    `tier1Icon` int(11) NOT NULL,
    `tier2Icon` int(11) NOT NULL,
    `tier3Icon` int(11) NOT NULL,
    `tier4Icon` int(11) NOT NULL,
    `meldable1` boolean NOT NULL,
    `meldable2` boolean NOT NULL,
    `meldable3` boolean NOT NULL,
    `meldable4` boolean NOT NULL,
    `meldable5` boolean NOT NULL,
    `meldable6` boolean NOT NULL,
    `meldable7` boolean NOT NULL,
    `meldable8` boolean NOT NULL,
    `meldable9` boolean NOT NULL,
    `meldable10` boolean NOT NULL,
    `meldable11` boolean NOT NULL,
    `meldable12` boolean NOT NULL,
    `meldable13` boolean NOT NULL,
    `meldable14` boolean NOT NULL,
    `meldable15` boolean NOT NULL,
    `meldable16` boolean NOT NULL,
    `meldable17` boolean NOT NULL,
    `meldable18` boolean NOT NULL,
    `meldable19` boolean NOT NULL,
    `meldable20` boolean NOT NULL,
    `meldable21` boolean NOT NULL,
    `meldable22` boolean NOT NULL,
    `meldable23` boolean NOT NULL,
    `meldable24` boolean NOT NULL,
    `meldable25` boolean NOT NULL,
    `meldable26` boolean NOT NULL,
    `meldable27` boolean NOT NULL,
    `meldable28` boolean NOT NULL,
    `meldable29` boolean NOT NULL,
    `meldable30` boolean NOT NULL,
    `meldable31` boolean NOT NULL,
    `meldable32` boolean NOT NULL,
    `meldable33` boolean NOT NULL,
    `meldable34` boolean NOT NULL,
    `meldable35` boolean NOT NULL,
    `meldable36` boolean NOT NULL,
    `meldable37` boolean NOT NULL,
    `meldable38` boolean NOT NULL,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

LOCK TABLES `gamedata_items_materia` WRITE;

INSERT INTO `gamedata_items_materia` VALUES (1,10100001,10100002,10100003,10100004,15001,10,14,17,20,25,29,32,35,40,44,47,50,55,60,65,70,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61628,61629,61630,61631,false,true,true,true,true,true,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (2,10100005,10100006,10100007,10100008,15002,10,14,17,20,22,23,24,25,27,28,29,30,32,33,34,35,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61628,61629,61630,61631,true,true,false,true,false,true,true,false,false,false,true,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (3,10100009,10100010,10100011,10100012,15001,5,7,8,10,12,14,16,18,20,22,24,25,28,30,32,35,15002,5,7,8,10,11,11,12,12,14,14,14,15,16,16,17,18,61628,61629,61630,61631,false,false,false,true,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (4,10100013,10100014,10100015,10100016,15004,1,2,3,5,6,7,8,10,11,12,13,15,16,17,18,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61652,61653,61654,61655,false,false,true,false,false,false,true,true,true,true,false,false,false,false,false,false,false,true,true,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (5,10100017,10100018,10100019,10100020,15005,1,2,3,5,6,7,8,10,11,12,13,15,16,17,18,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61652,61653,61654,61655,false,true,false,false,false,false,false,true,true,false,true,false,true,true,false,false,true,false,true,false,true,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (6,10100021,10100022,10100023,10100024,15006,1,2,3,5,6,7,8,10,11,12,13,15,16,17,18,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61652,61653,61654,61655,false,false,true,false,false,false,true,true,true,true,true,false,false,false,false,false,true,false,false,true,false,true,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (7,10100025,10100026,10100027,10100028,15007,1,2,3,5,6,7,8,10,11,12,13,15,16,17,18,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61652,61653,61654,61655,true,false,false,false,false,false,true,false,false,false,false,false,false,false,true,true,false,false,false,true,true,false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (8,10100029,10100030,10100031,10100032,15008,1,2,3,5,6,7,8,10,11,12,13,15,16,17,18,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61652,61653,61654,61655,true,false,false,false,false,false,false,true,false,false,true,false,true,true,true,true,false,true,false,false,false,true,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (9,10100033,10100034,10100035,10100036,15009,1,2,3,5,6,7,8,10,11,12,13,15,16,17,18,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61652,61653,61654,61655,false,true,false,false,false,false,false,false,false,true,true,false,true,true,true,true,false,false,false,false,false,false,true,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (10,10100037,10100038,10100039,10100040,15004,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,15005,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,61652,61653,61654,61655,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (11,10100041,10100042,10100043,10100044,15004,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,15006,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,61652,61653,61654,61655,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (12,10100045,10100046,10100047,10100048,15005,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,15006,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,61652,61653,61654,61655,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (13,10100049,10100050,10100051,10100052,15007,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,15008,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,61652,61653,61654,61655,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (14,10100053,10100054,10100055,10100056,15007,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,15009,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,61652,61653,61654,61655,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (15,10100057,10100058,10100059,10100060,15008,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,15009,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,61652,61653,61654,61655,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (16,10100061,10100062,10100063,10100064,15043,2,4,6,8,9,10,11,12,13,15,17,20,22,25,27,30,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (17,10100065,10100066,10100067,10100068,15044,2,4,6,8,9,10,11,12,13,15,17,20,22,25,27,30,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (18,10100069,10100070,10100071,10100072,15045,2,4,6,8,9,10,11,12,13,15,17,20,22,25,27,30,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (19,10100073,10100074,10100075,10100076,15046,2,4,6,8,9,10,11,12,13,15,17,20,22,25,27,30,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (20,10100077,10100078,10100079,10100080,15047,2,4,6,8,9,10,11,12,13,15,17,20,22,25,27,30,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (21,10100081,10100082,10100083,10100084,15048,2,4,6,8,9,10,11,12,13,15,17,20,22,25,27,30,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,false,false,false,true,false,true,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (22,10100085,10100086,10100087,10100088,15010,1,2,0,3,4,5,0,6,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,true,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,true,true,true,true,true,true,true,true);
INSERT INTO `gamedata_items_materia` VALUES (23,10100089,10100090,10100091,10100092,15011,1,2,0,3,4,5,0,6,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,true,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,true,true,true,true,true,true,true,true);
INSERT INTO `gamedata_items_materia` VALUES (24,10100093,10100094,10100095,10100096,15012,1,2,0,3,4,5,0,6,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,true,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,true,true,true,true,true,true,true,true);
INSERT INTO `gamedata_items_materia` VALUES (25,10100097,10100098,10100099,10100100,15013,1,2,0,3,4,5,0,6,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,true,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,true,true,true,true,true,true,true,true);
INSERT INTO `gamedata_items_materia` VALUES (26,10100101,10100102,10100103,10100104,15014,1,2,0,3,4,5,0,6,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,true,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,true,true,true,true,true,true,true,true);
INSERT INTO `gamedata_items_materia` VALUES (27,10100105,10100106,10100107,10100108,15015,1,2,0,3,4,5,0,6,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61656,61657,61658,61659,false,true,false,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,true,true,true,true,true,true,true,true);
INSERT INTO `gamedata_items_materia` VALUES (28,10100109,10100110,10100111,10100112,15018,4,6,8,10,12,13,14,15,20,22,23,25,26,27,28,30,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61640,61641,61642,61643,false,false,true,false,false,false,false,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (29,10100113,10100114,10100115,10100116,15016,2,4,6,8,9,10,11,12,11,12,13,15,16,17,18,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61640,61641,61642,61643,false,false,true,false,false,false,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (30,10100117,10100118,10100119,10100120,15024,1,2,0,3,4,5,0,6,8,9,0,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61620,61621,61622,61623,false,false,false,false,true,false,false,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (31,10100121,10100122,10100123,10100124,15028,2,4,6,8,9,10,11,12,11,12,13,15,16,17,18,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61620,61621,61622,61623,true,false,false,false,false,false,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (32,10100125,10100126,10100127,10100128,15020,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61640,61641,61642,61643,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (33,10100129,10100130,10100131,10100132,15022,10,12,16,20,22,24,26,30,32,34,36,40,42,44,46,50,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61640,61641,61642,61643,false,true,false,false,false,false,false,false,true,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (35,10100137,10100138,10100139,10100140,15038,10,12,16,20,22,24,26,30,32,34,36,40,42,44,46,50,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61620,61621,61622,61623,false,false,true,false,false,false,false,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (36,10100141,10100142,10100143,10100144,15040,1,3,7,10,11,12,13,15,16,17,18,20,21,22,23,25,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61632,61633,61634,61635,false,false,true,false,false,false,false,true,true,true,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (37,10100145,10100146,10100147,10100148,15033,1,0,0,0,2,0,0,0,3,0,0,4,5,0,0,6,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61624,61625,61626,61627,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (38,10100149,10100150,10100151,10100152,15035,1,0,0,0,2,0,0,0,3,0,0,4,5,0,0,6,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61624,61625,61626,61627,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (39,10100153,10100154,10100155,10100156,15034,1,0,0,0,2,0,0,0,3,0,0,4,5,0,0,6,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61624,61625,61626,61627,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (40,10100157,10100158,10100159,10100160,15030,1,0,0,0,2,0,0,0,3,0,0,4,5,0,0,6,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61624,61625,61626,61627,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (41,10100161,10100162,10100163,10100164,15031,1,0,0,0,2,0,0,0,3,0,0,4,5,0,0,6,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61624,61625,61626,61627,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (42,10100165,10100166,10100167,10100168,15032,1,0,0,0,2,0,0,0,3,0,0,4,5,0,0,6,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61624,61625,61626,61627,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,true,true,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (43,10100169,10100170,10100171,10100172,15017,1,3,7,10,11,12,13,15,16,17,18,20,21,22,23,25,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61632,61633,61634,61635,false,true,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (44,10100173,10100174,10100175,10100176,15029,1,3,7,10,11,12,13,15,16,17,18,20,21,22,23,25,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61620,61621,61622,61623,true,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (45,10100177,10100178,10100179,10100180,15019,2,4,6,8,9,10,11,12,11,12,13,15,16,17,18,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61632,61633,61634,61635,false,true,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (47,10100185,10100186,10100187,10100188,15074,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (48,10100189,10100190,10100191,10100192,15075,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (49,10100193,10100194,10100195,10100196,15076,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (50,10100197,10100198,10100199,10100200,15077,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (51,10100201,10100202,10100203,10100204,15078,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (52,10100205,10100206,10100207,10100208,15079,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (53,10100209,10100210,10100211,10100212,15080,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (54,10100213,10100214,10100215,10100216,15081,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (55,10100217,10100218,10100219,10100220,15082,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (56,10100221,10100222,10100223,10100224,15083,1,2,3,0,4,5,6,0,7,8,9,10,11,12,13,15,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (57,10100225,10100226,10100227,10100228,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61636,61637,61638,61639,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (61,10100241,10100242,10100243,10100244,15052,1,3,7,10,11,12,13,15,16,17,18,20,21,22,23,25,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61648,61649,61650,61651,false,false,false,false,false,true,false,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (62,10100245,10100246,10100247,10100248,15052,-1,-2,-3,-4,-5,-6,-7,-8,-9,-10,-11,-12,-13,-14,-15,-16,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61648,61649,61650,61651,false,false,false,false,false,true,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (68,10100269,10100270,10100271,10100272,15025,1,2,3,4,5,6,7,8,10,11,12,13,15,16,17,20,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61620,61621,61622,61623,true,false,false,false,false,false,false,false,false,false,false,false,true,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (82,10100325,10100326,10100327,10100328,15022,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,15001,1,3,7,10,11,12,13,15,16,17,18,20,21,22,23,25,61640,61641,61642,61643,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (83,10100329,10100330,10100331,10100332,15038,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,15002,5,7,8,10,11,11,12,12,14,14,14,15,16,16,17,18,61620,61621,61622,61623,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (84,10100333,10100334,10100335,10100336,15025,1,1,1,1,2,0,2,3,4,5,0,6,7,8,9,10,15002,5,7,8,10,11,0,12,12,14,14,0,15,16,16,17,18,61620,61621,61622,61623,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (85,10100337,10100338,10100339,10100340,15026,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61620,61621,61622,61623,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (86,10100341,10100342,10100343,10100344,15027,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61620,61621,61622,61623,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (87,10100345,10100346,10100347,10100348,15041,1,0,0,0,2,0,0,3,4,5,0,6,7,8,9,10,-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,61632,61633,61634,61635,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
INSERT INTO `gamedata_items_materia` VALUES (88,10100349,10100350,10100351,10100352,15051,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,15016,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,61640,61641,61642,61643,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);

UNLOCK TABLES;

COMMIT;