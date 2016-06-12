using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Android.Locations;
using FoodTrucks.Droid.CustomRenderer;
using FoodTrucks.Interface;
using FoodTrucks.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using FoodTrucks.Context;

[assembly: Dependency(typeof(CurrentLocation))]
namespace FoodTrucks.Droid.CustomRenderer
{
    public class CurrentLocation : Activity, ICurrentLocation
    {
        private Xamarin.Geolocation.Geolocator locator;
        private LocationManager _manager;
        private string[] _providers;
        private Activity context;

        public Task<Position> SetCurrentLocation()
        {
            var response = new TaskCompletionSource<Position>();

            context = Forms.Context as Activity;
            _manager = context.GetSystemService(Android.Content.Context.LocationService) as LocationManager;
            _providers = _manager.GetProviders(false).Where(s => s != LocationManager.PassiveProvider).ToArray();

            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    Geocoder coder = new Geocoder(this);

                    locator = new Xamarin.Geolocation.Geolocator(context) { DesiredAccuracy = 50 };
                    if (locator.IsListening != true)
                        locator.StartListening(minTime: 1000, minDistance: 0);

                    Xamarin.Geolocation.Position result = null;

                    Position obj = new Position();

                    if (locator.IsGeolocationEnabled)
                    {
                        try
                        {
                            if (!locator.IsListening)
                                locator.StartListening(1000, 1000);

                            Xamarin.Geolocation.Position position = await locator.GetPositionAsync(10000);

                            obj.Latitude = position.Latitude;
                            obj.Longitude = position.Longitude;
                            obj.Message = string.Empty;

                            var address = await coder.GetFromLocationAsync(position.Latitude, position.Longitude, 1);

                            foreach (var location in address)
                            {
                                obj.Address = location.GetAddressLine(0) + " " + location.GetAddressLine(1) + " " + location.GetAddressLine(2) + " " + location.GetAddressLine(3);
                            }

                            response.SetResult(obj);
                            FoodTruckContext.AlreadyEnable = true;
                            //System.Diagnostics.Debug.WriteLine("[GetPosition] Lat. : {0} / Long. : {1}", result.Latitude.ToString("N4"), result.Longitude.ToString("N4"));
                        }
                        catch (Exception e)
                        {
                            obj.Message = e.Message.ToString();

                            response.SetResult(obj);
                        }
                    }
                    else
                    {
                        //obj.Message = "Please enable location service on your device.";
                        //response.SetResult(obj);
                        //var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                        //StartActivity(intent);
                        Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                        Forms.Context.StartActivity(gpsSettingIntent);
                    }
                });
            }
            catch (Exception e)
            {
            }

            return response.Task;
        }

        public Task<Position> GetLocation(string locationAddress)
        {
            var result = new TaskCompletionSource<Position>();

            Device.BeginInvokeOnMainThread(async () =>
                {

                    Geocoder coder = new Geocoder(this);
                    double geolongitude = 0;
                    double geolatitude = 0;

                    var address = await coder.GetFromLocationNameAsync(locationAddress, 1);
                    if (address == null)
                    {
                        result.TrySetResult(new Position());
                    }
                    else
                    {
                        foreach (var location in address)
                        {
                            geolongitude = location.Latitude;
                            geolatitude = location.Longitude;
                        }

                        Position postion = new Position();
                        postion.Latitude = geolatitude;
                        postion.Longitude = geolongitude;
                        postion.Address = locationAddress;

                        result.TrySetResult(postion);
                    }
                });
            return result.Task;
            //var geolongitude = 38.6731610;
            //var geolatitude =  -77.3328850;

            //var streetViewUri = Android.Net.Uri.Parse("google.streetview:cbll=" + geolongitude + "," + geolatitude + "&cbp=1,60,,0,1.0&mz=20");
        }
    }
}