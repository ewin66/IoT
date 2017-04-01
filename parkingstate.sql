/*
Navicat MySQL Data Transfer

Source Server         : EasyJoin
Source Server Version : 50530
Source Host           : localhost:3306
Source Database       : parking

Target Server Type    : MYSQL
Target Server Version : 50530
File Encoding         : 65001

Date: 2017-04-01 17:57:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for parkingstate
-- ----------------------------
DROP TABLE IF EXISTS `parkingstate`;
CREATE TABLE `parkingstate` (
  `WPSD_ID` varchar(20) NOT NULL COMMENT '设备编号',
  `PARKINGLOT_ID` varchar(50) DEFAULT NULL COMMENT '停车场编号',
  `PARKINGNAME` varchar(50) NOT NULL COMMENT '停车位编号',
  `STATE` varchar(1) NOT NULL COMMENT '车位状态',
  `UPDATETIME` datetime DEFAULT NULL COMMENT '更新时间',
  `HEIGHT` int(11) DEFAULT NULL,
  `WIDTH` int(11) DEFAULT NULL,
  `LOCATIONX` int(11) DEFAULT NULL,
  `LOCATIONY` int(11) DEFAULT NULL,
  `WEBHEIGHT` int(11) DEFAULT NULL,
  `WEBWIDTH` int(11) DEFAULT NULL,
  `WEBTOP` int(11) DEFAULT NULL,
  `WEBLEFT` int(11) DEFAULT NULL,
  `RSSI` int(11) DEFAULT NULL,
  `Battery` float DEFAULT NULL,
  `ChangeTime` datetime DEFAULT NULL,
  PRIMARY KEY (`WPSD_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='车位状态';

-- ----------------------------
-- Records of parkingstate
-- ----------------------------
INSERT INTO `parkingstate` VALUES ('13DBDB52', 'SMDS001', '8', '2', '2017-04-01 17:55:43', '20', '18', '505', '443', '34', '22', '322', '470', '-96', '3.7', null);
INSERT INTO `parkingstate` VALUES ('13DBDC52', 'SMDS001', '7', '1', '2017-04-01 17:54:08', '20', '18', '522', '443', '34', '22', '322', '465', '-90', '3.6', null);
INSERT INTO `parkingstate` VALUES ('13DBDD52', 'SMDS001', '6', '2', '2017-04-01 17:33:10', '20', '18', '539', '443', '34', '22', '322', '467', '-102', '3.7', null);
INSERT INTO `parkingstate` VALUES ('13DBDE52', 'SMDS001', '5', '2', '2017-04-01 17:47:33', '20', '18', '553', '443', '34', '22', '322', '466', '-98', '3.7', null);
INSERT INTO `parkingstate` VALUES ('13DBDF52', 'SMDS001', '4', '1', '2017-04-01 17:56:53', '20', '18', '569', '443', '34', '22', '322', '469', '-104', '3.7', null);
INSERT INTO `parkingstate` VALUES ('13DBE052', 'SMDS001', '3', '1', '2017-04-01 17:53:50', '20', '18', '584', '443', '34', '22', '322', '471', '-110', '3.7', null);
INSERT INTO `parkingstate` VALUES ('13DBE152', 'SMDS001', '2', '1', '2017-04-01 17:55:12', '20', '18', '600', '443', '34', '22', '322', '468', '-98', '3.7', null);
INSERT INTO `parkingstate` VALUES ('13DBE252', 'SMDS001', '1', '1', '2017-04-01 17:48:07', '20', '18', '615', '443', '34', '22', '322', '464', '-97', '3.7', null);
