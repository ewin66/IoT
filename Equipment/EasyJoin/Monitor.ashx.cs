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
    /// Monitor 的摘要说明
    /// </summary>
    public class Monitor : IHttpHandler
    {
        private Bll_Equipment bll;
        ReturnValue resoult;
        MonitorEntity monitor;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(GetMonitorListJosin());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetMonitorListJosin()
        {
            List<MonitorEntity> list = new List<MonitorEntity>();

            bll = new Bll_Equipment();
            resoult = bll.GetEquipmentList();
            if (resoult.NoError)
            {
                for (int i = 0; i < resoult.Count; i++)
                {
                    monitor = new MonitorEntity();
                    monitor.ID = resoult.ResultDataSet.Tables[0].Rows[i]["ID"].ToString();
                    monitor.Name = resoult.ResultDataSet.Tables[0].Rows[i]["EName"].ToString();
                    monitor.POSITION = resoult.ResultDataSet.Tables[0].Rows[i]["POSITION"].ToString();
                    monitor.JOIN_TIME = resoult.ResultDataSet.Tables[0].Rows[i]["JOIN_TIME"].ToString();
                    monitor.JOINER = resoult.ResultDataSet.Tables[0].Rows[i]["JOINER"].ToString();
                    monitor.Long = resoult.ResultDataSet.Tables[0].Rows[i]["LONGITUDE"].ToString();
                    monitor.Lat = resoult.ResultDataSet.Tables[0].Rows[i]["LATITUDE"].ToString();
                    list.Add(monitor);
                }
            }
            else
            {

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
    }
}