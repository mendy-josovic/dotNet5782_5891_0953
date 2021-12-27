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
    /// a class for the grooping... its a class from twe typs
    /// </summary>
    //public class StatusAndWeightOfDrone
    //{
    //    public WEIGHT Weight { get; set; }
    //    public STATUS_OF_DRONE status { get; set; }

    //    public override string ToString()
    //    {
    //        return $"{Weight}-{status}";
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        StatusAndWeightOfDrone other = (StatusAndWeightOfDrone)obj;
    //        return Weight == other.Weight && status == other.status;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return 0;
    //    }
    //}

    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        IBl blObject;
        bool isCloseButtonPressed;
        /// <summary>
        /// elemnt named dronetolists that is alredy grooped
        /// </summary>
        public IEnumerable<IGrouping<STATUS_OF_DRONE, DroneToList>> ListOfDrones { get; set; }

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="blObject"></param>
        public DroneListWindow(IBl blObject)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            ListOfDrones = from l in blObject.DisplayDroneList()
                               //group l by new StatusAndWeightOfDrone { status = l.status, Weight = l.MaxWeight };
                           group l by l.status;

            InitializeComponent();

            //DronesListView.ItemsSource = droneToLists.SelectMany(x => x);
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
        /// display list of drones by selection of sel67ectors
        /// and its doing it thruh grooping
        /// </summary>
        private void DisplayListBySelectors()
        {
            if (MaxWeightSelector.SelectedIndex == -1)
            {
                if (StatusSelector.SelectedIndex == -1)
                    DronesListView.ItemsSource = ListOfDrones.SelectMany(x => x);
                else
                {
                    STATUS_OF_DRONE SelectedStatus = (STATUS_OF_DRONE)StatusSelector.SelectedItem;
                    ListOfDrones = from l in blObject.DisplayDroneList(x=>x.status == SelectedStatus)
                                   group l by l.status;
                    DronesListView.ItemsSource = ListOfDrones.SelectMany(x=>x);

                }
            }
            else
            {
                if (StatusSelector.SelectedIndex == -1)
                {
                    WEIGHT selectedMaxWeight = (WEIGHT)MaxWeightSelector.SelectedItem;
                    //DronesListView.ItemsSource = ListOfDrones.Where(x => x.Key.Weight == selectedMaxWeight).SelectMany(x => x);
                    ListOfDrones = from l in blObject.DisplayDroneList(x => x.MaxWeight == selectedMaxWeight)
                                   group l by l.status;
                    DronesListView.ItemsSource = ListOfDrones.SelectMany(x => x);
                }
                else
                {
                    WEIGHT selectedMaxWeight = (WEIGHT)MaxWeightSelector.SelectedItem;
                    STATUS_OF_DRONE selectedStatus = (STATUS_OF_DRONE)StatusSelector.SelectedItem;
                    //DronesListView.ItemsSource = ListOfDrones.Where(x => x.Key.status == selectedStatus && x.Key.Weight == selectedMaxWeight).SelectMany(x => x);
                    ListOfDrones = from l in blObject.DisplayDroneList(x => x.MaxWeight == selectedMaxWeight && x.status == selectedStatus)
                                   group l by l.status;
                    DronesListView.ItemsSource = ListOfDrones.SelectMany(x => x);
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayListBySelectors();
        }
    }
}