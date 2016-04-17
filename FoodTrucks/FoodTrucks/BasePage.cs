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

        #region Override Method
        /// <summary>
        /// On appearing method.
        /// </summary>
        protected override void OnAppearing()
        {
            LoadingIndicator.IsRunning = IsLoading;
            LoadingIndicator.IsVisible = IsLoading;

        }

        public void BindToolbar()
        {
            List<ToolbarItem> lstToolbarItem = new List<ToolbarItem>();



            lstToolbarItem.Add(new ToolbarItem
            {
                Text = "Logout",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(Logout)
            });

            foreach (ToolbarItem item in lstToolbarItem)
            {
                this.ToolbarItems.Add(item);
            }
        }

        private async void Logout()
        {
            Navigation.PushModalAsync(App.HomePage());
        }

        protected override void OnDisappearing()
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;

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
