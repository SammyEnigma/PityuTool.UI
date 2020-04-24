using PityuTool.UI.Models;
using PityuTool.UI.Repository;
using PityuTool.UI.Views;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    class ShadowCreatorService : ICreator
    {


        private readonly ShadowLayerData shadowLayerData;

        private readonly IRegionEventExecutable eventExecutable;

        private readonly ILocationEventExecutable locationEventExecutable;

        private readonly Form form;

        public Layer Layer { get; set; }

        public ShadowCreatorService(ShadowLayerData shadowLayerData, IRegionEventExecutable eventExecutable,
            ILocationEventExecutable locationEventExecutable, Form form)
        {
            this.shadowLayerData = shadowLayerData;
            this.eventExecutable = eventExecutable;
            this.locationEventExecutable = locationEventExecutable;
            this.form = form;
        }

        public void Create(Form parent, bool designMode)
        {

            ILocationUpdate shadowLocationUpdate = new ShadowLocationUpdateService(form, shadowLayerData);
            locationEventExecutable.Add(Layer, shadowLocationUpdate);
            ILayeredBitmap layeredBitmap = new LayeredBitmapService(Layer);
            IRegionModifable shadowRegionModifable = new ShadowRegionModifableService(shadowLayerData, form, layeredBitmap);
            eventExecutable.RegisterRegionModifable(Layer, shadowRegionModifable);
            if (!designMode)
            {
                parent.Owner = Layer;
                Layer.Show();
            }
        }
    }
}
