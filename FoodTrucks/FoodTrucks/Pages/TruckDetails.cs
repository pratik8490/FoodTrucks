using FoodTrucks.Context;
using FoodTrucks.Helper;
using FoodTrucks.Provider;
using FoodTrucks.Provider.Interface;
using FoodTrucks.Provider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FoodTrucks.Pages
{
    public class TruckDetails : BasePage
    {          
        private List<FoodTypeModel> _FoodTypeList = new List<FoodTypeModel>();
        private List<BarModel> _BarList = new List<BarModel>();
        private ITruckInfo _TruckInfoProvider = new TruckInfoProvider();
        private IFoodType _FoodTypeProvider = new FoodTypeProvider();
        private IBar _BarProvider = new BarProvider();
        private TruckInfoModel _TruckInfo = new TruckInfoModel();

        public TruckDetails(int truckId)
        {
            Title = "Truck Details";
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    //Call for food type and Bar
                    _FoodTypeList = await _FoodTypeProvider.GetFoodType();
                    _BarList = await _BarProvider.GetBar();
                    _TruckInfo = await _TruckInfoProvider.GetTruckDetailByTruckID(1);

                    TruckDetailsLayout();
                }
                catch (Exception ex)
                {

                }
            });

        }

        public void TruckDetailsLayout()
        {
            Map map = new Map
            {
                IsShowingUser = true,
                HeightRequest = 300,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(23, 73), Distance.FromMiles(3))); // Santa Cruz golf course

            Label lblTruckNameText = new Label { Text = "Truck Name :     ", FontSize = 22, TextColor = Color.Black };

            Label lblTruckName = new Label { FontSize = 22, TextColor = Color.Black };
            lblTruckName.Text = _TruckInfo.TruckName;

            StackLayout slTruckName = new StackLayout { Children = { lblTruckNameText, lblTruckName }, Orientation = StackOrientation.Horizontal };

            Label lblTruckDescText = new Label { Text = "Truck Description :   ", FontSize = 22, TextColor = Color.Black };

            Label lblTruckDesc = new Label { FontSize = 22, TextColor = Color.Black };
            lblTruckDesc.Text = _TruckInfo.Description;

            StackLayout slTruckDesc = new StackLayout { Children = { lblTruckDescText, lblTruckDesc }, Orientation = StackOrientation.Horizontal };

            Label lblFoodTypeText = new Label { Text = "Food Type :     ", FontSize = 22, TextColor = Color.Black };

            Label lblFoodType = new Label { FontSize = 22, TextColor = Color.Black };
            lblFoodType.Text = _FoodTypeList.Find(x => x.Id == _TruckInfo.FoodTypeId).Type.ToString();

            StackLayout slFoodType = new StackLayout { Children = { lblFoodTypeText, lblFoodType }, Orientation = StackOrientation.Horizontal };

            //Label lblActiveText = new Label { Text = "Active", FontSize = 22, TextColor = Color.Black };

            //Switch schActive = new Switch { HorizontalOptions = LayoutOptions.EndAndExpand };

            //StackLayout slActive = new StackLayout { Children = { lblActiveText, schActive }, Orientation = StackOrientation.Horizontal };

            Label lblBarText = new Label { Text = "In Front Of :     ", FontSize = 22, TextColor = Color.Black };

            Label lblBar = new Label { FontSize = 22, TextColor = Color.Black };
            lblBar.Text = _BarList.Find(x => x.Id == _TruckInfo.BarId).Name.ToString();

            StackLayout slBar = new StackLayout { Children = { lblBarText, lblBar }, Orientation = StackOrientation.Horizontal };

            Label lblLinkText = new Label { Text = "Link :  ", FontSize = 22, TextColor = Color.Black };

            Label lblLink = new Label { FontSize = 22, TextColor = Color.Black };
            lblLink.Text = _TruckInfo.Link;

            StackLayout slLink = new StackLayout { Children = { lblLinkText, lblLink }, Orientation = StackOrientation.Horizontal };

            Label lblMenuText = new Label { Text = "Menu :     ", FontSize = 22, TextColor = Color.Black };

            Image imgMenu = new Image { WidthRequest = 60, HeightRequest = 60 };
            if (!string.IsNullOrEmpty(_TruckInfo.Menu))
                imgMenu.Source = FileImageSource.FromUri(new Uri("http://foodlifttrucks.com/" + _TruckInfo.Menu));

            StackLayout slMenu = new StackLayout { Children = { lblMenuText, imgMenu }, Orientation = StackOrientation.Horizontal };

            StackLayout slTruckDetails = new StackLayout
            {
                Children = { map, slTruckName, slTruckDesc, slFoodType, slBar, slLink, slMenu },
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(10, Device.OnPlatform(40, 10, 0), 10, 10),
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slTruckDetails
            };
        }
    }
}
