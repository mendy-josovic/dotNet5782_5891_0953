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
        /// <summary>
        /// ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Opens the management window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagementButton_Click(object sender, RoutedEventArgs e)
        {
            new CodeWindow().Show();
            this.Close();
        }

        /// <summary>
        /// Opens the customer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerCode().Show();
            this.Close();
        }
    }
}

