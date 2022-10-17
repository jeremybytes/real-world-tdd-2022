# Test-Driven Development in the Real World

## Abstract  
Are you tired of trivial TDD examples like FizzBuzz? I am. So let's look at a real-world problem to see how Test-Driven Development (TDD) helps us think in small pieces, build provable code, and reduce the amount of unneeded code that creeps into our applications. In the real world, we have to deal with services, libraries, and dependencies. And we have to deal with strange bugs that crop up. In this session, we'll go beyond the simple examples and learn how to break down complexity, isolate dependencies with mocking, and capture expected exceptions.  

## Project Layout
To build and run the code, you will need to have .NET 6 installed on your machine. The demo project will run wherever .NET 6 will run (Windows, macOS, Linux).

There are 2 copies of the sample code:  
* **/Starter/** folder has the initial state of the application.  
* **/Completed/** folder has the final state with the unit tests and functionality in place.

The project solution is broken down as follows:  

**/HouseControl.Sunset/** is the code to be built. This code gets data from a service and transforms it into an appropriate date/time value.  
**/HouseControl.Sunset.Tests/** is the set of unit tests used to build the project above.  
**/SolarCalculator.Service/** is the service that provides the raw data.  
**/SunsetTestApp/** is a console application that can be used to run the final code.

The tests are used to build the "SolarServiceSunsetProvider" class that implements the "ISunsetProvider" interface.

*Note that the completed code is in an intermediate state. Depending on further needs, some of the static methods can be split out into separate classes, caching can be added, as well as additional error handling.*

## Development Environments
**Visual Studio 2022**  
The "HouseControl.sln" contains all of the projects listed above. Before running the console application (SunsetTestApp), you will need to start the service. See "Running the Service" below for instructions.

**Visual Studio Code**  
If you are using Visual Studio code, open the "Starter" or "Completed" folder to have access to all of the projects listed above.

## Running the Service
The **.NET service** can be started from the command line by navigating to the "../SolarCalculator.Service" directory and typing `dotnet run`. This provides endpoints at the following locations:

* http://localhost:8973/SolarCalculator  
Note that additional parameters are needed to get valid results.
  
This provides the sunrise, sunset, and other values used by the application code. The following is a sample URL (which includes a latitude/longitude location along with a date) produces the following result:

* [http://localhost:8973/SolarCalculator?lat=39.1081&lng=-84.5098&date=2022-10-20](http://localhost:8973/SolarCalculator?lat=39.1081&lng=-84.5098&date=2022-10-20
)  

```json
{
    "results": {
        "sunrise": "7:52:28 AM",
        "sunset": "6:53:14 PM",
        "solar_noon": "1:22:51 PM",
        "day_length": "11:00:46.4065871"
    },
    "status": "OK"
}
```

## Resources
* Book Review (Highly Recommended): [The Art of Unit Testing - Roy Osherove](http://jeremybytes.blogspot.com/2015/06/book-review-art-of-unit-testing-with.html)  
* Book Review: [Working Effectively with Legacy Code - Michael C. Feathers](http://jeremybytes.blogspot.com/2013/02/book-review-working-effectively-with.html)  
* Book Review: [Test-Driven Development by Example - Kent Beck](http://jeremybytes.blogspot.com/2013/03/book-review-test-driven-development-by.html)  
* Article Series (TDD & NUnit): [Coding Practice with Conway's Game of Life](http://www.jeremybytes.com/Downloads.aspx#ConwayTDD)  
* Video Series (Intro to TDD): [Unit Testing & TDD](https://www.youtube.com/watch?v=l4xhTq4qmC0&list=PLdbkZkVDyKZXqPu-xDFkzuP66QijGeewz)  

## Testing Practices  
* Code Coverage: [What Parts of Your Application Do Your Users *Not* Care About?](http://jeremybytes.blogspot.com/2015/02/unit-test-coverage-what-parts-of-your.html)  
* What to Test: [My Approach to Testing: Test Public Members](http://jeremybytes.blogspot.com/2015/04/my-approach-to-testing-test-public.html)  
* Setup Methods: [Unit Testing: Setup Methods or Not?](http://jeremybytes.blogspot.com/2015/06/unit-testing-setup-methods-or-not.html)  
* Assert Messages: [Unit Testing Asserts: Skip the Message Parameter](http://jeremybytes.blogspot.com/2015/07/unit-testing-asserts-skip-message.html)  