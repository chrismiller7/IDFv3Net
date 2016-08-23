using IDFv3Net.Attributes;
using System.Reflection;
using System.Linq;


namespace IDFv3Net.Sections
{
    public abstract class AbstractSection
    {
        public string SectionName
        {
            get
            {
                var attrib = this.GetType().GetCustomAttributes(true).OfType<SectionNameAttribute>().SingleOrDefault();
                return attrib.Name;
            }
        }
    }
}
