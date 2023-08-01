using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace plugins_browser_back_revit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Launcher : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                MainWindow mainWindow = new MainWindow(commandData.Application);
                mainWindow.Show();

                return Result.Succeeded;
            }
            catch
            {
                return Result.Failed;
            }
        }

        public static string GetPath()
        {
            return typeof(Launcher).Namespace + "." + nameof(Launcher);
        }
    }
}
