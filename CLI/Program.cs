using System;
using System.Collections.Generic;
using JLPMPDev.Datafeed.Core;

namespace JLPMPDev.Datafeed.CLI
{
  public class Program
  {
    public static readonly string CLIVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

    static void Main()
    {
      
      Config config = Config.Deserialize();      

      try
      {
        Console.WriteLine("Starting Datafeed v" + CLIVersion + "...");
        // read feed and build list
        Console.WriteLine("Attemping to read feed. Please wait...");
        string feedPath = config.Feed.Path ?? Feed.DefaultFeedPath;
        List<Core.Attribute> list = Core.Attribute.BuildList(feedPath);

        if (list.Count < 1)
        {
          Console.WriteLine("Feed is empty. Press any key to exit.");
          return;
        }

        Console.WriteLine("Attempting to send message. Please wait...");
        Mailer.SendMail(list, config);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        Console.ReadKey();
      }
    }
  }
}