using System;
using System.Collections.Generic;
using System.Linq;
using IDFv3Net.Sections;

namespace IDFv3Net.Extensions
{
    public static class GeometeryExtensions
    {
        public static void FlipHorizontal(this IDFFile file)
        {
            GetAllPoints(file).ToList().ForEach(s => s.X = -s.X);

            var compPlaceSection = file.GetAllSections().OfType<ComponentPlacementSection>().FirstOrDefault();
            if (compPlaceSection != null)
            {
                foreach (var placement in compPlaceSection.Placements)
                {
                    placement.RotationAngle *= -1;
                }
            }
        }

        public static void FlipVertical(this IDFFile file)
        {
            GetAllPoints(file).ToList().ForEach(s => s.Y = -s.Y);

            var compPlaceSection = file.GetAllSections().OfType<ComponentPlacementSection>().FirstOrDefault();
            if (compPlaceSection != null)
            {
                foreach (var placement in compPlaceSection.Placements)
                {
                    placement.RotationAngle *= -1;
                }
            }
        }

        public static void Translate(this IDFFile file, float X, float Y)
        {
            GetAllPoints(file).ToList().ForEach(s => { s.X += X; s.Y += Y; });
        }

        public static void Scale(this IDFFile file, float xPer, float yPer)
        {
            GetAllPoints(file).ToList().ForEach(s => { s.X *= xPer; s.Y *= yPer; });
        }

        public static void Rotate(this IDFFile file, float angleDegrees)
        {
            var rads = angleDegrees * 2.0f * Math.PI / 360.0f;
            foreach (var pt in GetAllPoints(file))
            {
                var nx = pt.X * Math.Cos(rads) - pt.Y * Math.Sin(rads);
                var ny = pt.Y * Math.Cos(rads) + pt.X * Math.Sin(rads);
                pt.X = (float)nx;
                pt.Y = (float)ny;
            }

            var compPlaceSection = file.GetAllSections().OfType<ComponentPlacementSection>().FirstOrDefault();
            if (compPlaceSection != null)
            {
                foreach (var placement in compPlaceSection.Placements)
                {
                    placement.RotationAngle += angleDegrees;
                }
            }
        }

        public static void Rotate(this IDFFile file, float angleDegrees, float centerX, float centerY)
        {
            Translate(file, -centerX, -centerY);
            Rotate(file, angleDegrees);
            Translate(file, centerX, centerY);
        }

        public static BoundingBox GetBoundingBox(this IDFFile file)
        {
            var pts = GetAllPoints(file);
            var min = new Point();
            var max = new Point();

            min.X = pts.Min(p => p.X);
            min.Y = pts.Min(p => p.Y);
            max.X = pts.Max(p => p.X);
            max.Y = pts.Max(p => p.Y);
            return new BoundingBox() { Min = min, Max = max };
        }

        public static Point GetCenterPoint(this IDFFile file)
        {
            var bounds = GetBoundingBox(file);
            Point center = new Point();
            center.X = (bounds.Min.X + bounds.Max.X) / 2.0f;
            center.Y = (bounds.Min.Y + bounds.Max.Y) / 2.0f;
            return center;
        }

        public static void Center(this IDFFile file)
        {
            var center = GetCenterPoint(file);
            Translate(file, -center.X, -center.Y);
        }

        public static Size GetSize(this IDFFile file)
        {
            var bounds = GetBoundingBox(file);
            Size size = new Size();
            size.Width = bounds.Max.X - bounds.Min.X;
            size.Height = bounds.Max.Y - bounds.Min.Y;
            return size;
        }

        public static Geometry[] GetTranslation(this Geometry[] geom, float x, float y)
        {
            List<Geometry> list = new List<Sections.Geometry>();
            foreach (var pt in geom)
            {
                Geometry g = new Sections.Geometry();
                g.Point.X = pt.Point.X + x;
                g.Point.Y = pt.Point.Y + y;
                g.Angle = pt.Angle;
                g.LoopLabel = pt.LoopLabel;
                list.Add(g);
            }
            return list.ToArray();
        }

        public static Geometry[] GetRotation(this Geometry[] geom, float angleDegrees)
        {
            List<Geometry> list = new List<Sections.Geometry>();

            var rads = angleDegrees * 2.0f * Math.PI / 360.0f;
            foreach (var pt in geom)
            {
                var g = new Geometry();
                g.Point.X = (float)(pt.Point.X * Math.Cos(rads) - pt.Point.Y * Math.Sin(rads));
                g.Point.Y = (float)(pt.Point.Y * Math.Cos(rads) + pt.Point.X * Math.Sin(rads));
                g.Angle = pt.Angle;
                g.LoopLabel = pt.LoopLabel;
                list.Add(g);
            }

            return list.ToArray();
        }

        public static IEnumerable<Point> GetAllPoints(this IDFFile file)
        {
            List<Point> list = new List<Point>();
            list.AddRange(GetAllGeometry(file).Select(s => s.Point));
            
            list.AddRange(file.GetAllSections().OfType<ComponentPlacementSection>().SelectMany(s => s.Placements).Select(s => s.Point));
            list.AddRange(file.GetAllSections().OfType<DrilledHolesSection>().SelectMany(s => s.DrilledHoles).Select(s => s.CenterPoint));
            list.AddRange(file.GetAllSections().OfType<NotesSection>().SelectMany(s => s.Notes).Select(s => s.Point));

            return list;
        }

        public static IEnumerable<Geometry> GetAllGeometry(this IDFFile file)
        {
            List<Geometry> list = new List<Geometry>();
            list.AddRange(file.GetAllSections().OfType<BoardOutlineSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<PanelOutlineSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<OtherOutlineSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<PlacementGroupAreaSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<PlacementKeepoutSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<PlacementOutlineSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<RoutingKeepoutSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<RoutingOutlineSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<ViaKeepoutSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<ElectricalComponentSection>().SelectMany(s => s.Geometry));
            list.AddRange(file.GetAllSections().OfType<MechanicalComponentSection>().SelectMany(s => s.Geometry));
            return list;
        }
    }

    public class BoundingBox
    {
        public Point Min = new Point();
        public Point Max = new Point();
    }

    public class Size
    {
        public float Width;
        public float Height;
    }

}
