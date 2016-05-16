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
        public string DeviceID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Pin { get; set; }
        public byte? IsNotified { get; set; }
        public byte? IsUser { get; set; }
    }
}
