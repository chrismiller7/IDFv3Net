using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("ROUTE_OUTLINE", SectionFileType.BoardPanel)]
    class RoutingOutlineSection : AbstractSection
    {
        public Owner OutlineOwner;
        [Record]
        public RoutingLayers RoutingLayers;

        public OutlinePoint[] Points;
    }
}
