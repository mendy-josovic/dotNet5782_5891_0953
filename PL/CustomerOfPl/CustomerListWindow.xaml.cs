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
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        IBl blObjects;
        IEnumerable<BO.CustomerToList> CustomerLists;
        bool isCloseButtonPressed;
        RefreshSimulatorEvent refreshSimulatorEvent = new();
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="blObject"></param>
        public CustomerListWindow(IBl blObject)
        {
            refreshSimulatorEvent.AddEventHandler(new Action(RefreshEventHandler));
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObjects = blObject;
            CustomerLists = blObjects.DisplayCustomerList();
            CustomerListView.ItemsSource = CustomerLists;
        }

        private void RefreshEventHandler()//event for the simulator
        {
            this.Dispatcher.Invoke(new Action(delegate ()
            {
                CustomerLists = blObjects.DisplayCustomerList();
                CustomerListView.ItemsSource = CustomerLists;
            }));
        }
        /// <summary>
        /// opening the window osf customer if we woant to add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(blObjects).ShowDialog();
            CustomerLists = blObjects.DisplayCustomerList();
            CustomerListView.ItemsSource = CustomerLists;
        }
        /// <summary>
        /// if we click from a item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {           
            new CustomerWindow(blObjects, blObjects.BLCustomer(CustomerLists.First(x => x == CustomerListView.SelectedItem).Id)).ShowDialog();
            CustomerLists = blObjects.DisplayCustomerList();
            CustomerListView.ItemsSource = CustomerLists;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            isCloseButtonPressed = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseButtonPressed;
        }
    }
}
