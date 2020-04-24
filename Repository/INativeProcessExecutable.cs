using PityuTool.UI.Misc;
using System.Windows.Forms;

namespace PityuTool.UI.Repository
{
    interface INativeProcessExecutable
    {
        void RegisterNativeProcess(FilterType type, Control control);

        void Bind();

    }
}
