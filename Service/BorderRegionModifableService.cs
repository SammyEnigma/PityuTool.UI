using PityuTool.UI.Misc;
using PityuTool.UI.Models;
using PityuTool.UI.Repository;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class BorderRegionModifableService : IRegionModifable
    {


        private readonly BorderLayerData borderLayerData;

        private readonly Control baseControl;

        public BorderRegionModifableService(BorderLayerData borderLayerData, Control baseControl)
        {
            this.borderLayerData = borderLayerData;
            this.baseControl = baseControl;
        }

        public void ModifyRegion(Control control)
        {

            Rectangle bounds = baseControl.Bounds;

            bounds.Inflate(borderLayerData.BorderSize, borderLayerData.BorderSize);

            control.Bounds = bounds;

            IntPtr handle = NativeMethods.CreateRoundRectRgn(0, 0, bounds.Width, bounds.Height, borderLayerData.BorderRadius, borderLayerData.BorderRadius);
            if (handle != IntPtr.Zero)
            {
                Region baseRegion = baseControl.Region == null ? new Region(baseControl.ClientRectangle) : baseControl.Region.Clone();
                Region borderRegion = Region.FromHrgn(handle);
                baseRegion.Translate(borderLayerData.BorderSize, borderLayerData.BorderSize);
                borderRegion.Exclude(baseRegion);
                control.Region = borderRegion;


                baseRegion.Dispose();
            }

            NativeMethods.DeleteObject(handle);

        }
    }
}
