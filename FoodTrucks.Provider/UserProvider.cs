using FoodTrucks.Provider.Interface;
using FoodTrucks.Provider.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks.Provider
{
    public class UserProvider : IUser
    {
        private string _SiteUrl = string.Empty;
        public UserProvider()
        {
            _SiteUrl = "http://foodlifttrucks.com/";
        }

        public Task<int> SignUpUser(UserModel userModel)
        {
            var res = new TaskCompletionSource<int>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var requestUri = "api/User";

                    var json = JsonConvert.SerializeObject(userModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(requestUri, content).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var readContent = await response.Content.ReadAsStringAsync();
                        res.SetResult(JsonConvert.DeserializeObject<int>(readContent));
                    }
                    else
                    {
                        res.SetResult(0);
                    }
                }
                catch (Exception ex)
                {
                    res.SetResult(0);
                }
            });

            return res.Task;
        }

        public Task<bool> LogInUser(string email, int pin)
        {
            var res = new TaskCompletionSource<bool>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var requestUri = "api/User?email=" + email + "&pin=" + pin;

                    HttpResponseMessage response = await client.PostAsync(requestUri, null).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var readContent = await response.Content.ReadAsStringAsync();
                        res.SetResult(JsonConvert.DeserializeObject<bool>(readContent));
                    }
                    else
                    {
                        res.SetResult(false);
                    }
                }
                catch (Exception ex)
                {
                    res.SetResult(false);
                }
            });

            return res.Task;
        }

        public Task<UserModel> CheckDeviceLoggedIn(string deviceID)
        {
            var LoggedIn = new TaskCompletionSource<UserModel>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);

                    var requestUri = "api/User?deviceID=" + deviceID;

                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        LoggedIn.TrySetResult(JsonConvert.DeserializeObject<UserModel>(content));
                    }
                    else
                    {
                        LoggedIn.SetResult(new UserModel());
                    }

                }
                catch (Exception ex)
                {
                    LoggedIn.SetResult(new UserModel());
                }
            });
            return LoggedIn.Task;
        }
    }
}
