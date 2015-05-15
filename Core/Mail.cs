using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using YamlDotNet.Serialization;

namespace JLPMPDev.Datafeed.Core
{
    public class Mail
    {
        public string name { get; set; }
        [YamlMember(Alias = "email-address")]
        public string emailAddress { get; set; }

        public static void SendMail(List<Attribute> list, Config config)
        {
            try
            {
                // initialize SmtpClient and MailMessage
                SmtpClient client = new SmtpClient("172.22.200.140", 25);
                MailMessage message = new MailMessage();

                message.IsBodyHtml = true;

                // mail from
                message.From = new MailAddress("admin@datafeed.co.uk", "Datafeed");
                // mail to
                foreach (Mail mail in config.mailingList)
                {
                    MailAddress to = new MailAddress(mail.emailAddress, mail.name);
                    message.To.Add(to);
                }

                // message subject and body
                string rightNow = DateTime.Now.ToShortDateString();
                string minusTen = DateTime.Now.AddMinutes(-10).ToShortTimeString();
                message.Subject = String.Format("{0}: {1} {2}", config.feedTitle, rightNow, minusTen);

                var groups = Attribute.BuildGroups(list);

                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    foreach (var group in groups)
                    {
                        sw.WriteLine("<h2>{0}</h2>", group.Key);
                        sw.WriteLine("<ul>");
                        foreach (var record in group)
                        {
                            string html = String.Format("<li>{0}: <strong>{1}</strong>", record.name, record.value);
                            if (record.description != String.Empty)
                            {
                                html += String.Format(" | <em>{0}</em>", record.description);
                            }
                            sw.WriteLine(html);
                        }
                        sw.WriteLine("</ul>");
                    }
                }

                // get last write time of file
                DateTime writeTime = File.GetLastWriteTime(config.feedPath);
                string modifiedTime;
                if (writeTime < DateTime.Now.AddMinutes(-30))
                {
                    modifiedTime = String.Format("<span class=\"old\">{0}</span>", writeTime.ToString());
                }
                else
                {
                    modifiedTime = String.Format(writeTime.ToString());
                }

                using (StreamReader sr = new StreamReader(config.templatePath))
                {
                    message.Body = sr.ReadToEnd().Replace("%main%", sb.ToString())
                                                 .Replace("%lastModified%", modifiedTime)
                                                 .Replace("%feedTitle%", config.feedTitle);
                }

                // send message
                client.Send(message);
            }

            // catches the exceptions, if any
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
