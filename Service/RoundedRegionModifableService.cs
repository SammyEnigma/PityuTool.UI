using PityuTool.UI.Misc;
using PityuTool.UI.Repository;
using System;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    internal class RoundedRegionModifableService : IRegionModifable
    {

        private readonly int radius;

        public RoundedRegionModifableService(int radius)
        {
            this.radius = radius;
        }

        public void ModifyRegion(Control control)
        {
            IntPtr handle = NativeMethods.CreateRoundRectRgn(0, 0, control.Width, control.Height, radius, radius);
            if (handle != IntPtr.Zero)
                control.Region = System.Drawing.Region.FromHrgn(handle);
            NativeMethods.DeleteObject(handle);
        }
    }
}
