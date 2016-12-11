using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ServiceControl
{
    public partial class ServiceControl : Form
    {
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
        public ServiceControl()
        {
            InitializeComponent();

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
            if (IsRuning) return;
            StartServer(8080);
            IsRuning = true;
            CommNo = txtComm.Text;
        }

        private void StartServer(int port)
        {
            ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            point = new IPEndPoint(ip, port);//绑定端口号
            ReceiveSocket.Bind(point);//绑定监听端口
            
            ReceiveSocket.Listen(10);//等待连接是一个阻塞过程，创建线程来监听
            SendSocket = ReceiveSocket.Accept();//服务端监听到的Socket为服务端发送数据的目标socket
            if (thReceive.ThreadState.HasFlag(ThreadState.Unstarted))
            {
                thReceive.Start();
            }
            timer.Enabled = true;
            tm.Start();

            WriteLog("服务已启动...");
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
    }
}
