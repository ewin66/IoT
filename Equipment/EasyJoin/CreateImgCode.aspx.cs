using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using System.Text;
using System.Drawing.Imaging;

public partial class CreateImgCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strImageType = string.Empty;
        string checkCode = string.Empty;
        try
        {
            strImageType = System.Configuration.ConfigurationManager.AppSettings["CreateImageType"];
        }
        catch
        {   
        }
        if (strImageType.Trim() == "1")
        {
            checkCode = CreateChinaCode();
            CreateImage_1(checkCode);
        }
        else
        {
            checkCode = CreateCode(4);
            CreateImage(checkCode);
        }
        Session["CheckCode"] = checkCode;
    }

    /// <summary>
    /// 产生验证码
    /// </summary>
    /// <param name="codeLength"></param>
    /// <returns></returns>
    public string CreateCode(int codeLength)
    {
        string so = "1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        string[] strArr = so.Split(',');
        string code = "";
        Random rand = new Random();
        for (int i = 0; i < codeLength; i++)
        {
            code += strArr[rand.Next(0, strArr.Length)];
        }
        return code;
    }

    /// <summary>
    /// 产生验证码[汉字]
    /// </summary>
    /// <returns></returns>
    private string CreateChinaCode()
    {
        //获取GB2312编码页（表）
        Encoding gb=Encoding.GetEncoding("gb2312");
        //调用函数产生4个随机中文汉字编码
        object[] bytes=CreateRegionCode(4);
        //根据汉字编码的字节数组解码出中文汉字
        string str1=gb.GetString((byte[])Convert.ChangeType(bytes[0], typeof(byte[])));
        string str2=gb.GetString((byte[])Convert.ChangeType(bytes[1], typeof(byte[])));
        string str3=gb.GetString((byte[])Convert.ChangeType(bytes[2], typeof(byte[])));
        string str4=gb.GetString((byte[])Convert.ChangeType(bytes[3], typeof(byte[])));
        return str1+str2+str3+str4;
    }

    /// <summary>
    /// 此函数在汉字编码范围内随机创建含两个元素的十六进制字节数组，每个字节数组代表一个汉字，并将 四个字节数组存储在object数组中。参数：strlength，代表需要产生的汉字个数
    /// </summary>
    /// <param name="strlength"></param>
    /// <returns></returns>
    public static object[] CreateRegionCode(int strlength)
    {
        //定义一个字符串数组储存汉字编码的组成元素
        string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

        Random rnd = new Random();

        //定义一个object数组用来
        object[] bytes = new object[strlength];

        /**/
        /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中
每个汉字有四个区位码组成
区位码第1位和区位码第2位作为字节数组第一个元素
区位码第3位和区位码第4位作为字节数组第二个元素
*/
        for (int i = 0; i < strlength; i++)
        {
            //区位码第1位
            int r1 = rnd.Next(11, 14);
            string str_r1 = rBase[r1].Trim();

            //区位码第2位
            rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的种子避免产生重复值
            int r2;
            if (r1 == 13)
            {
                r2 = rnd.Next(0, 7);
            }
            else
            {
                r2 = rnd.Next(0, 16);
            }
            string str_r2 = rBase[r2].Trim();

            //区位码第3位
            rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
            int r3 = rnd.Next(10, 16);
            string str_r3 = rBase[r3].Trim();

            //区位码第4位
            rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
            int r4;
            if (r3 == 10)
            {
                r4 = rnd.Next(1, 16);
            }
            else if (r3 == 15)
            {
                r4 = rnd.Next(0, 15);
            }
            else
            {
                r4 = rnd.Next(0, 16);
            }
            string str_r4 = rBase[r4].Trim();

            //定义两个字节变量存储产生的随机汉字区位码
            byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
            byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
            //将两个字节变量存储在字节数组中
            byte[] str_r = new byte[] { byte1, byte2 };

            //将产生的一个汉字的字节数组放入object数组中
            bytes.SetValue(str_r, i);

        }

        return bytes;
    }

    /// <summary>
    /// 产生验证图片
    /// </summary>
    /// <param name="code"></param>
    public void CreateImage(string code)
    {
        Bitmap image = new Bitmap(70, 24);
        Graphics g = Graphics.FromImage(image);
        WebColorConverter ww = new WebColorConverter();
        g.Clear((Color)ww.ConvertFromString("#FAE264"));

        Random random = new Random();
        //画图片的背景噪音线
        for (int i = 0; i < 12; i++)
        {
            int x1 = random.Next(image.Width);
            int x2 = random.Next(image.Width);
            int y1 = random.Next(image.Height);
            int y2 = random.Next(image.Height);

            g.DrawLine(new Pen(Color.LightGray), x1, y1, x2, y2);
        }
        Font font = new Font("Arial", 15, FontStyle.Bold | FontStyle.Italic);
        //Font font = new Font("Arial", 10, FontStyle.Italic | FontStyle.Strikeout | FontStyle.Underline);
        System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
            new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.Gray, 1.2f, true);
        g.DrawString(code, font, brush, 0, 0);

        //画图片的前景噪音点
        for (int i = 0; i < 10; i++)
        {
            int x = random.Next(image.Width);
            int y = random.Next(image.Height);
            image.SetPixel(x, y, Color.White);
        }

        //画图片的边框线
        g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        Response.ClearContent();
        Response.ContentType = "image/Gif";
        Response.BinaryWrite(ms.ToArray());
        g.Dispose();
        image.Dispose();
    }

    /// <summary>
    /// 产生验证图片
    /// </summary>
    /// <param name="chkCode"></param>
    public void CreateImage_1(string chkCode)
    {
        Random rnd = new Random();
        //颜色列表，用于验证码、噪线、噪点 
        Color[] color ={ Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
        //字体列表，用于验证码 
        string[] font ={ "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
        Bitmap bmp = new Bitmap(110, 27);
        Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        //画噪线
        for (int i = 0; i < 10; i++)
        {
            int x1 = rnd.Next(100);
            int y1 = rnd.Next(40);
            int x2 = rnd.Next(100);
            int y2 = rnd.Next(40);
            Color clr = color[rnd.Next(color.Length)];
            g.DrawLine(new Pen(clr), x1, y1, x2, y2);
        }
        //画验证码字符串 
        for (int i = 0; i < chkCode.Length; i++)
        {
            string fnt = font[rnd.Next(font.Length)];
            Font ft = new Font(fnt, 18);
            Color clr = color[rnd.Next(color.Length)];
            g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 25 + 1, (float)1);
        }
        //画噪点 
        for (int i = 0; i < 100; i++)
        {
            int x = rnd.Next(bmp.Width);
            int y = rnd.Next(bmp.Height);
            Color clr = color[rnd.Next(color.Length)];
            bmp.SetPixel(x, y, clr);
        }
        //清除该页输出缓存，设置该页无缓存 
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.AppendHeader("Pragma", "No-Cache");
        //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
        MemoryStream ms = new MemoryStream();
        try
        {
            bmp.Save(ms, ImageFormat.Png);
            Response.ClearContent();
            Response.ContentType = "image/Png";
            Response.BinaryWrite(ms.ToArray());
        }
        finally
        {
            //显式释放资源 
            bmp.Dispose();
            g.Dispose();
        }
    }
}
