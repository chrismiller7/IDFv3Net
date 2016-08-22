using IDFv3Net.Attributes;
using System.Reflection;

namespace IDFv3Net.Sections
{
    public abstract class AbstractSection
    {
        public string SectionName
        {
            get
            {
                var attrib = this.GetType().GetCustomAttribute(typeof(SectionNameAttribute)) as SectionNameAttribute;
                return attrib.Name;
            }
        }
    }
}
