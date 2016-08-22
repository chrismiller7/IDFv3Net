using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("MECHANICAL", SectionFileType.Library)]
    class MechanicalComponentSection : AbstractSection
    {
        [Record]
        public string GeometeryName;
        public string PartNumber;
        public Units Units;
        public float ComponentHeight;

        public OutlinePoint[] Points;
    }
}
