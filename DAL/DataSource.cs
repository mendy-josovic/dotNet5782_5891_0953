using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using namespace IDAL.DO;

namespace DalObject
{
	public class DataSource
	{
		internal static IDAL.DO.Drone[] Drones = new IDAL.DO.Drone[10];
		internal static Station[] Stations = new Station[5];
		internal static Customer[] Customers = new Customer[100];
		internal static Parcel[] Parcels = new Parcel[1000];
	}
}