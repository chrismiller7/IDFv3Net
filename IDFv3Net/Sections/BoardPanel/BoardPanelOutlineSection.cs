using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    [SectionName("BOARD_OUTLINE", SectionFileType.BoardPanel)]
    class BoardOutlineSection : AbstractSection
    {
        public Owner OutlineOwner;
        [Record]
        public float Thickness;
       
        public OutlinePoint[] Points;
    }

    [SectionName("PANEL_OUTLINE", SectionFileType.BoardPanel)]
    class PanelOutlineSection : AbstractSection
    {
        public Owner OutlineOwner;
        [Record]
        public float Thickness;

        public OutlinePoint[] Points;
    }

    class OutlinePoint
    {
        [Record]
        public int LoopLabel;
        public float X;
        public float Y;
        public float Angle;

        public override bool Equals(object obj)
        {
            OutlinePoint A = obj as OutlinePoint;
            if (A != null)
            {
                return A.LoopLabel == this.LoopLabel && A.X == this.X && A.Y == this.Y && A.Angle == this.Angle;
            }
            return false;
        }
    }
}