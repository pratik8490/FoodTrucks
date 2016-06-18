using FoodTrucks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Interface
{
    public interface IPhoneService
    {
        void LaunchMap(string address);

        Task LaunchNavigationAsync(NavigationModel navigationModel);
    }

}
