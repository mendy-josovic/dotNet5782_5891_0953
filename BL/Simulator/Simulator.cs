using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BO;
using static BL.BL;

namespace BL
{
    internal class Simulator
    {
        double droneSpeed;
        int timerDelay;
        Drone drone;
        BL bl;

        /// <summary>
        /// ctor of simulator
        /// </summary>
        /// <param name="blObject">object of BL</param>
        /// <param name="droneId">ID of this drone</param>
        /// <param name="simulatorProgress">delegate of function that updates the PL with the simulator progress</param>
        /// <param name="cancelSimulator">delegate of boolean function that determines when to stop the simulator</param>
        public Simulator(BL blObject, int droneId, Action simulatorProgress, Func<bool> cancelSimulator)
        {
            this.droneSpeed = 10;
            this.timerDelay = 1000;
            this.bl = blObject;
            lock (this.bl)
            {
                drone = bl.BLDrone(bl.DisplayDrone(droneId));
            }
            if (drone.status == StatusOfDrone.InMaintenance)
            {
                lock (this.bl)
                {
                    this.bl.ReturnDroneFromeCharging(droneId, 1);
                    drone = bl.BLDrone(bl.DisplayDrone(droneId));
                }
                RunProgress(simulatorProgress);
            }
            while (!cancelSimulator())
            {
                try
                {
                    switch (drone.status)
                    {
                        case StatusOfDrone.Available:
                            lock (this.bl)
                            {
                                bl.AssignDronToParcel(droneId);
                                drone = bl.BLDrone(bl.DisplayDrone(droneId));
                            }
                            break;

                        case StatusOfDrone.Delivery:
                            lock (this.bl)
                            {
                                DO.Parcel parcel = bl.DisplayParcel(drone.parcel.Id);
                                if (parcel.PickedUp == null)
                                {
                                    bl.PickUp(droneId);
                                    drone = bl.BLDrone(bl.DisplayDrone(droneId));
                                }
                                else if (parcel.Delivered == null)
                                {
                                    bl.Suuply(droneId);
                                    drone = bl.BLDrone(bl.DisplayDrone(droneId));
                                }
                            }
                            
                            break;
                        default:
                            break;

                    }
                    RunProgress(simulatorProgress);

                }
                catch(BlException ex)
                {
                    
                    while (drone.Battery != 100)
                    {
                        lock (this.bl)
                        {
                            bl.SendDroneToCarge(droneId);
                            drone = bl.BLDrone(bl.DisplayDrone(droneId));
                        }
                        RunProgress(simulatorProgress);
                        lock (this.bl)
                        {
                            bl.ReturnDroneFromeCharging(droneId, 1);
                            drone = bl.BLDrone(bl.DisplayDrone(droneId));
                        }
                        RunProgress(simulatorProgress);
                    }                    
                }
            }
        }

        /// <summary>
        /// Updates the PL with the simulator progress
        /// </summary>
        /// <param name="simulatorProgress">delegate of function that updates the PL with the simulator progress</param>
        private void RunProgress(Action simulatorProgress)
        {
            Thread.Sleep(timerDelay);
            simulatorProgress();
        }
    }
}
