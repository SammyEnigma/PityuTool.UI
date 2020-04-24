using System.Drawing;

namespace PityuTool.UI.Repository
{
    interface ILayeredBitmap
    {
        void DrawShadow(Bitmap bitmap, byte opacity);
    }
}
