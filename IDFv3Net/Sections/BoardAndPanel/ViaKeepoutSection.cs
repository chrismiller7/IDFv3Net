using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("VIA_KEEPOUT", SectionFileType.BoardPanel)]
    public class ViaKeepoutSection : AbstractSection
    {
        public Owner KeepoutOwner = Owner.UNOWNED;

        public Geometry[] Geometry = new Geometry[0];
    }
}