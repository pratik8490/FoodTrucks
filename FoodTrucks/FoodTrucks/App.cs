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
            MainPage = Main();
        }
        #endregion

        static NavigationPage navPage;

        public static Page Main()
        {
            navPage = new NavigationPage(new MainPage());
            navPage.BarBackgroundColor = LayoutHelper.BarBackGroundColor;
            navPage.BarTextColor = LayoutHelper.BarBackTextColor;

            return navPage;
        }

        public static Page SignUpPage()
        {
            return new SignUpPage();
        }

        public static Page HomePage()
        {
            return new MainPage();
        }

        public static Page MapPage()
        {
            return new MapPage();
        }


        public static Color BarTextColor()
        {
            return Color.White;
        }
        public static Page TruckListPage()
        {
            return new TrucksList();
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