using FoodTrucks.Context;
using FoodTrucks.Helper;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            LoadingInit();
        }
        ActivityIndicator LoadingIndicator;
        private void LoadingInit()
        {
            LoadingIndicator = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Color = Color.Black,
                IsVisible = false
            };
            this.Content = new StackLayout
            {
                Children = {                    
                    LoadingIndicator,                    
                },
                BackgroundColor = Color.White,
            };
        }
        public string GetDeviceID()
        {
            return CrossDeviceInfo.Current.Id;
        }
        #region Override Method
        /// <summary>
        /// On appearing method.
        /// </summary>
        protected override void OnAppearing()
        {
            LoadingIndicator.IsRunning = IsLoading;
            LoadingIndicator.IsVisible = IsLoading;

            if (FoodTruckContext.IsLoggedIn)
            {
                BindToolbar();
            }
        }
        protected override void OnDisappearing()
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;

            this.ToolbarItems.Clear();
        }

        public void BindToolbar()
        {

            List<ToolbarItem> lstToolbarItem = new List<ToolbarItem>();

            ExtendedToolbarItem menuToolbarItem = new ExtendedToolbarItem("Menu", Constants.ImagePath.LeftMenuIcon, ToolbarItemOrder.Primary, CategoryMenu);

            lstToolbarItem.Add(menuToolbarItem);

            lstToolbarItem.Add(new ToolbarItem
            {
                Text = "Filter",
                Icon = Constants.ImagePath.FilterIcon,
                Order = ToolbarItemOrder.Primary,
                Command = new Command(Filter)
            });

            foreach (ToolbarItem item in lstToolbarItem)
            {
                this.ToolbarItems.Add(item);
            }
        }

        public void Filter()
        {
            if (((ContentPage)((MasterDetailPage)(ParentView)).Detail).GetType().Name == "MapPage")
                Navigation.PushAsync(App.TruckListPage());
            else
                Navigation.PushAsync(App.MapPage());
        }
        public void CategoryMenu()
        {
            string ViewName = (ParentView.ParentView).GetType().Name;
            if ((ParentView.ParentView).GetType().Name != "NavigationPage")
                ((MasterDetailPage)(ParentView).ParentView).IsPresented = !((MasterDetailPage)(ParentView).ParentView).IsPresented;
            else
                ((MasterDetailPage)ParentView).IsPresented = !((MasterDetailPage)ParentView).IsPresented;
        }

        private async void Logout()
        {
            Navigation.PushModalAsync(App.HomePage(true));
        }

        #endregion

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
    }
}

