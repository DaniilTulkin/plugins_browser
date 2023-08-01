using Autodesk.Revit.UI;
using System;
using System.Windows;
using System.Windows.Input;

namespace plugins_browser_back_revit
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;

        public MainWindow(UIApplication app)
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel(app, webView);
            DataContext = viewModel;

            viewModel.CloseAction = new Action(this.Close);
        }

        private void dragWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }
    }
}
