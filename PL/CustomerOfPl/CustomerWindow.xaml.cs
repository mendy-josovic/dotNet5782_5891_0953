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
        Customer Customer1;
        bool isCloseRequired;

        public CustomerWindow(IBl blobject)
        {
            this.blObject = blobject;
            Customer1 = blObject.BLCustomer();
            InitializeComponent();
            AddCustomerButton.Content = "Add";
            CustomerWindowGrid.DataContext = Customer1;
            ParcelsSent.Visibility = Visibility.Hidden;
            ParcelsRecievd.Visibility = Visibility.Hidden;
            ParcelsSentLabel.Visibility = Visibility.Hidden;
            ParcelsReceivedLabel.Visibility = Visibility.Hidden;
        }
        public CustomerWindow(IBl blobjects, Customer Customer)
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
                    InitializeComponent();
                }
                if (AddCustomerButton.Content == "Add")
                {                   
                    blObject.AddCustomer(Customer1);
                    MessageBox.Show("Successfully added Customer!", "Congradulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                    isCloseRequired = true;
                    this.Close();
                }
            }
            catch(BO.BlException ex)
            {
                String message = String.Format("Something went wrong...\n{0}", ex.Message);
                MessageBox.Show(message, "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ParcelsSent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParcelAtCustomer p = (ParcelAtCustomer)ParcelsSent.SelectedItem;
            new ParcelWindow(blObject, p.Id).Show();
        }

        private void ParcelsRecievd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParcelAtCustomer p = (ParcelAtCustomer)ParcelsRecievd.SelectedItem;
            new ParcelWindow(blObject, p.Id).Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            isCloseRequired = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseRequired;
        }
    }
}
