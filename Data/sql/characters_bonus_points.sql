-- Attribute (bonus) point allotment per class [OFF 1.20 dev1228: points are allotted per class; jobs use
-- their base class's]. One row per character and class. Written by BonusPointCommand.lua through
-- Player.SetBonusAllotment; read by battle/stats.lua (player:GetBonusPoint). A class with no row uses
-- the automatic split (stats.bonusAllocation).
CREATE TABLE IF NOT EXISTS `characters_bonus_points` (
  `characterId` int(10) unsigned NOT NULL,
  `classId` tinyint(3) unsigned NOT NULL,
  `str` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `vit` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `dex` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `int` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `mnd` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `pie` tinyint(3) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`characterId`, `classId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
