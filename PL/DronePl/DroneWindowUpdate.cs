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
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace PL
{
    public partial class DroneWindow : Window
    {
        string PickupContent = "Pick-up at sender";
        string RecieverContent = "Recieve parcel";
        bool isManualModePressed;
        BackgroundWorker worker;
        RefreshSimulatorEvent refreshSimulatorEvent = new();

        /// <summary>
        /// A window ctor that shows drone details and gives update options 
        /// </summary>
        /// <param name="blObject">object of BL</param>
        /// <param name="dro">the drone of this window</param>
        public DroneWindow(IBl blObject, Drone dro)
        {
            InitializeComponent();
            AddADroneButton.Visibility = Visibility.Hidden;           
            IDTextBox.IsReadOnly = true;
            MaxWeightSelector.IsEnabled = false;
            StatusSelector.IsEnabled = false;
            ListOfStationsSelector.Visibility = Visibility.Hidden;
            x1.Visibility = Visibility.Hidden;
            x2.Visibility = Visibility.Hidden;
            x3.Visibility = Visibility.Hidden;
            x4.Visibility = Visibility.Hidden;
            x5.Visibility = Visibility.Hidden;
            x6.Visibility = Visibility.Hidden;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(Weight));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(StatusOfDrone));
            this.blObject = blObject;
            drone = dro;
            AddDrone.DataContext = drone;
            LongitudeTextBox.Text = ConvertToSexagesimal(drone.ThisLocation.Longitude);
            InitializeButtons(drone);
            AddDroneLabel.Content = String.Format("Drone {0}",dro.Id);
        }

        /// <summary>
        /// sends or returns drone from charging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Charging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!(drone.status==StatusOfDrone.InMaintenance))
                    blObject.SendDroneToCarge(drone.Id);
                else
                {
                    blObject.ReturnDroneFromeCharging(drone.Id,1);
                }
                drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                AddDrone.DataContext = drone;
                InitializeButtons(drone);
            }
            catch (BO.BlException ex)
            {
                try
                {
                    throw new PLExceptions(ex.Message);
                }
                catch (PLExceptions ex2)
                {
                    String message = String.Format("Something went wrong...\n{0}", ex2.Message);
                    MessageBox.Show(message, "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        /// <summary>
        /// Sends the drone for delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null)
                {
                    if (button.Content == "Send Drone To Delivery")
                    {
                        blObject.AssignDronToParcel(drone.Id);
                        drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                        AddDrone.DataContext = drone;
                        InitializeButtons(drone);
                    }
                    else if (button.Content == PickupContent)
                    {
                        blObject.PickUp(drone.Id);
                        drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                        AddDrone.DataContext = drone;
                        InitializeButtons(drone);
                    }
                    else
                    {
                        blObject.Suuply(drone.Id);
                        drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                        AddDrone.DataContext = drone;
                        InitializeButtons(drone);
                    }

                    refreshSimulatorEvent.RaiseEvent();
                }
            }
            catch (BO.BlException ex)
            {
                try
                {
                    throw new PLExceptions(ex.Message);
                }
                catch (PLExceptions ex2)
                {
                    String message = String.Format("Something went wrong...\n{0}", ex2.Message);
                    MessageBox.Show(message, "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        /// <summary>
        /// updates the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                blObject.UpdatDroneName(drone.Id, drone.Model);
                drone = blObject.BLDrone(blObject.DisplayDrone(drone.Id));
                AddDrone.DataContext = drone;
                InitializeButtons(drone);
                MessageBox.Show("Successfully updated drone!", "Congradulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                refreshSimulatorEvent.RaiseEvent();
            }
            catch (BO.BlException ex)
            {
                try
                {
                    throw new PLExceptions(ex.Message);
                }
                catch (PLExceptions ex2)
                {
                    String message = String.Format("Something went wrong...\n{0}", ex2.Message);
                    MessageBox.Show(message, "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Updates controls at the touch of a button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeliveryButton.IsEnabled = true;
            ChargingButton.IsEnabled = true;
        }

        /// <summary>
        /// Opens the window of the parcel that in delvery by this drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeliveryTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int.TryParse(DeliveryTextBox.Text, out int x);
            if (DeliveryTextBox.Text != "")
                new ParcelWindow(blObject, x).Show();
        }

        /// <summary>
        /// Updates controls at the touch of a button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManualButton_Click(object sender, RoutedEventArgs e)
        {
            isManualModePressed = true;
            ManualButton.Visibility = Visibility.Hidden;
            AutoButton.Visibility = Visibility.Visible;
            DeliveryButton.Visibility = Visibility.Visible;
            ChargingButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            CloseButton.IsEnabled = true;
            worker.CancelAsync();
        }

        /// <summary>
        /// Turns on the backgroundWorker and updates controls at the touch of a button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoButton_Click(object sender, RoutedEventArgs e)
        {
            ManualButton.Visibility = Visibility.Visible;
            AutoButton.Visibility = Visibility.Hidden;
            CloseButton.IsEnabled = false;
            DeliveryButton.Visibility = Visibility.Hidden;
            ChargingButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Dowork of the worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int droneId = 0;
            IDTextBox.Dispatcher.Invoke(new Action(delegate ()
            {
                droneId = Int32.Parse(IDTextBox.Text);
            }));
            Action workerProgress = new Action(WorkerProgress);
            Func<bool> cancelWorker = new Func<bool>(CancelWorker);
            blObject.RunSimulator(droneId, workerProgress, cancelWorker);
        }

        /// <summary>
        /// Closes the dorne window when the worker completed (the manual button pressed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isManualModePressed)
            {
                isCloseRequired = true;
                this.Close();
            }
        }


        /// <summary>
        /// updates the windows when simulator does changes
        /// </summary>
        private void WorkerProgress()
        {
            int droneId = 0;
            IDTextBox.Dispatcher.Invoke(new Action(delegate ()
            {
                droneId = Int32.Parse(IDTextBox.Text);
            }));

            drone = blObject.BLDrone(blObject.DisplayDrone(droneId));

            AddDrone.Dispatcher.Invoke(new Action(delegate ()
            {
                AddDrone.DataContext = drone;
            }));

            refreshSimulatorEvent.RaiseEvent();
        }


        /// <summary>
        /// The function returns true if the button to stop the simulator is pressed
        /// </summary>
        /// <returns></returns>
        private bool CancelWorker()
        {
            return isManualModePressed;
        }

        /// <summary>
        /// Updates the buttons according to the drone position
        /// </summary>
        /// <param name="drone"></param>
        private void InitializeButtons(Drone drone)
        {
            if (drone.status == StatusOfDrone.Available)
            {
                DeliveryButton.Content = "Send Drone To Delivery";
                ChargingButton.Content = "Send Drone to Charge";
                DeliveryButton.IsEnabled = true;
                UpdateButton.IsEnabled = true;
                ChargingButton.IsEnabled = true;
            }
            if (drone.status == StatusOfDrone.Delivery)
            {
                if (!drone.parcel.PickedUp)
                {
                    DeliveryButton.Content = PickupContent;
                    ChargingButton.IsEnabled = false;
                    UpdateButton.IsEnabled = false;
                }
                else
                {
                    DeliveryButton.Content = RecieverContent;
                    ChargingButton.IsEnabled = false;
                    UpdateButton.IsEnabled = false;
                }
            }
            if (drone.status == StatusOfDrone.InMaintenance)
            {
                ChargingButton.Content = "Return Drone From Charging";
                DeliveryButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
            }
        }
    }
}
