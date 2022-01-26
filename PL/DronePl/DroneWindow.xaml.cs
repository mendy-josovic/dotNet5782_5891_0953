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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBl blObject;
        Drone drone = new();
        bool isCloseRequired;

        /// <summary>
        /// A window ctor for adding a drone
        /// </summary>
        /// <param name="blObject">object of BL</param>
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
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(Weight));
            StatusSelector.Items.Add("In Maintenance");
            ListOfStationsSelector.ItemsSource = this.blObject.DisplayStationList(d => d.ReadyStandsInStation > 0);
            DeliveryButton.Visibility = Visibility.Hidden;
            ChargingButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// A toolTip that the battery field is automatically given
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BatteryProgressBar_MouseEnter(object sender, MouseEventArgs e)
        {
            if (AddADroneButton.Visibility == Visibility.Visible)
            {
                ProgressBar t = sender as ProgressBar;
                if (t != null)
                {
                    ToolTip tt = new ToolTip();
                    tt.Content = "This field is automatically given";
                    t.ToolTip = tt;
                }
            }
        }

        /// <summary>
        /// Add a drone and write a messageBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddADroneButton_Click(object sender, RoutedEventArgs e)
        {
            StationToList station = ListOfStationsSelector.SelectedItem as StationToList;
            if (station != null && isInputValid())
            {
                if (station.ReadyStandsInStation == 0)
                {
                    MessageBox.Show("There are no ready stands at this station!", "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    blObject.AddDrone(drone, station.Id);
                    MessageBox.Show("Successfully added drone!", "Congradulations!", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }

        /// <summary>
        /// Allows to enter numbers only to ID textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Allows to enter numbers and letters only to Model textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModelTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Closes this window when close-button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            isCloseRequired = true;
            this.Close();
        }

        /// <summary>
        /// Updates the location according to the station where the drone is located
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListOfStationsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationToList s = (StationToList)ListOfStationsSelector.SelectedItem;
            LongitudeTextBox.Text = blObject.GetLocationOfStation(s).Longitude.ToString();
            LatitudeTextBox.Text = blObject.GetLocationOfStation(s).Latitude.ToString();
        }


        /// <summary>
        /// Deselect item in Max-weight selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxWeightSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MaxWeightSelector.SelectedIndex = -1;
        }

        /// <summary>
        /// Checks if the window can be closed as requested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseRequired;
        }

        /// <summary>
        /// Checks whether a valid value has been obtained (not consumed because '-' cannot be inserted)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Convet for view the location in a sexagesimal form
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public string ConvertToSexagesimal(double? point)
        {
            if (point == null)
                throw new NullReferenceException("point is null");

            int Degrees = (int)point;
            point -= Degrees;
            point *= 60;
            int Minutes = (int)point;
            point -= Minutes;
            point *= 60;
            double? Second = point;
            return $"{Degrees}° {Minutes}' {string.Format("{0:0.###}", Second)}\" ";
        }
    }
}