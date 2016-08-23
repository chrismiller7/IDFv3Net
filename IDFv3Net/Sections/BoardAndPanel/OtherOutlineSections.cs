using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("OTHER_OUTLINE", SectionFileType.BoardPanel)]
    public class OtherOutlineSection : AbstractSection
    {
        public Owner OutlineOwner = Owner.UNOWNED;
        [NextRecord]
        public string OutlineID = "";
        [LengthUnit]
        public float ExtrudeThickness=0;
        public OtherOutlineBoardSide BoardSide = OtherOutlineBoardSide.TOP;

        public Geometry[] Geometry = new  Geometry[0];
    }
}
