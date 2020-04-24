using System;
using System.Windows.Forms;

namespace PityuTool.UI.Repository
{
    interface IRegionEventExecutable
    {

        void DrawBounds(object sender=null, EventArgs e=null);

        void RegisterRegionModifable(Control key, IRegionModifable regionModifable);

        void ClearAll();

    
    }
}
