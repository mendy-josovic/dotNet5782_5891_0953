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
        IEnumerable <ParcelToList> parcelToLists { set; get; }

        bool isCloseButtonPressed;

        public ParcelListWindow(IBl blObject)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.BlObject = blObject;
            parcelToLists = BlObject.DisplayParcelList();
            ParcelLiastView.ItemsSource = parcelToLists;
            //    StatusSelector.ItemsSource = Enum.GetValues(typeof(StatusOfDrone));
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelLiastView.ItemsSource);
            //PropertyGroupDescription groupDescription = new PropertyGroupDescription("Sender");
            //view.GroupDescriptions.Add(groupDescription);
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
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
            parcelToLists = BlObject.DisplayParcelList();
            ParcelLiastView.ItemsSource = parcelToLists;

        }
        /// <summary>
        /// the filter o the recwested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChoiseDate.SelectedDate.HasValue)
            {
                DateTime t = ChoiseDate.SelectedDates.First().Date;
                DateTime t2 = ChoiseDate.SelectedDates.Last().Date;
                IEnumerable<BO.Parcel> parcels = BlObject.DisplayParcelLists(w => w.TimeOfCreation.Value.Day >= t.Day && (w.TimeOfCreation.Value.Day <= t2.Day));
                parcelToLists = parcelToLists.Where(w => parcels.Any(x => x.Id == w.Id));
            }
            if(SenderTextBox.Text.Length>0)
            {
                parcelToLists = parcelToLists.Where(w => w.Sender == SenderTextBox.Text);
            }
            if (RecipientTextBox.Text.Length > 0)
            {
                parcelToLists = parcelToLists.Where(w => w.Recipient == RecipientTextBox.Text);
            }
            object var = priorityComboBox.SelectedItem;
            if(var!=null)
            {                
                parcelToLists = parcelToLists.Where(w => (int)w.Priority == (int)var);
            }
            ParcelLiastView.ItemsSource = parcelToLists;
            ClearBotten.Visibility = Visibility.Visible;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(BlObject).Show();
        }

        private void ParcelLiastView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ParcelWindow(BlObject, ((ParcelToList)ParcelLiastView.SelectedItem).Id).Show();
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
