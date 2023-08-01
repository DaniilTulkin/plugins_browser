using Autodesk.Revit.UI;
using System;
using System.Diagnostics;
using System.Linq;

namespace plugins_browser_back_revit
{
    public class App: IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "Plagins Browser";
            string ribbonPanelName = "Plugins Browser";
            RibbonPanel ribbonPanel = null;

            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            try
            {
                ribbonPanel = application.CreateRibbonPanel(tabName, ribbonPanelName);
            }
            catch
            {
                ribbonPanel = application.GetRibbonPanels(tabName)
                    .FirstOrDefault(panel => panel.Name.Equals(ribbonPanelName));
            }

            var btnData = new RevitPushButtonData
            {
                Label = "Plugins Browser",
                Panel = ribbonPanel,
                ToolTip = "Opens Plugins Browser that contain external instruments for Revit",
                CommandNamespacePath = Launcher.GetPath(),
                ImageName = "plugins-browser-back-revit_icon_16x16.png",
                LargeImageName = "plugins-browser-back-revit_icon_32x32.png",
                ToolTipImageName = "plugins-browser-back-revit_tooltip_image.png"
            };

            var btn = RevitPushButton.Create(btnData);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
