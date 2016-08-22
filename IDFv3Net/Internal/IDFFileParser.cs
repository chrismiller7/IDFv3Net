using System;
using System.Collections.Generic;
using System.IO;

namespace IDFv3Net.Internal
{
    public class IDFFileParser
    {
        public IDFFileSection[] Sections { get; private set; }

        public IDFFileParser(string file)
        {
            var lines = File.ReadAllLines(file);

            string currentSection = "";
            List<string> records = new List<string>();
            List<IDFFileSection> sections = new List<IDFFileSection>();

            foreach (var line in lines)
            {
                if (!line.StartsWith("#") && line.Trim().Length > 0)
                {
                    var str = line.ToUpper();
                    if (str.StartsWith(".END_"))
                    {
                        sections.Add(new IDFFileSection(records.ToArray()));
                    }
                    else if (str.StartsWith("."))
                    {
                        records.Clear();
                        var fields = ParserHelpers.GetFields(str);
                        currentSection = fields[0];
                        records.Add(str);
                    }
                    else
                    {
                        records.Add(str);
                    }
                }
            }

            Sections = sections.ToArray();
        }
    }
}
