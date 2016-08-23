using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("ROUTE_KEEPOUT", SectionFileType.BoardPanel)]
    public class RoutingKeepoutSection : AbstractSection
    {
        public Owner KeepoutOwner = Owner.UNOWNED;
        [NextRecord]
        public RoutingLayers RoutingLayers = RoutingLayers.ALL;

        public Geometry[] Geometry = new Geometry[0];
    }
}