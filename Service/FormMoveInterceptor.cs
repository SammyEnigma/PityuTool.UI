using PityuTool.UI.Misc;
using System;
using System.Security.Permissions;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class FormMoveInterceptor : NativeWindow
    {

        public FormMoveInterceptor(Control control)
        {
            AssignHandle(control.Handle);
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)Variable.HITTEST)
            {
                base.WndProc(ref m);
                if ((int)m.Result == (int)Variable.HITCLIENT)
                {
                    m.Result = (IntPtr)(int)Variable.HITCAPTION;
                }
                return;
            }
            base.WndProc(ref m);
        }
    }
}
