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
            return config;
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
    }

    public class Feed
    {
        public string Path { get; set; }

        public DateTime FeedTime()
        {
            return File.GetLastWriteTime(this.Path);
        }
    }

    public class Report
    {
        public string Title { get; set; }
    }

    public class SMTP
    {
        public string Host { get; set; }
        public string Port { get; set; }
    }
}