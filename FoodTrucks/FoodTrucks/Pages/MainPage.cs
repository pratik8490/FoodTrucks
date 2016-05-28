using FoodTrucks.Context;
using FoodTrucks.Helper;
using FoodTrucks.Interface;
using FoodTrucks.Provider;
using FoodTrucks.Provider.Interface;
using FoodTrucks.Provider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks.Pages
{
    public class MainPage : BasePage
    {
        public LoadingIndicator _Loader = new LoadingIndicator(50, 50);
        private IUser _UserProvider = new UserProvider();
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Main Page"/> class.
        /// </summary>
        public MainPage()
        {
            IsLoading = true;
            NavigationPage.SetHasNavigationBar(this, false);
            Device.BeginInvokeOnMainThread(async () =>
                   {
                       MainPageLayout();
                   });
        }
        #endregion

        #region Override Method
        protected override void OnAppearing()
        {

            bool isNetworkAvailable = DependencyService.Get<INetworkOperation>().IsInternetConnectionAvailable();

            Device.BeginInvokeOnMainThread(async () =>
                   {
                       if (FoodTruckContext.Position == null)
                       {
                           FoodTruckContext.Position = await DependencyService.Get<ICurrentLocation>().SetCurrentLocation();
                           if (FoodTruckContext.Position != null)
                           {
                               FoodTruckContext.AlreadyEnable = true;
                           }
                       }

                       if (!isNetworkAvailable)
                       {
                           await DisplayAlert("Connection", "Please check your internet connection.", "OK");
                       }
                   });
        }
        #endregion

        #region LogbookFillUpLayout
        /// <summary>
        /// Main Page Layout.
        /// </summary>
        public void MainPageLayout()
        {
            try
            {
                Image imgMiddle = new Image
                {
                    Source = ImageSource.FromFile(Constants.ImagePath.MainPageMiddle)
                };

                StackLayout slImgMiddle = new StackLayout
                {
                    Children = { imgMiddle, _Loader },
                    Padding = new Thickness(5),
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                Button btnGetStart = new Button
                {
                    Text = "LET'S GET STARTED",
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("f23e3e")
                };

                btnGetStart.Clicked += async (sender, e) =>
                    {
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        btnGetStart.IsVisible = false;
                        _Loader.IsShowLoading = true;

                        //Navigation.PushAsync(App.SignUpPage());

                        if (!FoodTruckContext.AlreadyEnable)
                        {
                            bool IsYesNo = false;

                            IsYesNo = await DisplayAlert(string.Empty, "Please enable location service on your device.", "OK", "Cancel");
                            if (IsYesNo)
                            {
                                btnGetStart.IsVisible = true;
                                _Loader.IsShowLoading = false;
                                FoodTruckContext.AlreadyEnable = true;

                                FoodTruckContext.Position = await DependencyService.Get<ICurrentLocation>().SetCurrentLocation();
                            }
                            else
                            {
                                btnGetStart.IsVisible = true;
                                _Loader.IsShowLoading = false;
                                return;
                            }
                        }
                        else
                        {
                            if (FoodTruckContext.Position == null)
                            {
                                FoodTruckContext.Position = await DependencyService.Get<ICurrentLocation>().SetCurrentLocation();
                            }
                            UserModel user = await _UserProvider.CheckDeviceLoggedIn(GetDeviceID());
                            if (user.Id != 0)
                            {
                                if (Convert.ToBoolean(user.IsUser))
                                {
                                    FoodTruckContext.UserName = user.Email;
                                    FoodTruckContext.UserID = user.Id;
                                    FoodTruckContext.IsLoggedIn = true;

                                    Navigation.PushAsync(App.MapPage());
                                }
                                else
                                {
                                    Navigation.PushAsync(App.LoginPage());
                                }
                            }
                            else
                            {
                                Navigation.PushAsync(App.SignUpPage());
                            }

                            btnGetStart.IsVisible = true;
                            _Loader.IsShowLoading = false;
                        }
                    };

                StackLayout slBtnGetStart = new StackLayout
                {
                    Children = { btnGetStart },
                    VerticalOptions = LayoutOptions.EndAndExpand,
                };

                StackLayout slMainPage = new StackLayout
                {
                    Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 20),
						    Children = {slImgMiddle, slBtnGetStart},
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Orientation= StackOrientation.Vertical
                        },
                    },
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };

                Content = new ScrollView
                {
                    Content = slMainPage,
                };
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
