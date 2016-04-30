using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Context
{
    public class FoodTruckContext
    {
        public static string UserName { get; set; }
        public static FoodTrucks.Models.Position Position { get; set; }
        public static bool AlreadyEnable { get; set; }
    }
}
