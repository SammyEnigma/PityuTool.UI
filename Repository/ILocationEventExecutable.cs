using System;
using System.Windows.Forms;

namespace PityuTool.UI.Repository
{
    interface ILocationEventExecutable
    {

        void UpdateLocation(object sender, EventArgs e);

        void Add(Control key, ILocationUpdate locationUpdate);
    }
}
