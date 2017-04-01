using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using System.Data;

namespace EasyJoin
{
    /// <summary>
    /// GetParkingState 的摘要说明
    /// </summary>
    public class GetParkingState : IHttpHandler
    {
        Business.Parking bllParking;
        ReturnValue resoult;
        ParkingStateEntity parkingState;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(GetParkingStateJosin());
        }

        private string GetParkingStateJosin()
        {
            bllParking = new Business.Parking();
            resoult = bllParking.GetParkingState("SMDS001");
            List<ParkingStateEntity> list = new List<ParkingStateEntity>();
            if (resoult.NoError && resoult.Count > 0)
            {
                DataTable tbparking = resoult.ResultDataSet.Tables[0];
                for (int i = 0; i < tbparking.Rows.Count; i++)
                {
                    DateTime time = tbparking.Rows[i]["ChangeTime"].ToString() == "" ? DateTime.Parse(tbparking.Rows[i]["UPDATETIME"].ToString()) : DateTime.Parse(tbparking.Rows[i]["ChangeTime"].ToString());
                    TimeSpan timeSpan = DateTime.Now.Subtract(time);
                    parkingState = new ParkingStateEntity();
                    parkingState.WPSD_ID = tbparking.Rows[i]["WPSD_ID"].ToString();
                    parkingState.STATE = tbparking.Rows[i]["STATE"].ToString();
                    parkingState.PARKINGNAME = tbparking.Rows[i]["PARKINGNAME"].ToString();
                    parkingState.UPDATETIME = DateTime.Parse(tbparking.Rows[i]["UPDATETIME"].ToString());
                    parkingState.RSSI= tbparking.Rows[i]["RSSI"].ToString();
                    parkingState.Battery = tbparking.Rows[i]["Battery"].ToString();
                    parkingState.ChangeTime = time;
                    parkingState.ToolTip = "传感器编号:" + tbparking.Rows[i]["WPSD_ID"].ToString() + @"
信号强度:"
                        + tbparking.Rows[i]["RSSI"].ToString() + @"
电池电压:"
                        + tbparking.Rows[i]["Battery"].ToString() + @"
最后接收信号时间:"
                        + tbparking.Rows[i]["UPDATETIME"].ToString() + @"
当前状态维持:"
                        + string.Format("{0}天{1}小时{2}分钟{3}秒",timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    list.Add(parkingState);
                }
            }
            return Serialize(list);
        }

        public string Serialize(object o)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            json.Serialize(o, sb);
            return sb.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}