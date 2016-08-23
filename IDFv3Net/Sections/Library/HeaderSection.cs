using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("HEADER", SectionFileType.Library)]
    public class LibraryHeaderSection : AbstractSection
    {
        [NextRecord]
        public FileType FileType = FileType.LIBRARY_FILE;
        public float IDFVersionNumber = 0;
        public string SourceSystemId = "";
        public string Date= "";
        public int LibraryFileVersion = 0;
    }
}
