using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace JLPMPDev.Datafeed.Core
{
  public class Config
  {
    public List<Email> Email { get; set; }
    public Template Template { get; set; }
    public Feed Feed { get; set; }
    public Report Report { get; set; }
    [YamlMember(Alias = "smtp")]
    public SMTP SMTP { get; set; }

    public static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.yaml");

    public static Config Deserialize()
    {
      var input = new StreamReader(ConfigPath);
      var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
      Config config = deserializer.Deserialize<Config>(input);
      config.NullHandling();
      return config;
    }

    private Config NullHandling()
    {
      this.Email = this.Email == null ? new List<Email>() : this.Email;
      this.Feed = this.Feed == null ? new Feed() : this.Feed;
      this.Template = this.Template == null ? new Template() : this.Template;
      this.Report = this.Report == null ? new Report() : this.Report;
      this.SMTP = this.SMTP == null ? new SMTP() : this.SMTP;
      return this;
    }
  }

  public class Email
  {
    public string Name { get; set; }
    public string Address { get; set; }
  }

  public class Template
  {
    public string Path { get; set; }

    public static readonly string DefaultTemplatePath = "template.html";
  }

  public class Feed
  {
    public string Path { get; set; }

    public static readonly string DefaultFeedPath = "feed.txt";

    public DateTime RetrievalTime()
    {
      return File.GetLastWriteTime(this.Path);
    }
  }

  public class Report
  {
    public string Title { get; set; }

    public static readonly string DefaultReportTitle = "Datafeed";
  }

  public class SMTP
  {
    public string Host { get; set; }
    public int? Port { get; set; }

    public static readonly int DefaultSMTPPort = 25;
  }
}