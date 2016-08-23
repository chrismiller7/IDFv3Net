using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("ROUTE_OUTLINE", SectionFileType.BoardPanel)]
    public class RoutingOutlineSection : AbstractSection
    {
        public Owner OutlineOwner = Owner.UNOWNED;
        [NextRecord]
        public RoutingLayers RoutingLayers = RoutingLayers.ALL;

        public Geometry[] Geometry = new Geometry[0];
    }
}
