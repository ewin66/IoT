using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;

namespace EasyJoin
{
    public partial class ParkingLock : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            //00 闭锁状态，01 开锁状态
            try
            {
                if (sokClient.Connected)
                {
                    if (btnLock.Text == "UnLock")//解锁
                    {
                        Byte[] bytSend = CommonMethod.strToToHexByte("01");
                        sokClient.Send(bytSend);
                    }
                    if (btnLock.Text == "Lock")//闭锁
                    {
                        Byte[] bytSend = CommonMethod.strToToHexByte("00");
                        sokClient.Send(bytSend);
                    }
                }
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.ToString();
            }
        }

        

        
    }
}