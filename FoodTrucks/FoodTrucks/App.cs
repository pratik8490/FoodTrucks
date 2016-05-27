using FoodTrucks.Helper;
using FoodTrucks.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FoodTrucks
{
    public class App : Application
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            // The root page of your application 
            //MainPage = new MainPage();
            MainPage = HomePage();
        }
        #endregion

        static NavigationPage navPage;

        public static Page HomePage(bool IsFromLogout = false)
        {
            if (!IsFromLogout)
            {
                navPage = new NavigationPage(new AddTruckInfo());
                navPage.BarBackgroundColor = LayoutHelper.BarBackGroundColor;
                navPage.BarTextColor = LayoutHelper.BarBackTextColor;

                return navPage;
            }
            else
            {
                return new MainPage();
            }
        }

        public static Page LoginPage()
        {
            return new LoginPage();
        }

        public static Page AddTuckPage()
        {
            return new AddTruckInfo();
        }

        public static Page SignUpPage()
        {
            return new SignUpPage();
        }

        public static Page MapPage()
        {
            return new MasterPage(new MapPage());
        }

        public static Page TruckListPage()
        {
            return new MasterPage(new TrucksList());
        }

        public static Page EditTruckListPage()
        {
            return new MasterPage(new EditTruckInfo());
        }

        public static Color BarTextColor()
        {
            return Color.White;
        }

        protected override void OnStart()
        {
            // Handle when your app starts 
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps 
        }

        protected override void OnResume()
        {
            // Handle when your app resumes 
        }
    }
}