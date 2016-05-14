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

        public class RegxValidation
        {
            public const string EmailValidationPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            public const string PhoneNumberRegx = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        }

        /// <summary>
        /// Image path constant.
        /// </summary>
        public class ImagePath
        {
            public const string MainPageMiddle = "mainpagemiddle.png";
            public const string FilterIcon = "filter.png";
            public const string SlideOut = "slideout.png";
            public static string DropDownArrow = "down_arrow.png";
            public static string DownArrow = "down_arrow.png";
            public static string RightArrow = "aerrow.png";
        }
    }
}
