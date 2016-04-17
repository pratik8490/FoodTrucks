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
    public class ReviewProvider : IReview
    {
        private string _SiteUrl = string.Empty;

        public ReviewProvider()
        {
            _SiteUrl = "http://foodlifttrucks.com/";
        }
        public Task<ReviewDetailModel> GetByTruckId(int truckId)
        {
            var review = new TaskCompletionSource<ReviewDetailModel>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);

                    var requestUri = "api/Review?truckId=" + truckId;

                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        review.TrySetResult(JsonConvert.DeserializeObject<ReviewDetailModel>(content));
                    }
                    else
                    {
                        review.SetResult(new ReviewDetailModel());
                    }
                }
                catch (Exception ex)
                {

                }
            });
            return review.Task;
        }
    }
}
