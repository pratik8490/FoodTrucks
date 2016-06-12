using Acr.UserDialogs;
using FoodTruck;
using FoodTrucks.Context;
using FoodTrucks.Helper;
using FoodTrucks.Provider;
using FoodTrucks.Provider.Interface;
using FoodTrucks.Provider.Models;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks.Pages
{
    public class UserRegisterPage : BasePage
    {
        private IUser _UserProvider = new UserProvider();
        private LoadingIndicator _Loader = new LoadingIndicator();

        public UserRegisterPage()
        {
            Title = "Sign up";

            Label lblSignUp = new Label
            {
                Text = "Sign up",
                FontSize = 16,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                TextColor = Color.Black,
                YAlign = TextAlignment.Center
            };

            StackLayout slTitle = new StackLayout
            {
                Children = { lblSignUp },
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Seperator spTitle = new Seperator();

            StackLayout slHeader = new StackLayout
            {
                Children = { slTitle },
                Orientation = StackOrientation.Horizontal
            };

            ExtendedEntry txtEmail = new ExtendedEntry { Keyboard = Keyboard.Email, Placeholder = "Email", TextColor = Color.Black };

            ExtendedEntry txtPassword = new ExtendedEntry { Placeholder = "Password", TextColor = Color.Black, IsPassword = true };

            Label lblNotifications = new Label
            {
                Text = "Get notified by food trucks in your area?",
                TextColor = Color.Black
            };

            StackLayout slNotifications = new StackLayout
            {
                Children = { lblNotifications },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Switch swcNotifications = new Switch { WidthRequest = 100 };

            Seperator spNotifications = new Seperator();

            Label lblLocations = new Label
            {
                Text = "Use my location for more accuracy?",
                TextColor = Color.Black
            };

            StackLayout slLocations = new StackLayout
            {
                Children = { lblLocations },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Switch swcLocations = new Switch { IsToggled = true, WidthRequest = 100 };

            Seperator spLocations = new Seperator();

            swcNotifications.Toggled += (sender, e) =>
            {
                //model.IsPresent = e.Value;
            };

            StackLayout slNotificationsLayout = new StackLayout
            {
                Children = { swcNotifications },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slLocationsLayout = new StackLayout
            {
                Children = { swcLocations },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            Label lblProvider = new Label { Text = "Is Provider?", TextColor = Color.Black };

            StackLayout slProviderText = new StackLayout
            {
                Children = { lblProvider },
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            Switch swcProvider = new Switch { IsToggled = true, WidthRequest = 100 };

            Seperator spProvider = new Seperator();

            StackLayout slSwcProvider = new StackLayout
            {
                Children = { swcProvider },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            StackLayout slProvider = new StackLayout
            {
                Children = { slProviderText, swcProvider },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                IsVisible = false
            };

            StackLayout slGrid1 = new StackLayout
            {
                Children = { slNotifications, slNotificationsLayout },
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout slGrid2 = new StackLayout
            {
                Children = { slLocations, slLocationsLayout },
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout slEntry = new StackLayout
            {
                Children = { txtEmail, txtPassword },
                Orientation = StackOrientation.Vertical
            };

            Seperator spEntry = new Seperator();

            Button btnSignUp = new Button
            {
                Text = "SIGN UP",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("f23e3e")
            };

            btnSignUp.Clicked += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        btnSignUp.IsVisible = false;
                        if (string.IsNullOrEmpty(txtEmail.Text))
                        {
                            UserDialogs.Instance.ShowError("Please enter email address.");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtPassword.Text))
                        {
                            UserDialogs.Instance.ShowError("Please enter pin.");
                            return;
                        }
                        if (Regex.IsMatch(txtEmail.Text.ToString(), Constants.RegxValidation.EmailValidationPattern, RegexOptions.IgnoreCase))
                        {
                            using (UserDialogs.Instance.Loading("Loading..."))
                            {
                                UserModel objUserModel = new UserModel();
                                objUserModel.DeviceID = GetDeviceID();
                                objUserModel.Email = txtEmail.Text;
                                objUserModel.Pin = txtPassword.Text;
                                objUserModel.FirstName = string.Empty;
                                objUserModel.LastName = string.Empty;
                                objUserModel.UserLocation = swcLocations.IsToggled;
                                objUserModel.IsNotified = swcNotifications.IsToggled;
                                objUserModel.IsUser = false;

                                int UserId = await _UserProvider.SignUpUser(objUserModel);

                                if (UserId != 0)
                                {
                                    FoodTruckContext.UserID = UserId;
                                    FoodTruckContext.UserName = txtEmail.Text;
                                    FoodTruckContext.IsLoggedIn = true;
                                    FoodTruckContext.IsProvider = false;
                                    Navigation.PushAsync(App.MapPage());
                                }
                                else
                                {
                                    UserDialogs.Instance.ShowError("Some error ocurred.");
                                }
                            }
                        }
                        else
                        {
                            UserDialogs.Instance.ShowError("Please enter correct format email address.");
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                });
                btnSignUp.IsVisible = true;
                _Loader.IsShowLoading = false;
            };

            StackLayout slSignUp = new StackLayout
            {
                Children = { btnSignUp, _Loader },
            };

            StackLayout slSignUpPage = new StackLayout
            {
                Children = { 
                        new StackLayout{
                            Children = {
                                new StackLayout {
                                    Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 0),
                                    Children = { slEntry},
                                    Orientation = StackOrientation.Vertical,
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                },
                                //spEntry.LineSeperatorView,
                                //new StackLayout {
                                //    Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 0),
                                //    Children = {slProvider},
                                //},
                                spProvider.LineSeperatorView,
                                new StackLayout {
                                    Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 0),
                                    Children = {slGrid1},
                                },
                                spNotifications.LineSeperatorView,
                                 new StackLayout {
                                    Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 0),
                                    Children = {slGrid2},
                                },
                                 new StackLayout {
                                    Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 10),
                                    Children = {slSignUp},
                                    VerticalOptions = LayoutOptions.EndAndExpand,
                                },
                            },
                            Padding = new Thickness(20, Device.OnPlatform(40, 20, 0), 20, 20),
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        },
                    },
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slSignUpPage,
            };
        }
    }
}
