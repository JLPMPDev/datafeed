using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JLPMPDev.Datafeed.Core
{
    public class Attribute
    {
        public string grouping { get; private set; }
        public string name { get; private set; }
        public int value { get; private set; }
        public int lower { get; private set; }
        public int upper { get; private set; }
        public string description { get; private set; }

        private Attribute(string grouping, string name, string value, string lower, string upper, string description)
        {
            this.grouping = grouping;
            this.name = name;
            this.value = int.Parse(value);
            this.lower = int.Parse(lower);
            this.upper = int.Parse(upper);
            this.description = description;
        }

        public static List<Attribute> BuildList(string path)
        {
            List<Attribute> attributes = new List<Attribute>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    var split = sr.ReadLine().Split(',');

                    Attribute attribute = new Attribute(split[0], split[1], split[2], split[3], split[4], split[5]);

                    attributes.Add(attribute);
                }
            }
            attributes = attributes.FindAll(x => (x.value < x.lower || x.value >= x.upper) || (x.lower == 0 && x.upper == 0));
            return attributes;
        }

        public static IOrderedEnumerable<IGrouping<string, Attribute>> BuildGroups(List<Attribute> attrs)
        {
            var groups =
                from a in attrs
                group a by a.grouping into groupings
                orderby groupings.Key
                select groupings;

            return groups;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", name, value);
        }
    }
}
