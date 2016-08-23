using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("HEADER", SectionFileType.BoardPanel)]
    public class HeaderSection : AbstractSection
    {
        [NextRecord]
        public FileType FileType = FileType.BOARD_FILE;
        public float IDFVersionNumber = 0;
        public string SourceSystemId = "";
        public string Date = "";
        public int BoardFileVersion = 0;
        [NextRecord]
        public string BoardName = "";
        public Units Units = Units.THOU;
    }
}
