using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace EasyJoinService
{
    public partial class EasyJoinService : ServiceBase
    {
        IPAddress ip;
        IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);
        Socket SendSocket;
        Socket ReceiveSocket;
        Bll_Equipment bllEquipment;
        public EasyJoinService()
        {
            bllEquipment = new Bll_Equipment();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //point = new IPEndPoint(ip, 8080);//绑定端口号
            //ReceiveSocket.Bind(point);//绑定监听端口
            //                          //MessageBox.Show("TCP Server绑定成功");
            //ReceiveSocket.Listen(10);//等待连接是一个阻塞过程，创建线程来监听
            //Thread thReceive = new Thread(TSReceive);
            //thReceive.IsBackground = true;
            //thReceive.Start();

            System.Timers.Timer timer = new System.Timers.Timer(5000);
            timer.Elapsed += new ElapsedEventHandler(Send);
            timer.Interval = 5000;
            timer.AutoReset = true;
            //开始计时
            timer.Enabled = true;


        }

        private void Send(object source, ElapsedEventArgs e)
        {
            //byte[] buffer = strToToHexByte("01 04 00 01 00 01 60 0a");
            //SendSocket.SendTo(buffer, point);
            Random ran = new Random();
            double value = ran.NextDouble();
            bllEquipment.InsertEquipmentData("RS232/RS485 TO RJ45&WIFI CONVERTER", "01 03 02 FF F2 78 31", value.ToString());
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
            SendSocket = ReceiveSocket.Accept();//服务端监听到的Socket为服务端发送数据的目标socket
            byte[] buffer = new byte[1024];
            while (true)
            {
                try
                {
                    string receiveString = string.Empty;
                    int r = SendSocket.Receive(buffer);//接收到监听的Socket的数据
                    if (r == 0)
                    {
                        System.Console.WriteLine("连接断开");
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

                    //string strRec = Encoding.Default.GetString(buffer, 0, r);
                    System.Console.WriteLine("【" + SendSocket.RemoteEndPoint.ToString() + "】" + receiveString );
                }
                catch
                { }
            }
        }

        protected override void OnStop()
        {
            //SendSocket.Close();
            //ReceiveSocket.Close();
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
    }
}
