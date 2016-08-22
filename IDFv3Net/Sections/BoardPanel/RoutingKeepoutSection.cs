using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("ROUTE_KEEPOUT", SectionFileType.BoardPanel)]
    class RoutingKeepoutSection : AbstractSection
    {
        public Owner KeepoutOwner;
        [Record]
        public RoutingLayers RoutingLayers;

        public OutlinePoint[] Points;
    }
}