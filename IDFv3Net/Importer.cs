using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using IDFv3Net.Attributes;

namespace IDFv3Net
{
    public class Importer
    {
        public Importer(string file)
        {
            FileParser parser = new FileParser(file);
            while (!parser.EndOfFile)
            {
                var section = ReadSection(parser);
            }     
        }

        object ReadSection(FileParser parser)
        {
            var sectionName = parser.NextField();
            var section = CreateSectionObjectFromName(sectionName);
            if (section != null)
            {
                PopulateFields(parser, section);
            }

            parser.NextRecord();
            return section;
        }

        bool PopulateFields(FileParser parser, object section)
        {
            foreach (var field in section.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.GetCustomAttribute<RecordAttribute>() != null)
                {
                    parser.NextRecord();
                }

                if (field.FieldType.IsArray)
                {
                    var elementType = field.FieldType.GetElementType();
                    Type listType = typeof(List<>).MakeGenericType(elementType);
                    IList list = (IList)Activator.CreateInstance(listType);
                    while (true)
                    {
                        var item = Activator.CreateInstance(elementType);
                        bool succ = PopulateFields(parser, item);
                        if (!succ) break;
                        list.Add(item);
                        //parser.NextRecord();
                    }
                    Array arr = Array.CreateInstance(elementType, list.Count);
                    list.CopyTo(arr, 0);
                    field.SetValue(section, arr);
                    return true;
                }
                else
                {
                    var fieldIdx = parser.FieldIdx;
                    var fieldStr = parser.NextField();

                    if (fieldIdx == 0 && fieldStr.StartsWith("."))
                    {
                        return false;
                    }

                    if (field.FieldType == typeof(string))
                    {
                        field.SetValue(section, fieldStr);
                    }
                    else if (field.FieldType == typeof(int))
                    {
                        field.SetValue(section, int.Parse(fieldStr));
                    }
                    else if (field.FieldType == typeof(float))
                    {
                        field.SetValue(section, float.Parse(fieldStr));
                    }
                    else
                    {
                        throw new Exception("Type not supported: " + field.FieldType);
                    }
                }
            }
            return true;
        }

        void SetFieldValue(FieldInfo field, object obj, string val)
        {
           
        }

        object CreateSectionObjectFromName(string sectionName)
        {
            foreach (Type type in Assembly.GetAssembly(typeof(SectionNameAttribute)).GetTypes())
            {
                var section = type.GetCustomAttribute(typeof(SectionNameAttribute)) as SectionNameAttribute;
                if (section != null && section.Names.Contains(sectionName.TrimStart('.')))
                {
                    return Activator.CreateInstance(type);
                }
            }
            return null;
        }
    }

    public class FileParser
    {
        string[] lines;
        public int LineIdx { get; private set; }
        public int FieldIdx { get; private set; }

        string[] fields;

        public FileParser(string file)
        {
            LineIdx = 0;
            FieldIdx = 0;
            lines = File.ReadAllLines(file);
            NextRecord();
        }

        public void NextRecord()
        {
            if (LineIdx < lines.Length)
            {
                fields = GetFields(lines[LineIdx++]);
                FieldIdx = 0;
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


        string[] GetFields(string line)
        {
            List<string> fields = new List<string>();

            var str = "";
            bool quote = false;
            for (int i=0; i<line.Length; i++)
            {
                if (line[i] == '\"')
                {
                    quote = !quote;
                }
                else if (char.IsWhiteSpace(line[i]) && quote || !char.IsWhiteSpace(line[i]))
                {
                    str += line[i];
                }
                else
                {
                    if (str.Length > 0)
                    {
                        fields.Add(str.Trim());
                        str = "";
                    }
                }
            }

            if (str.Length > 0)
            {
                fields.Add(str.Trim());
            }

            return fields.ToArray();
        }
    }

}
