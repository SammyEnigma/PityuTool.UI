using System.Windows.Forms;

namespace PityuTool.UI.Views
{
    public class Layer : Form
    {



        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_NOACTIVATE = 0x8000000;
        private const int WS_EX_LAYERED = 0x00080000;

        private readonly bool isTransparent;
        public Layer(bool isTransparent)
        {
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            this.isTransparent = isTransparent;
        }


        protected override CreateParams CreateParams
        {
            get
            {

                CreateParams cp = base.CreateParams;
                if (isTransparent)
                {
                    cp.ExStyle |= WS_EX_TRANSPARENT | WS_EX_NOACTIVATE;
                }
                else
                    cp.ExStyle |= WS_EX_LAYERED;
                return cp;
            }
        }
    }
}
