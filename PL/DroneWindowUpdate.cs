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
    public partial class DroneWindow : Window
    {
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
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WEIGHT));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(STATUS_OF_DRONE));
            this.blObject = blObject;
            drone = dro;
            AddDrone.DataContext = drone;
            InitializeButtons(drone);
            AddDroneLabel.Content = String.Format("Drone {0}",dro.Id);
        }
        private void Charging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    if (button.Content == "Send Drone to Charge")
                    {
                        blObject.SendDroneToCarge(drone.Id);
                        drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                        AddDrone.DataContext = drone;
                        InitializeButtons(drone);
                    }
                    //else
                    //{
                    //    blObject.ReturnDroneFromeCharging(drone.Id, );
                    //    drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                    //    AddDrone.DataContext = drone;
                    //    InitializeButtons(drone);
                    //}
                }
            }
            catch (IBL.BO.BlException ex)
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
            catch (IBL.BO.BlException ex)
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
            catch (IBL.BO.BlException ex)
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
            if (drone.status == STATUS_OF_DRONE.AVAILABLE)
            {
                DeliveryButton.Content = "Send Drone To Delivery";
                ChargingButton.Content = "Send Drone to Charge";
            }
            if (drone.status == STATUS_OF_DRONE.DELIVERY)
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
            if (drone.status == STATUS_OF_DRONE.IN_MAINTENANCE)
            {
                ChargingButton.Content = "Return Drone From Charging";
                DeliveryButton.IsEnabled = true;
                UpdateButton.IsEnabled = true;
            }
        }
    }
}
