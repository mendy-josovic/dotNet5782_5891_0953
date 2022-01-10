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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        IBl blObject;
        BO.CustomerToList Customer1 = new();
        public CustomerWindow(IBl blobject)
        {
            this.blObject = blobject;
            InitializeComponent();
            AddCustomerButton.Content = "Add";         
        }
        public CustomerWindow(IBl blobjects, BO.CustomerToList Customer)
        {
            InitializeComponent();
            Customer1 = Customer;
            AddCustomerButton.Content = "Update";
            x1.Visibility = Visibility.Hidden;
            x2.Visibility = Visibility.Hidden;
            x5.Visibility = Visibility.Hidden;
            x3.Visibility = Visibility.Hidden;
            IDTextBox.IsReadOnly = true;
            CustomerWindowGrid.DataContext = Customer1;
            blObject = blobjects;

        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddCustomerButton.Content == "Update")
                {
                    blObject.UpdateCosomerInfo(Customer1.Id, Customer1.Name, Customer1.Phone);
                    Customer1 = blObject.BLCustomerToList(blObject.DisplayCustomer(Customer1.Id));

                    
                }
                if (AddCustomerButton.Content == "Add")
                {                   
                    blObject.AddCustomer(blObject.BLCustomer(Customer1.Id));
                    MessageBox.Show("Successfully added Customer!", "Congradulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch(BO.BlException ex)
            {
                String message = String.Format("Something went wrong...\n{0}", ex.Message);
                MessageBox.Show(message, "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Phone_MouseDoubleClick(object sender, RoutedEventArgs e)
        {

        }

        private void ParcelsSentAndNotDeliveredTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
