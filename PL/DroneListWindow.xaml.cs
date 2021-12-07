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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        IBl blObject;
        public DroneListWindow(IBl blObject)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;

            DronesListView.ItemsSource = this.blObject.DisplayDroneList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(STATUS_OF_DRONE));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WEIGHT));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            STATUS_OF_DRONE selectedStatus = (STATUS_OF_DRONE)StatusSelector.SelectedItem;
            DronesListView.ItemsSource = this.blObject.DisplayDroneList(d=>d.status==selectedStatus);
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WEIGHT selectedMaxWeight = (WEIGHT)MaxWeightSelector.SelectedItem;
            DronesListView.ItemsSource = this.blObject.DisplayDroneList(d => d.MaxWeight == selectedMaxWeight);
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow droneWindow = new DroneWindow(blObject);
            droneWindow.Show();
        }
    }
}
