using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks.Helper
{
    public class Constants
    {
        public static Thickness IOSPadding = new Thickness(0, Device.OnPlatform(40, 0, 0), 0, 0);
        public static int Padding = 43;

        /// <summary>
        /// Image path constant.
        /// </summary>
        public class ImagePath
        {
            public const string MainPageMiddle = "mainpagemiddle.png";
            public const string FilterIcon = "filter.png";
            public const string SlideOut = "slideout.png";
        }
    }
}
