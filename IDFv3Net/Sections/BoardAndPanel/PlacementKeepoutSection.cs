using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("PLACE_KEEPOUT", SectionFileType.BoardPanel)]
    public class PlacementKeepoutSection : AbstractSection
    {
        public Owner KeepoutOwner = Owner.UNOWNED;
        [NextRecord]
        public PlacementBoardSide BoardSide = PlacementBoardSide.TOP;
        [LengthUnit]
        public float KeepoutHeight = 0;

        public Geometry[] Geometry = new Geometry[0];
    }
}