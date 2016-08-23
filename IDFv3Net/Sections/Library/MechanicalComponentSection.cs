using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("MECHANICAL", SectionFileType.Library)]
    public class MechanicalComponentSection : AbstractSection
    {
        [NextRecord]
        public string GeometeryName = "";
        public string PartNumber = "";
        public Units Units = Units.THOU;
        [LengthUnit]
        public float ComponentHeight = 0;

        public Geometry[] Geometry = new Geometry[0];
    }
}