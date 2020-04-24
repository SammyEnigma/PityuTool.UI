using PityuTool.UI.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class LocationEventExecutableService : ILocationEventExecutable
    {

        private readonly Dictionary<Control, ILocationUpdate> updatableControls = new Dictionary<Control, ILocationUpdate>();



        public void Add(Control key, ILocationUpdate locationUpdate)
        {
            updatableControls.Add(key, locationUpdate);
        }

        public void UpdateLocation(object sender, EventArgs e)
        {
            foreach (Control key in updatableControls.Keys)
            {
                ILocationUpdate locationUpdate = updatableControls[key];
                locationUpdate.Update(key);
            }
        }
    }
}
