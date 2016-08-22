using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("DRILLED_HOLES", SectionFileType.BoardPanel)]
    class DrilledHolesSection : AbstractSection
    {
        public DrilledHole[] DrilledHoles;
    }

    class DrilledHole
    {
        [Record]
        public float HoleDiameter;
        public float XOfCenter;
        public float YOfCenter;
        public PlatingStyle PlatingStyle;
        public string AssociatedPart;
        public HoleType HoleType;
        public Owner HoleOwner;
    }
}