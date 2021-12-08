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
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBl blObject;
        Drone drone = new();
        public DroneWindow(IBl blObject)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.blObject = blObject;
            
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WEIGHT));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(STATUS_OF_DRONE));


            //AddDroneLabel.Visibility = Visibility.Hidden;
        }
        public DroneWindow(IBl blObject, Drone dro)
        {
            InitializeComponent();
            AddDrone.DataContext = dro;


        }




        private void IDTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t != null)
            {
                ToolTip tt = new ToolTip();
                tt.Content = "The ID is automatically given";
                t.ToolTip = tt;
            }
        }

        private void BatteryTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t != null)
            {
                ToolTip tt = new ToolTip();
                tt.Content = "This file is automatically given";
                t.ToolTip = tt;
            }
        }

        private void DeliveryTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (t != null)
            {
                ToolTip tt = new ToolTip();
                tt.Content = "This file does not initialize here";
                t.ToolTip = tt;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddDrone.DataContext = drone;
            int idOfStation = 4;
            blObject.AddDrone(drone, idOfStation);
        }

        //private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        //{
        //    Regex regex = new Regex("[^0-9]");
        //    e.Handled = regex.IsMatch(e.Text);
        //}
    }
}
