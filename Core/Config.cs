using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace JLPMPDev.Datafeed.Core
{
    public class Config
    {
        [YamlMember(Alias = "mailing-list")]
        public List<Mail> mailingList { get; set; }
        [YamlMember(Alias = "template-path")]
        public string templatePath { get; set; }
        [YamlMember(Alias = "feed-path")]
        public string feedPath { get; set; }

        public static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.yaml");

        public static Config Deserialize()
        {
            var input = new StreamReader(ConfigPath);
            var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
            Config config = deserializer.Deserialize<Config>(input);
            return config;
        }
    }
}