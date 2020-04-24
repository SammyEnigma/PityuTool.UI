using PityuTool.UI.Models;
using PityuTool.UI.Repository;
using System.Drawing;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class ShadowLocationUpdateService : ILocationUpdate
    {

        private readonly Form mainForm;

        private readonly ShadowLayerData shadowLayerData;

        public ShadowLocationUpdateService(Form mainForm, ShadowLayerData shadowLayerData)
        {
            this.mainForm = mainForm;
            this.shadowLayerData = shadowLayerData;
        }

        public void Update(Control control)
        {
            Point position = mainForm.Location;
            int vLoc = shadowLayerData.VOffset - (shadowLayerData.Blur + shadowLayerData.ShadowSize);
            int hLoc = shadowLayerData.HOffset - (shadowLayerData.Blur + shadowLayerData.ShadowSize);
            position.Offset(vLoc, hLoc);
            control.Location = position;
        }
    }
}
