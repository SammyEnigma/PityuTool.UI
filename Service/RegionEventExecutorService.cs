using PityuTool.UI.Repository;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    class RegionEventExecutorService : IRegionEventExecutable
    {
        private readonly Dictionary<Control, IRegionModifable> regionModifables = new Dictionary<Control, IRegionModifable>();

        public void RegisterRegionModifable(Control key, IRegionModifable regionModifable)
        {
            if (!regionModifables.ContainsKey(key))
                regionModifables.Add(key, regionModifable);
        }

        public void DrawBounds(object sender = null, EventArgs e = null)
        {
            foreach (Control key in regionModifables.Keys)
            {
                IRegionModifable regionModifable = regionModifables[key];

                regionModifable.ModifyRegion(key);
            }
        }


        public void ClearAll()
        {
            regionModifables.Clear();
        }
    }
}
