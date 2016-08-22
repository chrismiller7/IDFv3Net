using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("HEADER", SectionFileType.BoardPanel)]
    class HeaderSection : AbstractSection
    {
        [Record]
        public FileType FileType;
        public float IDFVersionNumber;
        public string SourceSystemId;
        public string Date;
        public int BoardFileVersion;
        [Record]
        public string BoardName;
        public Units Units;
    }
}
