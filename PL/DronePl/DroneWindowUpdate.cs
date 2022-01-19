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
        /// 
        /// </summary>
        /// <param name="blObject"></param>
        /// <param name="dro"></param>
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
        /// 
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

        private void ModelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeliveryButton.IsEnabled = true;
            ChargingButton.IsEnabled = true;
        }

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

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isManualModePressed)
            {
                isCloseRequired = true;
                this.Close();
            }
        }

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

        private bool CancelWorker()
        {
            return isManualModePressed;
        }

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
