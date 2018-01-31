using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Equipment
{
    public partial class Picture485 : Form
    {
        SerialPort serialPort;
        List<string> Ports;
        string pathLog = string.Empty;
        string Port = "COM2";
        int lenth = 0;
        private delegate void myDelegate(string s);
        private string receiveString = string.Empty;
        private string picString = string.Empty;

        public Picture485()
        {
            InitializeComponent();
            Ports = new List<string>();
            string root = Assembly.GetExecutingAssembly().Location;
            root = root.Substring(0, root.LastIndexOf("\\"));
            pathLog = root + @"\JYLog.log";
            GetPorts();
        }

        private void GetPorts()
        {
            foreach (string PortName in SerialPort.GetPortNames())
            {
                if (PortName == Port)
                {
                    WriteLog("端口号：" + PortName.ToString());
                    Ports.Add(PortName);
                }
            }
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lenth = 0;
            /*拍照流程：  
             * a.停止当前帧刷新
             * b.获娶图片长度
             * c.获取图片(终端是按4096一包提取图片数据)
             * d.恢复帧更新
             * 
             * a. 停止当前帧刷新
		        主 机 发： 56 00 36 01 00
		        摄像头回： 76 00 36 00 00
               b. 获娶图片长度，图片长度为4Bytes
		        主 机 发： 56 00 34 01 00
		        摄像头回： 76 00 34 00 04 XX XX XX XX
               c. 获取图片
                0x56+序列号1B+0x32+0x0C+0x00+操作方式+起始地址4B+数据长度4B+延时时间
                操作方式：0X0A = UART，0X0C = HUART
		        主 机 发： 56 00 32 0C 00 0C 00 00 00 00 XX XX XX XX 00 FF
		        摄像头回： 首先回：76 00 32 00 00 
                再  回：图像数据
                发完回：76 00 32 00 00
	            注：延时时间指摄像返回命令与数据间的时间间隔，单位为0.01mS
                   终端提取图片数据时, 是按4096字节一包一包提取的
               d. 恢复帧更新
		            主 机 发： 56 00 36 01 02
		            摄像头回： 76 00 36 00 00

            */

            string SendMsg = textBox1.Text;
            Send(SendMsg);
        }

        private void Send(string message)
        {
            if (Ports.Count < 1) return;
            if (serialPort == null)
            {
                serialPort = new SerialPort(Ports[0], 115200, Parity.None, 8, StopBits.One);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            }
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                Byte[] bt = strToToHexByte(message);
                serialPort.Write(bt, 0, bt.Length);
                WriteLog("发送数据：" + message);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
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

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            myDelegate md = new myDelegate(SetText);
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
                    if (receiveString.Trim().Equals("76 00 36 00 00"))//
                    {
                        receiveString = "";
                        Send("56 00 34 01 00");//获取图片长度
                    }
                    if (receiveString.StartsWith("76 00 34 00 04"))
                    {
                        if (lenth != 0) return;
                        lenth = Int32.Parse(receiveString.Replace("76 00 34 00 04", "").Replace(" ",""), System.Globalization.NumberStyles.HexNumber);
                        receiveString = "";
                        Send("56 00 32 0C 00 0A 00 00 00 00 00 00 08 00 0B B8");//获取图片
                    }
                    if (receiveString.Trim().EndsWith("76 00 32 00 00")&& receiveString.Trim().StartsWith("76 00 32 00 00"))
                    {
                        Invoke(md, receiveString);
                    }
                }
            }
            catch (Exception ee)
            {
                this.richTextBox1.Text=ee.Message;
            }
        }

        private Image CreateImage(string str16)
        {            
            // 声明一个字节数组，长度为16进制字符串长度的一半：  
            byte[] buf = new byte[str16.Length / 2];
            for (int i = 0; i < buf.Length; i++)
            {
                // 由于16进制字符串都是两个一组，所以需要将两个字符一起转换成字节  
                buf[i] = Convert.ToByte(str16.Substring(i * 2, 2), 16);
            }

            MemoryStream ms = new MemoryStream(buf);
            Image img = Image.FromStream(ms, true);
            return img;
        }

        private void SetText(string s)
        {
            if (s.Length < 1) return;
            picString += s.Replace("76 00 32 00 00", "").Replace(" ","");
            receiveString = "";
            if (picString.Length/2 >= lenth)
            {
                richTextBox1.Text = picString;
                try
                {
                    pictureBox1.Image = CreateImage(picString);
                    picString = "";
                    Send("56 00 36 01 02");//恢复帧更新
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if(picString.Length / 2== Int32.Parse("800", System.Globalization.NumberStyles.HexNumber))
            {
                Send("56 00 32 0C 00 0A 00 00 08 00 00 00 08 00 0B B8");//获取图片
            }
            else if (picString.Length / 2 == Int32.Parse("800", System.Globalization.NumberStyles.HexNumber)*2)
            {
                Send("56 00 32 0C 00 0A 00 00 10 00 00 00 08 00 0B B8");//获取图片
            }
            else if (picString.Length / 2 == Int32.Parse("800", System.Globalization.NumberStyles.HexNumber) * 3)
            {
                Send("56 00 32 0C 00 0A 00 00 18 00 00 00 08 00 0B B8");//获取图片
            }
            else if (picString.Length / 2 == Int32.Parse("800", System.Globalization.NumberStyles.HexNumber) * 4)
            {
                if (Int32.Parse("800", System.Globalization.NumberStyles.HexNumber)*5 <= lenth)
                {
                    Send("56 00 32 0C 00 0A 00 00 20 00 00 00 08 00 0B B8");//获取图片
                }
                else
                {
                    int lenth32 = lenth - Int32.Parse("800", System.Globalization.NumberStyles.HexNumber) * 4;
                    string lenth16= lenth32.ToString("X8");
                    string sendStr = string.Format("56 00 32 0C 00 0A 00 00 20 00 {0} {1} {2} {3} 0B B8"
                        , lenth16.Substring(0, 2)
                        , lenth16.Substring(2, 2)
                        , lenth16.Substring(4, 2)
                        , lenth16.Substring(6, 2));
                    Send(sendStr);//获取图片
                }
            }
        }


        private void WriteLog(string msg)
        {
            try
            {
                System.IO.File.AppendAllText(pathLog, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + msg + "\r\n");
            }
            catch (Exception ex)
            { }
        }
    }
}
