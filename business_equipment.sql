/*
Navicat MySQL Data Transfer

Source Server         : MySQLDB
Source Server Version : 50530
Source Host           : localhost:3306
Source Database       : parking

Target Server Type    : MYSQL
Target Server Version : 50530
File Encoding         : 65001

Date: 2018-01-03 23:30:28
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for business_equipment
-- ----------------------------
DROP TABLE IF EXISTS `business_equipment`;
CREATE TABLE `business_equipment` (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `EQUIPMENT_TYPE_ID` int(11) DEFAULT NULL,
  `EQUIPMENT_MODEL_ID` int(11) DEFAULT NULL,
  `COMMUNICATION_NO` varchar(50) CHARACTER SET latin1 DEFAULT NULL,
  `ENAME` varchar(50) DEFAULT NULL,
  `LATITUDE` varchar(50) CHARACTER SET latin1 DEFAULT NULL,
  `LONGITUDE` varchar(50) CHARACTER SET latin1 DEFAULT NULL,
  `POSITION` varchar(200) DEFAULT NULL,
  `JOIN_TIME` datetime DEFAULT NULL,
  `JOINER` varchar(50) DEFAULT NULL,
  `ADDRESS_NO` varchar(10) CHARACTER SET latin1 DEFAULT NULL,
  `STATE` tinyint(4) NOT NULL DEFAULT '1' COMMENT '状态:1-启用(默认);2-停用',
  `CREATE_ID` int(11) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime NOT NULL COMMENT '创建时间',
  `UPDATE_ID` int(11) DEFAULT NULL COMMENT '最后操作人',
  `UPDATE_TIME` datetime DEFAULT NULL COMMENT '最后修改时间',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8 COMMENT='接入设备';

-- ----------------------------
-- Records of business_equipment
-- ----------------------------
INSERT INTO `business_equipment` VALUES ('1', '6', '1', 'RS232/RS485 TO RJ45&WIFI CONVERTER', '数贸大厦监测点', '22.564906', '113.498169', '中山市火炬开发区祥兴路6号数贸大厦1栋221室', '2016-11-25 11:39:44', '万岗', '1', '1', '1', '2016-11-25 11:39:44', '1', '2016-12-22 10:23:10');
INSERT INTO `business_equipment` VALUES ('2', '6', '1', 'ZHKBL0416050051', '市政府监测点', '22.52349', '113.400789', '中山市石岐区松苑路1号市政府大院', '2016-12-02 01:00:42', '万岗', '1', '1', '1', '2016-12-02 01:00:42', '1', '2016-12-07 14:26:20');
INSERT INTO `business_equipment` VALUES ('3', '6', '1', 'ZHKBL0416050052', '凯茵豪园监测点', '22.502753', '113.456144', '广东省中山市景观路与福获路交叉口', '2016-12-02 01:01:55', '万岗', '1', '1', '1', '2016-12-02 01:01:55', '1', '2016-12-07 14:29:33');
INSERT INTO `business_equipment` VALUES ('4', '6', '1', 'ZHKBL0416050053', '紫马岭公园监测点', '22.514527', '113.41727', '广东省中山市博爱六路', '2016-12-07 14:43:35', '万岗', '1', '1', '1', '2016-12-07 14:43:35', '1', '2016-12-11 15:02:03');
INSERT INTO `business_equipment` VALUES ('5', '6', '1', 'ZHKBL0416050059', '中山北站监测点', '22.563538', '113.389055', '中山市市辖区民盈西路与兴畅路交叉路口北', '2016-12-07 14:44:52', '万岗', '1', '1', '1', '2016-12-07 14:44:52', '1', '2016-12-11 15:05:35');
INSERT INTO `business_equipment` VALUES ('6', '6', '1', 'ZHKBL0416050059', '中山站监测站', '22.549678', '113.439692', '中山火炬开发区西部濠头中学对面', '2016-12-07 14:48:16', '万岗', '1', '1', '1', '2016-12-07 14:48:16', '1', '2016-12-11 14:46:20');
INSERT INTO `business_equipment` VALUES ('7', '6', '1', 'ZHKBL0416050059', '中山公园监测点', '22.529446', '113.376461', '广东省中山市公园大道', '2016-12-07 14:49:57', '万岗', '1', '1', '1', '2016-12-07 14:49:57', '1', '2016-12-11 14:58:31');
INSERT INTO `business_equipment` VALUES ('8', '6', '1', 'ZHKBL0416050059', '中山汽车总站监测点', '22.52728', '113.349599', '中山市西区街道富华道48号', '2016-12-07 14:51:16', '万岗', '1', '1', '1', '2016-12-07 14:51:16', '1', '2016-12-11 15:03:55');
INSERT INTO `business_equipment` VALUES ('9', '6', '1', 'ZHKBL0416050059', '南朗监测点', '22.495939', '113.540295', '南朗', '2016-12-11 15:07:28', '万岗', '1', '1', '1', '2016-12-11 15:07:28', '1', '2016-12-11 15:07:28');
INSERT INTO `business_equipment` VALUES ('10', '6', '1', 'ZHKBL0416050059', '小榄站监测点', '22.653592', '113.276382', '小榄', '2016-12-11 15:08:31', '万岗', '1', '1', '1', '2016-12-11 15:08:31', '1', '2016-12-11 15:08:31');
INSERT INTO `business_equipment` VALUES ('11', '6', '1', 'ZHKBL0416050059', '坦洲监测点', '22.260588', '113.474307 ', '坦洲', '2016-12-11 15:11:39', '万岗', '1', '1', '1', '2016-12-11 15:11:39', '1', '2016-12-11 15:11:39');
INSERT INTO `business_equipment` VALUES ('12', '6', '1', 'ZHKBL0416050059', '神湾监测点', '22.296039', '113.450735', '神湾', '2016-12-11 15:12:38', '万岗', '1', '1', '1', '2016-12-11 15:12:38', '1', '2016-12-11 15:12:38');
INSERT INTO `business_equipment` VALUES ('13', '6', '1', 'ZHKBL0416050059', '三乡监测点', '22.351537', '113.450735', '三乡', '2016-12-11 15:13:23', '万岗', '1', '1', '1', '2016-12-11 15:13:23', '1', '2016-12-11 15:13:23');
INSERT INTO `business_equipment` VALUES ('14', '6', '1', 'ZHKBL0416050059', '板芙监测点', '22.407682', '113.409342', '板芙', '2016-12-11 15:15:13', '万岗', '1', '1', '1', '2016-12-11 15:15:13', '1', '2016-12-11 15:15:13');
INSERT INTO `business_equipment` VALUES ('15', '6', '1', 'ZHKBL0416050059', '五桂山监测点', '22.422351', '113.473008', '五桂山', '2016-12-11 15:16:12', '万岗', '1', '1', '1', '2016-12-11 15:16:12', '1', '2016-12-11 15:16:12');
INSERT INTO `business_equipment` VALUES ('16', '6', '1', 'ZHKBL0416050059', '黄埔监测点', '22.715098', '113.343527', '黄埔', '2016-12-11 15:17:06', '万岗', '1', '1', '1', '2016-12-11 15:17:06', '1', '2016-12-11 15:17:06');
INSERT INTO `business_equipment` VALUES ('17', '6', '1', 'ZHKBL0416050059', '南头监测点', '22.727202', '113.320979', '南头', '2016-12-11 15:18:05', '万岗', '1', '1', '1', '2016-12-11 15:18:05', '1', '2016-12-11 15:18:05');
INSERT INTO `business_equipment` VALUES ('18', '6', '1', 'ZHKBL0416050059', '三角监测点', '22.682489', '113.424476', '三角', '2016-12-11 15:18:50', '万岗', '1', '1', '1', '2016-12-11 15:18:50', '1', '2016-12-11 15:18:50');
INSERT INTO `business_equipment` VALUES ('19', '6', '1', 'ZHKBL0416050059', '民众监测点', '22.627524', '113.500096', '民众', '2016-12-11 15:19:36', '万岗', '1', '1', '1', '2016-12-11 15:19:36', '1', '2016-12-11 15:19:36');
INSERT INTO `business_equipment` VALUES ('20', '6', '1', 'ZHKBL0416050059', '古镇监测点', '22.618673', '113.196999', '古镇', '2016-12-11 15:20:30', '万岗', '1', '1', '1', '2016-12-11 15:20:30', '1', '2016-12-11 15:20:30');
INSERT INTO `business_equipment` VALUES ('21', '6', '1', 'ZHKBL0416050059', '横栏监测点', '22.529531', '113.271203', '横栏', '2016-12-11 15:21:26', '万岗', '1', '1', '1', '2016-12-11 15:21:26', '1', '2016-12-11 15:21:26');
INSERT INTO `business_equipment` VALUES ('22', '6', '1', 'ZHKBL0416050059', '大涌监测点', '22.471243', '113.307187', '大涌', '2016-12-11 15:22:38', '万岗', '1', '1', '1', '2016-12-11 15:22:38', '1', '2016-12-11 15:22:38');
SET FOREIGN_KEY_CHECKS=1;
