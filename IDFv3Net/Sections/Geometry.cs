using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    public class Geometry
    {
        [NextRecord]
        public int LoopLabel = 0;
        public Point Point = new Point();
        public float Angle = 0;

        public override bool Equals(object obj)
        {
            Geometry A = obj as Geometry;
            if (A != null)
            {
                return A.LoopLabel == this.LoopLabel && A.Point.X == this.Point.X && A.Point.Y == this.Point.Y && A.Angle == this.Angle;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Point.X.GetHashCode() + Point.Y.GetHashCode();
        }
    }
}
