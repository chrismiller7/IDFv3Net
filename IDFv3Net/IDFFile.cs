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
        protected AbstractSection[] sections;

        public IDFFile()
        {
            sections = new AbstractSection[0];
        }

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

        public virtual AbstractSection[] GetAllSections()
        {
            return this.sections;
        }

        void ReadFile(string file, SectionFileType fileType)
        {
            IDFFileParser parser = new IDFFileParser(file);
            this.sections = parser.Sections.Select(s => ReadSection(s, fileType)).ToArray();
        }

        public IDFFile(AbstractSection[] sections)
        {
            this.sections = sections;
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
                if (field.GetCustomAttributes(true).OfType<NextRecordAttribute>().Count() > 0)
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
                    SetFieldValue(parser, field, section);
                }
            }
        }

        bool AreItemsSame(object A, object B)
        {
            return A.Equals(B);
        }

        void SetFieldValue(IDFFileSection parser, FieldInfo field, object section)
        {
            if (field.FieldType == typeof(string))
            {
                field.SetValue(section, parser.NextField());
            }
            else if (field.FieldType == typeof(int))
            {
                field.SetValue(section, int.Parse(parser.NextField()));
            }
            else if (field.FieldType == typeof(float))
            {
                field.SetValue(section, float.Parse(parser.NextField()));
            }
            else if (field.FieldType.IsEnum)
            {
                var val = Enum.Parse(field.FieldType, parser.NextField(), true);
                field.SetValue(section, val);
            }
            else
            {
                var obj = Activator.CreateInstance(field.FieldType);
                PopulateFields(parser, obj);
                field.SetValue(section, obj);
                //throw new Exception("Type not supported: " + field.FieldType);
            }
        }

        AbstractSection CreateSectionObjectFromName(string sectionName, SectionFileType fileType)
        {
            foreach (Type type in Assembly.GetAssembly(typeof(SectionNameAttribute)).GetTypes())
            {
                var section = type.GetCustomAttributes(true).OfType<SectionNameAttribute>().SingleOrDefault();
   
                if (section != null && section.Name.ToLower() == sectionName.ToLower().TrimStart('.') && section.FileType == fileType)
                {
                    return Activator.CreateInstance(type) as AbstractSection;
                }
            }
            return null;
        }
    }
}