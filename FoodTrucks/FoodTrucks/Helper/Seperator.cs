using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTruck
{
    public class Seperator : BoxView
    {
        public Seperator()
        {
            Color = Color.Gray;
            HeightRequest = 0.5;

            LineSeperatorView = new BoxView()
            {
                Color = Color.FromHex("#efefef"),
                WidthRequest = 100,
                HeightRequest = 1
            };
        }
        public BoxView LineSeperatorView { get; set; }
    }
}
