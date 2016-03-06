using FoodTrucks.CustomControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FoodTrucks.Pages
{
    public class MapPage : ContentPage
    {
        Map map;
        public MapPage()
        {
            Title = "Map";

            //Map map = new Map
            //{
            //    IsShowingUser = true,
            //    HeightRequest = 200,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};

            //map.MoveToRegion(MapSpan.FromCenterAndRadius(
            //    new Position(Convert.ToDouble("23.0396"), Convert.ToDouble("72.566")), Distance.FromMiles(3))); // Santa Cruz golf course

            var position = new Position(Convert.ToDouble("23.0396"), Convert.ToDouble("72.566")); // Latitude, Longitude
            //var pin = new Pin
            //{
            //    Type = PinType.Place,
            //    Position = position,
            //    Label = "Seema Hall",
            //    Address = "Seema hall Satelite Ahmedabad, Gujarat, India" + " " + "India"
            //};
            //map.Pins.Add(pin);

            var map = new CustomMap(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(5)))
            {
                IsShowingUser = true
            };

            ////Show current location for Win Phone
            //if (Device.OS == TargetPlatform.WinPhone)
            //{
            map.CustomPins.Add(new CustomPin
            {
                Position = position,
                Label = "Current Location",
                PinIcon = "CurrentLocation.png"
            });

            map.CustomPins.Add(new CustomPin
            {
                Position = new Position(Convert.ToDouble("23.0281"), Convert.ToDouble("72.5578")),
                Label = "Current Location"
            });

            map.CustomPins.Add(new CustomPin
            {
                Position = new Position(Convert.ToDouble("23.0341"), Convert.ToDouble("72.5223")),
                Label = "Current Location",
            });
            //}

            StackLayout MapLayout = new StackLayout
            {
                Padding = new Thickness(10),
                Orientation = StackOrientation.Vertical,
                Children = { map },
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            this.Content = new ScrollView
            {
                Content = MapLayout
            };
        }
    }
}