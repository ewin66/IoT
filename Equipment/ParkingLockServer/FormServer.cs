using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ParkingLockServer
{
    public partial class FormServer : Form
    {
        IPAddress ip;
        IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);
        Socket webSocket;
        Socket dtuSocket;
        Thread thReceive;
       
        bool IsRuning = false;
        /// <summary>
        /// 负责监听客户端连接请求的线程
        /// </summary>
        Thread threadWatch = null;
        /// <summary>
        /// 负责监听的套接字
        /// </summary>
        Socket socketWatch = null;
        Dictionary<string, Socket> listSocket = new Dictionary<string, Socket>();//存放套接字
        Dictionary<string, Thread> listThread = new Dictionary<string, Thread>();//存放线程
        public FormServer()
        {
            InitializeComponent();
            txtServerIP.Text = GetAddressIP();
            txtPort.Text = "10106";
            
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
        string GetAddressIP()
        {            
            string AddressIP = "";           
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {   
                    AddressIP = _IPAddress.ToString();
                    comboxIP.Items.Add(AddressIP);
                    ip = _IPAddress;//设定全局的IP
                }
            }
            return AddressIP;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (IsRuning) return;            
            // 创建负责监听的套接字，注意其中的参数；
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 获得文本框中的IP对象；
            IPAddress address = IPAddress.Parse(txtServerIP.Text.Trim());
            // 创建包含ip和端口号的网络节点对象；
            IPEndPoint endPoint = new IPEndPoint(address, int.Parse(txtPort.Text.Trim()));
            try
            {
                // 将负责监听的套接字绑定到唯一的ip和端口上；
                socketWatch.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                socketWatch.Bind(endPoint);
            }
            catch (SocketException se)
            {
                WriteLog("异常：" + se.Message);
                return;
            }
            // 设置监听队列的长度；
            socketWatch.Listen(10000);
            // 创建负责监听的线程；
            threadWatch = new Thread(WatchConnecting);
            threadWatch.IsBackground = true;
            threadWatch.Start();
            WriteLog("服务器启动监听成功！");
            IsRuning = true;
        }

        /// <summary>
        /// 监听客户端请求的方法；
        /// </summary>
        void WatchConnecting()
        {
            //WriteLog("新客户端连接成功！");
            while (true)  // 持续不断的监听客户端的连接请求；
            {
                // 开始监听客户端连接请求，Accept方法会阻断当前的线程；
                Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；
                var ssss = sokConnection.RemoteEndPoint.ToString().Split(':');
                //查找ListBox集合中是否包含此IP开头的项，找到为0，找不到为-1
                if (listConnection.FindString(ssss[0]) >= 0)
                {
                    RemoveList(listConnection,sokConnection.RemoteEndPoint.ToString());
                }

                AddList(listConnection,sokConnection.RemoteEndPoint.ToString());
                WriteLog(sokConnection.RemoteEndPoint.ToString()+" 连接成功！");

                // 将与客户端连接的 套接字 对象添加到集合中；
                listSocket.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);
                Thread thr = new Thread(RecMsg);
                thr.IsBackground = true;
                thr.Start(sokConnection);
                listThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程 添加 到线程的集合中去。
            }
        }
        private void AddList(ListBox list, string conncetcion)
        {
            if (list.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) => {
                    list.Items.Add(conncetcion);
                };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                list.Invoke(actionDelegate, conncetcion);
            }
            else
            {
                list.Items.Add(conncetcion);
            }
        }

        private void RemoveList(ListBox list, string conncetcion)
        {
            if (list.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) => {
                    list.Items.Remove(conncetcion);
                };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                list.Invoke(actionDelegate, conncetcion);
            }
            else
            {
                list.Items.Remove(conncetcion);
            }
        }

        void RecMsg(object sokConnectionparn)
        {
            Socket sokClient = sokConnectionparn as Socket;
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
                            //创建以二进制格式对对象进行序列化和反序列化
                            //BinaryFormatter bf = new BinaryFormatter();
                            //WriteLog("m.length" + m.ToArray().Length);
                            ////反序列化
                            //object dataObj = bf.Deserialize(m);
                            ////得到解析后的实体对象
                            //Student dt = dataObj as Student;
                            //WriteLog("接收客户端长度:" + listbyte.Count + " 反序列化结果是：ID:" + dt.ID +
                            //    " 姓名:" + dt.Name + " 当前时间:" + dt.Now_Time);

                            byte[] b = stream.ToArray();
                            //string s = System.Text.Encoding.UTF8.GetString(b, 0, b.Length);
                            //WriteLog(s);
                            string s = byteToHexStr(b);
                            Byte[] bytSend;
                            if (s == "ABC001")
                            {
                                dtuSocket = sokClient;
                            }
                            else if (s == "C001")
                            {
                                webSocket = sokClient;
                            }
                            else if (s == "11")//查询
                            {
                                if (dtuSocket.Connected)
                                {
                                    bytSend = strToToHexByte("55 91 01 06 19 AA");
                                    dtuSocket.Send(bytSend);
                                }
                            }
                            //00 闭锁状态，01 开锁状态
                            else if (s == "5A91020600E5AA")//闭锁状态
                            {
                                if (webSocket.Connected)
                                {
                                    bytSend = strToToHexByte("00");
                                    webSocket.Send(bytSend);
                                }
                            }
                            else if (s == "5A91020601BBAA")//开锁状态
                            {
                                if (webSocket.Connected)
                                {
                                    bytSend = strToToHexByte("01");
                                    webSocket.Send(bytSend);
                                }
                            }
                            else if (s == "00")//闭锁
                            {
                                if (dtuSocket.Connected)
                                {
                                    bytSend = strToToHexByte("55 91 01 02 78 AA");
                                    dtuSocket.Send(bytSend);
                                }
                            }
                            else if (s == "01")//解锁
                            {
                                if (dtuSocket.Connected)
                                {
                                    bytSend = strToToHexByte("55 91 01 01 9A AA");
                                    dtuSocket.Send(bytSend);
                                }
                            }
                            WriteLog(s);
                        }

                    }
                    else
                    {
                        // 从 通信套接字 集合中删除被中断连接的通信套接字;
                        listSocket.Remove(sokClient.RemoteEndPoint.ToString());
                        // 从通信线程集合中删除被中断连接的通信线程对象;
                        listThread.Remove(sokClient.RemoteEndPoint.ToString());
                        // 从列表中移除被中断的连接IP
                        RemoveList(listConnection,sokClient.RemoteEndPoint.ToString());
                        WriteLog("" + sokClient.RemoteEndPoint.ToString() + "断开连接\r\n");
                        //log.log("遇见异常"+se.Message);
                        break;
                    }
                }
                catch (SocketException se)
                {
                    // 从 通信套接字 集合中删除被中断连接的通信套接字;
                    listSocket.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从通信线程集合中删除被中断连接的通信线程对象;
                    listThread.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从列表中移除被中断的连接IP
                    listConnection.Items.Remove(sokClient.RemoteEndPoint.ToString());
                    WriteLog("" + sokClient.RemoteEndPoint.ToString() + "断开,异常消息：" + se.Message + "\r\n");
                    //log.log("遇见异常"+se.Message);
                    break;
                }
                catch (Exception e)
                {
                    // 从 通信套接字 集合中删除被中断连接的通信套接字;
                    listSocket.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从通信线程集合中删除被中断连接的通信线程对象;
                    listThread.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从列表中移除被中断的连接IP
                    listConnection.Items.Remove(sokClient.RemoteEndPoint.ToString());
                    WriteLog("异常消息：" + e.Message + "\r\n");
                    // log.log("遇见异常" + e.Message);
                    break;
                }
            }
        }

        public string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");//ToString("X2") 为C#中的字符串格式控制符
                }
            }
            return returnStr;
        }

        //void ShowMsg(string str)
        //{
        //    if (!BPS_Help.ChangeByte(txtMsg.Text, 2000))
        //    {
        //        txtMsg.Text = "";
        //        txtMsg.AppendText(str + "\r\n");
        //    }
        //    else
        //    {
        //        txtMsg.AppendText(str + "\r\n");
        //    }

        //}

        /// <summary>
        /// 判断文本框混合输入长度
        /// </summary>
        /// <param name="str">要判断的字符串</param>
        /// <param name="i">长度</param>
        /// <returns></returns>
        public static bool ChangeByte(string str, int i)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            int m = b.Length;
            if (m < i)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void WriteLog(string msg)
        {
            
            string log = System.DateTime.Now.ToString("MM/dd HH:mm:ss") + "  " + msg;
            if (listMessage.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) => {
                    if (listMessage.Items.Count > 100)
                        listMessage.Items.RemoveAt(0);
                    listMessage.Items.Add(log);
                };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                listMessage.Invoke(actionDelegate, log);
            }
            else
            {
                if (listMessage.Items.Count > 100)
                    listMessage.Items.RemoveAt(0);
                listMessage.Items.Add(log);
            }
        }

        

        private void listMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listMessage.Items.Clear();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress address = IPAddress.Parse("183.238.88.243");
            IPEndPoint endpoint = new IPEndPoint(address, 10095);
            Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socketClient.Connect(endpoint);
                WriteLog("连接服务端成功,准备发送实体Student");
                Student ms = new Student() { ID = 1, Name = "张三", Phone = "13237157517", sex = 1, Now_Time = DateTime.Now };
                using (MemoryStream memory = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(memory, ms);
                    WriteLog("发送长度:" + memory.ToArray().Length);
                    socketClient.Send(memory.ToArray());
                    WriteLog("我发送了 学生实体对象");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Serializable]
        class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int sex { get; set; }
            public DateTime Now_Time { get; set; }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string connection= listConnection.SelectedItem.ToString();
                Socket sokClient = listSocket[connection];
                
                
                if (sokClient.Connected)
                {
                    Byte[] bytSend = strToToHexByte(txtSend.Text);
                    sokClient.Send(bytSend);
                }
                else
                {
                    WriteLog("连接不可用");
                    return;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        private void comboxIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtServerIP.Text = comboxIP.SelectedItem.ToString();
            ip = IPAddress.Parse(txtServerIP.Text);
        }
    }
}
