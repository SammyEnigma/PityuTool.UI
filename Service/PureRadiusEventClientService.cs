using PityuTool.UI.Repository;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    class PureRadiusEventClientService : IEventClient
    {

        private readonly Form form;

        private readonly IRegionEventExecutable eventExecutable;

        public PureRadiusEventClientService(Form form, IRegionEventExecutable eventExecutable)
        {
            this.form = form;
            this.eventExecutable = eventExecutable;
        }

        public void Subscribe()
        {
            form.Shown += eventExecutable.DrawBounds;
        }
    }
}
