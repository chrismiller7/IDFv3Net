using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("NOTES", SectionFileType.BoardPanel)]
    public class NotesSection : AbstractSection
    {
        public Note[] Notes = new Note[0];
    }

    public class Note
    {
        [NextRecord]
        public Point Point = new Point();
        [LengthUnit]
        public float TextHeight=0;
        [LengthUnit]
        public float TextStringPhysicalLength=0;
        public string TextValue = "";
    }
}