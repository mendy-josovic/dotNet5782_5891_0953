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
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        IBl blObject;
        bool isCloseButtonPressed;
        RefreshSimulatorEvent refreshSimulatorEvent = new();

        /// <summary>
        /// Elemnt named dronetolists that is alredy grooped
        /// </summary>     
        public IEnumerable<IGrouping<StatusOfDrone, DroneToList>> ListOfDrones { get; set; }

        IEnumerable<StationToList> stations;

        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="blObject"></param>
        public DroneListWindow(IBl blObject)
        {
            //Write down the function for the event
            refreshSimulatorEvent.AddEventHandler(new Action(RefreshEventHandler));

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            ListOfDrones = (from l in blObject.DisplayDroneList()
                           group l by l.status);

            InitializeComponent();

            //A group with counter of items
            DronesListView.ItemsSource = ListOfDrones.Select(grp => new
            {
                Key = grp.Key,
                Value = grp.ToList(),
                Count = grp.ToList().Count()
            });

            StatusSelector.ItemsSource = Enum.GetValues(typeof(StatusOfDrone));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(Weight));
        }

        /// <summary>
        /// Writes down DisplayListBySelectors function for the event
        /// </summary>
        private void RefreshEventHandler()
        {
            this.Dispatcher.Invoke(new Action(DisplayListBySelectors));
        }

        /// <summary>
        /// Display list of drones by selection in StatusSelector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayListBySelectors();
            ClearButton1.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Display list of drones by selection in MaxWeightSelector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayListBySelectors();
            ClearButton2.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Opens a new drone window for adding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Opens the window of the drone that choosed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValueDronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView selectedListView = sender as ListView;
            if (selectedListView != null)
            {
                new DroneWindow(blObject, blObject.BLDrone((DroneToList)selectedListView.SelectedItem)).Show();
                DisplayListBySelectors();
            }
        }

        /// <summary>
        /// CLoses the window by click the CloseButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            isCloseButtonPressed = true;
            this.Close();
        }

        /// <summary>
        /// Deselect item in status selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StatusSelector.SelectedIndex = -1;
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
        /// Deselect item in status selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton1_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedIndex = -1;
            DisplayListBySelectors();
            ClearButton1.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Deselect item in Max-weight selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton2_Click(object sender, RoutedEventArgs e)
        {
            MaxWeightSelector.SelectedIndex = -1;
            DisplayListBySelectors();
            ClearButton2.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Checks if the window can be closed as requested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseButtonPressed;
        }

        /// <summary>
        /// Display list of drones by selection of selectors
        /// </summary>
        private void DisplayListBySelectors()
        {
            if (MaxWeightSelector.SelectedIndex == -1)
            {
                if (StatusSelector.SelectedIndex == -1)
                {
                    DronesListView.ItemsSource = (from l in blObject.DisplayDroneList()
                                                 group l by l.status)
                                                 .Select(grp => new
                                                 {
                                                     Key = grp.Key,
                                                     Value = grp.ToList(),
                                                     Count = grp.ToList().Count()
                                                 });
                }
                else
                {
                    StatusOfDrone SelectedStatus = (StatusOfDrone)StatusSelector.SelectedItem;

                    DronesListView.ItemsSource = (from l in blObject.DisplayDroneList(x => x.status == SelectedStatus)
                                                 group l by l.status)
                                                 .Select(grp => new
                                                 {
                                                     Key = grp.Key,
                                                     Value = grp.ToList(),
                                                     Count = grp.ToList().Count()
                                                 });
                }
            }
            else
            {
                if (StatusSelector.SelectedIndex == -1)
                {
                    Weight selectedMaxWeight = (Weight)MaxWeightSelector.SelectedItem;

                    DronesListView.ItemsSource = (from l in blObject.DisplayDroneList(x => x.MaxWeight == selectedMaxWeight)
                                                 group l by l.status)
                                                 .Select(grp => new
                                                 {
                                                     Key = grp.Key,
                                                     Value = grp.ToList(),
                                                     Count = grp.ToList().Count()
                                                 });
                }
                else
                {
                    Weight selectedMaxWeight = (Weight)MaxWeightSelector.SelectedItem;
                    StatusOfDrone selectedStatus = (StatusOfDrone)StatusSelector.SelectedItem;

                    DronesListView.ItemsSource = (from l in blObject.DisplayDroneList(x => x.MaxWeight == selectedMaxWeight && x.status == selectedStatus)
                                                 group l by l.status)
                                                 .Select(grp => new
                                                 {
                                                     Key = grp.Key,
                                                     Value = grp.ToList(),
                                                     Count = grp.ToList().Count()
                                                 });
                }
            }
        }

        /// <summary>
        /// Refreshes the buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayListBySelectors();
        }

        //public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        //{
        //    if (depObj != null)
        //    {
        //        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        //        {
        //            DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
        //            if (child != null && child is T)
        //            {
        //                yield return (T)child;
        //            }

        //            foreach (T childOfChild in FindVisualChildren<T>(child))
        //            {
        //                yield return childOfChild;
        //            }
        //        }
        //    }
        //}

        //public static childItem FindVisualChild<childItem>(DependencyObject obj)
        //    where childItem : DependencyObject
        //{
        //    foreach (childItem child in FindVisualChildren<childItem>(obj))
        //    {
        //        return child;
        //    }

        //    return null;
        //}
    }
}