using Acr.UserDialogs;
using FoodTruck;
using FoodTrucks.Context;
using FoodTrucks.Helper;
using FoodTrucks.Interface;
using FoodTrucks.Provider;
using FoodTrucks.Provider.Interface;
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

                if (!isNetworkAvailable)
                {
                    DisplayAlert("Connection", "Please check your internet connection.", Messages.Ok);
                }
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
            txtPassword.Keyboard = Keyboard.Numeric;
            txtPassword.Placeholder = "Pin";

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
                    UserDialogs.Instance.ShowError("Email address required", 1);
                }
                else if (!Regex.IsMatch(txtUserName.Text.ToString(), Constants.RegxValidation.EmailValidationPattern, RegexOptions.IgnoreCase))
                {
                    //DisplayAlert(Messages.Error, "Username is required.", "Ok");
                    UserDialogs.Instance.ShowError("Please enter correct format email address");
                }

                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    UserDialogs.Instance.ShowError("Password is required", 1);
                    //DisplayAlert(Messages.Error, "Password is required.", "Ok");
                }

                else if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            var loader = UserDialogs.Instance.Loading(string.Empty, null, null, true);
                            btnLogin.IsVisible = false;

                            //Login call
                            bool loggedIn = await _UserProvider.LogInUser(txtUserName.Text, Convert.ToInt32(txtPassword.Text));

                            if (loggedIn)
                            {
                                FoodTruckContext.UserName = txtUserName.Text;
                                FoodTruckContext.IsLoggedIn = true;

                                UserDialogs.Instance.ShowSuccess("Success");
                                //redirect to page
                                Navigation.PushModalAsync(App.MapPage());
                            }
                            loader.Hide();
                        }
                        catch (Exception ex)
                        {
                            btnLogin.IsVisible = true;
                        }

                        btnLogin.IsVisible = true;
                        //_loadingIndicator.IsShowLoading = false;
                    });
                }
            };

            var cvBtnLogin = new ContentView
            {
                Padding = new Thickness(10, 5, 10, 10),
                Content = btnLogin
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

            _loadingIndicator = new LoadingIndicator();

            StackLayout loginLayout = new StackLayout
            {
                Children = {
                    imageLogo,
                    cvTxtUserName,
                    cvTxtPassword ,
                    cvBtnLogin,
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
