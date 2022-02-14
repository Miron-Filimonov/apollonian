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
        }

        // отрисовка круга
        public void Draw(SKCanvas canvas)
        {
            //рандомный цвет
            Random r = new Random();
            SKColor randomColor = new SKColor((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));

            if (Radius > 0)
            {
                //обводка
                var circle = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.White
                };
                //круг
                var circleFill = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    Color = randomColor
                };
                canvas.DrawCircle(Center.X, Center.Y, Radius, circle);
                canvas.DrawCircle(Center.X, Center.Y, Radius, circleFill);
            }
        }

        public void Draw(SKCanvas canvas, SKColor color)
        {
            if (Radius > 0)
            {
                //круг
                var circle = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    Color = color
                };
                canvas.DrawCircle(Center.X, Center.Y, Radius, circle);
            }
        }
    }
}
