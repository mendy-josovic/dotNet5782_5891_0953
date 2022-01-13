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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ManagementWindow : Window
    {
        IBl blObject;
        bool isExitPressed;
        public ManagementWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            blObject = BlFactory.GetBl();

        }

        private void DroneListButton_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(blObject).Show();
        }

        private void CustomersListButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(blObject).Show();
        }

        private void StationsListButton_Click(object sender, RoutedEventArgs e)
        {
            new StationListWindow(blObject).Show();
        }

        private void ParcelsListButton_Click(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(blObject).Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            isExitPressed = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isExitPressed;
        }
    }
}