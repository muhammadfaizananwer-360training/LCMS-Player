using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail; 

namespace ICP4.CoursePlayer
{
    public class EmailUtility
    {

        public bool sendMail(string to, string from, string cc, string bcc, string subject, string body, string smtphost, int smtpport)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage message = new MailMessage();
            string[] aCC;
            string[] aBCC;

            aCC = cc.Split(';');
            aBCC = bcc.Split(';');
            try
            {
                foreach (string ccCopy in aCC)
                {
                    if (!ccCopy.Equals("") && ccCopy != null)
                        message.CC.Add(new MailAddress(ccCopy));
                }
                foreach (string bccHide in aBCC)
                {
                    if (!bccHide.Equals("") && bccHide != null)
                        message.Bcc.Add(new MailAddress(bccHide));
                }
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Host = smtphost;
                smtp.Port = smtpport;
                message.From = new MailAddress(from);
                smtp.Send(message);
                message.Dispose();
            }
            catch (SmtpException exp)
            {
                return false;
            }
            return true;
        }

    }
}
