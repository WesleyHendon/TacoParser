namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly static ILog logger = new TacoLogger();
        
        public static ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            string[] splitString = line.Split(',');
            if (splitString.Length < 3)
            {
                logger.LogError("Error parsing file; less than 3 results in one line");
                return null;
            }

            string latitudeString = splitString[0];
            string longitudeString = splitString[1];
            string name = splitString[2];
            name = name.Replace(".", "");

            double latitude;
            if (!double.TryParse(latitudeString, out latitude))
            { // If it fails, log an error
                logger.LogError("Information in file is incorrect");
            }
            double longitude = 0;
            if (!double.TryParse(longitudeString, out longitude))
            {
                logger.LogError("Information in file for longitude is incorrect");
            }

            TacoBell garbageButDeliciousFoodEstablishment = new TacoBell();
            garbageButDeliciousFoodEstablishment.Name = name;
            garbageButDeliciousFoodEstablishment.Location = new Point(latitude, longitude);
            logger.LogInfo($"Created a new Taco Bell named {name} at {latitude}, {longitude}");

            return garbageButDeliciousFoodEstablishment; // TODO Implement
        }
    }
}