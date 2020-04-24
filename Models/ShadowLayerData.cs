using System.Drawing;

namespace PityuTool.UI.Models
{
    sealed class ShadowLayerData
    {
        public Color ShadowColor { get; set; }

        public int ShadowSize { get; set; }

        public int VOffset { get; set; }

        public int HOffset { get; set; }

        public int Blur { get; set; }

        public int Radius { get; set; }

        public byte Opacity { get; set; }

        public bool Enabled { get; }

        public ShadowLayerData(int blur, int radius, byte opacity, bool enabled)
        {
            Blur = blur;
            Radius = radius;
            Opacity = opacity;
            Enabled = enabled;
        }
    }
}
