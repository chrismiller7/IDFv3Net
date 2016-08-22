using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("HEADER", SectionFileType.Library)]
    class LibraryHeaderSection : AbstractSection
    {
        [Record]
        public FileType FileType;
        public float IDFVersionNumber;
        public string SourceSystemId;
        public string Date;
        public int LibraryFileVersion;
    }
}
