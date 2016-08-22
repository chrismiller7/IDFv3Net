using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("NOTES", SectionFileType.BoardPanel)]
    class NotesSection : AbstractSection
    {
        public Note[] Notes;
    }

    class Note
    {
        [Record]
        public float X;
        public float Y;
        public float TextHeight;
        public float TextStringPhysicalLength;
        public string TextValue;
    }
}