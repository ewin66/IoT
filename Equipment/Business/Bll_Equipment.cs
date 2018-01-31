using Common;
using Common.Entity;
using DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Bll_Equipment
    {
        static string SQL_GetEquipmentTypeList = @"SELECT ID,EQUIPMENT_TYPE_NAME
FROM
	BAS_EQUIPMENT_TYPE";

        static string SQL_GetEquipmentModelList = @"SELECT ID,EQUIPMENT_MODEL_NAME
FROM
	BAS_EQUIPMENT_MODEL
WHERE EQUIPMENT_TYPE_ID ={0}";

        static string SQL_GetEquipmentList = @"SELECT
    E.ID,
	E.EQUIPMENT_TYPE_ID,
	T.EQUIPMENT_TYPE_NAME,
	E.EQUIPMENT_MODEL_ID,
	M.EQUIPMENT_MODEL_NAME,
	E.COMMUNICATION_NO,
    E.ENAME,
	E.LATITUDE,
	E.LONGITUDE,
	E.POSITION,
	E.JOIN_TIME,
	E.JOINER,
	E.ADDRESS_NO,
	E.STATE
FROM
    BUSINESS_EQUIPMENT E
LEFT JOIN BAS_EQUIPMENT_TYPE T ON T.ID = E.EQUIPMENT_TYPE_ID
LEFT JOIN BAS_EQUIPMENT_MODEL M ON M.ID = E.EQUIPMENT_MODEL_ID
AND M.EQUIPMENT_TYPE_ID = E.EQUIPMENT_TYPE_ID";

        static string SQL_GetPositionList = @"SELECT
    E.ID,
	E.EQUIPMENT_TYPE_ID,
	T.EQUIPMENT_TYPE_NAME,
	E.EQUIPMENT_MODEL_ID,
	M.EQUIPMENT_MODEL_NAME,
	E.COMMUNICATION_NO,
    E.ENAME,
	E.LATITUDE,
	E.LONGITUDE,
	E.POSITION,
	E.JOIN_TIME,
	E.JOINER,
	E.ADDRESS_NO,
	E.STATE
FROM
    healthmap E
LEFT JOIN BAS_EQUIPMENT_TYPE T ON T.ID = E.EQUIPMENT_TYPE_ID
LEFT JOIN BAS_EQUIPMENT_MODEL M ON M.ID = E.EQUIPMENT_MODEL_ID
AND M.EQUIPMENT_TYPE_ID = E.EQUIPMENT_TYPE_ID";

        static string SQL_GetEquipmentInfo = @"SELECT
    E.ID,
	E.EQUIPMENT_TYPE_ID,
	E.EQUIPMENT_MODEL_ID,
	E.COMMUNICATION_NO,
    E.ENAME,
	E.LATITUDE,
	E.LONGITUDE,
	E.POSITION,
	E.JOIN_TIME,
	E.JOINER,
	E.ADDRESS_NO,
	E.STATE
FROM
    BUSINESS_EQUIPMENT E Where E.ID = {0}";

        public string SQL_AddEquipment = @"INSERT INTO BUSINESS_EQUIPMENT (
   EQUIPMENT_TYPE_ID,
   EQUIPMENT_MODEL_ID,
   COMMUNICATION_NO,
    ENAME,
	LATITUDE,
	LONGITUDE,
   POSITION,
   JOIN_TIME,
   JOINER,
   ADDRESS_NO,
   STATE,
   CREATE_ID,
   CREATE_TIME,
   UPDATE_ID,
   UPDATE_TIME)
VALUE({0},{1},'{2}','{3}','{4}','{5}','{6}',now(),'{7}','{8}',{9},{10},now(),{11},now());";

        public string SQL_UpdateEquipment = @"Update BUSINESS_EQUIPMENT Set EQUIPMENT_TYPE_ID = {0},
   EQUIPMENT_MODEL_ID={1},
   COMMUNICATION_NO='{2}',
    ENAME='{3}',
	LATITUDE='{4}',
	LONGITUDE='{5}',
   POSITION='{6}',
   JOINER='{7}',
   ADDRESS_NO='{8}',
   STATE={9},
   UPDATE_ID={10},
   UPDATE_TIME=now() Where ID = {11};";

        public string InsertEquipmentData(string CommunicationNo, string receivedData, string receivedValue)
        {
            string sql = string.Format(@"INSERT INTO BUSINESS_EQUIPMENT_DATA (
   COMMUNICATION_NO,
   RECEIVED_DATA,
   RECEIVED_TIME,
   RECEIVED_VALUE,
   CREATE_ID,
   CREATE_TIME,
   UPDATE_ID,
   UPDATE_TIME)
	VALUE('{0}','{1}',now(),{2},1,now(),1,now());", CommunicationNo, receivedData, receivedValue);
            return DBConnect.ExecuteSql(sql);
        }

        public string DeleteEquipmentData(string CommunicationNo)
        {
            string sql = string.Format(@"DELETE From business_equipment_data where'{0}';", CommunicationNo);
            return DBConnect.ExecuteSql(sql);
        }

        public double GetLatestValue(string equipmentID)
        {
            string sql = string.Format(@"SELECT
	RECEIVED_VALUE
FROM
	BUSINESS_EQUIPMENT_DATA ED INNER JOIN business_equipment E 
on ED.COMMUNICATION_NO=E.COMMUNICATION_NO
WHERE
	E.ID = {0}
ORDER BY
	RECEIVED_TIME DESC
LIMIT 1;", equipmentID);
            object obj = DBConnect.GetSingle(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return double.Parse(obj.ToString());
            }
        }
        
        public ReturnValue GetLatestValues()
        {
            string sqlLatestValues = @"SELECT E.ID,
    MIN(E.ENAME) as Name,
(SELECT
	IFNULL(RECEIVED_VALUE,0)
FROM
	BUSINESS_EQUIPMENT_DATA
WHERE
	RECEIVED_TIME = MAX(ED.RECEIVED_TIME)
AND COMMUNICATION_NO = E.COMMUNICATION_NO) as Value
FROM
	business_equipment E Left Join BUSINESS_EQUIPMENT_DATA ED
on ED.COMMUNICATION_NO=E.COMMUNICATION_NO
GROUP BY E.ID";
            return DBConnect.Select(sqlLatestValues);
        }

        public ReturnValue GetEquipmentList()
        {
            return DBConnect.Select(SQL_GetEquipmentList);
        }

        public ReturnValue GetPositionList()
        {
            return DBConnect.Select(SQL_GetPositionList);
        }

        public ReturnValue GetEquipmentInfo(string ID)
        {
            return DBConnect.Select(string.Format(SQL_GetEquipmentInfo, ID));
        }

        public string AddorUpdate(MonitorEntity entity)
        {
            string sql = string.Empty;
            if (entity.ID.Length == 0)
            {
                sql = string.Format(SQL_AddEquipment
                    , entity.EQUIPMENT_TYPE_ID
                    , entity.EQUIPMENT_MODEL_ID
                    , entity.COMMUNICATION_NO
                    , entity.Name
                    , entity.Lat
                    , entity.Long
                    , entity.POSITION
                    , entity.JOINER
                    , entity.ADDRESS_NO
                    , entity.STATE                    
                    , entity.Create_ID
                    , entity.Updata_ID);
                return DBConnect.ExecuteSql(sql);
            }
            else
            {
                sql = string.Format(SQL_UpdateEquipment
                , entity.EQUIPMENT_TYPE_ID
                , entity.EQUIPMENT_MODEL_ID
                , entity.COMMUNICATION_NO
                , entity.Name
                , entity.Lat
                , entity.Long
                , entity.POSITION
                , entity.JOINER
                , entity.ADDRESS_NO
                , entity.STATE
                , entity.Updata_ID
                , entity.ID);
                return DBConnect.ExecuteSql(sql);
            }
        }

        public ReturnValue GetEquipmentTypeList()
        {
            return DBConnect.Select(SQL_GetEquipmentTypeList);
        }

        public ReturnValue GetEquipmentModelList(string typeID)
        {
            return DBConnect.Select(string.Format(SQL_GetEquipmentModelList,typeID));
        }
    }

}
