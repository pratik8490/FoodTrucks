using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Models
{
    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Message { get; set; }
        public string Address { get; set; }
    }

    public class NavigationModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DestinationName { get; set; }
        public string DestinationAddress { get; set; }
    }
}
