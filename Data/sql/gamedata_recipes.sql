-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.6.17 - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Version:             10.1.0.5464
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for ffxiv_server
CREATE DATABASE IF NOT EXISTS `ffxiv_server` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `ffxiv_server`;

-- Dumping structure for table ffxiv_server.gamedata_recipes
CREATE TABLE IF NOT EXISTS `gamedata_recipes` (
  `id` int(4) NOT NULL AUTO_INCREMENT,
  `craftedItem` int(11) NOT NULL,
  `craftedQuantity` int(11) NOT NULL,
  `job` char(4) NOT NULL COMMENT 'A=CRP,B=BSM,C=ARM,D=GSM,E=LTW,F=WVR,G=ALC,H=CUL',
  `level` tinyint(1) NOT NULL,
  `dated` tinyint(1) DEFAULT NULL,
  `kind` varchar(2) NOT NULL COMMENT 'AA=Material,BB=Parts,CC=Finished',
  `crystal0ID` int(11) NOT NULL DEFAULT '0',
  `crystal0Quantity` int(11) NOT NULL DEFAULT '0',
  `crystal1ID` int(11) NOT NULL DEFAULT '0',
  `crystal1Quantity` int(11) NOT NULL DEFAULT '0',
  `facilities` int(11) DEFAULT NULL,
  `material0` int(11) NOT NULL DEFAULT '0',
  `material1` int(11) NOT NULL DEFAULT '0',
  `material2` int(11) NOT NULL DEFAULT '0',
  `material3` int(11) NOT NULL DEFAULT '0',
  `material4` int(11) NOT NULL DEFAULT '0',
  `material5` int(11) NOT NULL DEFAULT '0',
  `material6` int(11) NOT NULL DEFAULT '0',
  `material7` int(11) NOT NULL DEFAULT '0',
  `subSkill0Job` int(11) DEFAULT NULL,
  `subSkill0Level` varchar(5) DEFAULT NULL,
  `subSkill1Job` int(11) DEFAULT NULL,
  `subSkill1Level` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
