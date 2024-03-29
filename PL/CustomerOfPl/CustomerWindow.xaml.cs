﻿using System;
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
        /// <summary>
        /// constractor to add showing the needed
        /// </summary>
        /// <param name="blobject"></param>
        public CustomerWindow(IBl blobject)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
        /// <summary>
        /// constractor with a customer showing him
        /// </summary>
        /// <param name="blobjects"></param>
        /// <param name="Customer"></param>
        public CustomerWindow(IBl blobjects, Customer Customer)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            Customer1 = Customer;
            AddCustomerButton.Content = "Update";
            x1.Visibility = Visibility.Hidden;
            x2.Visibility = Visibility.Hidden;
            x3.Visibility = Visibility.Hidden;
            x4.Visibility = Visibility.Hidden;
            x5.Visibility = Visibility.Hidden;
            x6.Visibility = Visibility.Hidden;
            IDTextBox.IsReadOnly = true;
            LongtitudeTextBox.IsReadOnly = true;
            LatitudeTextBox.IsReadOnly = true;
            CustomerWindowGrid.DataContext = Customer1;
            blObject = blobjects;
        }
        /// <summary>
        /// the same button dose all so it hAS TO DO WITH WHAT IS RETTEN on it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddCustomerButton.Content == "Update")//sayes updat so we update
                {
                    blObject.UpdateCosomerInfo(Customer1.Id, Customer1.Name, Customer1.Phone);
                    MessageBox.Show("Successfully updated Customer!", "Congradulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeComponent();
                }
                if (AddCustomerButton.Content == "Add")//says add so add
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
        /// <summary>
        /// showing the parcels in the customer and then opening the one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelsSent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParcelAtCustomer p = (ParcelAtCustomer)ParcelsSent.SelectedItem;
            new ParcelWindow(blObject, p.Id).Show();
        }
        /// <summary>
        /// same withe parcel thea r sent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
