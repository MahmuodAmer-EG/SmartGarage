using SmartGarage.MobileClient.Services;
namespace SmartGarage.MobileClient.Tests
{
    public class AuthServiceTests
    {
        [Test]
        public void Try_Authontication()
        {
            var username = "MahmuodAmer1@gmail.com";
            var password = "@Amer0114210857";

            var authService = new AuthService();


            var auth = authService.Authonticate(username, password);

            var userCred = auth.Result;

            Assert.That(userCred, Is.Not.Null);
        }
    }
}