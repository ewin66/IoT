using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace EasyJoin
{
    /// <summary>
    /// AlamInfo 的摘要说明
    /// </summary>
    public class AlamInfo : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(GetData());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GetData()
        {
            Business.WaterLevel waterLevel = new Business.WaterLevel();
            return waterLevel.GetLatestData("RS232/RS485 TO RJ45&WIFI CONVERTER");
        }       
    }
}