using System;
using System.Linq;
using IDFv3Net.Sections;
using System.IO;
using System.Reflection;
using IDFv3Net.Attributes;

namespace IDFv3Net.Extensions
{
    public static class ExportExtensions
    {

        public static void SaveAs(this IDFFile idf, string filename )
        {
            var dt = DateTime.Now.ToString("yyyy/MM/dd.hh:mm:ss");

            var h1 = idf.GetAllSections().OfType<HeaderSection>().SingleOrDefault();
            if (h1 != null)
            {
                h1.Date = dt;
                h1.BoardName = filename;
            }
            var h2 = idf.GetAllSections().OfType<LibraryHeaderSection>().SingleOrDefault();
            if (h2 != null)
            {
                h2.Date = dt;
            }
            using (var file = new StreamWriter(filename))
            {
                foreach(var section in idf.GetAllSections())
                {
                    WriteSection(file, section);
                }
            }
        }

        static void WriteSection(StreamWriter file, AbstractSection section)
        {
            file.Write("." + section.SectionName + " ");
            WriteFields(file, section);
            file.WriteLine();
            file.WriteLine(".END_" + section.SectionName);
        }

        static void WriteFields(StreamWriter file, object obj)
        {
            foreach (var field in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                if (field.GetCustomAttributes(true).OfType<NextRecordAttribute>().Count() > 0)
                {
                    file.WriteLine();
                }

                var type = field.FieldType;
                if (type.IsArray)
                {
                    var array = (Array)field.GetValue(obj);
                    if (array != null)
                    {
                        foreach (var item in array)
                        {
                            WriteFields(file, item);
                        }
                    }
                }
                else if (type == typeof(string))
                {
                    var val = field.GetValue(obj);
                    file.Write("\"{0}\" ", val);
                }
                else if (type == typeof(int) || type == typeof(float) || type.IsEnum)
                {
                    var val = field.GetValue(obj);
                    file.Write(val + " ");
                }
                else
                {
                    var val = field.GetValue(obj);
                    if (val != null)
                    {
                        WriteFields(file, val);
                    }
                }
            }
        }
    }
}
