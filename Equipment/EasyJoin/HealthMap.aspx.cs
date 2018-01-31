using System;
using EasyJoin.Class;
using Newtonsoft.Json;
using System.Web.UI;
using System.Timers;

namespace EasyJoin
{
    public partial class HealthMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(SetMarker);
            timer.Interval = 1000;// 设置引发时间的时间间隔,此处设置为１秒
            timer.Enabled = true;
            timer.AutoReset = true;
            //if (!IsPostBack)
            //{
            //    string theIP = YMethod.GetHostAddress();
            //    string msg = YMethod.GetInfoByUrl(BaiduApiControl.GetIPAPIUrl(theIP));
            //    msg = System.Text.RegularExpressions.Regex.Unescape(msg);
            //    BaiduApiStatus info = JsonConvert.DeserializeObject<BaiduApiStatus>(msg);
            //    if (info.status.Equals("0"))
            //    {
            //        BaiduApiLocation baiduApiLocation = JsonConvert.DeserializeObject<BaiduApiLocation>(msg);
            //        hf_X.Value = baiduApiLocation.content.point.x;
            //        hf_Y.Value = baiduApiLocation.content.point.y;
            //    }
            //    else if (info.status.Equals("1"))
            //    {
            //        BaiduApiMessage baiduApiMessage = JsonConvert.DeserializeObject<BaiduApiMessage>(msg);
            //        //show_html.InnerHtml = baiduApiMessage.message;
            //    }
            //}
        }





        private void SetMarker(object sender, ElapsedEventArgs e)
        {
            int i = System.DateTime.Now.Second;
            if (i < 10 || (i > 20 && i < 30) || (i > 40 && i < 50))
            {
                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "setMarkerAnimation();", true);
                //Button1_Click(null,null);
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>setMarkerAnimation();</script>", true);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(), "", "CancelMarkerAnimation();", true);
                //Button2_Click(null,null);
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>CancelMarkerAnimation();</script>", true);
            }
        }

        public string Getstr()
        {
            string aa = System.DateTime.Now.Second.ToString();
            return aa;
        }

        protected void btnFindPassWord_Click(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

        }
    }
}