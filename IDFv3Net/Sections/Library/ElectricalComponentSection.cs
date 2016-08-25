using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("ELECTRICAL", SectionFileType.Library)]
    public class ElectricalComponentSection : AbstractSection
    {
        [NextRecord]
        public string GeometeryName = "";
        public string PartNumber = "";
        public Units Units = Units.THOU;
        [LengthUnit]
        public float ComponentHeight = 0;

        public Geometry[] Geometry = new Geometry[0];

        public Property[] Properties = new Property[0];
    }

    public class Property
    {
        [NextRecord]
        public string PropertyKeyword = "PROP";
        public string PropertyName = "";
        public string PropertyValue = "";
    }

}
