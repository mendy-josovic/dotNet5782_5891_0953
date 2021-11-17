using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public enum WEIGHT { LIGHT, MEDIUM, HEAVY };
        public enum PRIORITY { REGULAR, FAST, EMERGENCY };
        public enum STATUS_OF_PARCEL { CREATED, ASSOCIATED, PICKEDUP, DELIVERED }
        public enum STATUS_OF_DRONE { AVAILABLE, IN_MAINTENANCE, DELIVERY }
        public enum MODE_OF_DRONE_IN_MOVING { LIGHT, MEDIUM, HEAVY, AVAILABLE }
    }
}
