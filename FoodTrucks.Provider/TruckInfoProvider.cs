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
    public class TruckInfoProvider : ITruckInfo
    {
        private string _SiteUrl = string.Empty;

        public TruckInfoProvider()
        {
            _SiteUrl = "http://foodlifttrucks.com/";
        }

        public Task<List<TruckInfoModel>> GetTruckList()
        {

            var TruckList = new TaskCompletionSource<List<TruckInfoModel>>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);

                    var requestUri = "api/TruckInfo";

                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        TruckList.TrySetResult(JsonConvert.DeserializeObject<List<TruckInfoModel>>(content));
                    }
                    else
                    {
                        TruckList.SetResult(new List<TruckInfoModel>());
                    }

                }
                catch (Exception ex)
                {

                }
            });

            return TruckList.Task;
        }

        public Task<int> Add(TruckInfoModel truckInfoModel)
        {
            var newId = new TaskCompletionSource<int>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);

                    var requestUri = "api/TruckInfo";

                    var json = JsonConvert.SerializeObject(truckInfoModel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(requestUri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var readContent = await response.Content.ReadAsStringAsync();
                        newId.SetResult(JsonConvert.DeserializeObject<int>(readContent));
                    }
                    else
                    {
                        newId.SetResult(0);
                    }
                }
                catch (Exception ex)
                {
                    newId.SetResult(0);
                }
            });
            return newId.Task;
        }
    }
}
