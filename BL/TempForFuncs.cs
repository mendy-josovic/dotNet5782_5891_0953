using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
namespace BL
{
    public partial class BL:IBL
    {
        public void AddDroneToList(DroneToList drt)
        {

        }
        public Location GetMinimumDistance(Location a, List<Station> stations)
        {
            Location loc;
            if (stations.Count == 0)
            {
                loc.Longitude = -1;
                loc.Latitude = -1;
                return loc;
            }
            double minimum = GetDistance(a, stations[0].location);
            loc = stations[0].location;
            for (int i = 1; i < stations.Count; i++)
            {
                if (minimum < GetDistance(a, stations[i].location))
                {
                    minimum = GetDistance(a, stations[i].location);
                    loc = stations[i].location;
                }
                Math.Min(minimum, GetDistance(a, stations[i].location));
            }
            return loc;
        }
        public double DistanceOfRout(Location lo, List<Location> lislo)
        {
            double distance = GetDistance(lo, lislo[0]);
            Location tl = lislo[0];
            foreach (Location item in lislo)
            {
                distance += GetDistance(tl, item);
                tl = item;
            }
            return distance;
        }

    }





}
