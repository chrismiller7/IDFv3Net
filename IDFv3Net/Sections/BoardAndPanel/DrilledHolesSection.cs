using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("DRILLED_HOLES", SectionFileType.BoardPanel)]
    public class DrilledHolesSection : AbstractSection
    {
        public DrilledHole[] DrilledHoles = new DrilledHole[0];
    }

    public class DrilledHole
    {
        [NextRecord]
        [LengthUnit]
        public float HoleDiameter=0;
        public Point CenterPoint = new Point();
        public PlatingStyle PlatingStyl = PlatingStyle.PTH;
        public string AssociatedPart = "";
        public HoleType HoleType = HoleType.Other;
        public Owner HoleOwner = Owner.UNOWNED;
    }
}