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
    public partial class EasyJoin : System.Web.UI.Page
    {
        private Bll_Equipment bll;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InintPage();
                if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
                {
                    lbMessage.Text = "";
                    txtID.Text = Request.QueryString["ID"].ToString();
                    bll = new Bll_Equipment();
                    ReturnValue resoult = bll.GetEquipmentInfo(txtID.Text);
                    if (resoult.NoError && resoult.Count > 0)
                    {
                        string TypeID = resoult.ResultDataSet.Tables[0].Rows[0]["EQUIPMENT_TYPE_ID"].ToString();
                        for (int i = 0; i < ddListEQIPMENT_TYPE.Items.Count; i++)
                        {
                            if (ddListEQIPMENT_TYPE.Items[i].Value == TypeID)
                            {
                                ddListEQIPMENT_TYPE.SelectedIndex = i;
                                BindModelType();
                                break;
                            }
                        }

                        string ModelID = resoult.ResultDataSet.Tables[0].Rows[0]["EQUIPMENT_MODEL_ID"].ToString();
                        for (int j = 0; j < ddListEQIPMENT_MODEL.Items.Count; j++)
                        {
                            if (ddListEQIPMENT_MODEL.Items[j].Value == ModelID)
                            {
                                ddListEQIPMENT_MODEL.SelectedIndex = j;
                                break;
                            }
                        }
                        txtName.Text = resoult.ResultDataSet.Tables[0].Rows[0]["EName"].ToString();
                        txtPOSITION.Text = resoult.ResultDataSet.Tables[0].Rows[0]["POSITION"].ToString();
                        lbJoinTime.Text = "接入时间："+resoult.ResultDataSet.Tables[0].Rows[0]["JOIN_TIME"].ToString();
                        txtJOINER.Text = resoult.ResultDataSet.Tables[0].Rows[0]["JOINER"].ToString();
                        txtADDRESS_NO.Text = resoult.ResultDataSet.Tables[0].Rows[0]["ADDRESS_NO"].ToString();
                        if (resoult.ResultDataSet.Tables[0].Rows[0]["STATE"].ToString() == "1")
                        {
                            rdBtnState1.Checked = true;
                        }
                        else if (resoult.ResultDataSet.Tables[0].Rows[0]["STATE"].ToString() == "2")
                        {
                            rdBtnState2.Checked = true;
                        }
                        txtLONGITUDE.Text = resoult.ResultDataSet.Tables[0].Rows[0]["LONGITUDE"].ToString();
                        txtLATITUDE.Text = resoult.ResultDataSet.Tables[0].Rows[0]["LATITUDE"].ToString();
                        this.DataBind();
                    }
                    else
                    {
                        lbMessage.Text = resoult.ErrorID;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lbMessage.Text = "";
            bll = new Bll_Equipment();
            MonitorEntity entity = new MonitorEntity();
            entity.ID = txtID.Text;
            entity.Name = txtName.Text;
            entity.EQUIPMENT_TYPE_ID = ddListEQIPMENT_TYPE.SelectedValue;
            entity.EQUIPMENT_MODEL_ID = ddListEQIPMENT_MODEL.SelectedValue;
            entity.COMMUNICATION_NO = txtCOMMUNICATION_NO.Text;
            entity.Lat = txtLATITUDE.Text;
            entity.Long = txtLONGITUDE.Text;
            entity.POSITION = txtPOSITION.Text;
            entity.JOIN_TIME = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            entity.JOINER = txtJOINER.Text;
            entity.ADDRESS_NO = txtADDRESS_NO.Text;
            if (rdBtnState1.Checked)
            {
                entity.STATE = "1";
            }
            else if (rdBtnState2.Checked)
            {
                entity.STATE = "2";
            }
            entity.Create_ID = "1";
            entity.Updata_ID = "1";
            string resoult = bll.AddorUpdate(entity);
            if (resoult=="0")
            {
                Response.Redirect("MonitorListManage.aspx");
            }
            else
            {
                lbMessage.Text = resoult;
            }
        }

        private void InintPage()
        {
            bll = new Bll_Equipment();
            ReturnValue resoult = bll.GetEquipmentTypeList();
            if (resoult.NoError && resoult.Count > 0)
            {
                ddListEQIPMENT_TYPE.DataSource = resoult.ResultDataSet.Tables[0];
                ddListEQIPMENT_TYPE.DataTextField = "EQUIPMENT_TYPE_NAME";
                ddListEQIPMENT_TYPE.DataValueField = "ID";
                ddListEQIPMENT_TYPE.DataBind();
                ddListEQIPMENT_TYPE.SelectedIndex = 0;
            }
            else
            {
                lbMessage.Text = resoult.ErrorID;
            }
        }

        protected void ddListEQIPMENT_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindModelType();
        }

        private void BindModelType()
        {
            bll = new Bll_Equipment();
            ReturnValue resoult = bll.GetEquipmentModelList(ddListEQIPMENT_TYPE.SelectedValue);
            if (resoult.NoError && resoult.Count > 0)
            {
                ddListEQIPMENT_MODEL.DataSource = resoult.ResultDataSet.Tables[0];
                ddListEQIPMENT_MODEL.DataTextField = "EQUIPMENT_MODEL_NAME";
                ddListEQIPMENT_MODEL.DataValueField = "ID";
                ddListEQIPMENT_MODEL.DataBind();
                ddListEQIPMENT_MODEL.SelectedIndex = 0;
            }
            else
            {
                lbMessage.Text = resoult.ErrorID;
            }
        }
    }
}