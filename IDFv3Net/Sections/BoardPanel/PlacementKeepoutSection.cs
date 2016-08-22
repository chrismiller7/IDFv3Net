using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("PLACE_KEEPOUT", SectionFileType.BoardPanel)]
    class PlacementKeepoutSection : AbstractSection
    {
        public Owner KeepoutOwner;
        [Record]
        public PlacementBoardSide BoardSide;
        public float KeepoutHeight;

        public OutlinePoint[] Points;
    }
}