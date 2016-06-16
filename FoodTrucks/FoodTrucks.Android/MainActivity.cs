using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Xamarin;
using Xamarin.Forms;
using Acr.UserDialogs;
using Android.Locations;
using System.Collections.Generic;
using Android.Util;

namespace FoodTrucks.Droid
{
    [Activity(Label = "FoodTrucks", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        Location _currentLocation;
        LocationManager _locationManager;
        string _locationProvider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);
            UserDialogs.Init(() => (Activity)Forms.Context);
            //Xamarin.Forms.Forms.SetTitleBarVisibility(Xamarin.Forms.AndroidTitleBarVisibility.Never);
            this.ActionBar.SetIcon(Android.Resource.Color.Transparent);
            LoadApplication(new App());
            InitializeLocationManager();
            
        }

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.High
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
            //Log.Debug(TAG, "Using " + _locationProvider + ".");
        }
    }
}

