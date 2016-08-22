using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDFv3Net.Attributes
{
    class SectionNameAttribute : Attribute
    {
        public string Name { get; private set; }
        public SectionFileType FileType { get; private set; }

        public SectionNameAttribute(string name, SectionFileType type)
        {
            this.Name = name;
            this.FileType = type;
        }
    }

    public enum SectionFileType
    {
        BoardPanel,
        Library
    }
}
