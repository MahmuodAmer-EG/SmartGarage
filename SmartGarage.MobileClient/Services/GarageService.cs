using Newtonsoft.Json;
using SmartGarage.MobileClient.Models;

namespace SmartGarage.MobileClient.Services
{
    public class GarageService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public GarageService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<CarReserveResponse> RequestToStoreCarAsync()
        {
            try
            {
                //Postman code
                //var httpClient = httpClientFactory.CreateClient("custom-httpclient");

                var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7029/RequestToStoreCar");
                request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJBQW1lciIsImp0aSI6ImZkZGMyY2Y2LWJiYjktNDg4Yi1iZTNkLTg5ZmRmZjAxMWRlMCIsImVtYWlsIjoiemV6b2FlbXIxMTNAZ21haWwuY29tIiwidWlkIjoiNDRjMjJmYjEtYjczMy00NTFkLWI2ZjMtMjY1NDg5Y2MxMWFkIiwicm9sZXMiOiJVc2VyIiwiZXhwIjoxNzEwNjQ0MDk1LCJpc3MiOiJTZWN1cmVBcGkiLCJhdWQiOiJTZWN1cmVBcGlVc2VyIn0.fwjjAAvpsNwD8BuGwS0ImLnvfbvXr2XaGUOZFRBA9Pk");
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();


                //serialize the response
                var reserveResponse = JsonConvert.DeserializeObject<CarReserveResponse>(await response.Content.ReadAsStringAsync());

                return reserveResponse;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
