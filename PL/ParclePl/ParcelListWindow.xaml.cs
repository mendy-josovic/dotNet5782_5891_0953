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
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {
        IBl BlObject;
        /// <summary>
        ///list of the parcel
        /// </summary>
        IEnumerable <ParcelToList> parcelToLists { set; get; }
        CustomerInParcel customer = null;// one customer for the binding

        bool isCloseButtonPressed;//for the buuton cancel
        RefreshSimulatorEvent refreshSimulatorEvent = new();
        /// <summary>
        /// cunstractor of the parcel for parcel list (list  window)
        /// </summary>
        /// <param name="blObject"></param>
        public ParcelListWindow(IBl blObject)
        {
            refreshSimulatorEvent.AddEventHandler(new Action(RefreshEventHandler));//event handler for the simulator
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.BlObject = blObject;
            MainGrid.DataContext = parcelToLists;//data binding
            parcelToLists = GetListByCustomer();
            ParcelLiastView.ItemsSource = parcelToLists;
            //    StatusSelector.ItemsSource = Enum.GetValues(typeof(StatusOfDrone));
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelLiastView.ItemsSource);
            //PropertyGroupDescription groupDescription = new PropertyGroupDescription("Sender");
            //view.GroupDescriptions.Add(groupDescription);
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));//data
        }

        /// <summary>
        /// the constractor with a parcel to display for a customer
        /// </summary>
        /// <param name="blObject"></param>
        /// <param name="cus"></param>
        public ParcelListWindow(IBl blObject, CustomerInParcel cus)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.BlObject = blObject;
            MainGrid.DataContext = parcelToLists;
            customer = cus;

            parcelToLists = GetListByCustomer();//intilizinng the parcels of this customer
            ParcelLiastView.ItemsSource = parcelToLists;
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
        }

        /// <summary>
        /// refresh in the simulator
        /// </summary>
        private void RefreshEventHandler()
        {
            this.Dispatcher.Invoke(new Action(delegate ()
            {
                DisplayListByFilters();
            }));
        }


        /// <summary>
        /// cleer the sort and get the hole list                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBotten_Click(object sender, RoutedEventArgs e)
        {
            SenderTextBox.Clear();
            RecipientTextBox.Clear();
            priorityComboBox.SelectedItem = null;
            ClearBotten.Visibility = Visibility.Hidden;
            parcelToLists = GetListByCustomer();
            ParcelLiastView.ItemsSource = parcelToLists;

        }
        /// <summary>
        /// the filter o the recwested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayListByFilters();
            ClearBotten.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// if we add the parcel we open a window with aprcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(BlObject, customer).ShowDialog();
            DisplayListByFilters();
            if (customer != null)
            {
                customer = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(customer.Id));
            }
        }

        /// <summary>
        /// if we click on one parcel opeinng the parcel tha was clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelLiastView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ParcelWindow(BlObject, ((ParcelToList)ParcelLiastView.SelectedItem).Id, customer).ShowDialog();
            DisplayListByFilters();
            if (customer != null)
            {
                customer = BlObject.BLCustomerInParcel(BlObject.DisplayCustomer(customer.Id));
            }
        }
        /// <summary>
        /// allowing to clos the window only from here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            isCloseButtonPressed = true;
            this.Close();
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseButtonPressed;
        }
        /// <summary>
        /// hlper func that returns the list of parcels of one customer
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ParcelToList> GetListByCustomer()
        {
            if (customer != null)
            {
                IEnumerable<BO.Parcel> parcelsList = BlObject.DisplayParcelLists(x => x.Sender.Id == customer.Id || x.Recipient.Id == customer.Id);
                return parcelsList.Select(x => BlObject.BLParcelToList(x)).ToList();
            }
            else
            {
                return BlObject.DisplayParcelList();
            }
        }
        /// <summary>
        /// the filter displyer of parcel
        /// </summary>
        private void DisplayListByFilters()
        {
            parcelToLists = GetListByCustomer();
            if (ChoiseDate.SelectedDate.HasValue)//acrding to a date range
            {
                DateTime t = ChoiseDate.SelectedDates.First().Date;
                DateTime t2 = ChoiseDate.SelectedDates.Last().Date;
                IEnumerable<BO.Parcel> parcels = BlObject.DisplayParcelLists(w => w.TimeOfCreation.Value.Day >= t.Day && (w.TimeOfCreation.Value.Day <= t2.Day));
                parcelToLists = parcelToLists.Where(w => parcels.Any(x => x.Id == w.Id));
            }
            if (SenderTextBox.Text.Length > 0)//filter with a sender
            {
                parcelToLists = parcelToLists.Where(w => w.Sender == SenderTextBox.Text);
            }
            if (RecipientTextBox.Text.Length > 0)//filter by recipint
            {
                parcelToLists = parcelToLists.Where(w => w.Recipient == RecipientTextBox.Text);
            }
            object var = priorityComboBox.SelectedItem;//acourding to prayority
            if (var != null)
            {
                parcelToLists = parcelToLists.Where(w => (int)w.Priority == (int)var);
            }
            ParcelLiastView.ItemsSource = parcelToLists;
            
        }
    }
}
