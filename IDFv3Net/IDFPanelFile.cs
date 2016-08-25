using System;
using System.Collections.Generic;
using System.Linq;
using IDFv3Net.Sections;

namespace IDFv3Net
{
    public class IDFPanelFile : IDFBoardPanelCommonFile
    {
        public PanelOutlineSection PanelOutline { get; private set; }

        public IDFPanelFile() : base(FileType.PANEL_FILE)
        {
            PanelOutline = new PanelOutlineSection();
        }

        public IDFPanelFile(string file) : base(file)
        {
            if (Header.FileType != FileType.PANEL_FILE) throw new Exception("FileType is not a Panel file.");
            PanelOutline = this.sections.OfType<PanelOutlineSection>().SingleOrDefault();
            if (PanelOutline == null) throw new Exception("PanelOutline section not found in file");
        }

        public override AbstractSection[] GetAllSections()
        {
            List<AbstractSection> list = new List<AbstractSection>(this.GetCommonSections());
            list.Insert(1, PanelOutline);
            return list.ToArray();
        }
    }
}