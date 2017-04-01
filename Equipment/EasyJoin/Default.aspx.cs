using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using System.Timers;

namespace EasyJoin
{
   public partial class Default : System.Web.UI.Page
    { 
        Business.Parking bllParking;
        ReturnValue resoult;
        List<ParkingButton> list;
        Dictionary<string, ParkingButton> DictionaryParking;
        System.Timers.Timer timer = new System.Timers.Timer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialParkingList();
            }
        }
        private void InitialParkingList()
        {
            list = new List<ParkingButton>();
            DictionaryParking = new Dictionary<string, ParkingButton>();
            bllParking = new Business.Parking();
            resoult = bllParking.GetParkingState("SMDS001");
            if (resoult.NoError && resoult.Count > 0)
            {
                string toolTip = string.Empty;
                DataTable tbparkingList = resoult.ResultDataSet.Tables[0];
                for (int i = 0; i < tbparkingList.Rows.Count; i++)
                {
                    DateTime time = tbparkingList.Rows[i]["ChangeTime"].ToString() == "" ? DateTime.Parse(tbparkingList.Rows[i]["UPDATETIME"].ToString()) : DateTime.Parse(tbparkingList.Rows[i]["ChangeTime"].ToString());
                    TimeSpan timeSpan = DateTime.Now.Subtract(time);
                    ParkingButton btn = new ParkingButton();
                    list.Add(btn);
                    DictionaryParking.Add(tbparkingList.Rows[i]["WPSD_ID"].ToString(), btn);
                    btn.ID = tbparkingList.Rows[i]["WPSD_ID"].ToString();
                    btn.Text = tbparkingList.Rows[i]["PARKINGNAME"].ToString();
                    toolTip = "传感器编号:" + tbparkingList.Rows[i]["WPSD_ID"].ToString() + @"
信号强度:"
                        + tbparkingList.Rows[i]["RSSI"].ToString() + @"
电池电压:"
                        + tbparkingList.Rows[i]["Battery"].ToString()+@"
最后接收信号时间:"
                        + tbparkingList.Rows[i]["UPDATETIME"].ToString() + @"
当前状态维持:"
                        + string.Format("{0}天{1}小时{2}分钟{3}秒", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    btn.ToolTip = toolTip;
                    btn.Height = int.Parse(tbparkingList.Rows[i]["WEBHEIGHT"].ToString());
                    btn.Width = int.Parse(tbparkingList.Rows[i]["WEBWIDTH"].ToString());
                    btn.Style.Value = string.Format("position: relative; top:{0}px; left:{1}px;readonly=true;"
                        , tbparkingList.Rows[i]["WEBTOP"].ToString()
                        , tbparkingList.Rows[i]["WEBLEFT"].ToString());
                    btn.OnClientClick = "return false";
                    btn.State = tbparkingList.Rows[i]["STATE"].ToString();
                    divMain.Controls.Add(btn);
                }
                DataBind();
            }
        }
    }
}