using System;
using System.Drawing;
using SkiaSharp;

namespace apollonian
{
    class Circle
    {
        public PointF Center;
        public float Radius;

        public Circle(float new_x, float new_y, float new_radius)
        {
            Center = new PointF(new_x, new_y);
            Radius = Math.Abs(new_radius);
            Random randomGen = new Random();
        }

        public void Draw(SKCanvas canvas, SKColor color)
        {
            if (Radius > 0)
            {
                var circle = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.White
                };
                var circleFill = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    Color = color
                };
                canvas.DrawCircle(Center.X, Center.Y, Radius, circle);
                canvas.DrawCircle(Center.X, Center.Y, Radius, circleFill);
            }
        }
    }
}
