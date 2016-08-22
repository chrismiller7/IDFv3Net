using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("PLACE_REGION", SectionFileType.BoardPanel)]
    class PlacementGroupAreaSection : AbstractSection
    {
        public Owner KeepoutOwner;
        [Record]
        public PlacementBoardSide BoardSide;
        public string ComponentGroupName;

        public OutlinePoint[] Points;
    }
}