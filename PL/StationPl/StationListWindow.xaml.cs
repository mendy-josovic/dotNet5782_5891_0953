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
        IEnumerable<IGrouping<int, StationToList>> stationToLists;
        public StationListWindow(IBl blObject)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            stationToLists = from l in blObject.DisplayStationList()
                             group l by l.ReadyStandsInStation into k
                             select k;
            StationsListView.ItemsSource = stationToLists.SelectMany(k => k);
            ReadyStandsSelector.Items.Add("Stations with ready stands");
            ReadyStandsSelector.Items.Add("Number of ready stands");
            ClearButton.Content = "Clear\nyour\nchoice";
        }

        private void ReadyStandsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayListBySelector();
        }

        private void DisplayListBySelector()
        {
            if (ReadyStandsSelector.SelectedIndex == -1)
            {
                StationsListView.ItemsSource = stationToLists.SelectMany(k => k);
            }
            if (ReadyStandsSelector.SelectedIndex == 0)
            {
                StationsListView.ItemsSource = stationToLists.SelectMany(k => k).OrderBy(k => k.ReadyStandsInStation == 0);
            }
            if (ReadyStandsSelector.SelectedIndex == 1)
            {
                StationsListView.ItemsSource = stationToLists.SelectMany(k => k).OrderBy(k => k.ReadyStandsInStation);
            }
            ClearButton.Visibility = Visibility.Visible;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ReadyStandsSelector.SelectedIndex = -1;
            ClearButton.Visibility = Visibility.Hidden;
        }

        private void ReadyStandsSelector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ReadyStandsSelector.SelectedIndex = -1;
            ClearButton.Visibility = Visibility.Hidden;
        }

        private void StationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //new StationWindow(blObject, blObject.BLStation((StationToList)StationsListView.SelectedItem)).ShowDialog();
            //DisplayListBySelector();
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
