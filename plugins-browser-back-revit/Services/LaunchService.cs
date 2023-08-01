using Autodesk.Revit.UI;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace plugins_browser_back_revit
{
    internal class LaunchService
    {
        private MainWindowViewModel mainWindowViewModel;
        private WebView2 webView;
        private RevitEvent revitEvent;

        public LaunchService(UIApplication app, 
                             WebView2 webView,
                             MainWindowViewModel mainWindowViewModel)
        {
            this.webView = webView;
            this.mainWindowViewModel = mainWindowViewModel;
            revitEvent = new RevitEvent();

            webView.Source = new Uri("http://localhost:4200");
            webView.WebMessageReceived += new EventHandler<CoreWebView2WebMessageReceivedEventArgs>(ReceiveWebMessage);            
        }

        private void ReceiveWebMessage(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            Message message = null;
            try
            {
                message = JsonConvert.DeserializeObject<Message>(e.WebMessageAsJson);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            if (message == null) return;

            switch ((EventsEnum)Enum.Parse(typeof(EventsEnum), message.@event))
            {
                case EventsEnum.ResizeWindow:
                    WebView2EventHandler.ResizeWindow(message.payload, revitEvent, mainWindowViewModel);
                    break;
                case EventsEnum.Close:
                    mainWindowViewModel.CloseAction();
                    break;
                case EventsEnum.PopulateElements:
                    WebView2EventHandler.PopulateElements(webView, revitEvent);
                    break;
                case EventsEnum.Run:
                    WebView2EventHandler.RunPlugin(message.payload, revitEvent);
                    break;
                default:
                    break;
            }
        }
    }
}
