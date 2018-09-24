using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ParkingLotManager;

namespace ParkingLot.Test
{
    [TestFixture]
    public class SearchServiceTests
    {
        private ISpotSearchService _svc;

        [SetUp]
        public void SetUp()
        {
            _svc = new SpotSearchService();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void SearchShouldReturnNullWhenNoMatchingElementAvailable()
        {
            ParkingSpot result = null;
            result = _svc.Search(GetFakeParkingSpots(3, Size.Hatchback), Size.Sedan);
            Assert.AreEqual(result, null);
            result = _svc.Search(GetFakeParkingSpots(3, Size.Hatchback), Size.Truck);
            Assert.AreEqual(result, null);
        }

        [Test]
        public void SearchShouldReturnNotOccupiedFirstElementOfSearchTypeWhenAvailable()
        {
            ParkingSpot result = null;
            result = _svc.Search(GetFakeParkingSpots(3, Size.Hatchback), Size.Hatchback);
            Assert.AreEqual(Size.Hatchback,result.SpotSize);
            Assert.AreEqual(null, result.OccupiedVehicle);
        }

        //Create Fake Parking Lots
        private static IEnumerable<ParkingSpot> GetFakeParkingSpots(int count, Size size)
        {
            var x = new List<ParkingSpot>();
            for (var j = 0; j < count; j++)
            {
                x.Add(new ParkingSpot { OccupiedVehicle = null, SpotSize = size });
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
            return new Vehicle { VehicleSize = size };
        }
    }
}
