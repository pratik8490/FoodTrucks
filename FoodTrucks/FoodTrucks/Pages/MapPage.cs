using Acr.UserDialogs;
using FoodTrucks.Context;
using FoodTrucks.CustomControls;
using FoodTrucks.Helper;
using FoodTrucks.Interface;
using FoodTrucks.Models;
using FoodTrucks.Provider;
using FoodTrucks.Provider.Interface;
using FoodTrucks.Provider.Models;
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
    public class MapPage : BasePage
    {
        private List<TruckInfoModel> _TruckInfoList = new List<TruckInfoModel>();
        private ITruckInfo _TruckInfoProvider = new TruckInfoProvider();
        private IFoodType _FoodTypeProvider = new FoodTypeProvider();
        private IReview _ReviewProvider = new ReviewProvider();
        public MapPage()
        {
            Title = "Maps";
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
                    {


                        //if (string.IsNullOrEmpty(location.Message))
                        //{
                        //Application.Current.Properties["Latitude"] = location.Latitude;
                        //Application.Current.Properties["Longitude"] = location.Longitude;

                        _TruckInfoList = await _TruckInfoProvider.GetTruckList();

                        MapPageLayout();
                        //}
                        //else
                        //{
                        //    await DisplayAlert(string.Empty, location.Message, "OK");
                        //}
                    });
        }

        public void MapPageLayout()
        {
            try
            {
                Image imgSliderOutMenu = new Image
                {
                    Source = ImageSource.FromFile(Constants.ImagePath.SlideOut)
                };

                StackLayout slSlideOutMenu = new StackLayout
                {
                    Children = { imgSliderOutMenu },
                    HorizontalOptions = LayoutOptions.StartAndExpand,
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
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand
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
                    new Position(FoodTruckContext.Position.Latitude, FoodTruckContext.Position.Longitude), Distance.FromMiles(3))); // Santa Cruz golf course


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

                Label lblTruckTitle = new Label
                {
                    TextColor = Color.Black,
                };

                Label lblTruckDetail = new Label
                {
                    //Text = "Fast Food & Drinks" + "\n" + "GPS coordinates, 15 KM away" + "\n" + "In front of",
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
                    //Text = "Jhon Doe" + "\n" + "Very Good!!",
                    TextColor = Color.FromHex("#ccc0c0"),
                };

                Label lblReviewDesc = new Label
                {
                    TextColor = Color.FromHex("#ccc0c0")
                };

                StackLayout slReviewDetails = new StackLayout
                {
                    Children = { lblReviews, lblReviewsName, lblReviewDesc },
                    Orientation = StackOrientation.Vertical
                };

                StackLayout slTruck = new StackLayout
                {
                    Children = { slTruckDetail, slReviewDetails },
                    Orientation = StackOrientation.Vertical,
                    Spacing = 10,
                    IsVisible = false
                };

                Button btnSeeMore = new Button
                {
                    Text = "SEE MORE",
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("e7b909"),
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
                    BackgroundColor = Color.FromHex("e61a1c"),
                    WidthRequest = 150
                };

                btnNavigateTo.Clicked += async (sender, e) =>
                    {
                        NavigationModel model = new NavigationModel();
                        await DependencyService.Get<IPhoneService>().LaunchNavigationAsync(model);
                        //Navigation.PushAsync(App.TruckListPage());
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

                LoadingIndicator loader = new LoadingIndicator();

                foreach (TruckInfoModel item in _TruckInfoList)
                {
                    Pin pin = new Pin();
                    pin.Type = PinType.Place;
                    pin.Label = item.TruckName;
                    pin.Position = new Position(Convert.ToDouble(item.Lattitude), Convert.ToDouble(item.Longitude));

                    pin.Clicked += async (sender, e) =>
                    {
                        lblReviewsName.Text = string.Empty;
                        lblReviewDesc.Text = string.Empty;

                        using (UserDialogs.Instance.Loading("Loading..."))
                        {

                            lblTruckTitle.Text = item.TruckName;

                            //GEt call for food type details
                            FoodTypeModel foodTypeModel = await _FoodTypeProvider.GetFoodTypeByID(Convert.ToInt32(item.FoodTypeId));

                            //Calculate Distance
                            DistanceCalculator distanceCal = new DistanceCalculator();
                            double totalKm = distanceCal.distance(Convert.ToDouble(FoodTruckContext.Position.Latitude), Convert.ToDouble(FoodTruckContext.Position.Longitude), Convert.ToDouble(item.Lattitude), Convert.ToDouble(item.Longitude), Convert.ToChar("K"));

                            lblTruckDetail.Text = foodTypeModel.Type + "\n" + "GPS coordinates, " + totalKm.ToString("0.00") + " KM away" + "\n" + "In front of";

                            //Get call for review details
                            ReviewDetailModel reviewDetail = await _ReviewProvider.GetByTruckId(item.Id);

                            if ((!string.IsNullOrEmpty(reviewDetail.FirstName) || !string.IsNullOrEmpty(reviewDetail.LastName)) && !string.IsNullOrEmpty(reviewDetail.Description))
                            {
                                lblReviewsName.Text = reviewDetail.FirstName + " " + reviewDetail.LastName;
                                lblReviewDesc.Text = reviewDetail.Description;
                            }
                            else
                            {
                                lblReviewsName.Text = "There is no review for this truck.";
                            }
                        }
                        slTruck.IsVisible = true;
                        //});
                    };

                    map.Pins.Add(pin);
                }

                StackLayout slMapPage = new StackLayout
                {
                    Children = {
                        new StackLayout { Children = { map }, VerticalOptions = LayoutOptions.StartAndExpand, Padding = new Thickness(0, 10, 0, 0)},
                        new StackLayout{
                            Padding = new Thickness(20, 0, 20, 20),
						    Children = {loader,slTruck,slButton },
                            VerticalOptions = LayoutOptions.EndAndExpand
                            },
                    },

                    Orientation = StackOrientation.Vertical,
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };

                Content = new ScrollView
                {
                    Content = slMapPage
                };
            }
            catch (Exception ex)
            {

            }
        }
    }
}