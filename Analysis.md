
Changes Made at a high level:
--------------------------------------

1) Implemented a DependencyInjection of ParkingSpots to ParkingLot Class.
   - It will help us to create ParkingLot on the fly for testing.
   - Also, this will make the parkinglot class focus on only one single task which is assigning spot(Single responsibility) and hence more testable.

2) Implemented a DependencyInjection of SpotService to ParkingLot Class.
   - It will help us to mock search process for various business scenarios.
   - Also it will help us to reuse the searchFunction and hence less redundnat code.
   - There is an oppurtunity to refactor the existing logic of finding spot. LINQ & LAMDA makes it code more clean and readable. Also it will improve the overall throughput of the function. 
   - Also, we can write test cases for the search function itself.

3) Adding Interfaces and searchService to the clasess will also help MOCKING during creating automation tests.