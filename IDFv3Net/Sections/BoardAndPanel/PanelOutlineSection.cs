using IDFv3Net.Attributes;
using System.Collections.Generic;

namespace IDFv3Net.Sections
{
    [SectionName("PANEL_OUTLINE", SectionFileType.BoardPanel)]
    public class PanelOutlineSection : AbstractSection
    {
        public Owner OutlineOwner = Owner.UNOWNED;
        [NextRecord]
        [LengthUnit]
        public float Thickness = 0;

        public Geometry[] Geometry = new Geometry[0];
    }
}