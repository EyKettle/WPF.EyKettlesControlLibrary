using System.Windows;
using System.Windows.Media;

namespace EyKettlesControlLibrary
{
    public static class GeometryFuction
    {
        public static Geometry CreateRoundedRectangleGeometry(Rect rect, CornerRadius cornerRadius)
        {
            var geometry = new StreamGeometry();
            using (var context = geometry.Open())
            {
                context.BeginFigure(
                    new Point(rect.X + cornerRadius.TopLeft, rect.Y), true /* is filled */, true /* is closed */);

                context.LineTo(new Point(rect.X + rect.Width - cornerRadius.TopRight, rect.Y), true, true);
                context.ArcTo(new Point(rect.X + rect.Width, rect.Y + cornerRadius.TopRight),
                    new Size(cornerRadius.TopRight, cornerRadius.TopRight), 0, false, SweepDirection.Clockwise, true, true);

                context.LineTo(new Point(rect.X + rect.Width, rect.Y + rect.Height - cornerRadius.BottomRight), true, true);
                context.ArcTo(new Point(rect.X + rect.Width - cornerRadius.BottomRight, rect.Y + rect.Height),
                    new Size(cornerRadius.BottomRight, cornerRadius.BottomRight), 0, false, SweepDirection.Clockwise, true, true);

                context.LineTo(new Point(rect.X + cornerRadius.BottomLeft, rect.Y + rect.Height), true, true);
                context.ArcTo(new Point(rect.X, rect.Y + rect.Height - cornerRadius.BottomLeft),
                    new Size(cornerRadius.BottomLeft, cornerRadius.BottomLeft), 0, false, SweepDirection.Clockwise, true, true);

                context.LineTo(new Point(rect.X, rect.Y + cornerRadius.TopLeft), true, true);
                context.ArcTo(new Point(rect.X + cornerRadius.TopLeft, rect.Y),
                    new Size(cornerRadius.TopLeft, cornerRadius.TopLeft), 0, false, SweepDirection.Clockwise, true, true);
            }

            geometry.Freeze();
            return geometry;
        }
        public static Geometry CreateSmoothRoundedRectangleGeometry(Rect rect, CornerRadius cornerRadius)
        {
            var width = rect.Width;
            var height = rect.Height;
            PathGeometry pathGeometry = new();

            PathFigure pathFigure = new()
            {
                StartPoint = new(cornerRadius.TopLeft + rect.Left, rect.Top)
            };

            pathFigure.Segments.Add(new LineSegment(new Point(width - cornerRadius.TopRight + rect.Left, rect.Top), true));
            pathFigure.Segments.Add(new BezierSegment(
                new Point(width - cornerRadius.TopRight + cornerRadius.TopRight * 2 / 3 + rect.Left, rect.Top),
                new Point(width + rect.Left, cornerRadius.TopRight - cornerRadius.TopRight * 2 / 3 + rect.Top),
                new Point(width + rect.Left, cornerRadius.TopRight + rect.Top),
                true));

            pathFigure.Segments.Add(new LineSegment(new Point(width + rect.Left, height - cornerRadius.BottomRight + rect.Top), true));
            pathFigure.Segments.Add(new BezierSegment(
                new Point(width + rect.Left, height - cornerRadius.BottomRight + cornerRadius.BottomRight * 2 / 3 + rect.Top),
                new Point(width - cornerRadius.BottomRight + cornerRadius.BottomRight * 2 / 3 + rect.Left, height + rect.Top),
                new Point(width - cornerRadius.BottomRight + rect.Left, height + rect.Top),
                true));

            pathFigure.Segments.Add(new LineSegment(new Point(cornerRadius.BottomLeft + rect.Left, height + rect.Top), true));
            pathFigure.Segments.Add(new BezierSegment(
                new Point(cornerRadius.BottomLeft - cornerRadius.BottomLeft * 2 / 3 + rect.Left, height + rect.Top),
                new Point(rect.Left, height - cornerRadius.BottomLeft + cornerRadius.BottomLeft * 2 / 3 + rect.Top),
                new Point(rect.Left, height - cornerRadius.BottomLeft + rect.Top),
                true));

            pathFigure.Segments.Add(new LineSegment(new Point(rect.Left, cornerRadius.TopLeft + rect.Top), true));
            pathFigure.Segments.Add(new BezierSegment(
                new Point(rect.Left, cornerRadius.TopLeft - cornerRadius.TopLeft * 2 / 3 + rect.Top),
                new Point(cornerRadius.TopLeft - cornerRadius.TopLeft * 2 / 3 + rect.Left, rect.Top),
                new Point(cornerRadius.TopLeft + rect.Left, rect.Top),
                true));

            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;
        }
    }
}