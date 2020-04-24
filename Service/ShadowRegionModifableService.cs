using PityuTool.UI.Models;
using PityuTool.UI.Repository;
using System.Drawing;
using System.Windows.Forms;
using PityuTool.UI.Extensions;

namespace PityuTool.UI.Service
{
    sealed class ShadowRegionModifableService : IRegionModifable
    {

        private readonly ShadowLayerData shadowLayerData;

        private readonly Control baseControl;

        private readonly ILayeredBitmap layeredBitmap;

        public ShadowRegionModifableService(ShadowLayerData shadowLayerData, Control baseControl, ILayeredBitmap layeredBitmap)
        {
            this.shadowLayerData = shadowLayerData;
            this.baseControl = baseControl;
            this.layeredBitmap = layeredBitmap;
        }


        public void ModifyRegion(Control control)
        {

            Rectangle bounds = baseControl.Bounds;
            int vLoc = shadowLayerData.VOffset - (shadowLayerData.Blur + shadowLayerData.ShadowSize);
            int hLoc = shadowLayerData.HOffset - (shadowLayerData.Blur + shadowLayerData.ShadowSize);

            bounds.Inflate(shadowLayerData.ShadowSize + shadowLayerData.Blur, shadowLayerData.ShadowSize + shadowLayerData.Blur);

            control.Bounds = bounds;
            Point position = baseControl.Location;
            position.Offset(vLoc, hLoc);
            control.Location = position;
            using (Bitmap bmp = DrawShadowBitmap())
            {
                Region baseRegion = baseControl.Region == null ? new Region(baseControl.ClientRectangle) : baseControl.Region.Clone();

                Region shadowRegion = new Region(control.ClientRectangle);


                baseRegion.Translate(-vLoc, -hLoc);
                shadowRegion.Exclude(baseRegion);
                control.Region = shadowRegion;
                layeredBitmap.DrawShadow(bmp, shadowLayerData.Opacity);
                baseRegion.Dispose();
            }



        }


        private Bitmap DrawShadowBitmap()
        {
            int extended = shadowLayerData.Blur + shadowLayerData.ShadowSize;
            int width = baseControl.Width + 2 * extended;
            int height = baseControl.Height + 2 * extended;

            int pureWidth = baseControl.Width + 2 * shadowLayerData.ShadowSize;
            int pureHeight = baseControl.Height + 2 * shadowLayerData.ShadowSize;

            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {

                using (SolidBrush brush = new SolidBrush(shadowLayerData.ShadowColor))
                {
                    g.FillRectangle(brush, shadowLayerData.Blur, shadowLayerData.Blur, pureWidth, pureHeight);

                    if (shadowLayerData.Blur > 0)
                    {


                        if (shadowLayerData.Radius < 1)
                        {
                            g.DrawSimpleCorner(shadowLayerData.Blur, width, height, shadowLayerData.ShadowColor);
                            g.DrawDir(shadowLayerData.Blur, width, height, pureWidth, pureHeight, shadowLayerData.ShadowColor);
                        }
                        else
                        {

                            g.DrawCorner(shadowLayerData.Blur, width, height, shadowLayerData.ShadowColor);
                            g.DrawDir(shadowLayerData.Blur, width, height, pureWidth,
                                pureHeight, shadowLayerData.ShadowColor);
                        }

                    }
                }
            }


            return bitmap;
        }

    }
}
