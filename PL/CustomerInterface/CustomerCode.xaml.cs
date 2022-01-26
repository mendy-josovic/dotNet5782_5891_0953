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
    /// Interaction logic for CustomerCode.xaml
    /// </summary>
    public partial class CustomerCode : Window
    {
        IBl blObject;

        /// <summary>
        /// ctor
        /// </summary>
        public CustomerCode()
        {
            blObject = BlFactory.GetBl();
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Opens the window of the customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (IDTextBox.Text != null && IDTextBox.Text != "")
            {
                int customerID = Int32.Parse(IDTextBox.Text);
                try
                {
                    BO.CustomerInParcel cus = blObject.BLCustomerInParcel(blObject.DisplayCustomer(customerID));
                    new ParcelListWindow(blObject, cus).Show();
                    this.Close();
                }
                catch (BlException ex)
                {
                    MessageBox.Show(ex.Message, "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
                    IDTextBox.BorderBrush = Brushes.Red;
                }
            }
        }

        /// <summary>
        /// Opens a window to ad a new customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(blObject).ShowDialog();
        }
    }
}
