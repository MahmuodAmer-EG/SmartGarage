using Newtonsoft.Json;
using SmartGarage.API.Models;
using SmartGarage.MobileClient.Model;
using SmartGarage.MobileClient.Repositories;

namespace SmartGarage.MobileClient.Services
{
    public class AuthService
    {
        private readonly SecureStorageUserRepository storageUserRepository;
        private readonly IHttpClientFactory httpClientFactory;


        public AuthService(SecureStorageUserRepository storageUserRepository, IHttpClientFactory httpClientFactory)
        {
            this.storageUserRepository = storageUserRepository;
            this.httpClientFactory = httpClientFactory;
            
        }
        
        public bool IsAuthenticated()
        {
            var credintials = storageUserRepository.ReadUser();

            if (credintials == null)
                return false;
            else
                return true;

        }
        
        public async Task<UserCredintials> Authonticate(string username, string password)
        {
            try
            {
                //Postman code
                var httpClient = httpClientFactory.CreateClient("custom-httpclient");
                
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/Auth/token");
                
                var content = new StringContent($"{{\r\n      \"email\": \"{username}\",\r\n      \"password\": \"{password}\"\r\n}}", null, "application/json");
                request.Content = content;
                var token = await httpClient.SendAsync(request);
                token.EnsureSuccessStatusCode();


                //serialize the response
                var authModel = JsonConvert.DeserializeObject<AuthModel>(await token.Content.ReadAsStringAsync());


                var userCredintials = new UserCredintials { Auth = authModel };

                return userCredintials;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public void SaveToSecureStorage(UserCredintials userCredintials)
        {
            //write the token down
            storageUserRepository.SaveUser(userCredintials);
        }
    }
}
