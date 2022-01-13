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
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        IBl BlObject;
        Parcel Parcel = new();
        public ParcelWindow(IBl blObject)
        {
            InitializeComponent();        
            this.BlObject = blObject;
            IEnumerable<CustomerToList> customers = BlObject.DisplayCustomerList();
            List<String> Nams = BlObject.DisplayCustomerList().Select(x => x.Name).ToList();
            SemderComboBox.ItemsSource = Nams;
            RecipientComboBox.ItemsSource = Nams;
            List<int> idofsender = customers.Where(x => x.Name == (String)SemderComboBox.SelectedItem).Select(x => x.Id).ToList();        
            WeihComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
            ButtoneEnblaer();
            VisibiltyIndecatoer();
        }

        public ParcelWindow(IBl blObject, int Id)
        {
            this.BlObject = blObject;
            Parcel = BlObject.BLParcel(BlObject.DisplayParcel(Id));      
            InitializeComponent();
            ParcelMainGrid.DataContext = Parcel;
            ButtoneEnblaer();
            VisibiltyIndecatoer();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> idofsender = BlObject.DisplayCustomerList().Where(x => x.Name == (String)SemderComboBox.SelectedItem).Select(x => x.Id).ToList();
            List<int> Recipient = BlObject.DisplayCustomerList().Where(x => x.Name == (String)RecipientComboBox.SelectedItem).Select(x => x.Id).ToList();
            Parcel.Sender = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(idofsender.First()));
            Parcel.Recipient= BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(idofsender.Last()));
            Parcel.Weight = (Weight)WeihComboBox.SelectedItem;
            Parcel.Priority = (Priority)PriorityComboBox.SelectedItem;
            BlObject.AddParcel(Parcel);
        }

        private void PickUpButton_Click(object sender, RoutedEventArgs e)
        {
          
      
        }
        /// <summary>
        /// call the fuc in bl to delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            BlObject.DeletAParcel(Parcel.Id);

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
        /// <summary>
        /// sets the able and enable of the buttons
        /// </summary>
        public void ButtoneEnblaer()
        {
            if(Parcel.Scheduled!= DateTime.MinValue&& Parcel.PickedUp==null)
            {
                DeleteButton.IsEnabled = true;
                UpdateButton.IsEnabled = true;
                PickUpButton.IsEnabled = true;
            }
            else
            {
                DeleteButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
                PickUpButton.IsEnabled = false;
            }
        }
        public void VisibiltyIndecatoer()
        {
            if(Parcel.Id>0)
            {
                DeleteButton.Visibility = Visibility.Visible;
                UpdateButton.Visibility = Visibility.Visible;
                PickUpButton.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Hidden;
            }
            else
            {
                DeleteButton.Visibility = Visibility.Hidden;
                UpdateButton.Visibility = Visibility.Hidden;
                PickUpButton.Visibility = Visibility.Hidden;
                AddButton.Visibility = Visibility.Visible;
            }
        }

        private void SenderTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CustomerWindow(BlObject, BlObject.BLCustomer(Parcel.Sender.Id)).Show();
        }

        private void RecipientTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CustomerWindow(BlObject, BlObject.BLCustomer(Parcel.Recipient.Id)).Show();
        }
    }
}
