using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager
{
    public interface IParkingLotService
    {
        ParkingSpot AssignParkingSpot(Vehicle vehicle);
    }
    
    public class ParkingLot : IParkingLotService
    {
        private readonly IEnumerable<ParkingSpot> _parkingSpots;
        private readonly ISpotSearchService _spotSearchService;

        //Injecting ParkingSpots to ParkingLot
        public ParkingLot(IEnumerable<ParkingSpot> parkingSpots, ISpotSearchService spotSearchService)
        {
            if (parkingSpots == null || !parkingSpots.Any() || spotSearchService == null)
            {
                throw new ApplicationException("Invalid parking lot setup");
            }

            _parkingSpots = parkingSpots;
            _spotSearchService= spotSearchService;
        }

        //Assumption: SpotSize Enum will be maintained in the order of the vehicle size.
        public ParkingSpot AssignParkingSpot(Vehicle vehicle)
        {
            ParkingSpot spot = null;

            //Search spot
            switch (vehicle.VehicleSize)
            {
                case Size.Hatchback:
                    spot=(_spotSearchService.Search(_parkingSpots, Size.Hatchback) ?? _spotSearchService.Search(_parkingSpots, Size.Sedan)) ??
                         _spotSearchService.Search(_parkingSpots, Size.Truck);
                    break;
                case Size.Sedan:
                    spot = _spotSearchService.Search(_parkingSpots, Size.Sedan) ?? _spotSearchService.Search(_parkingSpots, Size.Truck);
                    break;
                case Size.Truck:
                    spot= _spotSearchService.Search(_parkingSpots, Size.Truck);
                    break;
                default:
                    throw new InvalidEnumArgumentException("Unsupported vehicle size");
            }

            //Assign spot
            if (spot != null)
            {
                spot.OccupiedVehicle = vehicle;
            }
            return spot;
        }
    }
}
