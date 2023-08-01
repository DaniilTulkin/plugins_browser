using Autodesk.Revit.UI;
using System;
using System.Reflection;

namespace plugins_browser_back_revit
{
    internal class RevitPushButton
    {
        public static PushButton Create(RevitPushButtonData data)
        {
            var btnDataName = Guid.NewGuid().ToString();

            var btnData = new PushButtonData(btnDataName, data.Label, Assembly.GetExecutingAssembly().Location, data.CommandNamespacePath)
            {
                ToolTip = data.ToolTip,
                Image = ResourceImage.GetImage("Icons", data.ImageName),
                LargeImage = ResourceImage.GetImage("Icons", data.LargeImageName),
                ToolTipImage = ResourceImage.GetImage("Images", data.ToolTipImageName)
            };

            return data.Panel.AddItem(btnData) as PushButton;
        }
    }
}
