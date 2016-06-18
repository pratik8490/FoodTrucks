using System;
using Android.App;
using Android.Net;
using Android.Content;
using FoodTrucks.Interface;
using FoodTrucks.Droid.DependencyService;
using Xamarin.Forms;
using System.Threading.Tasks;
using FoodTrucks.Models;

[assembly: Dependency(typeof(PhoneService))]
namespace FoodTrucks.Droid.DependencyService
{
    public class PhoneService : IPhoneService
    {
        public void LaunchMap(string address)
        {
            var geoUri = Android.Net.Uri.Parse(string.Format("geo:0,0?q={0}", address));

            StartActivity(geoUri);
        }

        public async Task LaunchNavigationAsync(NavigationModel navigationModel)
        {
            var uri = Android.Net.Uri.Parse("google.navigation:q=" + navigationModel.DestinationAddress);

            StartActivity(uri);
        }
        private void StartActivity(Android.Net.Uri uri)
        {
            var intent = new Intent(Intent.ActionView, uri);
            Forms.Context.StartActivity(intent);
        }

    }
}