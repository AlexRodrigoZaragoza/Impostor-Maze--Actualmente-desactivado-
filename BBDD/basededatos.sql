-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.4.21-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for themaze
CREATE DATABASE IF NOT EXISTS `themaze` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `themaze`;

-- Dumping structure for table themaze.table_maze
CREATE TABLE IF NOT EXISTS `table_maze` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `SEED` int(11) DEFAULT NULL,
  `COMPLETED` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `SEED` (`SEED`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table themaze.table_maze: ~5 rows (approximately)
/*!40000 ALTER TABLE `table_maze` DISABLE KEYS */;
INSERT INTO `table_maze` (`ID`, `SEED`, `COMPLETED`) VALUES
	(1, 54354323, 1),
	(2, 53456233, 1),
	(3, 863455764, 1),
	(4, 8786456, 1),
	(5, 9090, 0),
	(6, 9091, 1);
/*!40000 ALTER TABLE `table_maze` ENABLE KEYS */;

-- Dumping structure for table themaze.table_message
CREATE TABLE IF NOT EXISTS `table_message` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `MESSAGE` text DEFAULT NULL,
  `USER` int(11) DEFAULT NULL,
  `POSITION` text DEFAULT NULL,
  `CHUNK` int(11) DEFAULT NULL,
  `DATE` timestamp NULL DEFAULT NULL,
  `MAZE` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `MESSAGE_TO_MAZE` (`MAZE`),
  KEY `MESSAGE_TO_USER` (`USER`),
  CONSTRAINT `MESSAGE_TO_MAZE` FOREIGN KEY (`MAZE`) REFERENCES `table_maze` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `MESSAGE_TO_USER` FOREIGN KEY (`USER`) REFERENCES `table_user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table themaze.table_message: ~9 rows (approximately)
/*!40000 ALTER TABLE `table_message` DISABLE KEYS */;
INSERT INTO `table_message` (`ID`, `MESSAGE`, `USER`, `POSITION`, `CHUNK`, `DATE`, `MAZE`) VALUES
	(1, 'Es por la derecha', 2, '32343', 2, '2021-10-22 20:30:14', 2),
	(2, 'Arriba', 1, '443', 1, '2018-10-22 20:30:42', 2),
	(3, 'Cuidado', 2, '435678', 4, '2021-10-22 20:27:00', 1),
	(4, 'En la pared', 4, '5456547', 3, '2021-06-22 20:31:19', 4),
	(5, 'Más arriba', 3, '543621', 2, '2020-10-22 20:31:47', 3),
	(6, 'A la izquierda', 4, '9543', 1, '2017-10-22 20:32:10', 4),
	(7, 'NOOOOOOOOO', 1, '5346', 1, '2021-10-18 20:32:30', 3),
	(8, 'ASI SI', 2, '97423', 4, '2019-10-22 20:32:57', 2),
	(9, 'En el suelo', 4, '94523', 3, '2015-07-19 20:31:23', 4),
	(10, '"Estoy probando ok?"', 1, '"(10,0.5,0.5)"', 1, '0000-00-00 00:00:00', 5);
/*!40000 ALTER TABLE `table_message` ENABLE KEYS */;

-- Dumping structure for table themaze.table_rel_user_maze
CREATE TABLE IF NOT EXISTS `table_rel_user_maze` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `USER` int(11) DEFAULT NULL,
  `MAZE` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `USER` (`USER`),
  UNIQUE KEY `MAZE` (`MAZE`),
  CONSTRAINT `REL_TO_MAZE` FOREIGN KEY (`MAZE`) REFERENCES `table_maze` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `REL_TO_USER` FOREIGN KEY (`USER`) REFERENCES `table_user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table themaze.table_rel_user_maze: ~4 rows (approximately)
/*!40000 ALTER TABLE `table_rel_user_maze` DISABLE KEYS */;
INSERT INTO `table_rel_user_maze` (`ID`, `USER`, `MAZE`) VALUES
	(1, 3, 1),
	(2, 4, 4),
	(3, 2, 2),
	(4, 1, 3);
/*!40000 ALTER TABLE `table_rel_user_maze` ENABLE KEYS */;

-- Dumping structure for table themaze.table_traps
CREATE TABLE IF NOT EXISTS `table_traps` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DEAD` int(11) DEFAULT NULL,
  `MAZE` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `TRAPS_TO_MAZE` (`MAZE`),
  CONSTRAINT `TRAPS_TO_MAZE` FOREIGN KEY (`MAZE`) REFERENCES `table_maze` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table themaze.table_traps: ~13 rows (approximately)
/*!40000 ALTER TABLE `table_traps` DISABLE KEYS */;
INSERT INTO `table_traps` (`ID`, `DEAD`, `MAZE`) VALUES
	(1, 34, 2),
	(2, 634, 3),
	(3, 2, 3),
	(4, 234, 4),
	(5, 423425, 1),
	(6, 5345, 1),
	(7, 333, 2),
	(8, 54365, 4),
	(9, 565, 2),
	(10, 654742, 3),
	(11, 54, 3),
	(12, 6564, 1),
	(13, 654, 4),
	(14, 0, 5);
/*!40000 ALTER TABLE `table_traps` ENABLE KEYS */;

-- Dumping structure for table themaze.table_user
CREATE TABLE IF NOT EXISTS `table_user` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NICK` text DEFAULT NULL,
  `PASSWORD` text DEFAULT NULL,
  `CURRENT_POINTS` int(11) DEFAULT NULL,
  `GLOBAL_POINTS` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `NICK` (`NICK`) USING HASH
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table themaze.table_user: ~37 rows (approximately)
/*!40000 ALTER TABLE `table_user` DISABLE KEYS */;
INSERT INTO `table_user` (`ID`, `NICK`, `PASSWORD`, `CURRENT_POINTS`, `GLOBAL_POINTS`) VALUES
	(1, 'Pedro', '123456', 20, 20),
	(2, 'Juan', '465743', 568456, 2147483647),
	(3, 'Carla', '3545365', 5345345, 75675675),
	(4, 'Marcos', 'Holacomoestas', 56, 7657),
	(5, 'Cris', '123456', 0, 0),
	(6, 'Carlos', '123456', 0, 0),
	(7, 'ainoa', '123456', 0, 0),
	(8, 'casa', '123456', 0, 0),
	(9, 'Genaro', '123456', 0, 0),
	(10, 'mikasa', '123456', 0, 0),
	(11, 'jejr', '1234', 0, 0),
	(12, 'Collins', '12', 0, 0),
	(13, 'Geni', '654321', 0, 0),
	(14, 'pepi', '654321', 0, 0),
	(15, 'pipi', '678', 0, 0),
	(16, 'kk', '342', 0, 0),
	(17, 'uyuy', '5467', 0, 0),
	(18, 'ili', '67', 0, 0),
	(19, 'juy', '567', 0, 0),
	(20, 'rte', '34', 0, 0),
	(21, '435', 'rtert', 0, 0),
	(22, 'ggff', '45345', 0, 0),
	(23, 'grht', 'fgndnhfg', 0, 0),
	(24, 'hndnh', 'dhnh', 0, 0),
	(25, 'tggtg', '45', 0, 0),
	(26, 'trhsrt', 'srthstr', 0, 0),
	(27, 'sdf', 'sdf', 0, 0),
	(28, 'dcd', 'dzc', 0, 0),
	(29, 'scasca', 'cas', 0, 0),
	(30, 'zxc', 'xzcxz', 0, 0),
	(31, 'zxz', 'zxz', 0, 0),
	(32, 'prueba', '342234', 0, 0),
	(33, 'Alex', 'kk', 0, 0),
	(34, '', '', 0, 0),
	(35, NULL, '123456', 0, 0),
	(36, '"Pedro"', '123456', 0, 0),
	(37, 'caca', '123456', 0, 0);
/*!40000 ALTER TABLE `table_user` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
