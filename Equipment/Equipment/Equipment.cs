using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Equipment
{
    public partial class Equipment : Form
    {
        private delegate void myDelegate(string s);
        SerialPort serialPort;
        private string receiveString = string.Empty;
        private DataTable WarterLevelDataSource;
        Timer timer = new Timer();
        public Equipment()
        {
            InitializeComponent();
            serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            InitiData();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            btnOpenPort.Text = "打开端口";
            btnOpenPort.BackColor = Color.Red;
            WarterLevelDataSource = new DataTable();
            DataColumn colTime = new DataColumn("Time");
            DataColumn colLevel = new DataColumn("Level");
            WarterLevelDataSource.Columns.Add(colTime);
            WarterLevelDataSource.Columns.Add(colLevel);
            for (int i = 1; i < 13; i++)
            {
                DataRow row = WarterLevelDataSource.NewRow();
                row["Time"] = "#";
                row["Level"] = 0;
                WarterLevelDataSource.Rows.Add(row);
            }

            chartWaterLevel.Series.Clear();
            chartWaterLevel.DataSource = WarterLevelDataSource;
            Series series = new Series("水位");
            series.ChartType = SeriesChartType.Spline;
            series.Color = Color.Red;
            series.BorderWidth = 3;
            chartWaterLevel.Series.Add(series);
            series.XValueMember = "Time";
            series.YValueMembers = "Level";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Send(txtSend.Text);
        }

        private void InitiData()
        {
            //PV值 01 03 00 00 00 01 84 0A
            cobSendSelect.Items.Add("PV[水位]-01 03 00 00 00 01 84 0A");
            //PUH值01 03 02 0C 00 02 05 B0
            cobSendSelect.Items.Add("PUH[上限]-01 03 02 0C 00 02 05 B0");
            cobSendSelect.SelectedIndex = 0;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
            listReceive.Items.Clear();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send(txtSend.Text);
        }

        private void Send(string message)
        {
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("请打开端口！");
                return;
            }
            Byte[] bt = strToToHexByte(message);
            if (rdBtnUSBRS485.Checked)
            {
                serialPort.Write(bt, 0, bt.Length);
            }
            else if (rdBtnNetRS485.Checked)
            {

            }
        }

        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            myDelegate md = new myDelegate(SetText);
            try
            {
                if (serialPort.IsOpen == true)
                {
                    //byte[] readBuffer = new byte[serialPort.ReadBufferSize];
                    //string readstr = byteToHexStr(readBuffer);
                    //Invoke(md, readstr);

                    //serialPort.Read(readBuffer, 0, readBuffer.Length);
                    //string readstr = Encoding.UTF8.GetString(readBuffer);
                    //Invoke(md, readstr);                    

                    int size = serialPort.BytesToRead;                    
                    for (int i = 0; i < size; i++)
                    {
                        int tempByte = serialPort.ReadByte();
                        //string tempStr = "0X";
                        string tempStr = "";
                        if (tempByte <= 0X0F)
                        {
                            //tempStr = "0X0";
                            tempStr = "0";
                        }
                        receiveString += tempStr + Convert.ToString(tempByte, 16).ToUpper() + " ";
                    }
                }
                if (receiveString.Length >= 20)
                {
                    Invoke(md, receiveString);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        //字节数组转16进制字符串
        private string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        private void SetText(string s)
        {
            listReceive.Items.Add(s);
            receiveString = string.Empty;

            GetLevelData(s);
            
            chartWaterLevel.DataBind();
        }

        private void GetLevelData(string s)
        {
            string time = DateTime.Now.Second.ToString();
            string level1 = s.Substring(8,3).Trim();
            string level2 = s.Substring(12, 3).Trim();
            double level = Convert.ToInt32(level1, 16)+ Convert.ToInt32(level2, 16)/(double)100;
            SetTableData(time, level);
        }

        private void SetTableData(string time, double level)
        {
            DataRow row = WarterLevelDataSource.NewRow();
            row["Time"] = time;
            row["Level"] = level;
            WarterLevelDataSource.Rows.Add(row);
            if (WarterLevelDataSource.Rows.Count > 12)
            {
                WarterLevelDataSource.Rows.RemoveAt(0);
            }
        }

        private void cobSendSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sendText = cobSendSelect.SelectedItem.ToString();
            txtSend.Text = sendText.Substring(sendText.IndexOf("-")+1);
        }

        private void btnAutomatic_Click(object sender, EventArgs e)
        {
            if (btnAutomatic.Text == "Automatic")
            {
                timer.Start();
                btnAutomatic.Text = "Stop";
            }
            else if(btnAutomatic.Text == "Stop")
            {
                timer.Stop();
                btnAutomatic.Text = "Automatic";
            }
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                btnOpenPort.Text = "打开端口";
                btnOpenPort.BackColor = Color.Red;
                timer.Stop();
                btnAutomatic.Text = "Automatic";
            }
            else
            {
                serialPort.Open();
                btnOpenPort.Text = "关闭端口";
                btnOpenPort.BackColor = Color.Green;
            }
        }

        private void btnTestMySql_Click(object sender, EventArgs e)
        {
            Bll_Equipment waterLevel = new Bll_Equipment();
            string strResoult = waterLevel.InsertEquipmentData("RS232/RS485 TO RJ45&WIFI CONVERTER", "01 03 02 FF F2 78 31","0.21");
            if ("0"==strResoult)
            {
                MessageBox.Show("插入成功！");
            }
            else
            {
                MessageBox.Show(strResoult);
            }
        }
    }
}
