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

        /// <summary>
        /// ctor
        /// </summary>
        public ManagementWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            blObject = BlFactory.GetBl();

        }

        /// <summary>
        /// Opens the drones-list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneListButton_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(blObject).Show();
        }

        /// <summary>
        /// Opens the customers-list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomersListButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(blObject).Show();
        }

        /// <summary>
        /// Opens the stations-list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationsListButton_Click(object sender, RoutedEventArgs e)
        {
            new StationListWindow(blObject).Show();
        }

        /// <summary>
        /// Opens the parcels-list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelsListButton_Click(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(blObject).Show();
        }

        /// <summary>
        /// Closes this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            isExitPressed = true;
            this.Close();
        }

        /// <summary>
        /// Checks if the window can be closed as requested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isExitPressed;
        }
    }
}