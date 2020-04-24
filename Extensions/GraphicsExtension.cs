using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PityuTool.UI.Extensions
{
    public static class GraphicsExtension
    {

        public static void DrawDir(this Graphics g, int blur, int width, int height, int pureWidth, int pureHeight, Color color)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0), new Point(blur, 0), Color.Transparent, color))
            {
                if (g == null)
                {
                    throw new ArgumentNullException(nameof(g), PityuResource.GraphicsError);
                }
                g.FillRectangle(brush, 0, blur, blur, pureHeight);
                brush.RotateTransform(90);
                g.FillRectangle(brush, blur, 0, pureWidth, blur);
                brush.ResetTransform();
                brush.TranslateTransform(width % (blur), height % (blur));
                brush.RotateTransform(180);
                g.FillRectangle(brush, width - blur, blur, blur, pureHeight);
                brush.RotateTransform(90);
                g.FillRectangle(brush, blur, height - blur, pureWidth, blur);
            }
        }

        public static void DrawCorner(this Graphics g, int blur, int width, int height, Color color)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                if (g == null)
                {
                    throw new ArgumentNullException(nameof(g), PityuResource.GraphicsError);
                }
                path.AddEllipse(0, 0, blur * 2,
                    blur * 2);


                using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
                {
                    pathGradientBrush.CenterColor = color;
                    pathGradientBrush.SurroundColors = new[] { Color.Transparent };
                    pathGradientBrush.CenterPoint = new Point(blur, blur);


                    g.FillPie(pathGradientBrush, 0, 0, blur * 2, blur * 2, 180, 90);

                    Matrix matrix = new Matrix();
                    matrix.Translate(width - blur * 2, 0);
                    pathGradientBrush.Transform = matrix;
                    g.FillPie(pathGradientBrush, width - blur * 2, 0, blur * 2, blur * 2, 270, 90);

                    matrix.Translate(0, height - blur * 2);
                    pathGradientBrush.Transform = matrix;
                    g.FillPie(pathGradientBrush, width - blur * 2, height - blur * 2, blur * 2, blur * 2, 0, 90);

                    matrix.Reset();
                    matrix.Translate(0, height - blur * 2);
                    pathGradientBrush.Transform = matrix;
                    g.FillPie(pathGradientBrush, 0, height - blur * 2, blur * 2, blur * 2, 90, 90);
                }


            }


        }


        public static void DrawSimpleCorner(this Graphics g, int blur, int width, int height, Color color)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                if (g == null)
                {
                    throw new ArgumentNullException(nameof(g), PityuResource.GraphicsError);
                }
                Rectangle rectangle = new Rectangle(0, 0, 2 * blur, 2 * blur);
                path.AddRectangle(rectangle);

                using (PathGradientBrush pathGradientBrush = new PathGradientBrush(path))
                {
                    g.PixelOffsetMode = PixelOffsetMode.Half;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    pathGradientBrush.CenterColor = color;
                    pathGradientBrush.SurroundColors = new[] { Color.Transparent };
                    pathGradientBrush.CenterPoint = new Point(blur, blur);
                    pathGradientBrush.WrapMode = WrapMode.Clamp;

                    g.FillRectangle(pathGradientBrush, 0, 0, blur, blur);

                    Matrix matrix = new Matrix();
                    matrix.Translate(width, 0);
                    matrix.Rotate(90);
                    pathGradientBrush.Transform = matrix;
                    g.FillRectangle(pathGradientBrush, width - blur, 0, blur, blur);

                    matrix.Reset();
                    matrix.Translate(0, height);
                    matrix.Rotate(270);
                    pathGradientBrush.Transform = matrix;
                    g.FillRectangle(pathGradientBrush, 0, height - blur, blur, blur);

                    matrix.Reset();
                    matrix.Translate(width, height);
                    matrix.Rotate(180);
                    pathGradientBrush.Transform = matrix;
                    g.FillRectangle(pathGradientBrush, width - blur, height - blur, blur, blur);
                }


            }

        }
    }
}
