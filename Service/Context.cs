using PityuTool.UI.Models;
using PityuTool.UI.Repository;
using PityuTool.UI.Views;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    class Context : IContext
    {
       
        private BorderLayerData BorderLayerData { get; set; }
        private ShadowLayerData ShadowLayerData { get; set; }
        private GeneralLayerData GeneralLayerData { get; set; }
        public bool DesignMode { get; set; }



        private IEventClient eventClient;

        private readonly IRegionEventExecutable eventExecutable;

        private readonly ILocationEventExecutable locationEventExecutable;

        private readonly Form mainForm;


       
        public Context(BorderLayerData borderLayerData, ShadowLayerData shadowLayerData, GeneralLayerData generalLayerData)
        {
            BorderLayerData = borderLayerData;
            ShadowLayerData = shadowLayerData;
            GeneralLayerData = generalLayerData;

            mainForm = (Form)GeneralLayerData.Target;

            locationEventExecutable = new LocationEventExecutableService();

            eventExecutable = new RegionEventExecutorService();

        }

        private void SetEventClient()
        {
            if (GeneralLayerData.Enabled)
            {
                INativeProcessExecutable nativeProcessExecutable = new NativeProcessExecutableService();
                IFilter filter = new FilterService(GeneralLayerData, nativeProcessExecutable);
                eventClient = new GeneralEventClientService(mainForm, eventExecutable, locationEventExecutable, filter);
            }
            else if (BorderLayerData.Enabled || ShadowLayerData.Enabled)
            {
                eventClient = new LayerEventClientService(mainForm, eventExecutable, locationEventExecutable);
            }
            else if (GeneralLayerData.RadiusEnabled && !DesignMode)
            {
                eventClient = new PureRadiusEventClientService(mainForm, eventExecutable);
            }
            eventClient?.Subscribe();
        }

        private void GenerateLayout()
        {
            Form parent = mainForm;

            IGeneralCreator generalCreator = new GeneralCreatorService(GeneralLayerData, eventExecutable);
            generalCreator.Create();


            if (BorderLayerData.Enabled)
            {

                Layer borderLayer = new Layer(true)
                {
                    BackColor = BorderLayerData.Color
                };
                ICreator borderCreator = new BorderCreatorService(BorderLayerData, eventExecutable, locationEventExecutable)
                {
                    Layer = borderLayer
                };
                borderCreator.Create(mainForm, DesignMode);
                parent = borderLayer;
            }
            if (ShadowLayerData.Enabled)
            {
                Layer shadowLayer = new Layer(false);
                ICreator shadowCreator = new ShadowCreatorService(ShadowLayerData, eventExecutable, locationEventExecutable, mainForm)
                {
                    Layer = shadowLayer
                };
                shadowCreator.Create(parent, DesignMode);
            }

        }


        public void Refresh()
        {
            if (DesignMode)
            {
                eventExecutable.ClearAll();
                mainForm.Invalidate();
                GenerateLayout();
            }
            eventExecutable.DrawBounds();
        }

        public void Start()
        {
            SetEventClient();
            GenerateLayout();
        }

    }
}
