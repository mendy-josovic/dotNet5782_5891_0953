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
        
        public void AddEventHandler(Action handler)
        {
            refreshEvent += handler;
        }

        public void RaiseEvent()
        {
            if (refreshEvent != null)
            {
                refreshEvent();
            }
        }
    }
}
