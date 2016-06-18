using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Enums;
using XLabs.Forms.Controls;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace FoodTrucks.Helper
{
    public class Global
    {
        static int DefW = 750;
        public Global()
        {
            //PixelWidth = GetDisplay().Width;
            //PixelHeight = GetDisplay().Height;//resolution
            //MobileWidth = GetDisplay().WidthRequestInInches(GetDisplay().ScreenWidthInches());
            //MobileHeight = GetDisplay().HeightRequestInInches(GetDisplay().ScreenHeightInches());//active resolution
        }

        public static double MinPad = Val(30);
        public static double Pad = Val(40);
        public static double MaxPad = Val(50);
        public enum sz { Micro, Small, Normal, Large, Big, Vast }
        public static IDisplay GetDisplay()
        {
            return Resolver.Resolve<IDevice>().Display;
        }
        public static int ButtonSizeWidthLarge = 475;
        public static int ButtonSizeWidthMedium = 370;
        public static int ButtonSizeWidthSmall = 100;//251;
        public static int ButtonSizeHeight = 80;
        public static double ButtonWidth = Val(ButtonSizeWidthMedium);
        public static double ButtonHeight = Val(ButtonSizeHeight);

        private static double _PixelWidth = GetDisplay().Width;
        public static double PixelWidth { get { return _PixelWidth; } }

        private static double _PixelHeight = GetDisplay().Height;
        public static double PixelHeight { get { return _PixelHeight; } }

        private static double _MobileWidth = GetDisplay().WidthRequestInInches(GetDisplay().ScreenWidthInches());
        public static double MobileWidth { get { return _MobileWidth; } }

        private static double _MobileHeight = GetDisplay().HeightRequestInInches(GetDisplay().ScreenHeightInches());//active resolution
        public static double MobileHeight { get { return _MobileHeight; } }

        public static int iVal(double n)
        {
            return (int)(PixelWidth * n / DefW);
        }
        public static double Val(double n)
        {
            return MobileWidth * n / DefW;
        }
        public static ImageButton GetButton(string name, sz _sz)
        {
            return GetButton(name, _sz, ImageOrientation.ImageToLeft);
        }
        public static ImageButton GetButton(string name, sz _sz, ImageOrientation orientation)
        {
            int w = 0, d = 35;
            if (_sz == sz.Small)
            {
                w = Global.ButtonSizeWidthSmall;
                d = 30;
            }
            else if (_sz == sz.Normal)
            {
                w = Global.ButtonSizeWidthMedium;
                d = 30;
            }
            else if (_sz == sz.Large)
            {
                w = Global.ButtonSizeWidthLarge;
                d = 30;
            }

            return new ImageButton
            {
                Orientation = orientation,
                BackgroundColor = Color.Transparent,
                ImageWidthRequest = iVal(w),
                ImageHeightRequest = iVal(Global.ButtonSizeHeight),
                WidthRequest = Val(w + d),
                HeightRequest = ButtonHeight * Device.OnPlatform(1.5, 1, 1),
                Source = ImageSource.FromFile(name + "0.png"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
        }
    }
}
