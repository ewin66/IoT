using System;
using System.Net.Mail;

namespace Common
{
    public class Method
    {
        public static bool CheckYearMonth(string YearMonth)
        {
            bool blFlag = false;
            int intYear = 0;
            int intMonth = 0;
            if (YearMonth.Trim().Length == 6)
            {
                try
                {
                    intYear = Int32.Parse(YearMonth.Substring(0, 4));
                    intMonth = Int32.Parse(YearMonth.Substring(4, 2));
                    if (1900 <= intYear && intYear <= 2100 && 1 <= intMonth && intMonth <= 12)
                    {
                        blFlag = true;
                    }
                }
                catch
                {
                }
            }
            return blFlag;
        }

        public static string GetClintInfo()
        {
            string strClintInfo = string.Empty;
            //strClintInfo += "IP:" + Page.Request.UserHostAddress;
            return strClintInfo;
        }

        public static bool SendMail1(string strMailTo,string strTitle,string strMailBody,out string strMessage)
        {
            strMessage="";
            System.Net.Mail.MailMessage objMailMessage = new System.Net.Mail.MailMessage();
            objMailMessage.From = new MailAddress("wgang10@foxmail.com");
            objMailMessage.To.Add(new MailAddress(strMailTo));
            objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            objMailMessage.Subject = strTitle;
            objMailMessage.Body = strMailBody;
            objMailMessage.IsBodyHtml = false;
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.Host = "smtp.qq.com";
            objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            objSmtpClient.Credentials = new System.Net.NetworkCredential("wgang10@foxmail.com", "wangang10");
            //objSmtpClient.EnableSsl = true;//SMTP ������Ҫ��ȫ������Ҫ���ô�����
            try
            {
                objSmtpClient.Send(objMailMessage);
                strMessage = "�ʼ����ͳɹ���";
                return true;
            }
            catch (Exception ex)
            {
                strMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPWD(string password)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        }

        /// <summary>
        /// ������֤��
        /// </summary>
        /// <returns></returns>
        public static string GenerateVerifictionCode()
        {
            return GenerateVerifictionCode(System.Web.Configuration.FormsAuthPasswordFormat.SHA1);
        }

        /// <summary>
        /// ������֤��
        /// </summary>
        /// <returns></returns>
        public static string GenerateVerifictionCode(System.Web.Configuration.FormsAuthPasswordFormat passwordFormat)
        {
            string guid = System.Guid.NewGuid().ToString();
            if (passwordFormat == System.Web.Configuration.FormsAuthPasswordFormat.MD5)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(guid, "MD5");
            }
            else if (passwordFormat == System.Web.Configuration.FormsAuthPasswordFormat.SHA1)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(guid, "SHA1");
            }
            else
            {
                return guid;
            }
        }

        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="strMailTo">�ռ���</param>
        /// <param name="strTitle">����</param>
        /// <param name="strMailBody">����</param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        public static bool SendMail2(string strMailTo, string strTitle, string strMailBody, out string strMessage)
        {
            strMessage = "";
            System.Net.Mail.MailMessage objMailMessage = new System.Net.Mail.MailMessage();
            objMailMessage.From = new MailAddress("server@ziyangsoft.com");
            objMailMessage.To.Add(new MailAddress(strMailTo));
            objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            objMailMessage.Subject = strTitle;
            objMailMessage.Body = strMailBody;
            objMailMessage.IsBodyHtml = false;
            SmtpClient objSmtpClient = new SmtpClient();
            //objSmtpClient.Host = "smtp.qq.com";
            objSmtpClient.Host = "smtp.exmail.qq.com";
            objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //objSmtpClient.Credentials = new System.Net.NetworkCredential("wgang10@foxmail.com", "wangang10");
            objSmtpClient.Credentials = new System.Net.NetworkCredential("server@ziyangsoft.com", "q1w2e3r4``");
            //objSmtpClient.EnableSsl = true;//SMTP ������Ҫ��ȫ������Ҫ���ô�����
            try
            {
                objSmtpClient.Send(objMailMessage);
                strMessage = "�ʼ����ͳɹ���";
                return true;
            }
            catch (Exception ex)
            {
                UtilityLog.WriteError(ex.Message);
                strMessage = ex.Message;
                return false;
            }
        }

        public static string GetMsgInfo(string strMsgNo, string strReplace)
        {
            string strMsgInfo = string.Empty;
            switch (strMsgNo)
            {
                case "E11001":
                    strMsgInfo = "���ݿ���������Ӵ���";
                    break;
                case "E11002":
                    strMsgInfo = "�������ݿ�ʱ��������";
                    break;
                case "Q22001":
                    strMsgInfo = "ȷ��Ҫ�˳���?";
                    break;
                case "Q22007":
                    strMsgInfo = "ȷ��Ҫɾ����������?";
                    break;
                case "W11003":
                    strMsgInfo = "�����ɹ������]���κ����ݱ����¡�";
                    break;
                case "W11020":
                    strMsgInfo = "ͼ���С����С�� 2M";
                    break;
                case "W11023":
                    strMsgInfo = "�����������ݲ�����!";
                    break;
                case "W18888":
                    strMsgInfo = "�����Ѿ�����!";
                    break;
                default:
                    strMsgInfo = "";
                    break;
            }
            return strMsgInfo;
        }
    }
}
