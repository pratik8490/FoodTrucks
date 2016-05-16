using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.Models
{
    public class MenuModel
    {
        #region Property
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
        #endregion
    }
}
