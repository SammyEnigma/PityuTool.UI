using PityuTool.UI.Views;

namespace PityuTool.UI.Repository
{
    interface ICreator
    {
        Layer Layer { get; set; }
        void Create(System.Windows.Forms.Form parent, bool designMode);
    }
}
