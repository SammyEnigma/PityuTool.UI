using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class ControlMoveInterceptor : NativeWindow
    {

        private readonly Control child;
        private bool isMoveable;

        private int xLoc;
        private int yLoc;
        public ControlMoveInterceptor(Control child)
        {
            this.child = child;
            this.child.MouseDown += Child_MouseDown;
            this.child.MouseUp += Child_MouseUp;
            this.child.MouseMove += Child_MouseMove;
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoveable)
            {
                Form form = child.FindForm();
                form.Location = new System.Drawing.Point(form.Location.X - xLoc + e.X, form.Location.Y - yLoc + e.Y);

            }
        }

        private void Child_MouseUp(object sender, MouseEventArgs e)
        {
            isMoveable = false;
        }

        private void Child_MouseDown(object sender, MouseEventArgs e)
        {
            xLoc = e.X;
            yLoc = e.Y;
            isMoveable = true;
        }
    }
}
