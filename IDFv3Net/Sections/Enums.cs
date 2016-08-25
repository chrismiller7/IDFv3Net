namespace IDFv3Net.Sections
{
    public enum FileType
    {
        BOARD_FILE,
        PANEL_FILE,
        LIBRARY_FILE,
    }

    public enum Units
    {
        MM, THOU,
    }

    public enum Owner
    {
        MCAD,ECAD,UNOWNED,
    }

    public enum ComponentPlacementSide
    {
        TOP,BOTTOM
    }

    public enum PlacementBoardSide
    {
        TOP, BOTTOM, BOTH
    }

    public enum RoutingLayers
    {
        TOP,BOTTOM,BOTH,INNER,ALL
    }

    public enum PlatingStyle
    {
        PTH, NPTH,
    }

    public enum HoleType
    {
        PIN,VIA,MTG,TOOL,Other
    }

    public enum PlacementStatus
    {
        PLACED,UNPLACED,MCAD,ECAD
    }
}
