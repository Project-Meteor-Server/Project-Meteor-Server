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

-- Dumping structure for table ffxiv_server.server_zones_privateareas
DROP TABLE IF EXISTS `server_zones_privateareas`;
CREATE TABLE IF NOT EXISTS `server_zones_privateareas` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `parentZoneId` int(10) unsigned NOT NULL,
  `className` varchar(64) NOT NULL,
  `privateAreaName` varchar(32) NOT NULL,
  `privateAreaType` int(10) unsigned NOT NULL,
  `canExitArea` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `music` smallint(6) unsigned DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table ffxiv_server.server_zones_privateareas: ~25 rows (approximately)
/*!40000 ALTER TABLE `server_zones_privateareas` DISABLE KEYS */;
REPLACE INTO `server_zones_privateareas` (`id`, `parentZoneId`, `className`, `privateAreaName`, `privateAreaType`, `canExitArea`, `music`) VALUES
	(1, 184, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 1, 0, 66),
	(2, 230, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 1, 0, 59),
	(4, 133, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 2, 0, 40),
	(5, 155, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 1, 0, 51),
	(6, 155, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 2, 0, 40),
	(8, 175, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 3, 0, 66),
	(9, 175, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 4, 0, 40),
	(10, 180, '/Area/PrivateArea/PrivateAreaMasterBranch', 'PrivateAreaMasterMarket', 102, 0, 48),
	(11, 230, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 3, 0, 40),
	(12, 230, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 4, 0, 40),
	(13, 209, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 5, 1, 66),
	(14, 230, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 5, 0, 37),
	(15, 128, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 2, 0, 0),
	(16, 133, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 3, 0, 40),
	(17, 230, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 6, 0, 40),
	(18, 230, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 7, 0, 42),
	(19, 230, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 8, 1, 40),
	(20, 192, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 0, 0, 42),
	(21, 192, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 1, 0, 9),
	(22, 128, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 3, 0, 40),
	(23, 209, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 5, 1, 66),
	(24, 206, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 5, 1, 51),
	(25, 128, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 5, 1, 53),
	(26, 181, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 5, 1, 61),
	(27, 172, '/Area/PrivateArea/PrivateAreaMasterPast', 'PrivateAreaMasterPast', 5, 1, 72);
/*!40000 ALTER TABLE `server_zones_privateareas` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
