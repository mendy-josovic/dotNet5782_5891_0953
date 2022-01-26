using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class RefreshSimulatorEvent
    {
        public static event Action refreshEvent;
        
        /// <summary>
        /// adds a function to the event
        /// </summary>
        /// <param name="handler">The function to add</param>
        public void AddEventHandler(Action handler)
        {
            refreshEvent += handler;
        }

        /// <summary>
        /// Runs the event
        /// </summary>
        public void RaiseEvent()
        {
            if (refreshEvent != null)
            {
                refreshEvent();
            }
        }
    }
}
