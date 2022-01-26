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
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
    {
        IBl blObject;
        bool isCloseButtonPressed;
        RefreshSimulatorEvent refreshSimulatorEvent = new();

        /// <summary>
        /// elemnt named ListOfStations that is alredy grooped
        /// </summary>
        IEnumerable<IGrouping<int, StationToList>> ListOfStations { get; set; }

        /// <summary>
        /// ctor of stationsList window
        /// </summary>
        /// <param name="blObject">object of BL</param>
        public StationListWindow(IBl blObject)
        {
            //Write down the function for the event
            refreshSimulatorEvent.AddEventHandler(new Action(RefreshEventHandler));

            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            ListOfStations = from l in blObject.DisplayStationList()
                             group l by l.ReadyStandsInStation;

            StationsListView.ItemsSource = ListOfStations.SelectMany(x => x);
            GroupedStationsListView.Visibility = Visibility.Hidden;
            StationsListView.Visibility = Visibility.Visible;

            GroupByComboBox.Items.Add("Has free stations");
            GroupByComboBox.Items.Add("# ready stands");
        }

        /// <summary>
        /// Writes down DisplayListBySelectors function for the event
        /// </summary>
        private void RefreshEventHandler()
        {
            this.Dispatcher.Invoke(new Action(DisplayListBySelector));
        }

        /// <summary>
        /// Groups the stations list by the choose at the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayListBySelector();
        }

        /// <summary>
        /// Display list of drones by selection of selectors
        /// </summary>
        private void DisplayListBySelector()
        {
            if (GroupByComboBox.SelectedIndex == -1)
            {
                ListOfStations = from l in blObject.DisplayStationList()
                                 group l by l.ReadyStandsInStation;
                StationsListView.ItemsSource = ListOfStations.SelectMany(x => x);
                GroupedStationsListView.Visibility = Visibility.Hidden;
                StationsListView.Visibility = Visibility.Visible;
            }
            if (GroupByComboBox.SelectedIndex == 0)
            {
                GroupedStationsListView.ItemsSource = (from l in blObject.DisplayStationList()
                                                       group l by l.ReadyStandsInStation > 0)
                                                      .Select(grp => new
                                                      {
                                                          Key = grp.Key == true ? "Has Free stands" : "All stands are busy",
                                                          Value = grp.ToList(),
                                                          Count = grp.ToList().Count()
                                                      });
                    
                    
                GroupedStationsListView.Visibility = Visibility.Visible;
                StationsListView.Visibility = Visibility.Hidden;
            }
            if (GroupByComboBox.SelectedIndex == 1)
            {
                GroupedStationsListView.ItemsSource = (from l in blObject.DisplayStationList()
                                                       group l by l.ReadyStandsInStation)
                                                       .Select(grp => new
                                                       {
                                                           Key = String.Format("# free stands: {0}", grp.Key),
                                                           Value = grp.ToList(),
                                                           Count = grp.ToList().Count()
                                                       });

                GroupedStationsListView.Visibility = Visibility.Visible;
                StationsListView.Visibility = Visibility.Hidden;
            }
            if (GroupByComboBox.SelectedIndex != -1)
                ClearButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Deselect item in the selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            GroupByComboBox.SelectedIndex = -1;
            ClearButton.Visibility = Visibility.Hidden;
            ListOfStations = from l in blObject.DisplayStationList()
                             group l by l.ReadyStandsInStation;
            StationsListView.ItemsSource = ListOfStations.SelectMany(x => x);
        }

        /// <summary>
        /// Deselect item in the selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupByComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GroupByComboBox.SelectedIndex = -1;
            ClearButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Opens the window of the station that choosed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValueStationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView selectedListView = sender as ListView;
            if (selectedListView != null)
            {
                new StationWindow(blObject, blObject.BLStation(((StationToList)selectedListView.SelectedItem).Id)).ShowDialog();
                DisplayListBySelector();
            }
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
        /// Opens a new station window for adding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow(blObject).ShowDialog();
            DisplayListBySelector();
        }
    }
}
