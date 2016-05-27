using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Provider.Models
{
    public class TruckInfoModel
    {
        public int Id { get; set; }
        public string TruckName { get; set; }
        public string Description { get; set; }
        public int? FoodTypeId { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
        public byte? IsActive { get; set; }
        public string Link { get; set; }
        public string Menu { get; set; }
        public int? BarId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifedAt { get; set; }
        public int? UserID { get; set; }
        public byte[] MenuImage { get; set; }
    }
}
