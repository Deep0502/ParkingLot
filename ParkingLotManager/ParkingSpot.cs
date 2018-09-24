using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager
{
    public class ParkingSpot 
    {
        public Size SpotSize { get; set; }
        public Vehicle OccupiedVehicle { get; set; }
    }

    public enum Size
    {
        Hatchback=1,
        Sedan=2,
        Truck=3
    }
}
