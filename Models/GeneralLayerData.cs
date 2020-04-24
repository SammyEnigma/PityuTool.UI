using PityuTool.UI.Misc;
using System.Windows.Forms;

namespace PityuTool.UI.Models
{
    sealed class GeneralLayerData
    {
        public Control Target { get; }

        public bool Resize { get; }

        public Control NameLimiter { get; }

        public ControlType[] TypeLimiter { get; }

        public int Radius { get; set; }

        public bool Enabled { get; }

        public bool RadiusEnabled { get; set; }


        public GeneralLayerData(Control target, bool resize, Control nameLimiter, ControlType[] typeLimiter, int radius, bool enabled)
        {
            Target = target;
            Resize = resize;
            NameLimiter = nameLimiter;
            TypeLimiter = typeLimiter;
            Radius = radius;
            Enabled = enabled;
        }
    }
}
