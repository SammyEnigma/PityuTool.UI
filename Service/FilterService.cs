using PityuTool.UI.Misc;
using PityuTool.UI.Models;
using PityuTool.UI.Repository;
using System;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class FilterService : IFilter
    {
        private readonly GeneralLayerData layerData;
        private readonly INativeProcessExecutable nativeProcessExecutable;

        public FilterService(GeneralLayerData layerData, INativeProcessExecutable nativeProcessExecutable)
        {
            this.layerData = layerData;
            this.nativeProcessExecutable = nativeProcessExecutable;
        }

        public void FilterByProperties(object sender, EventArgs e)
        {
            Iterate(layerData.Target);
            if (layerData.Resize)
            {
                nativeProcessExecutable.RegisterNativeProcess(FilterType.RESIZE, layerData.Target);
            }

            nativeProcessExecutable.Bind();
        }

        private void Iterate(Control cntrl)
        {
            string controlName = cntrl.Name;
            FilterByName(cntrl, controlName);

            FilterByType(cntrl);

            foreach (Control control in cntrl.Controls)
            {
                Iterate(control);
            }

        }

        private void FilterByType(Control cntrl)
        {
            if (layerData.TypeLimiter.Length > 0)
            {
                foreach (ControlType controlType in layerData.TypeLimiter)
                {
                    if (cntrl.GetType().Name.Equals(controlType.ToString(), StringComparison.InvariantCulture))
                    {
                        nativeProcessExecutable.RegisterNativeProcess(FilterType.MOVE_CONTROL, cntrl);
                    }
                    else if (controlType == ControlType.Form && typeof(Form).IsAssignableFrom(cntrl.GetType()))
                    {
                        nativeProcessExecutable.RegisterNativeProcess(FilterType.MOVE_FORM, cntrl);
                    }
                }
            }
        }

        private void FilterByName(Control cntrl, string controlName)
        {
            if (layerData.NameLimiter != null && layerData.NameLimiter.Name.Equals(controlName, StringComparison.InvariantCulture))
            {
                if (typeof(Form).IsAssignableFrom(cntrl.GetType()))
                {
                    nativeProcessExecutable.RegisterNativeProcess(FilterType.MOVE_FORM, cntrl);
                }
                else
                {
                    nativeProcessExecutable.RegisterNativeProcess(FilterType.MOVE_CONTROL, cntrl);
                }

            }
        }

    }
}
