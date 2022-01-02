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
        /// <summary>
        /// elemnt named dronetolists that is alredy grooped
        /// </summary>
        IEnumerable<IGrouping<int, StationToList>> ListOfStations { get; set; }

        public StationListWindow(IBl blObject)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            ListOfStations = from l in blObject.DisplayStationList()
                             group l by l.ReadyStandsInStation;

            InitializeComponent();

            StationsListView.ItemsSource = ListOfStations.SelectMany(x => x);
            GroupedStationsListView.Visibility = Visibility.Hidden;
            StationsListView.Visibility = Visibility.Visible;


            GroupByComboBox.Items.Add("Has free stations");
            GroupByComboBox.Items.Add("Number of ready stands");
            ClearButton.Content = "Clear\nyour\nchoice";
        }

        private void GroupByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayListBySelector();
        }

        private void DisplayListBySelector()
        {
            if (GroupByComboBox.SelectedIndex == -1)
            {
                StationsListView.ItemsSource = ListOfStations.SelectMany(x => x);
                GroupedStationsListView.Visibility = Visibility.Hidden;
                StationsListView.Visibility = Visibility.Visible;
            }
            if (GroupByComboBox.SelectedIndex == 0)
            {
                GroupedStationsListView.ItemsSource = (from l in blObject.DisplayStationList()
                                                       group l by l.ReadyStandsInStation)
                                                      .Select(grp => new
                                                      {
                                                          Key = grp.Key > 0 ? "Has Free stands" : "All stands are busy",
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
            ClearButton.Visibility = Visibility.Visible;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            GroupByComboBox.SelectedIndex = -1;
            ClearButton.Visibility = Visibility.Hidden;
        }

        private void GroupByComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GroupByComboBox.SelectedIndex = -1;
            ClearButton.Visibility = Visibility.Hidden;
        }

        private void ValueStationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView selectedListView = sender as ListView;
            if (selectedListView != null)
            {
                new DroneWindow(blObject, blObject.BLDrone((DroneToList)selectedListView.SelectedItem)).ShowDialog();
                //DisplayListBySelectors();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseButtonPressed;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            isCloseButtonPressed = true;
            this.Close();
        }
    }
}
