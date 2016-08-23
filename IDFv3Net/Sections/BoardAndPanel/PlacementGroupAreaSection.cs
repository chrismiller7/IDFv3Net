using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("PLACE_REGION", SectionFileType.BoardPanel)]
    public class PlacementGroupAreaSection : AbstractSection
    {
        public Owner KeepoutOwner = Owner.UNOWNED;
        [NextRecord]
        public PlacementBoardSide BoardSide = PlacementBoardSide.TOP;
        public string ComponentGroupName = "";

        public Geometry[] Geometry = new Geometry[0];
    }
}