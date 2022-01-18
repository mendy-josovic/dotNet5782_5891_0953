using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagementWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ManagementButton_Click(object sender, RoutedEventArgs e)
        {
            new CodeWindow().Show();
            //this.Close();
        }

        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerCode().Show();
            this.Close();
        }
    }
}

