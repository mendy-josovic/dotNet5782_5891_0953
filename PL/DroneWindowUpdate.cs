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
            AddADrone.Visibility = Visibility.Hidden;
            AddDrone.DataContext = dro;
            IDTextBox.IsReadOnly = true;
            MaxWeightSelector.IsEnabled = false;
            StatusSelector.IsEnabled= false;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WEIGHT));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(STATUS_OF_DRONE));
            this.blObject = blObject;
            drone = dro;
            if (dro.status == STATUS_OF_DRONE.AVAILABLE)
            {
                Delivery.Content = "Send to delivery";
                Charging.Content = "send drone to charge";
            }
            if (dro.status == STATUS_OF_DRONE.DELIVERY)
            {
                if (!dro.parcel.PickedUp)
                {
                    Delivery.Content = "update picke up";
                    Charging.IsEnabled = true;
                    UpdateButton.IsEnabled = true;
                }
                else
                {
                    Delivery.Content = "update dilivery";
                    Charging.IsEnabled = true;
                    UpdateButton.IsEnabled = true;
                }
            }
            if (dro.status == STATUS_OF_DRONE.IN_MAINTENANCE)
            {
                Charging.Content = "return drone from charging";
                Delivery.IsEnabled = true;
                UpdateButton.IsEnabled = true;
            }
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
            Delivery.IsEnabled = true;
            Charging.IsEnabled = true;
        }
    }
}
