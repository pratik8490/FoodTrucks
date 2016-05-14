using FoodTrucks.Provider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Provider.Interface
{
    public interface IFoodType
    {
        Task<List<FoodTypeModel>> GetFoodType();
        Task<FoodTypeModel> GetFoodTypeByID(int id);
    }
}
