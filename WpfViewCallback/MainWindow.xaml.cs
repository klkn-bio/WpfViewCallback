using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfViewCallback.ViewModels;

namespace WpfViewCallback
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly string _name;

        
        public MainWindow(string name)
        {
            _name = name;
            Title = name;
            InitializeComponent();
        }

        public void ActionWithoutParameters()
        {
            MessageBox.Show("ActionWithoutParameters", _name);
        }

        private void ActionWithParameters(string arg)
        {
            MessageBox.Show($"ActionWithParameters: {arg}", _name);
        }

        public string FunctionWithoutParameters()
        {
            return $"Hello from {_name}";
        }
        
        private string FunctionWithParameters(string arg)
        {
            return $"Hello from {_name}. Arg: {arg}";
        }
    }
}
