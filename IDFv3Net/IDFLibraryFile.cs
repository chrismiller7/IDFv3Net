using System;
using System.Collections.Generic;
using IDFv3Net.Attributes;
using IDFv3Net.Sections;
using System.Linq;

namespace IDFv3Net
{
    public class IDFLibraryFile : IDFFile
    {
        public LibraryHeaderSection Header { get; private set; }
        public List<ElectricalComponentSection> ElectricalComponents { get; private set; }
        public List<MechanicalComponentSection> MechanicalComponents { get; private set; }

        public IDFLibraryFile() : base()
        {
            Header = new Sections.LibraryHeaderSection();
            Header.FileType = FileType.LIBRARY_FILE;
            ElectricalComponents = new List<ElectricalComponentSection>();
            MechanicalComponents = new List<MechanicalComponentSection>();
        }

        public IDFLibraryFile(string file) : base(file, SectionFileType.Library)
        {
            Header = this.sections.OfType<LibraryHeaderSection>().SingleOrDefault();
            if (Header == null) throw new Exception("Header section not found.");
            ElectricalComponents = this.sections.OfType<ElectricalComponentSection>().ToList();
            MechanicalComponents = this.sections.OfType<MechanicalComponentSection>().ToList();
        }

        public override AbstractSection[] GetAllSections()
        {
            List<AbstractSection> list = new List<Sections.AbstractSection>();
            list.Add(Header);
            list.AddRange(ElectricalComponents);
            list.AddRange(MechanicalComponents);
            return list.ToArray();
        }
    }
}