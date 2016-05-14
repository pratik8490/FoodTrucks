using System;

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

namespace FoodTrucks.Droid
{
    [Activity(Label = "FoodTrucks", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);
            UserDialogs.Init(() => (Activity)Forms.Context);
            //Xamarin.Forms.Forms.SetTitleBarVisibility(Xamarin.Forms.AndroidTitleBarVisibility.Never);
            this.ActionBar.SetIcon(Android.Resource.Color.Transparent);
            LoadApplication(new App());
            
        }
    }
}

