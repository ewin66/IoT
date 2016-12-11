using Business;
using Common;
using Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EasyJoin
{
    public partial class MonitorListManage : System.Web.UI.Page
    {
        private Bll_Equipment bll;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridData();
            }
        }

        private void BindGridData()
        {
            List<MonitorEntity> list = new List<MonitorEntity>();
            bll = new Bll_Equipment();
            ReturnValue resoult = bll.GetEquipmentList();
            if (resoult.NoError)
            {
                for (int i = 0; i < resoult.Count; i++)
                {
                    MonitorEntity monitor = new MonitorEntity();
                    monitor.ID = resoult.ResultDataSet.Tables[0].Rows[i]["ID"].ToString();
                    monitor.EQUIPMENT_TYPE_ID = resoult.ResultDataSet.Tables[0].Rows[i]["EQUIPMENT_TYPE_ID"].ToString();
                    monitor.EQUIPMENT_TYPE_NAME = resoult.ResultDataSet.Tables[0].Rows[i]["EQUIPMENT_TYPE_NAME"].ToString();
                    monitor.EQUIPMENT_MODEL_ID = resoult.ResultDataSet.Tables[0].Rows[i]["EQUIPMENT_MODEL_ID"].ToString();
                    monitor.EQUIPMENT_MODEL_NAME = resoult.ResultDataSet.Tables[0].Rows[i]["EQUIPMENT_MODEL_NAME"].ToString();
                    monitor.Name = resoult.ResultDataSet.Tables[0].Rows[i]["EName"].ToString();
                    monitor.POSITION = resoult.ResultDataSet.Tables[0].Rows[i]["POSITION"].ToString();
                    monitor.JOIN_TIME = resoult.ResultDataSet.Tables[0].Rows[i]["JOIN_TIME"].ToString();
                    monitor.JOINER = resoult.ResultDataSet.Tables[0].Rows[i]["JOINER"].ToString();
                    monitor.ADDRESS_NO = resoult.ResultDataSet.Tables[0].Rows[i]["ADDRESS_NO"].ToString();
                    monitor.STATE = resoult.ResultDataSet.Tables[0].Rows[i]["STATE"].ToString();
                    //monitor.IsAlarm = resoult.ResultDataSet.Tables[0].Rows[i]["IsAlarm"].ToString();
                    //monitor.Value = resoult.ResultDataSet.Tables[0].Rows[i]["Value"].ToString();
                    monitor.Long = resoult.ResultDataSet.Tables[0].Rows[i]["LONGITUDE"].ToString();
                    monitor.Lat = resoult.ResultDataSet.Tables[0].Rows[i]["LATITUDE"].ToString();
                    monitor.LatLong = monitor.Lat +"-"+ monitor.Long;
                    list.Add(monitor);
                }                
            }
            else
            {

            }
            this.gridViewMonitorList.DataSource = list;
            this.DataBind();
        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridViewRow row = gridViewMonitorList.Rows[Convert.ToInt32(e.CommandArgument)];
                string strKeyValue = row.Cells[0].Text;
                try
                {
                    //bool blFalg = bll.DeleteMember(Convert.ToInt32(strKeyValue));
                    //if (blFalg)
                    //{
                    //    //Response.Write("<script>alert('删除成功!');</script>");
                    //    BindGridData();
                    //}
                    //else
                    //{
                    //    //Response.Write("<script>alert('删除失败!');</script>");
                    //    //this.DataBind();
                    //}

                }
                catch
                {
                    //Response.Write("<script>alert('发生错误!');</script>");
                    //Response.Redirect("ErrorPage.aspx");
                    //this.DataBind();
                }
            }
            else if (e.CommandName == "Update")
            {
                GridViewRow row = gridViewMonitorList.Rows[Convert.ToInt32(e.CommandArgument)];
                string strKeyValue = row.Cells[0].Text;
                Response.Redirect("EasyJoin.aspx?ID=" + strKeyValue);
                return;
            }
        }

        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "javascript:style.backgroundColor='#F8F8F8';");
                e.Row.Attributes.Add("onmouseout", "javascript:this.style.backgroundColor='#ebf7fb';");
                e.Row.Attributes.Add("ondblclick", "javascript:window.location='EasyJoin.aspx?ID=" + e.Row.Cells[0].Text + "';");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("EasyJoin.aspx");
        }
    }
}