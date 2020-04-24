using PityuTool.UI.Models;
using PityuTool.UI.Repository;
using PityuTool.UI.Views;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    class BorderCreatorService : ICreator
    {


        private readonly BorderLayerData borderLayerData;

        private readonly IRegionEventExecutable eventExecutable;

        private readonly ILocationEventExecutable locationEventExecutable;


        public Layer Layer { get; set; }


        public BorderCreatorService(BorderLayerData borderLayerData, IRegionEventExecutable eventExecutable, ILocationEventExecutable locationEventExecutable)
        {
            this.borderLayerData = borderLayerData;
            this.eventExecutable = eventExecutable;
            this.locationEventExecutable = locationEventExecutable;
        }



        public void Create(Form parent, bool designMode)
        {


            ILocationUpdate borderLocationUpdate = new BorderLocationUpdateService(parent, borderLayerData.BorderSize);
            locationEventExecutable.Add(Layer, borderLocationUpdate);
            IRegionModifable borderRegionModifable = new BorderRegionModifableService(borderLayerData, parent);

            eventExecutable.RegisterRegionModifable(Layer, borderRegionModifable);
            if (!designMode)
            {
                parent.Owner = Layer;
                Layer.Show();
            }
        }


    }
}
