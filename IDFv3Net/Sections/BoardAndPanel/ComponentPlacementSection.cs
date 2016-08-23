using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("PLACEMENT", SectionFileType.BoardPanel)]
    public class ComponentPlacementSection: AbstractSection
    {
        public ComponentPlacement[] Placements = new ComponentPlacement[0];
    }

    public class ComponentPlacement
    {
        [NextRecord]
        public string PackageName = "";
        public string PartNumber = "";
        public string ReferenceDesignator = "";

        [NextRecord]
        public Point Point = new Point();

        [LengthUnit]
        public float MountingOffset=0;
        public float RotationAngle=0;
        public OtherOutlineBoardSide SideOfBoard = OtherOutlineBoardSide.TOP;
        public PlacementStatus PlacementStatus = PlacementStatus.PLACED;
    }
}