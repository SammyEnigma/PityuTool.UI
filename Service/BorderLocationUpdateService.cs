
using PityuTool.UI.Repository;
using System.Drawing;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class BorderLocationUpdateService : ILocationUpdate
    {

        private readonly Form mainForm;

        private readonly int offset;

        public BorderLocationUpdateService(Form mainForm, int offset)
        {
            this.mainForm = mainForm;
            this.offset = offset;
        }

        public void Update(Control control)
        {
            Point position = mainForm.Location;
            position.Offset(-offset, -offset);
            control.Location = position;
        }
    }
}
