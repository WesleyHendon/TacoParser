using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            //logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(TacoParser.Parse).ToArray();

            logger.LogInfo("Retrieved all store data, beginning to check locations");

            ITrackable tacoOne = null;
            ITrackable tacoTwo = null;
            var longestDistance = 0.0;
            foreach (ITrackable trackable in locations)
            {
                GeoCoordinate coord = new GeoCoordinate(trackable.Location.Latitude, trackable.Location.Longitude);
                foreach (ITrackable trackable2 in locations)
                {
                    GeoCoordinate destinationCoord = new GeoCoordinate(trackable2.Location.Latitude, trackable2.Location.Longitude);
                    double distanceTo = coord.GetDistanceTo(destinationCoord);
                    System.Console.WriteLine(distanceTo);
                    if (distanceTo > longestDistance)
                    {
                        tacoOne = trackable;
                        tacoTwo = trackable2;
                        longestDistance = distanceTo;
                        logger.LogInfo("Found a new store pair that is farther apart\nStore One: "
                                        + tacoOne.Name + "\nStore Two: "
                                        + tacoTwo.Name);
                    }
                }
            }

            if (tacoOne == null && tacoTwo == null) return;

            System.Console.WriteLine("----------------------------------------------------------------");
            System.Console.WriteLine("These two taco bells are the farthest from each other in Alabama");
            System.Console.WriteLine("----------------------------------------------------------------");
            System.Console.WriteLine("Store One: " + tacoOne.Name);
            System.Console.WriteLine("Store Two: " + tacoTwo.Name);

            // TODO:  Find the two Taco Bells in Alabama that are the furthest from one another.
            // HINT:  You'll need two nested forloops
        }
    }
}
