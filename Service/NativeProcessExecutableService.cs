using PityuTool.UI.Extensions;
using PityuTool.UI.Misc;
using PityuTool.UI.Models;
using PityuTool.UI.Repository;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PityuTool.UI.Service
{
    sealed class NativeProcessExecutableService : INativeProcessExecutable
    {

        private readonly Dictionary<int, FilterData> bindingList = new Dictionary<int, FilterData>();

        private byte counter = 0;
        public void Bind()
        {
            foreach (int i in bindingList.Keys)
            {
                FilterData filterData = bindingList[i];
                CreateNativeWindow(filterData.FilterType, filterData.Control);
            }
        }

        public void RegisterNativeProcess(FilterType type, Control control)
        {
            FilterData filterData = new FilterData(type, control);
            if (!bindingList.IsExistKeyAndValue(filterData))
            {
                bindingList.Add(counter++, filterData);
            }
        }

        private NativeWindow CreateNativeWindow(FilterType type, Control control)
        {
            switch (type)
            {
                case FilterType.MOVE_CONTROL:
                    return new ControlMoveInterceptor(control);
                case FilterType.MOVE_FORM:
                    return new FormMoveInterceptor(control);
                case FilterType.RESIZE:
                    return new ResizeInterceptor(control);
                default:
                    return null;
            }
        }
    }
}
