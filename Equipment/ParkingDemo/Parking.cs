using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using Common;

namespace ParkingDemo
{
    public partial class Parking : Form
    {
        Business.Parking bllParking;
        Dictionary<string, ParkingButton> DictionaryParking;
        ReturnValue resoult;
        Timer timer = new Timer();
        private Point location;
        private Button btn;
        List<ParkingButton> list;

        public Parking()
        {
            bllParking = new Business.Parking();
            DictionaryParking = new Dictionary<string, ParkingButton>();
            list = new List<ParkingButton>();
            InitializeComponent();
            InitialParkingList();
            timer.Interval = 10000;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GetParkingState();
        }

        private void GetParkingState()
        {
            resoult = bllParking.GetParkingState("SMDS001");
            if (resoult.NoError && resoult.Count > 0)
            {
                DataTable tbparking = resoult.ResultDataSet.Tables[0];
                for (int i = 0; i < tbparking.Rows.Count; i++)
                {
                    if (DictionaryParking.Keys.Contains(tbparking.Rows[i]["WPSD_ID"].ToString()))
                    {
                        DictionaryParking[tbparking.Rows[i]["WPSD_ID"].ToString()].State = tbparking.Rows[i]["STATE"].ToString();
                    }
                }
            }
        }        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (ParkingButton b in list)
            {
                if (b.Text == txtParkingName.Text)
                {
                    MessageBox.Show("车位号已经存在！");
                    return;
                }
                if (b.Tag.ToString() == txtWPSD_ID.Text)
                {
                    MessageBox.Show("该设备已经绑定！");
                    return;
                }
            }
            ParkingButton btn = new ParkingButton();
            list.Add(btn);
            DictionaryParking.Add(txtWPSD_ID.Text, btn);
            btn.Text = txtParkingName.Text;
            btn.Tag = txtWPSD_ID.Text;
            btn.Height =int.Parse(txtHeight.Text);
            btn.Width = int.Parse(txtWidth.Text);
            this.toolTip1.SetToolTip(btn, txtWPSD_ID.Text);
            this.Controls.Add(btn);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool bl = true;
            foreach (ParkingButton b in list)
            {
                bl=bllParking.AddorUpdateParking("SMDS001",b.Tag.ToString(), b.Text, b.Height, b.Width, b.Location.X, b.Location.Y);
                if (!bl)
                {
                    MessageBox.Show(string.Format("保存{0}失败",b.Text));
                    return;
                }
            }
            if (bl)
            {
                MessageBox.Show("保存成功！");
            }
        }

        private void InitialParkingList()
        {
            resoult = bllParking.GetParkingState("SMDS001");
            if (resoult.NoError && resoult.Count > 0)
            {
                DataTable tbparkingList = resoult.ResultDataSet.Tables[0];
                Point point = new Point();
                for (int i = 0; i < tbparkingList.Rows.Count; i++)
                {
                    ParkingButton btn = new ParkingButton();
                    list.Add(btn);
                    DictionaryParking.Add(tbparkingList.Rows[i]["WPSD_ID"].ToString(), btn);
                    btn.Text = tbparkingList.Rows[i]["PARKINGNAME"].ToString();
                    btn.Tag = tbparkingList.Rows[i]["WPSD_ID"].ToString();
                    this.toolTip1.SetToolTip(btn, tbparkingList.Rows[i]["WPSD_ID"].ToString());
                    btn.Height = int.Parse(tbparkingList.Rows[i]["HEIGHT"].ToString());
                    btn.Width = int.Parse(tbparkingList.Rows[i]["WIDTH"].ToString());
                    point.X = int.Parse(tbparkingList.Rows[i]["LOCATIONX"].ToString());
                    point.Y = int.Parse(tbparkingList.Rows[i]["LOCATIONY"].ToString());
                    btn.Location = point;
                    this.Controls.Add(btn);
                }
            }
            timer.Start();//开始刷新状态
        }
    }
}
