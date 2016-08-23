using System.Linq;
using IDFv3Net.Sections;

namespace IDFv3Net.Extensions
{
    public static class HeaderExtensions
    {
        public static AbstractSection GetHeader(this IDFFile idf)
        {
            var boardPanelHeader = idf.GetAllSections().OfType<HeaderSection>().SingleOrDefault();
            if (boardPanelHeader != null)
            {
                return boardPanelHeader;
            }
            var libraryHeader = idf.GetAllSections().OfType<LibraryHeaderSection>().SingleOrDefault();
            if (libraryHeader != null)
            {
                return libraryHeader;
            }
            return null;
        }

        public static FileType GetFileType(this IDFFile idf)
        {
            var header = GetHeader(idf);
            if (header is HeaderSection)
            {
                return ((HeaderSection)header).FileType;
            }
            else
            {
                return ((LibraryHeaderSection)header).FileType;
            }
        }

        public static float VersionNumber(this IDFFile idf)
        {
            var header = GetHeader(idf);
            if (header is HeaderSection)
            {
                return ((HeaderSection)header).IDFVersionNumber;
            }
            else
            {
                return ((LibraryHeaderSection)header).IDFVersionNumber;
            }
        }
    }
}
