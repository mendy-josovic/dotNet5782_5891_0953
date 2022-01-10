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
    public partial class DroneWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blObject"></param>
        /// <param name="dro"></param>
        public DroneWindow(IBl blObject, Drone dro)
        {
            InitializeComponent();
            AddADroneButton.Visibility = Visibility.Hidden;           
            IDTextBox.IsReadOnly = true;
            MaxWeightSelector.IsEnabled = false;
            StatusSelector.IsEnabled = false;
            ListOfStationsSelector.Visibility = Visibility.Hidden;
            x1.Visibility = Visibility.Hidden;
            x2.Visibility = Visibility.Hidden;
            x3.Visibility = Visibility.Hidden;
            x4.Visibility = Visibility.Hidden;
            x5.Visibility = Visibility.Hidden;
            x6.Visibility = Visibility.Hidden;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(Weight));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(StatusOfDrone));
            this.blObject = blObject;
            drone = dro;
            AddDrone.DataContext = drone;
            LongitudeTextBox.Text = ConvertToSexagesimal(drone.ThisLocation.Longitude);

            InitializeButtons(drone);
            AddDroneLabel.Content = String.Format("Drone {0}",dro.Id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Charging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!(drone.status==StatusOfDrone.InMaintenance))
                    blObject.SendDroneToCarge(drone.Id);
                else
                {
                    blObject.ReturnDroneFromeCharging(drone.Id,1);
                }
                drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                AddDrone.DataContext = drone;
                InitializeButtons(drone);
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

        private void Delivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    if (button.Content == "Send Drone To Delivery")
                    {
                        blObject.AssignDronToParcel(drone.Id);
                        drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                        AddDrone.DataContext = drone;
                        InitializeButtons(drone);
                    }
                    else if (button.Content == "Update Pick-Up")
                    {
                        blObject.PickUp(drone.Id);
                        drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                        AddDrone.DataContext = drone;
                        InitializeButtons(drone);
                    }
                    else
                    {
                        blObject.Suuply(drone.Id);
                        drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                        AddDrone.DataContext = drone;
                        InitializeButtons(drone);
                    }
                }
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
            try
            {
                blObject.UpdatDroneName(drone.Id, drone.Model);
                drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                AddDrone.DataContext = drone;
                InitializeButtons(drone);
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

        private void ModelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeliveryButton.IsEnabled = true;
            ChargingButton.IsEnabled = true;
        }

        private void InitializeButtons(Drone drone)
        {
            if (drone.status == StatusOfDrone.Available)
            {
                DeliveryButton.Content = "Send Drone To Delivery";
                ChargingButton.Content = "Send Drone to Charge";
                DeliveryButton.IsEnabled = true;
                UpdateButton.IsEnabled = true;
                ChargingButton.IsEnabled = true;
            }
            if (drone.status == StatusOfDrone.Delivery)
            {
                if (!drone.parcel.PickedUp)
                {
                    DeliveryButton.Content = "Update Pick-Up";
                    ChargingButton.IsEnabled = false;
                    UpdateButton.IsEnabled = false;
                }
                else
                {
                    DeliveryButton.Content = "Update Delivery";
                    ChargingButton.IsEnabled = false;
                    UpdateButton.IsEnabled = false;
                }
            }
            if (drone.status == StatusOfDrone.InMaintenance)
            {
                ChargingButton.Content = "Return Drone From Charging";
                DeliveryButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
            }
        }
    }
}
