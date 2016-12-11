using Business;
using Common;
using Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyJoin
{
    /// <summary>
    /// MonitorData 的摘要说明
    /// </summary>
    public class MonitorData : IHttpHandler
    {
        private Bll_Equipment bll;
        ReturnValue resoult;
        MonitorEntity monitor;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(GetMonitorDataJosin());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetMonitorDataJosin()
        {
            Bll_Equipment equipmentData = new Bll_Equipment();
            //double leastValue = equipmentData.GetLatestValue("1");
            List<MonitorEntity> list = new List<MonitorEntity>();
            bll = new Bll_Equipment();
            resoult = bll.GetLatestValues();
            if (resoult.NoError)
            {
                for (int i = 0; i < resoult.Count; i++)
                {
                    monitor = new MonitorEntity();
                    monitor.ID = resoult.ResultDataSet.Tables[0].Rows[i]["ID"].ToString();
                    monitor.Name = resoult.ResultDataSet.Tables[0].Rows[i]["Name"].ToString();
                    if (resoult.ResultDataSet.Tables[0].Rows[i]["Value"].ToString().Length == 0)
                    {
                        monitor.Value = 0;
                    }
                    else
                    {
                        monitor.Value =double.Parse(resoult.ResultDataSet.Tables[0].Rows[i]["Value"].ToString());
                    }
                    list.Add(monitor);
                }
            }
            else
            {

            }
            return Serialize(list);

            //List<MonitorDataEntity> list = new List<MonitorDataEntity>();
            //list.Add(new MonitorDataEntity() { ID = "1", Name = "监测点-数贸大厦", Value = leastValue });
            //list.Add(new MonitorDataEntity() { ID = "2", Name = "监测点-市政府", Value = 0.06});
            //list.Add(new MonitorDataEntity() { ID = "3", Name = "监测点-凯茵豪园", Value = 0.11});
            //list.Add(new MonitorDataEntity() { ID = "4", Name = "监测点-紫马岭公园", Value = 0.14});
            //list.Add(new MonitorDataEntity() { ID = "5", Name = "监测点-中山北站", Value = 0.09});
            //list.Add(new MonitorDataEntity() { ID = "6", Name = "监测点-中山站", Value = 0.02});
            //list.Add(new MonitorDataEntity() { ID = "7", Name = "监测点-中山公园", Value = 0.1});
            //list.Add(new MonitorDataEntity() { ID = "8", Name = "监测点-中山汽车总站", Value = 0.01});
            return Serialize(list);
        }

        public string Serialize(object o)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            json.Serialize(o, sb);
            return sb.ToString();
        }


    }
}