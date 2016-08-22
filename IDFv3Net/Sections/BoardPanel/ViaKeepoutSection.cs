using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("VIA_KEEPOUT", SectionFileType.BoardPanel)]
    class ViaKeepoutSection : AbstractSection
    {
        public Owner KeepoutOwner;

        public OutlinePoint[] Points;
    }
}