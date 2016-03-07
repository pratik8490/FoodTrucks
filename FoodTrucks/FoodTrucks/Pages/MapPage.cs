using FoodTrucks.CustomControls;
using FoodTrucks.Helper;
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
        public MapPage()
        {

            Image imgSliderOutMenu = new Image
            {
                Source = ImageSource.FromFile(Constants.ImagePath.SlideOut)
            };

            StackLayout slSlideOutMenu = new StackLayout
            {
                Children = { imgSliderOutMenu },
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Label lblMaps = new Label
            {
                Text = "Maps",
                FontSize = 14,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                TextColor = Color.Black
            };

            StackLayout slMapsText = new StackLayout
            {
                Children = { lblMaps },
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Image imgFilter = new Image
            {
                Source = ImageSource.FromFile(Constants.ImagePath.FilterIcon)
            };

            StackLayout slImgFilter = new StackLayout
            {
                Children = { imgFilter },
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slHeader = new StackLayout
            {
                Children = { slSlideOutMenu, slMapsText, slImgFilter },
                Orientation = StackOrientation.Horizontal
            };

            Map map = new Map
            {
                IsShowingUser = true,
                HeightRequest = 300,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(Convert.ToDouble("23.0396"), Convert.ToDouble("72.566")), Distance.FromMiles(3))); // Santa Cruz golf course

            var position = new Position(Convert.ToDouble("23.0396"), Convert.ToDouble("72.566")); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Seema Hall",
                Address = "Seema hall Satelite Ahmedabad, Gujarat, India" + " " + "India"
            };
            map.Pins.Add(pin);

            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Position = new Position(Convert.ToDouble("23.0281"), Convert.ToDouble("72.5578")),
                Label = "Location 2",
                Address = "Test 2"
            });

            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Position = new Position(Convert.ToDouble("23.0341"), Convert.ToDouble("72.5223")),
                Label = "Location 3",
                Address = "Test 3"
            });

            //CustomMap map = new CustomMap(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(5)))
            //{
            //    IsShowingUser = true,
            //    HeightRequest = 200,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};

            ////Show current location for Win Phone
            //if (Device.OS == TargetPlatform.WinPhone)
            //{
            //map.CustomPins.Add(new CustomPin
            //{
            //    Position = position,
            //    Label = "Current Location",
            //    PinIcon = "CurrentLocation.png",
            //    Address = "Seema hall Satelite Ahmedabad, Gujarat, India" + " " + "India"
            //});

            //map.CustomPins.Add(new CustomPin
            //{
            //    Position = new Position(Convert.ToDouble("23.0281"), Convert.ToDouble("72.5578")),
            //    Label = "Location 2",
            //    Address = "Test 2"
            //});

            //map.CustomPins.Add(new CustomPin
            //{
            //    Position = new Position(Convert.ToDouble("23.0341"), Convert.ToDouble("72.5223")),
            //    Label = "Location 3",
            //    Address = "Test 3"
            //});
            //}

            Label lblTruckTitle = new Label
            {
                Text = "Burger Quickening",
                TextColor = Color.Black,
            };

            Label lblTruckDetail = new Label
            {
                Text = "Fast Food & Drinks" + "\n" + "GPS coordinates, 15 KM away" + "\n" + "In front of",
                TextColor = Color.FromHex("#ccc0c0"),
            };

            StackLayout slTruckDetail = new StackLayout
            {
                Children = { lblTruckTitle, lblTruckDetail },
                Orientation = StackOrientation.Vertical
            };

            Label lblReviews = new Label
            {
                Text = "Reviews:",
                TextColor = Color.Black,
            };

            Label lblReviewsName = new Label
            {
                Text = "Jhon Doe" + "\n" + "Very Good!!",
                TextColor = Color.FromHex("#ccc0c0"),
            };

            StackLayout slReviewDetails = new StackLayout
            {
                Children = { lblReviews, lblReviewsName },
                Orientation = StackOrientation.Vertical
            };

            StackLayout slTruck = new StackLayout
            {
                Children = { slTruckDetail, slReviewDetails },
                Orientation = StackOrientation.Vertical,
                Spacing = 10
            };

            Button btnSeeMore = new Button
            {
                Text = "SEE MORE",
                TextColor = Color.White,
                BackgroundColor = Color.Yellow,
                WidthRequest = 150
            };

            StackLayout slSeeMore = new StackLayout
            {
                Children = { btnSeeMore },
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Button btnNavigateTo = new Button
            {
                Text = "NAVIGATE TO",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("f23e3e"),
                WidthRequest = 150
            };

            StackLayout slNavigateTo = new StackLayout
            {
                Children = { btnNavigateTo },
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slButton = new StackLayout
            {
                Children = { btnSeeMore, btnNavigateTo },
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(3)
            };

            StackLayout slMapPage = new StackLayout
            {
                Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 0),
						    Children = { slHeader },
                            VerticalOptions = LayoutOptions.Start
                        },new StackLayout { Children = { map}, VerticalOptions = LayoutOptions.StartAndExpand},
                        new StackLayout{
                            Padding = new Thickness(20, 0, 20, 20),
						    Children = {slTruck, slButton },
                            VerticalOptions = LayoutOptions.EndAndExpand
                            },
                    },
                Orientation = StackOrientation.Vertical,
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slMapPage,
            };
        }

        void LoadContet()
        {

        }
    }
}