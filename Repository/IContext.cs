namespace PityuTool.UI.Repository
{
    interface IContext
    {
        bool DesignMode { get; set; }

        void Start();

        void Refresh();

    }
}
