using FoodTrucks.Provider.Interface;
using FoodTrucks.Provider.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodTrucks.Provider
{
    public class BarProvider : IBar
    {
        private string _SiteUrl = string.Empty;

        public BarProvider()
        {
            _SiteUrl = "http://foodlifttrucks.com/";
        }
        public Task<List<BarModel>> GetBar()
        {
            var bar = new TaskCompletionSource<List<BarModel>>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);

                    var requestUri = "api/Bar";
                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        bar.TrySetResult(JsonConvert.DeserializeObject<List<BarModel>>(content));
                    }
                    else
                    {
                        bar.SetResult(new List<BarModel>());
                    }
                }
                catch (Exception ex)
                {

                }
            });

            return bar.Task;
        }
    }
}
