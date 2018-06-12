using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;


namespace EmailTLS
{
    public class EnviarEmail
    {
        //public void enviarEmail(string sFrom, string sTo, string sBcc, string sCC, string sSubjet, string sBody, string sSmtp, string sUSer, string sPassword)
        //{

        //    SmtpMail oMail = new SmtpMail("TryIt");
        //    SmtpClient oSmtp = new SmtpClient();
            
        //    // Set sender email address, please change it to yours
        //    oMail.From = sFrom;

        //    // Set recipient email address, please change it to yours
        //    oMail.To = sTo;
        //    oMail.Cc = sCC;
        //    oMail.Bcc = sBcc;

        //    // Set email subject
        //    oMail.Subject = sSubjet;

        //    // Set email body
        //    //oMail.TextBody = sBody;
        //    oMail.HtmlBody = sBody;

        //    // Your SMTP server address
        //    SmtpServer oServer = new SmtpServer(sSmtp);

        //    // User and password for ESMTP authentication, if your server doesn't require
        //    // User authentication, please remove the following codes.
        //    oServer.User = sUSer;
        //    oServer.Password = sPassword;

        //    // If your smtp server requires SSL connection, please add this line
        //    oServer.ConnectType = SmtpConnectType.ConnectSTARTTLS;

        //    try
        //    {
        //        oSmtp.SendMail(oServer, oMail);
        //    }
        //    catch (Exception ep)
        //    {
        //        Console.WriteLine("Erro ao enviar email: " + ep.Message);
        //    } 
        //}


        public void enviarEmail(string sFrom, string sTo, string sBcc, string sCC, string sSubjet, string sBody, string sSmtp, string sUSer, string sPassword)
        {
            try
            {
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

                string[] slistaEmails;

                if (sTo != "")
                {
                    slistaEmails = sTo.Split(',');
                    for (int i = 0; i < slistaEmails.Length; i++)
                    {
                        msg.To.Add(new System.Net.Mail.MailAddress(slistaEmails[i].ToString()));
                    }
                }

                if (sBcc != "")
                {
                    slistaEmails = sBcc.Split(',');
                    for (int i = 0; i < slistaEmails.Length; i++)
                    {
                        msg.Bcc.Add(new System.Net.Mail.MailAddress(slistaEmails[i].ToString()));
                    }
                }

                if (sCC != "")
                {
                    slistaEmails = sCC.Split(',');
                    for (int i = 0; i < slistaEmails.Length; i++)
                    {
                        msg.CC.Add(new System.Net.Mail.MailAddress(slistaEmails[i].ToString()));
                    }                    
                }

                msg.From = new System.Net.Mail.MailAddress(sFrom, "Group Software");
                msg.Subject = sSubjet;
                msg.IsBodyHtml = true;
                msg.Body = sBody;
                

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(sUSer, sPassword);
                client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
                client.Host = sSmtp;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;

                client.Send(msg);
            }
            catch (Exception erro)
            {
                throw new ApplicationException("Erro ao enviar email: " + erro.Message);
            }
        }
    }
}
