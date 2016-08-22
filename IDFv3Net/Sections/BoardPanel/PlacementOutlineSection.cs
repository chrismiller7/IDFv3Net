using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("PLACE_OUTLINE", SectionFileType.BoardPanel)]
    class PlacementOutlineSection : AbstractSection
    {
        public Owner OutlineOwner;
        [Record]
        public PlacementBoardSide BoardSide;
        public float OutlineHeight;

        public OutlinePoint[] Points;
    }
}