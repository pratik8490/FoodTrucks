using FoodTrucks.Provider.Interface;
using FoodTrucks.Provider.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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

        public void UploadBitmapAsync(byte[] bitmapData, int id)
        {
            //var fileContent = new ByteArrayContent(bitmapData);

            //fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
            //fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            //{
            //    Name = "file",
            //    FileName = id.ToString() + ".jpg"
            //};
            Device.BeginInvokeOnMainThread(async () =>
            {

                Uri webService = new Uri(_SiteUrl + "api/TruckInfo?id=" + id.ToString());

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, _SiteUrl);
                requestMessage.Headers.ExpectContinue = false;

                HttpContent stringContent = new StringContent(id.ToString());

                //string result = System.Text.Encoding.UTF8.GetString(bitmapData, 0, 0);

               MultipartFormDataContent multipartContent = new MultipartFormDataContent();
               //MemoryStream byteArrayContent1 = new MemoryStream(bitmapData);
                //byteArrayContent1.Headers.Add("Content-Type", "application/octet-stream");

                //multipartContent.Add(stringContent, "Id");
                multipartContent.Add(new StreamContent(new MemoryStream(bitmapData)), "File1", id.ToString() + ".jpg");

                HttpClient httpClient = new HttpClient();

                var httpRequest = await httpClient.PostAsync(webService, multipartContent);
                //HttpResponseMessage httpResponse = httpRequest.Result;
                //HttpStatusCode statusCode = httpResponse.StatusCode;

                //HttpContent responseContent = httpResponse.Content;

                if (httpRequest.IsSuccessStatusCode)
                {
                    //Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
                    //String stringContents = stringContentsTask.Result;
                }
            });
        }

        public Task<TruckInfoModel> GetTruckDetail(int UserID)
        {
            var TruckList = new TaskCompletionSource<TruckInfoModel>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(_SiteUrl);

                    var requestUri = "api/TruckInfo?UserID=" + UserID;

                    var response = await client.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        TruckList.TrySetResult(JsonConvert.DeserializeObject<TruckInfoModel>(content));
                    }
                    else
                    {
                        TruckList.SetResult(new TruckInfoModel());
                    }

                }
                catch (Exception ex)
                {

                }
            });

            return TruckList.Task;
        }

    }
}
