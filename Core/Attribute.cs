using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JLPMPDev.Datafeed.Core
{
    public class Attribute
    {
        public string Grouping { get; private set; }
        public string Name { get; private set; }
        public int Value { get; private set; }
        public int Lower { get; private set; }
        public int Upper { get; private set; }
        public string Description { get; private set; }

        private Attribute(string grouping, string name, string value, string lower, string upper, string description)
        {
            this.Grouping = grouping;
            this.Name = name;
            this.Value = int.Parse(value);
            this.Lower = int.Parse(lower);
            this.Upper = int.Parse(upper);
            this.Description = description;
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
            attributes = attributes.FindAll(x => (x.Value < x.Lower || x.Value >= x.Upper) || (x.Lower == 0 && x.Upper == 0));
            return attributes;
        }

        public static IOrderedEnumerable<IGrouping<string, Attribute>> BuildGroups(List<Attribute> attrs)
        {
            var groups =
                from a in attrs
                group a by a.Grouping into groupings
                orderby groupings.Key
                select groupings;

            return groups;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Value);
        }
    }
}
