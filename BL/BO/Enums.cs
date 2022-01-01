using System;
using System.Collections.Generic;
using System.Text;

    namespace BO
    {
        public enum Weight { Light, Medium, Heavy };
        public enum Priority { Regular, Fast, Emergency };
        public enum StatusOfParcel { Created, Associated, PickedUp, Delivered }
        public enum StatusOfDrone { Available, InMaintenance, Delivery }
        public enum ModeOfDroneInMoving { Light, Medium, Heavy, Available }
    }
