using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("ELECTRICAL", SectionFileType.Library)]
    class ElectricalComponentSection : AbstractSection
    {
        [Record]
        public string GeometeryName;
        public string PartNumber;
        public Units Units;
        public float ComponentHeight;

        public OutlinePoint[] Points;

        public Property[] Properties;
    }

    class Property
    {
        [Record]
        public string PropertyKeyword = "PROP";
        public string PropertyName;
        public string PropertyValue;
    }

}
