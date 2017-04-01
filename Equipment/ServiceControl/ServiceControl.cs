using Business;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace ServiceControl
{
    public partial class ServiceControl : Form
    {
        private EasyJoinServiceReference.EasyJoinServiceSoapClient easyJoinService;
        private delegate void myDelegate(string s);
        private string receiveString = string.Empty;
        IPAddress ip;
        IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);
        Socket SendSocket;
        Socket ReceiveSocket;
        Bll_Equipment equipmentData;
        System.Timers.Timer timer;
        System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
        Thread thReceive;
        string CommNo = "RS232/RS485 TO RJ45&WIFI CONVERTER";
        bool IsRuning = false;
        double OldLevel = -1;
        int DataCount = 1;
        AutoResetEvent autoEvent;
        SerialPort serialPort;
        private string PV = "01 03 00 00 00 01 84 0A";
        System.Windows.Forms.Timer timer485 = new System.Windows.Forms.Timer();
        public ServiceControl()
        {
            easyJoinService = new EasyJoinServiceReference.EasyJoinServiceSoapClient();
            InitializeComponent();
            GetPorts();
            

            equipmentData = new Bll_Equipment();

            GetAddressIP();

            timer = new System.Timers.Timer(3000);
            timer.Elapsed += new ElapsedEventHandler(Send);
            timer.AutoReset = true;

            tm.Interval = 1;
            tm.Tick += new EventHandler(tm_Tick);

            thReceive = new Thread(TSReceive);
            thReceive.IsBackground = true;

            autoEvent = new AutoResetEvent(false);


            timer485.Interval = 10000;
            timer485.Tick += Timer485_Tick;
            btnOpenport.Text = "打开端口";
            btnOpenport.BackColor = Color.Red;
        }

        private void Timer485_Tick(object sender, EventArgs e)
        {
            Send485(PV);
        }

        private void Send485(string message)
        {
            string msg = "";
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("请打开端口！");
                return;
            }
            Byte[] bt = strToToHexByte(message);

            try
            {
                serialPort.Write(bt, 0, bt.Length);
                msg = "发送请求指令:"+message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            WriteLog(msg);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            myDelegate md = new myDelegate(GetDate);
            try
            {
                if (serialPort.IsOpen == true)
                {              

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

        private void GetDate(string s)
        {
            WriteLog("收到数据:" + receiveString);
            receiveString = string.Empty;
            double level = GetLevelData(s);
            if (OldLevel != level)
            {
                if (ckbClearData.Checked && DataCount > 100)
                {
                    //equipmentData.DeleteEquipmentData(CommNo);
                    easyJoinService.DeleteEquipmentData(CommNo);
                    WriteLog("自动清理数据..................");
                    DataCount = 1;
                }
                DataCount++;
                //equipmentData.InsertEquipmentData(CommNo, receiveString, level.ToString());
                easyJoinService.InsertEquipmentData(CommNo, s, level.ToString());
                OldLevel = level;
            }
        }

        void tm_Tick(object sender, EventArgs e)
        {
            autoEvent.Set(); //通知阻塞的线程继续执行  
        }

        private void listLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listLog.Items.Clear();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            CommNo = txtComm.Text;
            if (rdbNet.Checked)
            {
                if (IsRuning) return;
                StartServer(8080);
                IsRuning = true;                
            }
            else if (rdb485.Checked)
            {
                if (!serialPort.IsOpen)
                {
                    MessageBox.Show("请打开端口！");
                    return;
                }
                if (btnStart.Text == "启动")
                {
                    WriteLog("启动....");
                    timer485.Start();
                    btnStart.Text = "停止";
                }
                else if (btnStart.Text == "停止")
                {
                    WriteLog("停止....");
                    timer485.Stop();
                    btnStart.Text = "启动";
                }
            }
        }

        /// <summary>
        /// 获取系统端口
        /// </summary>
        private void GetPorts()
        {
            foreach (string vPortName in SerialPort.GetPortNames())
            {
                cmbPort.Items.Add(vPortName);
                if (cmbPort.Items.Count > 0)
                {
                    cmbPort.SelectedIndex = 0;
                }
            }
        }

        private void StartServer(int port)
        {
            try
            {
                ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                WriteLog("实例化Socket");
                point = new IPEndPoint(ip, port);//绑定端口号
                WriteLog("绑定端口号" + ip.ToString() + ":" + port.ToString());
                ReceiveSocket.Bind(point);//绑定监听端口
                WriteLog("绑定监听端口");
                ReceiveSocket.Listen(10);//等待连接是一个阻塞过程，创建线程来监听
                SendSocket = ReceiveSocket.Accept();//服务端监听到的Socket为服务端发送数据的目标socket
                if (thReceive.ThreadState.HasFlag(ThreadState.Unstarted))
                {
                    WriteLog("开始启动接收线程");
                    thReceive.Start();
                    WriteLog("启动接收线程完成");
                }
                timer.Enabled = true;
                tm.Start();

                WriteLog("服务已启动...");
            }
            catch (Exception ex)
            {
                WriteLog("发生错误:"+ex.Message);
            }
        }

        private void Send(object source, ElapsedEventArgs e)
        {
            if (SendSocket == null) return;
            string msg = "";
            byte[] buffer = strToToHexByte("01 03 00 00 00 01 84 0A");
            try
            {
                SendSocket.SendTo(buffer, point);
                msg = "发送请求指令:01 03 00 00 00 01 84 0A";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            WriteLog(msg);
        }

        private void WriteLog(string msg)
        {
            if (!ckbMessage.Checked)
                return;
            string log = System.DateTime.Now.ToString("MM/dd HH:mm:ss") + "  " + msg;
            if (listLog.InvokeRequired)
            {                
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) => {
                    if (listLog.Items.Count > 100)
                        listLog.Items.RemoveAt(0);
                    listLog.Items.Add(log);
                };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                listLog.Invoke(actionDelegate, log);
            }
            else
            {
                if (listLog.Items.Count > 100)
                    listLog.Items.RemoveAt(0);
                listLog.Items.Add(log);
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

        void TSReceive()
        {            
            byte[] buffer = new byte[1024];
            while (true)
            {
                try
                {
                    string receiveString = string.Empty;
                    int r = SendSocket.Receive(buffer);//接收到监听的Socket的数据
                    if (r == 0)
                    {
                        WriteLog("连接断开..");
                        break;
                    }

                    for (int i = 0; i < r; i++)
                    {
                        int tempByte = buffer[i];
                        //string tempStr = "0X";
                        string tempStr = "";
                        if (tempByte <= 0X0F)
                        {
                            //tempStr = "0X0";
                            tempStr = "0";
                        }
                        receiveString += tempStr + Convert.ToString(tempByte, 16).ToUpper() + " ";
                    }
                    double level = GetLevelData(receiveString);
                    if (OldLevel != level)
                    {                        
                        if (ckbClearData.Checked && DataCount > 100)
                        {
                            equipmentData.DeleteEquipmentData(CommNo);
                            WriteLog("自動清理數據..................");
                            DataCount = 1;
                        }
                        DataCount++;
                        equipmentData.InsertEquipmentData(CommNo, receiveString, level.ToString());
                        OldLevel = level;                        
                    }
                    WriteLog("收到数据来自" + SendSocket.RemoteEndPoint.ToString() + "的数据:" + receiveString);
                }
                catch
                { }
                autoEvent.WaitOne();
            }
        }

        private double GetLevelData(string s)
        {
            string level1 = s.Substring(8, 3).Trim();
            string level2 = s.Substring(12, 3).Trim();
            double level = Convert.ToInt32(level1, 16) + Convert.ToInt32(level2, 16) / (double)100;
            return level;
        }

        string GetAddressIP()
        {
            string AddressIP = "";
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    ip = _IPAddress;//设定全局的IP
                }
            }
            return AddressIP;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!IsRuning) return;
            tm.Stop();
            timer.Enabled = false;
            SendSocket.Close();
            ReceiveSocket.Close();
            WriteLog("服务停止.");
            IsRuning = false;
        }

        private void btnOpenport_Click(object sender, EventArgs e)
        {
            if (serialPort == null)
            {
                serialPort = new SerialPort(cmbPort.SelectedItem.ToString(), 9600, Parity.None, 8, StopBits.One);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            }
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    WriteLog("关闭端口......");
                    btnOpenport.Text = "打开端口";
                    btnOpenport.BackColor = Color.Red;
                    timer.Stop();
                    btnStart.Text = "启动";
                }
                else
                {
                    WriteLog("开打端口......");
                    serialPort.Open();
                    btnOpenport.Text = "关闭端口";
                    btnOpenport.BackColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }
    }
}
