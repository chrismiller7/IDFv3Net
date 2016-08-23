using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("PLACE_OUTLINE", SectionFileType.BoardPanel)]
    public class PlacementOutlineSection : AbstractSection
    {
        public Owner OutlineOwner = Owner.UNOWNED;
        [NextRecord]
        public PlacementBoardSide BoardSide = PlacementBoardSide.TOP;
        [LengthUnit]
        public float OutlineHeight = 0;

        public Geometry[] Geometry = new Geometry[0];
    }
}