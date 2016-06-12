using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Provider.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }
        public Nullable<bool> IsNotified { get; set; }
        public Nullable<bool> UserLocation { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DeviceID { get; set; }
        public Nullable<bool> IsUser { get; set; }
    }
}
