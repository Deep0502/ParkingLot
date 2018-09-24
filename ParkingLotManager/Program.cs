using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotManager
{
    class Program
    {
        //Create Fake Parking Lots
        private static IEnumerable<ParkingSpot> GetFakeParkingSpots(int count, Size size)
        {
            var x = new List<ParkingSpot>();
            for (var j = 0; j < count; j++)
            {
                x.Add(new ParkingSpot {OccupiedVehicle = null,SpotSize = size});
            }
            return x;
        }
        private static IEnumerable<ParkingSpot> GetFakeParkingLot()
        {
            var x = new List<ParkingSpot>();
            x.AddRange(GetFakeParkingSpots(5, Size.Hatchback));
            x.AddRange(GetFakeParkingSpots(5, Size.Sedan));
            x.AddRange(GetFakeParkingSpots(5, Size.Truck));
            return x;
        }

        //Create Fake Vehicles
        private static Vehicle GetFakeVehicle(Size size)
        {
            return new Vehicle {VehicleSize = size};
        }


        //Main Method who calls the AssignParking
        static void Main(string[] args)
        {
            
            IParkingLotService myTestParkingLot = new ParkingLot(GetFakeParkingLot(), new SpotSearchService());

            ParkingSpot spot;

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);


            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Console.WriteLine(spot == null ? "No spot available" : "Assigned spot with size: " + spot.SpotSize);
                                   
            Console.Read();
        }
    }
}
