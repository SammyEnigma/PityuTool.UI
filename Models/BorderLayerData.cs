using System.Drawing;

namespace PityuTool.UI.Models
{
    sealed class BorderLayerData
    {
        public Color Color { get; set; }

        public int BorderRadius { get; }

        public int BorderSize { get; set; }

        public bool Enabled { get; }

        public BorderLayerData(Color color, int borderRadius, int borderSize, bool enabled)
        {
            Color = color;
            BorderRadius = borderRadius;
            BorderSize = borderSize;
            Enabled = enabled;
        }
    }
}
