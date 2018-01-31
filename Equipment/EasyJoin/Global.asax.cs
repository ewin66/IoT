using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace EasyJoin
{
    public class Global : System.Web.HttpApplication
    {        
        public static System.Timers.Timer timer;
        public static Thread threadWatch;
        public static IPEndPoint remoteEP;
        public static Socket sokClient;
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            remoteEP = new IPEndPoint(IPAddress.Parse("183.238.88.243"), 10106);
            sokClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sokClient.Connect(remoteEP);
            Byte[] bytSend = CommonMethod.strToToHexByte("C001");//注册
            Thread thr = new Thread(RecMsg);
            thr.IsBackground = true;
            thr.Start();
            sokClient.Send(bytSend);
            bytSend = CommonMethod.strToToHexByte("11");//刷新
            sokClient.Send(bytSend);
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += new ElapsedEventHandler(RefreshState);
            timer.AutoReset = true;
            timer.Start();
        }

        private void RefreshState(object source, ElapsedEventArgs e)
        {
            try
            {
                if (sokClient.Connected)
                {
                    Byte[] bytSend = CommonMethod.strToToHexByte("11");
                    sokClient.Send(bytSend);
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
            }
        }

        void RecMsg()
        {
            while (true)
            {
                // 定义一个缓存区；
                byte[] arrMsgRec = new byte[1024];
                // 将接受到的数据存入到输入  arrMsgRec中；
                int length = -1;
                try
                {
                    length = sokClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度；
                    if (length > 0)
                    {
                        //主业务                        
                        List<byte> listbyte = new List<byte>();
                        while (true)
                        {
                            //将接受到的数据存入arrMsgRec数组,并返回真正接收到的数据长度  

                            if (length > arrMsgRec.Length)
                            {
                                listbyte.AddRange(arrMsgRec);
                            }
                            else
                            {
                                for (int i = 0; i < length; i++)
                                    listbyte.Add(arrMsgRec[i]);
                                break;
                            }
                        }
                        //创建内存流
                        using (MemoryStream stream = new MemoryStream(listbyte.ToArray()))
                        {
                            byte[] b = stream.ToArray();
                            //string s = System.Text.Encoding.UTF8.GetString(b, 0, b.Length);
                            //WriteLog(s);
                            string s = CommonMethod.byteToHexStr(b);
                            //00 闭锁状态，01 开锁状态， 02 - 中间状态0~90°03 - 中间状态90~180°，88 运动状态
                            if (s == "00")
                            {
                                Application.Lock();
                                Application["name"] = "UnLock";
                                Application.UnLock();
                            }
                            else if (s == "01")
                            {
                                Application.Lock();
                                Application["name"] = "Lock";
                                Application.UnLock();
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch (SocketException se)
                {
                    Session["Error"] = se.ToString();
                    break;
                }
                catch (Exception e)
                {
                    Session["Error"] = e.ToString();
                    break;
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}