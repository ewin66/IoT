using DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class WaterLevel
    {
        public string GetCollecteData(string CommunicationNo, string startTime,string endTime)
        {
            string sql = string.Format(@"Select COMMUNICATION_NO,RECEIVED_DATA,RECEIVED_TIME from BUSINESS_EQUIPMENT_DATA
Where COMMUNICATION_NO='{0}' and RECEIVED_TIME> '{1}' and RECEIVED_TIME< '{2}';", CommunicationNo, startTime, endTime);
            return DBConnect.ExecuteSql(sql);
        }

        public string GetLatestData(string CommunicationNo)
        {
            string sql = string.Format(@"SELECT
	RECEIVED_DATA
FROM
	BUSINESS_EQUIPMENT_DATA
WHERE
	COMMUNICATION_NO = 'RS232/RS485 TO RJ45&WIFI CONVERTER'
ORDER BY
	RECEIVED_TIME DESC
LIMIT 1;", CommunicationNo);
            object obj = DBConnect.GetSingle(sql);
            if (obj==null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }
    }
}
