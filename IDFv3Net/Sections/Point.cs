using IDFv3Net.Attributes;

namespace IDFv3Net.Sections
{
    public class Point
    {
        public Point() { }

        public Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        [LengthUnit]
        public float X = 0;
        [LengthUnit]
        public float Y = 0;
    }
}
