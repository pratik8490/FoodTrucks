using System;
using Xamarin.Forms;
using System.Collections.Generic;
using FoodTrucks.Helper;
using FoodTruck;
using FoodTrucks.Provider.Models;
using FoodTrucks.Provider;
using FoodTrucks.Provider.Interface;

namespace FoodTrucks
{
    public class TrucksList : BasePage
    {
        private List<TruckInfoModel> _TruckInfoList = new List<TruckInfoModel>();
        private ITruckInfo _TruckInfoProvider = new TruckInfoProvider();

        public TrucksList()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                _TruckInfoList = await _TruckInfoProvider.GetTruckList();
                TrucksListLayout();
            });
        }

        public class Trucks
        {
            public string Truck { get; set; }
        }

        public void TrucksListLayout()
        {
            Label lblTitle = new Label
            {
                Text = "Truck List",
                FontSize = 14,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                TextColor = Color.Black
            };

            StackLayout slTitle = new StackLayout
            {
                Children = { lblTitle },
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
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout slHeader = new StackLayout
            {
                Children = { slTitle, slImgFilter },
                Orientation = StackOrientation.Horizontal
            };

            Seperator spTitle = new Seperator();

            //List of Escrow
            ListView trucksListView = new ListView { RowHeight = 55, SeparatorColor = Color.FromHex("#eeeeee") };
            trucksListView.ItemsSource = _TruckInfoList;
            trucksListView.ItemTemplate = new DataTemplate(() => new TruckCell());

            //			myEscrowListView.ItemSelected += (sender, e) =>
            //			{
            //				if (e.SelectedItem == null) return;
            //				EscrowModel escrowModel = (EscrowModel)e.SelectedItem;
            //
            //				((ListView)sender).SelectedItem = null;
            //				Navigation.PushAsync(App.EditEscrowPage(escrowModel.EscrowId));
            //			};

            Seperator spHeader = new Seperator();

            StackLayout slTruckListPage = new StackLayout
            {

                Children = { 
                        new StackLayout{
                            Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 0),
						    Children = { slHeader },
                            VerticalOptions = LayoutOptions.Start
                        },
                        new StackLayout {
                            Padding = new Thickness(0),
                            Children = {spHeader.LineSeperatorView},
                            //VerticalOptions = LayoutOptions.StartAndExpand
                        },
                        new StackLayout {
                            Padding = new Thickness(20, 0, 20, 0),
                            Children = { trucksListView},
                        }
                    },
                Orientation = StackOrientation.Vertical,
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slTruckListPage,
            };
        }
    }
}

