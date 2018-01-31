/*
Navicat MySQL Data Transfer

Source Server         : EasyJoin
Source Server Version : 50530
Source Host           : localhost:3306
Source Database       : parking

Target Server Type    : MYSQL
Target Server Version : 50530
File Encoding         : 65001

Date: 2017-04-14 18:09:19
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for parkinginfo
-- ----------------------------
DROP TABLE IF EXISTS `parkinginfo`;
CREATE TABLE `parkinginfo` (
  `ParkingID` varchar(20) NOT NULL COMMENT '停车场编号',
  `ParkingName` varchar(50) NOT NULL COMMENT '停车位编号',
  `Lat` varchar(50) DEFAULT NULL COMMENT '纬度',
  `Lon` varchar(50) DEFAULT NULL COMMENT '经度',
  `Position` varchar(200) DEFAULT NULL COMMENT '位置',
  `ServerAddress` varchar(50) DEFAULT NULL COMMENT '服务器地址',
  `Manager` varchar(50) DEFAULT NULL COMMENT '联系人',
  `PhoneNum` varchar(50) DEFAULT NULL COMMENT '电话',
  `Email` varchar(50) DEFAULT NULL COMMENT '邮箱',
  `TotleNum` int(11) DEFAULT NULL COMMENT '车位总数',
  `FreeNum` int(11) DEFAULT NULL COMMENT '当前空余车位数',
  `FeeScale` varchar(100) DEFAULT NULL COMMENT '收费标准',
  `UpdateDataTime` datetime DEFAULT NULL COMMENT '更新时间',
  `CreateDataTime` datetime DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`ParkingID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='停车场信息';

-- ----------------------------
-- Records of parkinginfo
-- ----------------------------
INSERT INTO `parkinginfo` VALUES ('SMDS001', '数贸大厦停车场', '22.564906', '113.498169', '中山市火炬开发区祥兴路6号', null, null, null, null, '355', '106', '0', '2017-04-14 11:20:28', '2017-04-14 11:20:34');
