using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrucks.CustomControls
{
    public class CustomPin
    {
        public string Label { get; set; }

        public string Address { get; set; }

        public string PinIcon { get; set; }

        public Xamarin.Forms.Maps.Position Position { get; set; }

    }
}
