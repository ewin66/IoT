using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Entity
{
    public class BaiduApiControl
    {
        /*IP 定位 API地址： 
         * http://developer.baidu.com/map/index.php?title=webapi/ip-api#.E4.BD.BF.E7.94.A8.E6.96.B9.E6.B3.95
        */

        private const string THE_KEY = "KGEgH0KtySkG09bItE7MGLGn0npqjEEc"; //百度访问应用（AK）
        /// <summary>返回UTF-8编码服务地址</summary>
        /// <returns>服务地址</returns>
        public static string GetIPAPIUrl(string theIP)
        {
            string postUrl = "http://api.map.baidu.com/location/ip?ak=" + THE_KEY + "&ip=" + theIP + "&coor=bd09ll";
            return postUrl;
        }
    }
}