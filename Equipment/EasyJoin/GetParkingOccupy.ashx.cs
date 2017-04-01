using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using System.Data;

namespace EasyJoin
{
    /// <summary>
    /// GetParkingOccupy 的摘要说明
    /// </summary>
    public class GetParkingOccupy : IHttpHandler
    {
        Business.Parking bllParking;
        ReturnValue resoult;
        ParkingOccupyEntity parkingOccupyEntity;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(GetParkingOccupyJosin());
        }

        private string GetParkingOccupyJosin()
        {
            bllParking = new Business.Parking();
            resoult = bllParking.GetParkingOccupy("SMDS001");
            List<ParkingOccupyEntity> list = new List<ParkingOccupyEntity>();
            if (resoult.NoError && resoult.Count > 0)
            {
                DataTable tbparking = resoult.ResultDataSet.Tables[0];
                for (int i = 0; i < tbparking.Rows.Count; i++)
                {
                    parkingOccupyEntity = new ParkingOccupyEntity();
                    parkingOccupyEntity.UpdateTime = tbparking.Rows[i]["UpdateTime"].ToString();
                    parkingOccupyEntity.Vacant =Int32.Parse(tbparking.Rows[i]["Vacant"].ToString());
                    parkingOccupyEntity.Occupy =Int32.Parse(tbparking.Rows[i]["Occupy"].ToString());
                    list.Add(parkingOccupyEntity);
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