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
        public CustomerListWindow(IBl blObject)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObjects = blObject;
            CustomerLists = blObjects.DisplayCustomerList();
            CustomerListView.ItemsSource = CustomerLists;
        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(blObjects).ShowDialog();
        }

        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object var=CustomerListView.SelectedItem;
            new CustomerWindow(blObjects,(BO.CustomerToList)CustomerListView.SelectedItem).ShowDialog();
            InitializeComponent();
        }
    }
}
