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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public class CustomerListWindow : Window
    {
        IBl blobject;
        public CustomerListWindow(IBl blobject)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blobject = blobject;

            
        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
