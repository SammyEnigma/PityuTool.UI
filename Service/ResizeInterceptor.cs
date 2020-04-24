using PityuTool.UI.Misc;
using System;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class ResizeInterceptor : NativeWindow
    {
        private readonly Control control;

        public ResizeInterceptor(Control control)
        {
            this.control = control;
            AssignHandle(control.Handle);
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {

            switch (m.Msg)
            {
                case (int)Variable.HITTEST:
                    base.WndProc(ref m);

                    Resize(ref m);
                    return;
            }

            base.WndProc(ref m);
        }

        private void Resize(ref Message m)
        {
            Point screenPoint = new Point(m.LParam.ToInt32());
            Point clientPoint = control.PointToClient(screenPoint);

            int resizeHandle = (int)Variable.RESIZEHANDLESIZE;
            if (clientPoint.Y <= resizeHandle)
            {
                if (clientPoint.X <= resizeHandle)
                    m.Result = (IntPtr)(int)Variable.HITTOPLEFT;
                else if (clientPoint.X < (control.Size.Width - resizeHandle))
                    m.Result = (IntPtr)(int)Variable.HITTOP;
                else
                    m.Result = (IntPtr)(int)Variable.HITTOPRIGHT;
            }
            else if (clientPoint.Y <= (control.Size.Height - resizeHandle))
            {
                if (clientPoint.X <= resizeHandle)
                    m.Result = (IntPtr)(int)Variable.HITLEFT;
                else if (clientPoint.X < (control.Size.Width - resizeHandle)) { }
                else
                    m.Result = (IntPtr)(int)Variable.HITRIGHT;
            }
            else
            {
                if (clientPoint.X <= resizeHandle)
                    m.Result = (IntPtr)(int)Variable.HITBOTTOMLEFT;
                else if (clientPoint.X < (control.Size.Width - resizeHandle))
                    m.Result = (IntPtr)(int)Variable.HITBOTTOM;
                else
                    m.Result = (IntPtr)(int)Variable.HITBOTTOMRIGHT;
            }
        }
    }
}
