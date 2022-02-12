using System;
using SkiaSharp;
using Xamarin.Forms;

namespace apollonian
{
    class Circle
    {
        public Point Center;
        public float Radius;

        public Circle(float new_x, float new_y, float new_radius)
        {
            Center = new Point(new_x, new_y);
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
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.Black
                };
                var circleFill = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    Color = color
                };
                canvas.DrawCircle((float)Center.X, (float)Center.Y, Radius, circle);
                canvas.DrawCircle((float)Center.X, (float)Center.Y, Radius, circleFill);
            }
        }
    }
}
