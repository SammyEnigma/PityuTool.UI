using PityuTool.UI.Repository;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    class LayerEventClientService : IEventClient
    {

        private readonly Form form;

        private readonly IRegionEventExecutable eventExecutable;

        private readonly ILocationEventExecutable locationUpdate;

        public LayerEventClientService(Form form, IRegionEventExecutable eventExecutable, ILocationEventExecutable locationUpdate)
        {
            this.form = form;
            this.eventExecutable = eventExecutable;
            this.locationUpdate = locationUpdate;
        }

        public void Subscribe()
        {
            form.Resize += eventExecutable.DrawBounds;
            form.Shown += eventExecutable.DrawBounds;
            form.LocationChanged += locationUpdate.UpdateLocation;
        }
    }
}
