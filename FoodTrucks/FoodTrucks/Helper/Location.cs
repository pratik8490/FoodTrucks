using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Helper
{
    public class Location
    {
        public static async Task<Plugin.Geolocator.Abstractions.Position> GetPosition()
        {
            var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 50;
            try
            {
                return await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            }
            catch
            {
                return new Plugin.Geolocator.Abstractions.Position();
            }
        }
        public static async Task<string> GetPositionString()
        {
            var pos = await GetPosition();
            return "Latitude:" + pos.Latitude + "\n" +
                 "Longitude:" + pos.Longitude;
        }

        public static string GetString(string latitude, string longitude)
        {
            return latitude + "," + longitude;
        }
    }
}
