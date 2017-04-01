/*
Navicat MySQL Data Transfer

Source Server         : EasyJoin
Source Server Version : 50530
Source Host           : localhost:3306
Source Database       : parking

Target Server Type    : MYSQL
Target Server Version : 50530
File Encoding         : 65001

Date: 2017-04-01 17:57:27
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for parkingoccupy
-- ----------------------------
DROP TABLE IF EXISTS `parkingoccupy`;
CREATE TABLE `parkingoccupy` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PARKINGLOT_ID` varchar(50) DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `Vacant` int(11) DEFAULT NULL,
  `Occupy` int(11) DEFAULT NULL,
  `Times` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=86878 DEFAULT CHARSET=utf8;
