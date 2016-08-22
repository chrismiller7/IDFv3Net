using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("PLACEMENT", SectionFileType.BoardPanel)]
    class ComponentPlacementSection: AbstractSection
    {
        public ComponentPlacement[] Placements;
    }

    class ComponentPlacement
    {
        [Record]
        public string PackageName;
        public string PartNumber;
        public string ReferenceDesignator;

        [Record]
        public float X;
        public float Y;
        public float MountingOffset;
        public float RotationAngle;
        public OtherOutlineBoardSide SideOfBoard;
        public PlacementStatus PlacementStatus;
    }
}