using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace EyKettlesControlLibrary
{
    public class FocusAdorner : Adorner
    {
        public FocusAdorner(UIElement adornedElement) : base(adornedElement) { }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            var buttonControl = AdornedElement as ButtonControl;
            if (buttonControl == null)
                return;

            var DefaultRect = new Rect(buttonControl.RenderSize);

            // 焦点框
            Rect focusBoxRect = new(
                DefaultRect.X - 4,
                DefaultRect.Y - 4,
                DefaultRect.Width + 8,
                DefaultRect.Height + 8);
            CornerRadius focusBoxCornerRadius = new CornerRadius(
                buttonControl.CornerRadius.TopLeft + 4,
                buttonControl.CornerRadius.TopRight + 4,
                buttonControl.CornerRadius.BottomRight + 4,
                buttonControl.CornerRadius.BottomLeft + 4);
            dc.DrawGeometry(null, new Pen(Brushes.Black, 2) { LineJoin = PenLineJoin.Round },
                GeometryFuction.CreateSmoothRoundedRectangleGeometry(focusBoxRect, focusBoxCornerRadius));
        }
    }
}