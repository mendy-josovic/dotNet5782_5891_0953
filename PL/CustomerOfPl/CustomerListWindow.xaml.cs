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
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        IBl blObjects;
        IEnumerable<BO.CustomerToList> CustomerLists;
        bool isCloseButtonPressed;
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
