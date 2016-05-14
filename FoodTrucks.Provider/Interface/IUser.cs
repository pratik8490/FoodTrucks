﻿using FoodTrucks.Provider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Provider.Interface
{
    public interface IUser
    {
        Task<int> SignUpUser(UserModel userModel);
        Task<bool> CheckDeviceLoggedIn(string deviceID);
        Task<bool> LogInUser(string email, int pin);
    }
}
