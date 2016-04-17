using FoodTrucks.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks.Pages
{
    public class MainPage : ContentPage
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Main Page"/> class.
        /// </summary>
        public MainPage()
        {
            //
            MainPageLayout();
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
                    Children = { imgMiddle },
                    Padding = new Thickness(5),
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                Button btnGetStart = new Button
                {
                    Text = "LET'S GET STARTED",
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("f23e3e")
                };

                btnGetStart.Clicked += (sender, e) =>
                    {
                        Navigation.PushModalAsync(App.SignUpPage());
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
