using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager
{
    public interface ISpotSearchService
    {
        ParkingSpot Search(IEnumerable<ParkingSpot> parkingSpots, Size type);
    }

    public class SpotSearchService : ISpotSearchService
    {
        public ParkingSpot Search(IEnumerable<ParkingSpot> parkingSpots, Size type)
        {
            return parkingSpots.FirstOrDefault(x => x.OccupiedVehicle == null && x.SpotSize == type);
        }
    }
}
