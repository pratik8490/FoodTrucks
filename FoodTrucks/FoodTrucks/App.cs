using FoodTrucks.Helper;
using FoodTrucks.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Mvvm;

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
            Init();
            MainPage = HomePage();
        }
        #endregion
        /// <summary>
        /// Initializes the application.
        /// </summary>
        public static void Init()
        {

            var app = Resolver.Resolve<IXFormsApp>();
            if (app == null)
            {
                return;
            }

            app.Closing += (o, e) => Debug.WriteLine("Application Closing");
            app.Error += (o, e) => Debug.WriteLine("Application Error");
            app.Initialize += (o, e) => Debug.WriteLine("Application Initialized");
            app.Resumed += (o, e) => Debug.WriteLine("Application Resumed");
            app.Rotation += (o, e) => Debug.WriteLine("Application Rotated");
            app.Startup += (o, e) => Debug.WriteLine("Application Startup");
            app.Suspended += (o, e) => Debug.WriteLine("Application Suspended");
        }
        static NavigationPage navPage;

        public static Page HomePage(bool IsFromLogout = false)
        {
            navPage = new NavigationPage(new LoginPage());
            navPage.BarBackgroundColor = LayoutHelper.BarBackGroundColor;
            navPage.BarTextColor = LayoutHelper.BarBackTextColor;

            return navPage;
        }

        public static Page LoginPage(bool IsLogout = false)
        {
            if (IsLogout)
            {
                navPage = new NavigationPage(new LoginPage());
                navPage.BarBackgroundColor = LayoutHelper.BarBackGroundColor;
                navPage.BarTextColor = LayoutHelper.BarBackTextColor;
                return navPage;
            }
            else
            {
                return new LoginPage();
            }
        }

        public static Page UserRegisterPage()
        {
            return new UserRegisterPage();
        }

        public static Page ProviderRegisterPage()
        {
            return new ProviderRegister();
        }

        public static Page MapPage()
        {
            //return new MapPage();
            return new MasterPage(new MapPage());
        }

        #region Truck Pages
        public static Page TruckListPage()
        {
            return new MasterPage(new TrucksList());
        }
        public static Page TruckDetailPage(int truckID)
        {
            return new MasterPage(new TruckDetails(truckID));
        }
        public static Page AddTuckPage()
        {
            return new AddTruckInfo();
        }

        public static Page EditTruckListPage()
        {
            return new MasterPage(new EditTruckInfo());
        }

        #endregion

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