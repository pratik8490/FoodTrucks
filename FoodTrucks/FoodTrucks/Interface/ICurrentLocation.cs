using FoodTrucks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Interface
{
    public interface ICurrentLocation
    {
        Task<Position> SetCurrentLocation();
        Task<Position> GetLocation(string address);
    }
}
