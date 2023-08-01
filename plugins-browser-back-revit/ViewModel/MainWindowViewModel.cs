using Autodesk.Revit.UI;
using Microsoft.Web.WebView2.Wpf;
using System;

namespace plugins_browser_back_revit
{
    internal class MainWindowViewModel: ModelBase
    {
        public Action CloseAction { get; set; }
        private LaunchService launchService;

        private double mainWindowHeight = 0;
        public double MainWindowHeight
        {
            get
            {
                return mainWindowHeight;
            }
            set
            {
                mainWindowHeight = value;
                OnPropertyChanged("MainWindowHeight");
            }
        }

        private double mainWindowWidth = 0;
        public double MainWindowWidth
        {
            get
            {
                return mainWindowWidth;
            }
            set
            {
                mainWindowWidth = value;
                OnPropertyChanged("MainWindowWidth");
            }
        }

        public MainWindowViewModel(UIApplication app, WebView2 webView)
        {
            launchService = new LaunchService(app, webView, this);
        }
    }
}
