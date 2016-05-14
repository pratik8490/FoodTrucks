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
    public class FoodTypeProvider : IFoodType
    {
        private string _SiteUrl = string.Empty;

        public FoodTypeProvider()
        {
            _SiteUrl = "http://foodlifttrucks.com/";
        }
        public Task<List<FoodTypeModel>> GetFoodType()
        {
            var foodType = new TaskCompletionSource<List<FoodTypeModel>>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);

                    var requestUri = "api/FoodType";
                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        foodType.TrySetResult(JsonConvert.DeserializeObject<List<FoodTypeModel>>(content));
                    }
                    else
                    {
                        foodType.SetResult(new List<FoodTypeModel>());
                    }
                }
                catch (Exception ex)
                {

                }
            });

            return foodType.Task;
        }
        public Task<FoodTypeModel> GetFoodTypeByID(int id)
        {
            var foodType = new TaskCompletionSource<FoodTypeModel>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);

                    var requestUri = "api/FoodType?id=" + id;

                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        foodType.TrySetResult(JsonConvert.DeserializeObject<FoodTypeModel>(content));
                    }
                    else
                    {
                        foodType.SetResult(new FoodTypeModel());
                    }

                }
                catch (Exception ex)
                {

                }
            });

            return foodType.Task;
        }
    }
}
