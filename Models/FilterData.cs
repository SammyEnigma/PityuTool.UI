using PityuTool.UI.Misc;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PityuTool.UI.Models
{
    sealed class FilterData
    {
        public FilterType FilterType { get; }
        public Control Control { get; }

        public FilterData(FilterType filterType, Control control)
        {
            FilterType = filterType;
            Control = control;
        }

        public override bool Equals(object obj)
        {
            return obj is FilterData data &&
                   FilterType == data.FilterType &&
                   EqualityComparer<Control>.Default.Equals(Control, data.Control);
        }

        public override int GetHashCode()
        {
            var hashCode = 1500372613;
            hashCode = hashCode * -1521134295 + FilterType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Control>.Default.GetHashCode(Control);
            return hashCode;
        }
    }
}

