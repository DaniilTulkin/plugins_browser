using Autodesk.Revit.UI;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace plugins_browser_back_revit
{
    internal static class WebView2EventHandler
    {
        #region PopulatePlugins

        internal static void PopulateElements(WebView2 webView, RevitEvent revitEvent)
        {
            revitEvent.Run(app =>
            {
                var payload = Json.Read<List<Card>>("Updater");

                Message message = new Message (EventsEnum.PopulateElements, payload);
                Message.SendMessage(webView, message);
            });
        }

        #endregion

        #region RunPlugin

        internal static void RunPlugin(object payload, RevitEvent revitEvent)
        {
            string json = JsonConvert.SerializeObject(payload);
            Dictionary<string, object> payloadDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            string className = Convert.ToString(payloadDict["className"]);

            DirectoryInfo directoryInfo = new DirectoryInfo(Json.mainFolderPath + "\\Nika_RD_Dll");
            FileInfo[] files = directoryInfo.GetFiles("*.dll");

            string classPath = null;
            foreach (FileInfo file in files)
                if (Path.GetFileNameWithoutExtension(file.FullName) == className)
                    classPath = file.FullName;

            if (classPath == null) return;

            revitEvent.Run(app =>
            {
                IExternalEventHandler externalEvent = SetExternalEvent(className, classPath);
                if (externalEvent != null)
                {
                    ExternalEvent.Create(externalEvent).Raise();
                }
            });
        }

        private static IExternalEventHandler SetExternalEvent(string className, string classPath)
        {
            try
            {
                byte[] assemblyBytes = File.ReadAllBytes(classPath);
                System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(assemblyBytes);
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsClass && type.FullName == $"{className}.Model.{className}")
                    {
                        return Activator.CreateInstance(type) as IExternalEventHandler;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        #endregion

        #region ResizeWindow

        internal static void ResizeWindow(object payload, 
                                          RevitEvent revitEvent, 
                                          MainWindowViewModel mainWindowViewModel)
        {
            string json = JsonConvert.SerializeObject(payload);
            Dictionary<string, object> payloadDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            int height = Convert.ToInt32(payloadDict["height"]);
            int width = Convert.ToInt32(payloadDict["width"]);

            revitEvent.Run(app =>
            {                
                mainWindowViewModel.MainWindowHeight = height;
                mainWindowViewModel.MainWindowWidth = width;
            });
        }

        #endregion
    }
}
