using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace JLPMPDev.Datafeed.Core
{
    public class Mailer
    {
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
                foreach (Email mail in config.email)
                {
                    MailAddress to = new MailAddress(mail.address, mail.name);
                    message.To.Add(to);
                }

                // message subject and body
                message.Subject = String.Format("{0}: {1}", config.report.title, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

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
                string feedTime = config.feed.FeedTime().ToString("dd/MM/yyyy HH:mm");
                string feedStatus;
                if (config.feed.FeedTime() < DateTime.Now.AddMinutes(-30))
                {
                    feedStatus = String.Format("Feed Status: <span class=\"old\">Out of date. [{0}]</span>", feedTime);
                }
                else
                {
                    feedStatus = "Feed Status: No problems found.";
                }

                using (StreamReader sr = new StreamReader(config.template.path))
                {
                    message.Body = sr.ReadToEnd().Replace("%main%", sb.ToString())
                                                 .Replace("%feedStatus%", feedStatus)
                                                 .Replace("%reportTitle%", config.report.title);
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
