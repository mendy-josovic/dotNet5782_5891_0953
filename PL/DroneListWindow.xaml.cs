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
        bool isCloseButtonPressed;
        public DroneListWindow(IBl blObject)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;

            DronesListView.ItemsSource = this.blObject.DisplayDroneList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(STATUS_OF_DRONE));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WEIGHT));
            ClearButton1.Content = "Clear\nyour\nchoice";
            ClearButton2.Content = "Clear\nyour\nchoice";
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayListBySelectors();
            ClearButton1.Visibility = Visibility.Visible;
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayListBySelectors();
            ClearButton2.Visibility = Visibility.Visible;
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(blObject).ShowDialog();
            DisplayListBySelectors();
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new DroneWindow(blObject, blObject.BLDrone((DroneToList)DronesListView.SelectedItem)).ShowDialog();
            DisplayListBySelectors();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            isCloseButtonPressed = true;
            this.Close();
        }

        private void StatusSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StatusSelector.SelectedIndex = -1;
        }

        private void MaxWeightSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MaxWeightSelector.SelectedIndex = -1;
        }

        private void ClearButton1_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedIndex = -1;
            DisplayListBySelectors();
            ClearButton1.Visibility = Visibility.Hidden;
        }

        private void ClearButton2_Click(object sender, RoutedEventArgs e)
        {
            MaxWeightSelector.SelectedIndex = -1;
            DisplayListBySelectors();
            ClearButton2.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseButtonPressed;
        }

        /// <summary>
        /// display list of drones by selection of selectors
        /// </summary>
        private void DisplayListBySelectors()
        {
            if (MaxWeightSelector.SelectedIndex == -1)
            {
                if (StatusSelector.SelectedIndex == -1)
                    DronesListView.ItemsSource = this.blObject.DisplayDroneList();
                else
                {
                    STATUS_OF_DRONE selectedStatus = (STATUS_OF_DRONE)StatusSelector.SelectedItem;
                    DronesListView.ItemsSource = this.blObject.DisplayDroneList(d => d.status == selectedStatus);
                }
            }
            else
            {
                if (StatusSelector.SelectedIndex == -1)
                {
                    WEIGHT selectedMaxWeight = (WEIGHT)MaxWeightSelector.SelectedItem;
                    DronesListView.ItemsSource = this.blObject.DisplayDroneList(d => d.MaxWeight == selectedMaxWeight);
                }
                else
                {
                    WEIGHT selectedMaxWeight = (WEIGHT)MaxWeightSelector.SelectedItem;
                    STATUS_OF_DRONE selectedStatus = (STATUS_OF_DRONE)StatusSelector.SelectedItem;
                    DronesListView.ItemsSource = this.blObject.DisplayDroneList(d => d.status == selectedStatus && d.MaxWeight == selectedMaxWeight);

                }
            }
        }
    }
}
