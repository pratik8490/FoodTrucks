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
    }
}
