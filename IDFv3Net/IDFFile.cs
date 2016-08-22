using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using IDFv3Net.Attributes;
using IDFv3Net.Internal;
using IDFv3Net.Sections;
using System.Linq;
using System.IO;

namespace IDFv3Net
{
    public class IDFFile
    {
        public AbstractSection[] Sections { get; private set; }

        public IDFFile(string file)
        {
            var ext = Path.GetExtension(file);
            if (ext.ToLower() == ".emn")
            {
                ReadFile(file, SectionFileType.BoardPanel);
            }
            else if (ext.ToLower() == ".emp")
            {
                ReadFile(file, SectionFileType.Library);
            }
            else
            {
                throw new Exception("File extension not recognized: " + ext);
            }
        }

        public IDFFile(string file, SectionFileType fileType)
        {
            ReadFile(file, fileType);
        }

        void ReadFile(string file, SectionFileType fileType)
        {
            IDFFileParser parser = new IDFFileParser(file);
            this.Sections = parser.Sections.Select(s => ReadSection(s, fileType)).ToArray();
        }

        public IDFFile(AbstractSection[] sections)
        {
            this.Sections = sections;
        }

        AbstractSection ReadSection(IDFFileSection parser, SectionFileType fileType)
        {
            var sectionName = parser.NextField();

            var section = CreateSectionObjectFromName(sectionName, fileType);
            if (section != null)
            {
                PopulateFields(parser, section);
            }

            parser.NextRecord();
            return section;
        }

        void PopulateFields(IDFFileSection parser, object section)
        {
            foreach (var field in section.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.GetCustomAttribute<RecordAttribute>() != null)
                {
                    parser.NextRecord();
                }

                if (field.FieldType.IsArray)
                {
                    if (parser.EndOfFile) return;

                    var elementType = field.FieldType.GetElementType();
                    Type listType = typeof(List<>).MakeGenericType(elementType);
                    IList list = (IList)Activator.CreateInstance(listType);
                    while (true)
                    {
                        var item = Activator.CreateInstance(elementType);
                        PopulateFields(parser, item);
                        list.Add(item);
                        if (parser.EndOfFile || list.Count >= 2 && AreItemsSame(list[0], list[list.Count-1]))
                        {
                            break;
                        }
                    }
                    Array arr = Array.CreateInstance(elementType, list.Count);
                    list.CopyTo(arr, 0);
                    field.SetValue(section, arr);
                }
                else
                {
                    var fieldIdx = parser.FieldIdx;
                    var fieldStr = parser.NextField();

                    SetFieldValue(field, section, fieldStr);
                }
            }
        }

        bool AreItemsSame(object A, object B)
        {
            return A.Equals(B);
        }

        void SetFieldValue(FieldInfo field, object section, string fieldStr)
        {
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
            else if (field.FieldType.IsEnum)
            {
                var val = Enum.Parse(field.FieldType, fieldStr, true);
                field.SetValue(section, val);
            }
            else
            {
                throw new Exception("Type not supported: " + field.FieldType);
            }
        }

        AbstractSection CreateSectionObjectFromName(string sectionName, SectionFileType fileType)
        {
            foreach (Type type in Assembly.GetAssembly(typeof(SectionNameAttribute)).GetTypes())
            {
                var section = type.GetCustomAttribute(typeof(SectionNameAttribute)) as SectionNameAttribute;
   
                if (section != null && section.Name.ToLower() == sectionName.ToLower().TrimStart('.') && section.FileType == fileType)
                {
                    return Activator.CreateInstance(type) as AbstractSection;
                }
            }
            return null;
        }
    }
}
