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
        public Simulator(BL.BL blObject, int droneId, Action simulatorProgress, Func<bool> cancelSimulator)
        {
            this.droneSpeed = 10;
            this.timerDelay = 500;

            while (!cancelSimulator())
            {
                try
                {
                    blObject.AssignDronToParcel(droneId);
                    blObject.PickUp(droneId);
                    RunProgress(simulatorProgress);
                    blObject.Suuply(droneId);
                    RunProgress(simulatorProgress);
                }
                catch(BlException ex)
                {
                    BO.Drone drone = blObject.BLDrone(blObject.DisplayDrone(droneId));
                    if (drone.Battery < 100)
                    {
                        blObject.SendDroneToCarge(droneId);
                        RunProgress(simulatorProgress);
                        blObject.ReturnDroneFromeCharging(droneId, 1);
                        RunProgress(simulatorProgress);
                    }
                    else
                    {
                        Thread.Sleep(timerDelay);
                    }
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
