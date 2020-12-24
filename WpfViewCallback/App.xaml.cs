using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfViewCallback.ViewModels;

namespace WpfViewCallback
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void AppStartup(object sender, StartupEventArgs e)
        {
            var viewModel = new ViewModel();
            MainWindow = new MainWindow("Main") {DataContext = viewModel};
            MainWindow.Show();
            new MainWindow("First") { DataContext = viewModel }.Show();
            new MainWindow("Second") { DataContext = viewModel }.Show();
        }
    }
}
