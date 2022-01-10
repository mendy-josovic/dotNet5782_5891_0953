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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        IBl blObject;
        Station station = new();
        IEnumerable<DroneToList> ListOfDrones; 
        bool isCloseRequired = false;

        public StationWindow(IBl blObject)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            ADrone.DataContext = station;
            ListOfDrones = blObject.DisplayDroneList(w => w.ThisLocation.Longitude == station.location.Longitude && w.ThisLocation.Latitude == station.location.Latitude && w.status == StatusOfDrone.InMaintenance);
        }

        public StationWindow(IBl blObject, Station s)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            station = s;           
            ADrone.DataContext = station;
            DronesListView.ItemsSource = station.ListOfDrones;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !isCloseRequired;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isCloseRequired = true;
            this.Close();
        }
    }


}
