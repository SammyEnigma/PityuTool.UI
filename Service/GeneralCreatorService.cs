using PityuTool.UI.Models;
using PityuTool.UI.Repository;

namespace PityuTool.UI.Service
{
    class GeneralCreatorService : IGeneralCreator
    {
        private readonly GeneralLayerData generalLayerData;

        private readonly IRegionEventExecutable eventExecutable;


        public GeneralCreatorService(GeneralLayerData generalLayerData, IRegionEventExecutable eventExecutable)
        {
            this.generalLayerData = generalLayerData;
            this.eventExecutable = eventExecutable;
        }

        public void Create()
        {

            IRegionModifable regionModifable = new RoundedRegionModifableService(generalLayerData.Radius);
            eventExecutable.RegisterRegionModifable(generalLayerData.Target, regionModifable);

        }
    }
}
