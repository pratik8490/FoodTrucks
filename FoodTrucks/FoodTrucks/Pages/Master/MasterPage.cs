using FoodTrucks.Context;
using FoodTrucks.Helper;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MenuItem = FoodTrucks.Models.MenuModel;

namespace FoodTrucks.Pages
{
    public class MasterPage : MasterDetailPage
    {
        MenuPage menuPage;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Master Page"/> class.
        /// </summary>
        public MasterPage(ContentPage DetailPage)
        {
            menuPage = new MenuPage { Icon = Constants.ImagePath.SlideOut };

            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);
            Master = menuPage;

            //this.Master = new MenuPage { Icon = Constants.ImagePath.CategoryLineMenuIcon };
            this.Detail = DetailPage;
        }
        #endregion

        protected override void OnDisappearing()
        {
            GC.Collect();
        }
        void NavigateTo(MenuItem menu)
        {
            if (menu == null)
                return;

            if (menu.Title == "Edit Profile")
            {
                Navigation.PushAsync(App.EditTruckListPage());
            }
            else if (menu.Title == "Home")
            {
                Navigation.PushModalAsync(App.HomePage());
            }
            else
            {
                //API call for remove deviceID
                FoodTruckContext.Clear();
                Navigation.PushModalAsync(App.LoginPage());
            }

            //Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            //Detail = new NavigationPage(displayPage);

            //menuPage.Menu.SelectedItem = null;
            //IsPresented = false;
        }
    }
}
