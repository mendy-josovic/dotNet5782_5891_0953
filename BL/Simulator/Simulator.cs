using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BO;
using static BL.BL;

namespace Simulator
{
    internal class Simulator
    {
        double droneSpeed;
        int timerDelay;
        Drone drone;
        BL.BL bl;
        public Simulator(BL.BL blObject, int droneId, Action simulatorProgress, Func<bool> cancelSimulator)
        {
            this.droneSpeed = 10;
            this.timerDelay = 500;
            bl = blObject;
            lock (bl)
            {
                drone = blObject.BLDrone(blObject.DisplayDrone(droneId));
            }
            if (drone.status == StatusOfDrone.InMaintenance)
            {
                lock (bl)
                {
                    bl.ReturnDroneFromeCharging(droneId, 1);
                    drone = blObject.BLDrone(blObject.DisplayDrone(droneId));
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
                            lock (bl)
                            {
                                blObject.AssignDronToParcel(droneId);
                                drone = blObject.BLDrone(blObject.DisplayDrone(droneId));
                            }
                            break;

                        case StatusOfDrone.Delivery:
                            lock (bl)
                            {
                                DO.Parcel parcel = blObject.DisplayParcel(drone.parcel.Id);
                                if (parcel.PickedUp == null)
                                {
                                    blObject.PickUp(droneId);
                                    drone = blObject.BLDrone(blObject.DisplayDrone(droneId));
                                }
                                else if (parcel.Delivered == null)
                                {
                                    blObject.Suuply(droneId);
                                    drone = blObject.BLDrone(blObject.DisplayDrone(droneId));
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
                        lock (bl)
                        {
                            blObject.SendDroneToCarge(droneId);
                            drone = blObject.BLDrone(blObject.DisplayDrone(droneId));
                        }
                        RunProgress(simulatorProgress);
                        lock (bl)
                        {
                            blObject.ReturnDroneFromeCharging(droneId, 1);
                            drone = blObject.BLDrone(blObject.DisplayDrone(droneId));
                        }
                        RunProgress(simulatorProgress);
                    }
                    Thread.Sleep(timerDelay);
                }
            }
        }

        private void RunProgress(Action simulatorProgress)
        {
            Thread.Sleep(timerDelay);
            simulatorProgress();
        }
    }
}
