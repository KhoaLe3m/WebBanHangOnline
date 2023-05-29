using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace WebBanHangOnline
{
    public class Common
    {
        public static bool SendEmail(string fromAddress, string toAddress, string subject, string body)
        {
            try
            {
                // Thông tin tài khoản email gửi đi
                string username = ConfigurationManager.AppSettings["Email"];
                string password = ConfigurationManager.AppSettings["PasswordEmail"];

                // Tạo đối tượng SmtpClient và cấu hình thông tin SMTP server
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(username, password);

                // Tạo đối tượng MailMessage và cấu hình thông tin email
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromAddress);
                mailMessage.To.Add(toAddress);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                // Gửi email
                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu có)
                // ...

                return false;
            }
        }
        public static string FormatNumber(object value,int soSauDauPhay =2)
        {
            bool isNumber = IsNumberic(value);
            decimal GT = 0;
            if (isNumber)
            {
                GT = Convert.ToDecimal(value);
            }
            string str = "";
            string thapPhan = "";
            for(int i = 0; i < soSauDauPhay; i++)
            {
                thapPhan += "#";
            }
            if (thapPhan.Length > 0) thapPhan = "." + thapPhan;
            string snumformat = string.Format("0:#,##0{0}", thapPhan);
            str = String.Format("{" + snumformat + "}", GT);
            return str;
        }

        private static bool IsNumberic(object value)
        {
            return value is sbyte
                 || value is byte
                 || value is short
                 || value is ushort
                 || value is int
                 || value is uint
                 || value is long
                 || value is ulong
                 || value is float
                 || value is double
                 || value is decimal;
        }

    }
}