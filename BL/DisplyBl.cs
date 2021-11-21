using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using IBL.BO;
using System.Collections;
using IBL;
namespace BL
{
    public partial class BL : IBl
    {
        public IDAL.DO.Station DisplayStation(int id)
        {
            List<IDAL.DO.Station> tempDataStations = new(Data.PrintStationList());
            return (tempDataStations.Find(w => w.Id == id));
        }
    }
}
