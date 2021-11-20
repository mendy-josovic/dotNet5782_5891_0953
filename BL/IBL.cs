using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using IBL.BO;
namespace IBL
{
    public interface IBl
    {
        public void AddStation(Station sta);
        public void AddDrone(Drone dro, int IDOfStation);
        public void AddCustomer(Customer cus);
        public void AddParcel(Parcel par);
        public int GetClosestStation(Location a);
        public double GetDistance(Location a, Location b);
        public Location GetLocationOfStation(int ID);
        
    }
}
