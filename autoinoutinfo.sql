/*
Navicat MySQL Data Transfer

Source Server         : EasyJoin
Source Server Version : 50530
Source Host           : localhost:3306
Source Database       : parking

Target Server Type    : MYSQL
Target Server Version : 50530
File Encoding         : 65001

Date: 2017-04-14 18:09:08
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for autoinoutinfo
-- ----------------------------
DROP TABLE IF EXISTS `autoinoutinfo`;
CREATE TABLE `autoinoutinfo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '流水编号',
  `ParkingID` varchar(50) DEFAULT NULL COMMENT '停车场编号',
  `DoorIn` varchar(50) DEFAULT NULL,
  `DoorOut` varchar(50) DEFAULT NULL COMMENT '通道',
  `CarNo` varchar(50) NOT NULL COMMENT '车牌号码',
  `InTime` datetime DEFAULT NULL COMMENT '入场时间',
  `OutTime` datetime DEFAULT NULL COMMENT '出场时间',
  `State` varchar(1) DEFAULT NULL COMMENT '0入场1出场',
  `TimeLong` int(11) DEFAULT NULL COMMENT '停车时长(分钟)',
  `TotalCost` float DEFAULT NULL COMMENT '停车费用',
  `PreCost` float DEFAULT NULL COMMENT '优惠金额',
  `FinalCost` float DEFAULT NULL COMMENT '最终收费',
  `Model` varchar(50) DEFAULT NULL COMMENT '收费方式',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COMMENT='车辆进出记录';

-- ----------------------------
-- Records of autoinoutinfo
-- ----------------------------
