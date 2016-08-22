using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("OTHER_OUTLINE", SectionFileType.BoardPanel)]
    class OtherOutlineSection : AbstractSection
    {
        public Owner OutlineOwner;
        [Record]
        public string OutlineID;
        public float ExtrudeThickness;
        public OtherOutlineBoardSide BoardSide;

        public OutlinePoint[] Points;
    }
}
