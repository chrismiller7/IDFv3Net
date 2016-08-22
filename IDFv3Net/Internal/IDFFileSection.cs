using System;

namespace IDFv3Net.Internal
{
    public class IDFFileSection
    {
        string[] lines;
        public int LineIdx { get; private set; }
        public int FieldIdx { get; private set; }

        string[] fields;

        public IDFFileSection(string[] records)
        {
            if (records == null || records.Length == 0) throw new Exception("Must have at least one record in this section.");

            LineIdx = 0;
            FieldIdx = 0;
            lines = records;
            NextRecord();
        }

        public string SectionName
        {
            get
            {
                var fields = ParserHelpers.GetFields(lines[0]);
                return fields[0];
            }
        }

        public void NextRecord()
        {
            if (LineIdx < lines.Length)
            {
                fields = ParserHelpers.GetFields(lines[LineIdx++]);
                FieldIdx = 0;

                while (LineIdx < lines.Length && fields.Length > 0 && fields[0].StartsWith("#") || fields.Length == 0)
                {
                    fields = ParserHelpers.GetFields(lines[LineIdx++]);
                    FieldIdx = 0;
                }
            }
        }

        public string NextField()
        {
            return fields[FieldIdx++];
        }

        public bool EndOfFile
        {
            get
            {
                return LineIdx >= lines.Length;
            }
        }

        public override string ToString()
        {
            return "IDF Section: " + SectionName;
        }
    }
}
