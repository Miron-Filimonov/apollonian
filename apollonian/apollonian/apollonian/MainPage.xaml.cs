using Xamarin.Forms;
using SkiaSharp;
using System;
using SkiaSharp.Views.Forms;

namespace apollonian
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            var view = sender as SKCanvasView;
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Black);
            MakeImage(canvas);
        }

        private void MakeImage(SKCanvas canvas)
        {
            float imageWidth = Math.Min(App.ScreenWidth, App.ScreenHeight);
            float imageHeight = Math.Max(App.ScreenWidth, App.ScreenHeight);
            FindApollonianPacking(canvas, imageWidth, imageHeight);
        }

        private void FindApollonianPacking(SKCanvas canvas, float width, float height)
        {
            // 3 центральных круга
            float radius = width * 0.225f;
            float x = width / 2;
            float y = height / 2 - radius;
            Circle circle0 = new Circle(x, y, radius);
            x -= radius;
            y += (float)(radius * Math.Sqrt(3));
            Circle circle1 = new Circle(x, y, radius);
            x += 2 * radius;
            Circle circle2 = new Circle(x, y, radius);

            // круг включающий в себя все другие круги
            Circle big_circle = FindApollonianCircle(
                circle0, circle1, circle2, -1, -1, -1);

            // нариовать круги
            big_circle.Draw(canvas, SKColors.Bisque);
            circle0.Draw(canvas, SKColors.LightBlue);
            circle1.Draw(canvas, SKColors.LightGreen);
            circle2.Draw(canvas, SKColors.LightPink);
        }

        private Circle FindApollonianCircle(Circle c1, Circle c2, Circle c3, int s1, int s2, int s3)
        {
            const float tiny = 0.0001f;
            if ((Math.Abs(c2.Center.X - c1.Center.X) < tiny) ||
                (Math.Abs(c2.Center.Y - c1.Center.Y) < tiny))
            {
                Circle temp_circle = c2;
                c2 = c3;
                c3 = temp_circle;
                int temp_s = s2;
                s2 = s3;
                s3 = temp_s;
            }
            if ((Math.Abs(c2.Center.X - c3.Center.X) < tiny) ||
                (Math.Abs(c2.Center.Y - c3.Center.Y) < tiny))
            {
                Circle temp_circle = c2;
                c2 = c1;
                c1 = temp_circle;
                int temp_s = s2;
                s2 = s1;
                s1 = temp_s;
            }

            float x1 = c1.Center.X;
            float y1 = c1.Center.Y;
            float r1 = c1.Radius;
            float x2 = c2.Center.X;
            float y2 = c2.Center.Y;
            float r2 = c2.Radius;
            float x3 = c3.Center.X;
            float y3 = c3.Center.Y;
            float r3 = c3.Radius;

            float v11 = 2 * x2 - 2 * x1;
            float v12 = 2 * y2 - 2 * y1;
            float v13 = x1 * x1 - x2 * x2 + y1 * y1 - y2 * y2 - r1 * r1 + r2 * r2;
            float v14 = 2 * s2 * r2 - 2 * s1 * r1;

            float v21 = 2 * x3 - 2 * x2;
            float v22 = 2 * y3 - 2 * y2;
            float v23 = x2 * x2 - x3 * x3 + y2 * y2 - y3 * y3 - r2 * r2 + r3 * r3;
            float v24 = 2 * s3 * r3 - 2 * s2 * r2;

            float w12 = v12 / v11;
            float w13 = v13 / v11;
            float w14 = v14 / v11;

            float w22 = v22 / v21 - w12;
            float w23 = v23 / v21 - w13;
            float w24 = v24 / v21 - w14;

            float P = -w23 / w22;
            float Q = w24 / w22;
            float M = -w12 * P - w13;
            float N = w14 - w12 * Q;

            float a = N * N + Q * Q - 1;
            float b = 2 * M * N - 2 * N * x1 + 2 * P * Q - 2 * Q * y1 + 2 * s1 * r1;
            float c = x1 * x1 + M * M - 2 * M * x1 + P * P + y1 * y1 - 2 * P * y1 - r1 * r1;

            double[] solutions = QuadraticSolutions(a, b, c);
            if (solutions.Length < 1) return null;
            float rs = (float)solutions[0];
            float xs = M + N * rs;
            float ys = P + Q * rs;


            if ((Math.Abs(xs) < tiny) || (Math.Abs(ys) < tiny) || (Math.Abs(rs) < tiny)) return null;
            return new Circle(xs, ys, rs);
        }

        private double[] QuadraticSolutions(double a, double b, double c)
        {
            const double tiny = 0.000001;
            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
                return new double[] { };

            if (discriminant < tiny)
                return new double[] { -b / (2 * a) };

            return new double[]
            {
                (-b + Math.Sqrt(discriminant)) / (2 * a),
                (-b - Math.Sqrt(discriminant)) / (2 * a),
            };
        }
    }
}
