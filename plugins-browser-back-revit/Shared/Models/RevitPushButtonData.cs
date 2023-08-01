using Autodesk.Revit.UI;

namespace plugins_browser_back_revit
{
    internal class RevitPushButtonData
    {
        public string Label { get; set; }
        public RibbonPanel Panel { get; set; }
        public string CommandNamespacePath { get; set; }
        public string ToolTip { get; set; }
        public string ImageName { get; set; }
        public string LargeImageName { get; set; }
        public string ToolTipImageName { get; set; }
    }
}
