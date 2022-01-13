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
using System.Text.RegularExpressions;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        IBl blObject;
        Station station;
        IEnumerable<DroneInCharging> ListOfDrones; 
        bool isCloseRequired = false;

        public StationWindow(IBl blObject)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            station = blObject.BLStation();
            ADrone.DataContext = station;
           // ListOfDrones = blObject.DisplayDronesInCharging((w => blObject.GetDistance(station.location, blObject.DisplayDrone(w.Id).ThisLocation) == 0));
            StationLabel.Content = "Add a station";
            IDTextBox.IsReadOnly = false;
            LongitudeTextBox.IsReadOnly = false;
            LatitudeTextBox.IsReadOnly = false;
            ListOfDronesLabel.Content = "";
            UpDateButton.Visibility = Visibility.Hidden;
        }

        public StationWindow(IBl blObject, Station s)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            station = s;           
            ADrone.DataContext = station;
            DronesListView.ItemsSource = station.ListOfDrones;
            StationLabel.Content = String.Format("Drone {0}", station.Id);
            AddButton.Visibility = Visibility.Hidden;
            IDTextBox.BorderBrush = null;
            NameTextBox.BorderBrush = null;
            LongitudeTextBox.BorderBrush = null;
            LatitudeTextBox.BorderBrush = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseRequired;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isCloseRequired = true;
            this.Close();
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView selectedListView = sender as ListView;
            if (selectedListView != null)
            {
                new DroneWindow(blObject, blObject.BLDrone(blObject.DisplayDrone(((DroneInCharging)selectedListView.SelectedItem).Id))).ShowDialog();
                station.ListOfDrones = blObject.DisplayDronesInCharging((w => blObject.GetDistance(station.location, blObject.DisplayDrone(w.Id).ThisLocation) == 0)).ToList();
                DronesListView.ItemsSource = station.ListOfDrones;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(NameTextBox.Text != station.Name)
                UpDateButton.IsEnabled = true;
        }

        private void UpDateButton_Click(object sender, RoutedEventArgs e)
        {
                blObject.UpdateStation(station.Id, NameTextBox.Text, -1);
                UpDateButton.IsEnabled = false;   
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.AddStation(station);
                MessageBox.Show("Successfully added station!", "Congradulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                isCloseRequired = true;
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

        private void IDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void NameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void LongitudeAndLatitudeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9.]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
