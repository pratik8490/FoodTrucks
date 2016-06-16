using Acr.UserDialogs;
using FoodTruck;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks.Pages
{
    public class LoginPage : BasePage
    {
        private IUser _UserProvider = new UserProvider();
        private ExtendedEntry txtUserName, txtPassword;
        private Button btnLogin;
        private LoadingIndicator _loadingIndicator;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Login Page"/> class.
        /// </summary>
        public LoginPage()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                LoginLayout();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

        #region Overrride methods
        protected override void OnAppearing()
        {
            try
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
            catch (Exception ex)
            {
            }

        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (FoodTruckContext.IsLoggedIn)
                {
                    base.OnBackButtonPressed();
                }
            });
            return true;
        }
        #endregion

        #region LoginLayout
        /// <summary>
        /// Login Page Layout.
        /// </summary>
        public void LoginLayout()
        {
            txtPassword = new ExtendedEntry();
            txtPassword.IsPassword = true;

            txtUserName = new ExtendedEntry();
            txtUserName.Text = string.Empty;
            txtUserName.BackgroundColor = Color.Gray;
            txtUserName.TextColor = Color.Black;
            txtUserName.Placeholder = "Email";

            txtPassword.Text = string.Empty;
            txtPassword.BackgroundColor = Color.Gray;
            txtPassword.TextColor = Color.Black;
            txtPassword.Placeholder = "Password";

            var imageLogo = new Image { Source = Constants.ImagePath.MainPageMiddle };
            var cvTxtUserName = new ContentView
            {
                Padding = new Thickness(10, 10, 10, 5),
                Content = txtUserName
            };
            var cvTxtPassword = new ContentView
            {
                Padding = new Thickness(10, 5, 10, 10),
                Content = txtPassword
            };

            btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.TextColor = Color.White;
            btnLogin.BackgroundColor = LayoutHelper.ButtonColor;

            btnLogin.Clicked += (sender, e) =>
            {

                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    UserDialogs.Instance.ShowError("Email address required");
                }
                else if (!Regex.IsMatch(txtUserName.Text.ToString(), Constants.RegxValidation.EmailValidationPattern, RegexOptions.IgnoreCase))
                {
                    //DisplayAlert(Messages.Error, "Username is required.", "Ok");
                    UserDialogs.Instance.ShowError("Please enter correct format email address");
                }

                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    UserDialogs.Instance.ShowError("Password is required");
                    //DisplayAlert(Messages.Error, "Password is required.", "Ok");
                }

                else if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            using (UserDialogs.Instance.Loading("Loading..."))
                            {
                                //Login call
                                UserModel model = await _UserProvider.LogInUser(txtUserName.Text, txtPassword.Text);

                                if (model.Id != 0)
                                {
                                    FoodTruckContext.UserName = model.Email;
                                    FoodTruckContext.UserID = model.Id;
                                    FoodTruckContext.IsLoggedIn = true;
                                    FoodTruckContext.IsProvider = Convert.ToBoolean(model.IsUser);
                                    //UserDialogs.Instance.ShowSuccess("Success");
                                    //redirect to page
                                    Navigation.PushAsync(App.MapPage());
                                }
                                else
                                {
                                    ///
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        //_loadingIndicator.IsShowLoading = false;
                    });
                }
            };

            var cvBtnLogin = new ContentView
            {
                Padding = new Thickness(10, 5, 10, 10),
                Content = btnLogin
            };

            Button btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.TextColor = Color.White;
            btnRegister.BackgroundColor = LayoutHelper.ButtonColor;

            btnRegister.Clicked += async (sender, e) =>
            {
                bool isUserProvider = await UserDialogs.Instance.ConfirmAsync(Messages.CustomMessage.ChooseProviderUser, "Register Option", Messages.User, Messages.Provider);

                using (UserDialogs.Instance.Loading("Loading..."))
                {
                    //Get location
                    if (FoodTruckContext.Position == null)
                    {
                        FoodTruckContext.Position = await DependencyService.Get<ICurrentLocation>().SetCurrentLocation();
                    }
                    if (isUserProvider)
                    {
                        //redirect provider register page
                        Navigation.PushAsync(App.UserRegisterPage());
                    }
                    else
                    {
                        Navigation.PushAsync(App.ProviderRegisterPage());
                    }
                }
            };

            var cvBtnRegister = new ContentView
             {
                 Padding = new Thickness(10, 5, 10, 10),
                 Content = btnRegister
             };

            Label lblForgot = new Label
            {
                Text = "Forgot Password?",
                TextColor = LayoutHelper.LinkColor,
                BackgroundColor = Color.Transparent,
                FontSize = 16,
                HeightRequest = 36,
                HorizontalOptions = LayoutOptions.End,
            };

            ContentView cvLblForgetPassword = new ContentView
            {
                Padding = new Thickness(8, 5, 8, 0),
                Content = lblForgot
            };


            Label lblCopyrights = new Label
            {
                Text = "Copyright © 2016. All rights reserved.",
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                FontSize = 14,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };

            var barLower = new StackLayout
            {
                BackgroundColor = Color.Black,
                Spacing = 0,
                Padding = new Thickness(0, 0, 0, 0),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                Children =
                {
                    lblCopyrights,
                },
            };

            StackLayout slRegiter = new StackLayout
            {
                Children = { cvBtnRegister },
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            _loadingIndicator = new LoadingIndicator();

            StackLayout loginLayout = new StackLayout
            {
                Children = {
                    imageLogo,
                    cvTxtUserName,
                    cvTxtPassword ,
                    cvBtnLogin,
                    slRegiter,
                    cvLblForgetPassword,
                    _loadingIndicator,
                    barLower,
                    
                },
                BackgroundColor = LayoutHelper.PageBackgroundColor,
            };

            loginLayout.Padding = LayoutHelper.IOSPadding(0, 20, 0, 0);

            ScrollView scrollCartNewLayout = new ScrollView
            {
                Content = loginLayout
            };
            Content = scrollCartNewLayout;
        }
        #endregion
    }
}
