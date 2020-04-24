using PityuTool.UI.Misc;
using PityuTool.UI.Repository;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class LayeredBitmapService : ILayeredBitmap
    {
        private readonly Form form;

        public LayeredBitmapService(Form form)
        {
            this.form = form;
        }

        public void DrawShadow(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new FormatException(PityuResource.BitmapError);


            IntPtr screenDc = NativeMethods.GetDC(IntPtr.Zero);
            IntPtr memDc = NativeMethods.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = NativeMethods.SelectObject(memDc, hBitmap);

                Size size = new Size(bitmap.Width, bitmap.Height);
                Point pointSource = new Point(0, 0);
                Point topPos = new Point(form.Left, form.Top);
                NativeMethods.Blend blend = new NativeMethods.Blend
                {
                    BlendOp = NativeMethods.AC_SRC_OVER,
                    BlendFlags = 0,
                    SourceConstantAlpha = opacity,
                    AlphaFormat = NativeMethods.AC_SRC_ALPHA
                };

                NativeMethods.UpdateLayeredWindow(form.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend,
                    NativeMethods.ULW_ALPHA);
            }
            finally
            {
                NativeMethods.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    NativeMethods.SelectObject(memDc, oldBitmap);
                    NativeMethods.DeleteObject(hBitmap);
                }
                NativeMethods.DeleteDC(memDc);
            }

        }

    }
}
