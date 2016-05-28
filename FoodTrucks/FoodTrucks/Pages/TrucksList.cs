using System;
using Xamarin.Forms;
using System.Collections.Generic;
using FoodTrucks.Helper;
using FoodTruck;
using FoodTrucks.Provider.Models;
using FoodTrucks.Provider;
using FoodTrucks.Provider.Interface;
using System.Collections.ObjectModel;

namespace FoodTrucks
{
    public class TrucksList : BasePage
    {
        private List<TruckInfoModel> _TruckInfoList = new List<TruckInfoModel>();
        private ITruckInfo _TruckInfoProvider = new TruckInfoProvider();

        public TrucksList()
        {
            IsLoading = true;
            Title = "Truck List";
            Device.BeginInvokeOnMainThread(async () =>
            {
                _TruckInfoList = await _TruckInfoProvider.GetTruckList();
                Items = new ObservableCollection<TruckInfoModel>(_TruckInfoList);
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
            ListView trucksListView = new ListView { RowHeight = 80, SeparatorColor = Color.FromHex("#eeeeee"), VerticalOptions = LayoutOptions.FillAndExpand };
            trucksListView.ItemsSource = Items;
            trucksListView.ItemTemplate = new DataTemplate(() => new TruckCell());

            StackLayout slTruckListView = new StackLayout
            {
                Children = { trucksListView },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            trucksListView.ItemTapped += (sender, e) =>
                        {
                            // don't do anything if we just de-selected the row
                            if (e.Item == null) return;
                            TruckInfoModel model = (TruckInfoModel)e.Item;
                            Navigation.PushAsync(App.TruckDetailPage(model.Id));
                            // do something with e.SelectedItem
                            ((ListView)sender).SelectedItem = null; // de-select the row after ripple effect
                        };

            Seperator spHeader = new Seperator();

            StackLayout slTruckListPage = new StackLayout
            {

                Children = { 
                    slTruckListView
                        //new StackLayout{
                        //    Padding = new Thickness(20, Device.OnPlatform(40,20,0), 20, 0),
                        //    Children = { slHeader },
                        //    VerticalOptions = LayoutOptions.Start
                        //},
                        //new StackLayout {
                        //    Padding = new Thickness(0),
                        //    Children = {spHeader.LineSeperatorView},
                        //    //VerticalOptions = LayoutOptions.StartAndExpand
                        //},
                        //new StackLayout {
                        //    Padding = new Thickness(5),
                        //    Children = { slTruckListView},
                        //},
                    },
                //Padding = new Thickness(20, Device.OnPlatform(40, 20, 0), 20, 20),
                //Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = LayoutHelper.PageBackgroundColor
            };

            Content = new ScrollView
            {
                Content = slTruckListPage,
            };
        }
        #region Custom Property

        private ObservableCollection<TruckInfoModel> _items = new ObservableCollection<TruckInfoModel>();
        public ObservableCollection<TruckInfoModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}

