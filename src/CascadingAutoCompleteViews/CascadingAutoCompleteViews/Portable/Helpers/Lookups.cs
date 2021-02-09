using System.Collections.Generic;

namespace CascadingAutoCompleteViews.Portable.Helpers
{
    public static class Lookups
    {
        public static List<string> StatesLookup(string country)
        {
            var statesResult = new List<string>();

            if (country == "United States")
            {
                statesResult.Add("Massachusetts");
                statesResult.Add("California");
                statesResult.Add("Texas");
            }

            if (country == "Canada")
            {
                statesResult.Add("Quebec");
                statesResult.Add("Ontario");
                statesResult.Add("Manitoba");
            }

            return statesResult;
        }

        public static List<string> CitiesLookup(string state)
        {
            var citiesResult = new List<string>();

            if (state == "Massachusetts")
            {
                citiesResult.Add("Boston");
                citiesResult.Add("Salem");
                citiesResult.Add("Bedford");
            }
            if (state == "California")
            {
                citiesResult.Add("Sacramento");
                citiesResult.Add("San Diego");
            }
            if (state == "Texas")
            {
                citiesResult.Add("Austin");
                citiesResult.Add("Dallas");
            }

            if (state == "Quebec")
            {
                citiesResult.Add("Quebec");
            }
            if (state == "Ontario")
            {
                citiesResult.Add("Toronto");
                citiesResult.Add("Ottawa");
            }
            if (state == "Manitoba")
            {
                citiesResult.Add("Winnipeg");
            }

            return citiesResult;
        }
    }
}
