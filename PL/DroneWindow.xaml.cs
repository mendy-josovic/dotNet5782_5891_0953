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
using IBL;
using IBL.BO;
using System.Text.RegularExpressions;


namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBl blObject;
        Drone drone = new();
        bool isCloseRequired;
        public DroneWindow(IBl blObject)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            AddDrone.DataContext = drone;
            DeliveryOrStationsSLabel.Content = "Select a station\nto charge first";
            DeliveryOrStationsSLabel.FontSize = 14;
            DeliveryOrStationsSLabel.Margin = new(165, 252, 0, 0);
            DeliveryTextBox.Visibility = Visibility.Hidden;
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WEIGHT));
            StatusSelector.Items.Add("IN_MAINTENANCE");
            ListOfStationsSelector.ItemsSource = this.blObject.DisplayStationList(d => d.ReadyStandsInStation > 0);
            DeliveryButton.Visibility = Visibility.Hidden;
            ChargingButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;            
        }

        private void IDTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t != null)
            {
                ToolTip tt = new ToolTip();
                tt.Content = "The ID is automatically given";
                t.ToolTip = tt;
            }
        }

        private void BatteryTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t != null)
            {
                ToolTip tt = new ToolTip();
                tt.Content = "This field is automatically given";
                t.ToolTip = tt;
            }
        }

        private void AddADroneButton_Click(object sender, RoutedEventArgs e)
        {
            StationToList station = ListOfStationsSelector.SelectedItem as StationToList;
            if (station != null && isInputValid())
            {
                try 
                {
                    blObject.AddDrone(drone, station.Id);
                    MessageBox.Show("Successfully added drone!", "Congradulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                    isCloseRequired = true;
                    this.Close();
                }
                catch(IBL.BO.BlException ex)
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
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ModelTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            isCloseRequired = true;
            this.Close();
        }

        private void ListOfStationsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationToList s = (StationToList)ListOfStationsSelector.SelectedItem;
            LongitudeTextBox.Text = blObject.GetLocationOfStation(s).Longitude.ToString();
            LatitudeTextBox.Text = blObject.GetLocationOfStation(s).Latitude.ToString();
        }

        private void MaxWeightSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MaxWeightSelector.SelectedIndex = -1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseRequired;
        }

        private bool isInputValid()
        {
            IDTextBox.BorderBrush = Brushes.Black;
            ModelTextBox.BorderBrush = Brushes.Black;

            bool valid = true;
            if (drone.Id <= 0)
            {
                IDTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show("ID must be positive", "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
                valid = false;
            }

            return valid;
            
        }
    }
}