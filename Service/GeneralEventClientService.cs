using PityuTool.UI.Repository;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class GeneralEventClientService : IEventClient
    {

        private readonly Form form;

        private readonly IRegionEventExecutable eventExecutable;

        private readonly ILocationEventExecutable locationUpdate;

        private readonly IFilter filter;

        public GeneralEventClientService(Form form, IRegionEventExecutable eventExecutable, ILocationEventExecutable locationUpdate, IFilter filter)
        {
            this.form = form;
            this.eventExecutable = eventExecutable;
            this.locationUpdate = locationUpdate;
            this.filter = filter;
        }

        public void Subscribe()
        {
            form.Resize += eventExecutable.DrawBounds;
            form.Shown += eventExecutable.DrawBounds;
            form.Load += filter.FilterByProperties;
            form.LocationChanged += locationUpdate.UpdateLocation;
        }
    }
}
