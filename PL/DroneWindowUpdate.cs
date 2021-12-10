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
            AddDrone.DataContext = dro;
            IDTextBox.IsReadOnly = true;
            MaxWeightSelector.IsEnabled = false;
            StatusSelector.IsEnabled= false;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WEIGHT));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(STATUS_OF_DRONE));
            this.blObject = blObject;
            drone = dro;
            InitializeButtons();
            AddDroneLabel.Content = String.Format("Drone {0}",dro.Id);
        }
        private void Charging_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delivery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            blObject.UpdatDroneName(drone.Id, drone.Model);

        }

        private void ModelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeliveryButton.IsEnabled = true;
            ChargingButton.IsEnabled = true;
        }

        private void InitializeButtons()
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
                    ChargingButton.IsEnabled = true;
                    UpdateButton.IsEnabled = true;
                }
                else
                {
                    DeliveryButton.Content = "Update Delivery";
                    ChargingButton.IsEnabled = true;
                    UpdateButton.IsEnabled = true;
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
