using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ParkingLotManager;

namespace Test
{

    [TestFixture]
    public class ParkingLotServiceTests
    {

        private Mock<ISpotSearchService> _searchService;
        private IParkingLotService _svc;

        [SetUp]
        public void SetUp()
        {
            _searchService = new Mock<ISpotSearchService>(MockBehavior.Strict);
        }

        [TearDown]
        public void TearDown()
        {
            _searchService.VerifyAll();
        }

        [TestCase(Size.Truck)]
        [TestCase(Size.Sedan)]
        [TestCase(Size.Hatchback)]
        public void ShouldAssignSameSizedSpotWhenAvailable(Size size)
        {
            var testParkingLot = GetFakeParkingLot(); //Fake lot with  5 slots in each type.
            var testVehicle = GetFakeVehicle(size);
            var expectedSpot = new ParkingSpot {OccupiedVehicle = testVehicle, SpotSize = testVehicle.VehicleSize};

            _svc = new ParkingLotManager.ParkingLot(testParkingLot, _searchService.Object);
            switch (size)
            {
                case Size.Hatchback:
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Hatchback)).Returns(expectedSpot);
                    
                    break;
                case Size.Sedan:
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Sedan)).Returns(expectedSpot);

                    break;
                case Size.Truck:
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Truck)).Returns(expectedSpot);
                    break;
            }
            ParkingSpot result = _svc.AssignParkingSpot(testVehicle);

            Assert.AreEqual(result.SpotSize, expectedSpot.SpotSize);
            Assert.AreEqual(result.OccupiedVehicle, expectedSpot.OccupiedVehicle);
        }

        [TestCase(Size.Hatchback)]
        [TestCase(Size.Sedan)]
        [TestCase(Size.Truck)]
        public void ShouldAssignLargerSpotWhenSameSizedSpotsAreUnavailable(Size size)
        {
            var testParkingLot = GetFakeParkingLot(); //Fake lot with  5 slots in each type.

            var testVehicle = GetFakeVehicle(size);
            ParkingSpot expectedSpot = new ParkingSpot {OccupiedVehicle = testVehicle};
            switch (size)
            {
                case Size.Hatchback:
                    expectedSpot.SpotSize = size = Size.Sedan;
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Hatchback)).Returns((ParkingSpot)null);
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Sedan)).Returns(expectedSpot);

                    break;
                case Size.Sedan:
                    expectedSpot.SpotSize = size = Size.Truck;
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Sedan)).Returns((ParkingSpot)null);
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Truck)).Returns(expectedSpot);
                    
                    break;
                case Size.Truck:
                    expectedSpot.SpotSize = size = Size.Truck;
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Truck)).Returns(expectedSpot);
                    break;
            }

            _svc = new ParkingLotManager.ParkingLot(testParkingLot, _searchService.Object);

            ParkingSpot result = _svc.AssignParkingSpot(testVehicle);

            Assert.AreEqual(result.SpotSize, expectedSpot.SpotSize);
            Assert.AreEqual(result.OccupiedVehicle, expectedSpot.OccupiedVehicle);
        }


        [TestCase(Size.Hatchback)]
        [TestCase(Size.Sedan)]
        [TestCase(Size.Truck)]
        public void ShouldSayParkingFullWhenNoSlotsAvailableForAllSize(Size size)
        {
            var testParkingLot = GetFakeParkingLot(); //Fake lot with  5 slots in each type.

            var testVehicle = GetFakeVehicle(size);

            _svc = new ParkingLotManager.ParkingLot(testParkingLot, _searchService.Object);

            switch (size)
            {
                case Size.Hatchback:
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Hatchback)).Returns((ParkingSpot)null);
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Sedan)).Returns((ParkingSpot)null);
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Truck)).Returns((ParkingSpot)null);

                    break;
                case Size.Sedan:
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Sedan)).Returns((ParkingSpot)null);
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Truck)).Returns((ParkingSpot)null);

                    break;
                case Size.Truck:
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Truck)).Returns((ParkingSpot)null);
                    break;
            }

            ParkingSpot result = _svc.AssignParkingSpot(testVehicle);
            
            Assert.AreEqual(result, null);
        }

        [TestCase(Size.Sedan)]
        [TestCase(Size.Truck)]
        public void ShouldSayParkingFullWhenHatchbackisAvailablButSedanAndTruckAreUnavailable(Size size)
        {
            var testParkingLot = GetFakeParkingLot(); //Fake lot with  5 slots in each type.

            var testVehicle = GetFakeVehicle(size);

            _svc = new ParkingLotManager.ParkingLot(testParkingLot, _searchService.Object);

            switch (size)
            {
                case Size.Sedan:
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Sedan)).Returns((ParkingSpot)null);
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Truck)).Returns((ParkingSpot)null);

                    break;
                case Size.Truck:
                    _searchService.Setup(x => x.Search(testParkingLot, Size.Truck)).Returns((ParkingSpot)null);
                    break;
            }

            ParkingSpot result = _svc.AssignParkingSpot(testVehicle);

            Assert.AreEqual(result, null);
        }

        [Test]
        public void ShouldAssignTruckSpotWhenHatchBackAndSedanIsUnavailable()
        {
            var testParkingLot = GetFakeParkingLot(); //Fake lot with  5 slots in each type.

            var testVehicle = GetFakeVehicle(Size.Hatchback);

            var expectedSpot = new ParkingSpot { OccupiedVehicle = testVehicle, SpotSize = Size.Truck };

            _svc = new ParkingLotManager.ParkingLot(testParkingLot, _searchService.Object);

            _searchService.Setup(x => x.Search(testParkingLot, Size.Hatchback)).Returns((ParkingSpot)null);
            _searchService.Setup(x => x.Search(testParkingLot, Size.Sedan)).Returns((ParkingSpot)null);
            _searchService.Setup(x => x.Search(testParkingLot, Size.Truck)).Returns(expectedSpot);

            ParkingSpot result = _svc.AssignParkingSpot(testVehicle);

            Assert.AreEqual(result.SpotSize, expectedSpot.SpotSize);
            Assert.AreEqual(result.OccupiedVehicle, expectedSpot.OccupiedVehicle);
        }

        [Test]
        public void CompleteParkingTest()
        {
            IParkingLotService myTestParkingLot = new ParkingLotManager.ParkingLot(GetFakeParkingLot(), new SpotSearchService());

            ParkingSpot spot;

            //Sedan Getting Sedan
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Assert.That(spot.OccupiedVehicle.VehicleSize == Size.Sedan);
            Assert.AreEqual(spot.SpotSize, Size.Sedan);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Assert.That(spot.OccupiedVehicle.VehicleSize == Size.Sedan);
            Assert.AreEqual(spot.SpotSize, Size.Sedan);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Assert.That(spot.OccupiedVehicle.VehicleSize == Size.Sedan);
            Assert.AreEqual(spot.SpotSize, Size.Sedan);
            

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Assert.That(spot.OccupiedVehicle.VehicleSize == Size.Sedan);
            Assert.AreEqual(spot.SpotSize, Size.Sedan);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Assert.That(spot.OccupiedVehicle.VehicleSize == Size.Sedan);
            Assert.AreEqual(spot.SpotSize, Size.Sedan);

            //Sedan getting Trucks spots

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Sedan);
            Assert.AreEqual(spot.SpotSize, Size.Truck);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Sedan));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Sedan);
            Assert.AreEqual(spot.SpotSize, Size.Truck);

            ////Truck getting truck spots

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Truck);
            Assert.AreEqual(spot.SpotSize, Size.Truck);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Truck);
            Assert.AreEqual(spot.SpotSize, Size.Truck);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Truck);
            Assert.AreEqual(spot.SpotSize, Size.Truck);

            //No parking slots avialable
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Assert.IsNull(spot);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Truck));
            Assert.IsNull(spot);
            
            //Confirm hatchback slots.
            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Hatchback);
            Assert.AreEqual(spot.SpotSize, Size.Hatchback);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Hatchback);
            Assert.AreEqual(spot.SpotSize, Size.Hatchback);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Hatchback);
            Assert.AreEqual(spot.SpotSize, Size.Hatchback);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Hatchback);
            Assert.AreEqual(spot.SpotSize, Size.Hatchback);

            spot = myTestParkingLot.AssignParkingSpot(GetFakeVehicle(Size.Hatchback));
            Assert.AreEqual(spot.OccupiedVehicle.VehicleSize, Size.Hatchback);
            Assert.AreEqual(spot.SpotSize, Size.Hatchback);
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
