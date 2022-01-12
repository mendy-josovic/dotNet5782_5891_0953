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
using System.Collections.ObjectModel;
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
        ///         
        public IEnumerable<IGrouping<StatusOfDrone, DroneToList>> ListOfDrones { get; set; }

        IEnumerable<StationToList> stations;
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="blObject"></param>
        public DroneListWindow(IBl blObject)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            ListOfDrones = from l in blObject.DisplayDroneList()
                           group l by l.status;

            InitializeComponent();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(StatusOfDrone));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(Weight));
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
            stations = blObject.DisplayStationList(d => d.ReadyStandsInStation > 0);
            if (stations.Count() > 0)
            {
                new DroneWindow(blObject).ShowDialog();
                DisplayListBySelectors();
            }
            else
            {
                MessageBox.Show("Sorry, but there are no stations with ready stands.\nYou can try again later.", "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ValueDronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView selectedListView = sender as ListView;
            if (selectedListView != null)
            {
                new DroneWindow(blObject, blObject.BLDrone((DroneToList)selectedListView.SelectedItem)).Show();
                DisplayListBySelectors();
            }
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
                {
                    DronesListView.ItemsSource = from l in blObject.DisplayDroneList()
                                                 group l by l.status;
                }
                else
                {
                    StatusOfDrone SelectedStatus = (StatusOfDrone)StatusSelector.SelectedItem;

                    DronesListView.ItemsSource = from l in blObject.DisplayDroneList(x => x.status == SelectedStatus)
                                                 group l by l.status;
                }
            }
            else
            {
                if (StatusSelector.SelectedIndex == -1)
                {
                    Weight selectedMaxWeight = (Weight)MaxWeightSelector.SelectedItem;

                    DronesListView.ItemsSource = from l in blObject.DisplayDroneList(x => x.MaxWeight == selectedMaxWeight)
                                                 group l by l.status;
                }
                else
                {
                    Weight selectedMaxWeight = (Weight)MaxWeightSelector.SelectedItem;
                    StatusOfDrone selectedStatus = (StatusOfDrone)StatusSelector.SelectedItem;

                    DronesListView.ItemsSource = from l in blObject.DisplayDroneList(x => x.MaxWeight == selectedMaxWeight && x.status == selectedStatus)
                                                 group l by l.status;
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayListBySelectors();
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren<childItem>(obj))
            {
                return child;
            }

            return null;
        }
    }
}