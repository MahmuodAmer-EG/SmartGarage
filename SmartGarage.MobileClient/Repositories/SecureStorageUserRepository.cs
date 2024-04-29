using SmartGarage.MobileClient.Model;
using System.Text.Json;

namespace SmartGarage.MobileClient.Repositories
{
    public class SecureStorageUserRepository
    {
        private const string CURRENT_USER_CREDINTIALS_KEY = "user_credintials";
        

        private readonly ISecureStorage _secureStorage;
        public SecureStorageUserRepository(ISecureStorage secureStorage)
        {
            _secureStorage = secureStorage;
        }

        /// <summary>
        /// Read the user Data 
        /// If faild return null
        /// </summary>
        /// <returns></returns>
        public UserCredintials ReadUser()
        {
            try
            {
                string userCredintialsJson = Task.Run(() => _secureStorage.GetAsync(CURRENT_USER_CREDINTIALS_KEY)).Result;


                UserCredintials credential = JsonSerializer.Deserialize<UserCredintials>(userCredintialsJson);


                return credential;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SaveUser(UserCredintials Credential)
        {
            try
            {
                
                string credentialJson = JsonSerializer.Serialize(Credential);

                _secureStorage.SetAsync(CURRENT_USER_CREDINTIALS_KEY, credentialJson).GetAwaiter().GetResult();
                
            }
            catch (Exception)
            {

            }
        }

        public void DeleteUser()
        {
            _secureStorage.Remove(CURRENT_USER_CREDINTIALS_KEY);
        }

        public bool UserExists()
        {
            var UserCredintials = ReadUser();

            return UserCredintials != null;
        }
    }
}
