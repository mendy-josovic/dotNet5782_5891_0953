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
        String pickUpContent = "Pick Up";
        String supplyContent = "Supply";
        IBl BlObject;
        Parcel Parcel = new();
        CustomerInParcel customer = null;
        bool isCustomerSender = false;
        bool isCustomerReciever = false;
        bool isCloseButtonPressed = false;

        public ParcelWindow(IBl blObject, CustomerInParcel cus = null)
        {
            InitializeComponent();        
            this.BlObject = blObject;
            IEnumerable<CustomerToList> customers = BlObject.DisplayCustomerList();
            List<CustomerToList> customerNames = BlObject.DisplayCustomerList().ToList();
            SenderComboBox.ItemsSource = customerNames;
            SenderComboBox.DisplayMemberPath = "Name";
            SenderComboBox.SelectedValuePath = "Id";
            RecipientComboBox.ItemsSource = customerNames;
            RecipientComboBox.DisplayMemberPath = "Name";
            RecipientComboBox.SelectedValuePath = "Id";
            WeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
            if (cus != null)
            {
                customer = cus;
                isCustomerSender = true;
            }
            ButtonEnabler();
            VisibiltyIndicator();
        }

        public ParcelWindow(IBl blObject, int Id, CustomerInParcel cus = null)
        {
            this.BlObject = blObject;
            Parcel = BlObject.BLParcel(BlObject.DisplayParcel(Id));      
            InitializeComponent();
            ParcelMainGrid.DataContext = Parcel;
            if (cus != null)
            {
                customer = cus;
                if (Parcel.Sender.Id == customer.Id)
                {
                    isCustomerSender = true;
                }
                else if (Parcel.Recipient.Id == customer.Id)
                {
                    isCustomerReciever = true;
                }
            }
            ButtonEnabler();
            VisibiltyIndicator();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (customer != null)
            {
                Parcel.Sender = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(customer.Id));
            }
            else
            {
                Parcel.Sender = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(Int32.Parse(SenderComboBox.SelectedValue.ToString())));
            }
            Parcel.Recipient= BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(Int32.Parse(RecipientComboBox.SelectedValue.ToString())));
            Parcel.Weight = (Weight)WeightComboBox.SelectedItem;
            Parcel.Priority = (Priority)PriorityComboBox.SelectedItem;
            try
            {
                BlObject.AddParcel(Parcel);
                MessageBox.Show("Successfully added Parcel!", "Congradulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                isCloseButtonPressed = true;
                this.Close();

            }
            catch (BO.BlException ex)
            {
                try
                {
                    throw new PLExceptions(ex.Message);
                }
                catch (PLExceptions ex2)
                {
                    String message = String.Format("Something went wrong...\n{0}", ex2.Message);
                    MessageBox.Show(message, "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void PickUpButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                if ((String)b.Content == pickUpContent)
                {
                    BlObject.PickUp(Parcel.Drone.Id);
                }
                else if((String)b.Content == supplyContent)
                {
                    BlObject.Suuply(Parcel.Drone.Id);
                }
            }
            UpdateContollers();
        }
        /// <summary>
        /// call the fuc in bl to delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BlObject.DeletAParcel(Parcel.Id);
                MessageBox.Show("Successfully deleted Parcel!", "Congradulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                isCloseButtonPressed = true;
                this.Close();

            }
            catch (BO.BlException ex)
            {
                try
                {
                    throw new PLExceptions(ex.Message);
                }
                catch (PLExceptions ex2)
                {
                    String message = String.Format("Something went wrong...\n{0}", ex2.Message);
                    MessageBox.Show(message, "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
        /// <summary>
        /// sets the able and enable of the buttons
        /// </summary>
        public void ButtonEnabler()
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
            }
            if (Parcel.Drone == null)
            {
                PickUpButton.IsEnabled = false;
            }
        }
        public void VisibiltyIndicator()
        {
            if(Parcel.Id>0) // existing parcel
            {
                if (customer != null) // customer interface
                {
                    ParcelIDTextBlock.Visibility = Visibility.Visible;
                    SenderTextBox.Visibility = Visibility.Visible;
                    RecipientTextBox.Visibility = Visibility.Visible;
                    PriorityTextBox.Visibility = Visibility.Visible;
                    DroneTextBox.Visibility = Visibility.Visible;
                    WeightTextBox.Visibility = Visibility.Visible;
                    Drone.Visibility = Visibility.Visible;
                    DeleteButton.Visibility = Visibility.Hidden;
                    UpdateButton.Visibility = Visibility.Hidden;
                    AddButton.Visibility = Visibility.Hidden;
                    SenderComboBox.Visibility = Visibility.Hidden;
                    RecipientComboBox.Visibility = Visibility.Hidden;
                    PriorityComboBox.Visibility = Visibility.Hidden;
                    WeightComboBox.Visibility = Visibility.Hidden;

                    if (Parcel.Drone == null)
                    {
                        PickUpButton.Visibility = Visibility.Hidden;
                    }

                    else if (Parcel.Scheduled != null)
                    {
                        if (Parcel.PickedUp == null)
                        {
                            if (isCustomerSender)
                            {
                                PickUpButton.Content = pickUpContent;
                            }
                            else
                            {
                                PickUpButton.Visibility = Visibility.Hidden;
                            }
                        }
                        else if (Parcel.Delivered == null)
                        {
                            if (isCustomerReciever)
                            {
                                PickUpButton.Content = supplyContent;
                            }
                            else
                            {
                                PickUpButton.Visibility = Visibility.Hidden;
                            }
                        }
                        else
                        {
                            PickUpButton.Visibility = Visibility.Hidden;
                        }
                    }
                }
                else // manager interface
                {
                    ParcelIDTextBlock.Visibility = Visibility.Visible;
                    SenderTextBox.Visibility = Visibility.Visible;
                    RecipientTextBox.Visibility = Visibility.Visible;
                    PriorityTextBox.Visibility = Visibility.Visible;
                    DroneTextBox.Visibility = Visibility.Visible;
                    WeightTextBox.Visibility = Visibility.Visible;
                    Drone.Visibility = Visibility.Visible;
                    DeleteButton.Visibility = Visibility.Visible;
                    UpdateButton.Visibility = Visibility.Visible;
                    PickUpButton.Visibility = Visibility.Visible;
                    AddButton.Visibility = Visibility.Hidden;
                    SenderComboBox.Visibility = Visibility.Hidden;
                    RecipientComboBox.Visibility = Visibility.Hidden;
                    PriorityComboBox.Visibility = Visibility.Hidden;
                    WeightComboBox.Visibility = Visibility.Hidden;

                    if (Parcel.Scheduled != null)
                    {
                        if (Parcel.PickedUp == null)
                        {
                            PickUpButton.Content = pickUpContent;
                        }
                        else if (Parcel.Delivered == null)
                        {
                            PickUpButton.Content = supplyContent;
                        }
                        else
                        {
                            PickUpButton.Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
            else // new parcel
            {
                if (customer != null) // customer interface
                {
                    ParcelIDTextBlock.Visibility = Visibility.Hidden;
                    SenderTextBox2.Visibility = Visibility.Visible;
                    SenderTextBox.Visibility = Visibility.Hidden;
                    SenderTextBox2.Text = customer.Name;
                    SenderTextBox.IsReadOnly = true;
                    RecipientTextBox.Visibility = Visibility.Hidden;
                    WeightTextBox.Visibility = Visibility.Hidden;
                    PriorityTextBox.Visibility = Visibility.Hidden;
                    DroneTextBox.Visibility = Visibility.Hidden;
                    DeleteButton.Visibility = Visibility.Hidden;
                    UpdateButton.Visibility = Visibility.Hidden;
                    PickUpButton.Visibility = Visibility.Hidden;
                    AddButton.Visibility = Visibility.Visible;
                    RecipientComboBox.Visibility = Visibility.Visible;
                    PriorityComboBox.Visibility = Visibility.Visible;
                    WeightComboBox.Visibility = Visibility.Visible;
                }
                else // manager interface
                {
                    ParcelIDTextBlock.Visibility = Visibility.Hidden;
                    SenderTextBox.Visibility = Visibility.Hidden;
                    RecipientTextBox.Visibility = Visibility.Hidden;
                    WeightTextBox.Visibility = Visibility.Hidden;
                    PriorityTextBox.Visibility = Visibility.Hidden;
                    DroneTextBox.Visibility = Visibility.Hidden;
                    DeleteButton.Visibility = Visibility.Hidden;
                    UpdateButton.Visibility = Visibility.Hidden;
                    PickUpButton.Visibility = Visibility.Hidden;
                    AddButton.Visibility = Visibility.Visible;
                    SenderComboBox.Visibility = Visibility.Visible;
                    RecipientComboBox.Visibility = Visibility.Visible;
                    PriorityComboBox.Visibility = Visibility.Visible;
                    WeightComboBox.Visibility = Visibility.Visible;
                }
            }
        }

        private void SenderTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (customer == null && Parcel.Sender != null)
            {
                new CustomerWindow(BlObject, BlObject.BLCustomer(Parcel.Sender.Id)).ShowDialog();
                Parcel.Sender = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(Parcel.Sender.Id));
                ParcelMainGrid.DataContext = Parcel;
                SenderTextBox.Text = Parcel.Sender.Name;
            }
            else if (customer != null && isCustomerSender && Parcel.Sender != null)
            {
                new CustomerWindow(BlObject, BlObject.BLCustomer(customer.Id)).ShowDialog();
                CustomerInParcel cus = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(customer.Id));
                customer = cus;
                Parcel.Sender = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(customer.Id));
                ParcelMainGrid.DataContext = Parcel;
                SenderTextBox.Text = Parcel.Sender.Name;
            }
        }

        private void SenderTextBox2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Parcel.Sender == null && customer != null && isCustomerSender)
            {
                new CustomerWindow(BlObject, BlObject.BLCustomer(customer.Id)).ShowDialog();
                CustomerInParcel cus = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(customer.Id));
                customer = cus;
                SenderTextBox2.Text = customer.Name;
            }
        }

        private void RecipientTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (customer == null && Parcel.Recipient != null)
            {
                new CustomerWindow(BlObject, BlObject.BLCustomer(Parcel.Recipient.Id)).ShowDialog();
                Parcel.Recipient = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(Parcel.Recipient.Id));
                ParcelMainGrid.DataContext = Parcel;
                RecipientTextBox.Text = Parcel.Recipient.Name;
            }
            else if (customer != null && isCustomerReciever && Parcel.Recipient != null)
            {
                new CustomerWindow(BlObject, BlObject.BLCustomer(customer.Id)).ShowDialog();
                CustomerInParcel cus = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(customer.Id));
                customer = cus;
                Parcel.Recipient = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(Parcel.Recipient.Id));
                ParcelMainGrid.DataContext = Parcel;
                RecipientTextBox.Text = Parcel.Recipient.Name;
            }
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

        private void DroneTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DroneTextBox.Text != "" && customer == null)
            {
                new DroneWindow(BlObject, BlObject.BLDrone(BlObject.DisplayDrone(Parcel.Drone.Id))).ShowDialog();
                UpdateContollers();
            }
        }

        private void UpdateContollers()
        {
            Parcel = BlObject.BLParcel(BlObject.DisplayParcel(Parcel.Id));
            ButtonEnabler();
            VisibiltyIndicator();
        }
    }
}
