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
            MainPage = new MapPage();
        }
        #endregion

        static NavigationPage navPage;
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