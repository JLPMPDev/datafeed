using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace JLPMPDev.Datafeed.Core
{
    public class Config
    {
        public List<Email> email { get; set; }
        public Template template { get; set; }
        public Feed feed { get; set; }
        public Report report { get; set; }

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
        public string name { get; set; }
        public string address { get; set; }
    }

    public class Template
    {
        public string path { get; set; }
    }

    public class Feed
    {
        public string path { get; set; }
    }

    public class Report
    {
        public string title { get; set; }
    }
}