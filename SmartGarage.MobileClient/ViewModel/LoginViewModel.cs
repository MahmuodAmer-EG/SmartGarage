using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGarage.MobileClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartGarage.MobileClient.ViewModel
{
    public partial class LoginViewModel:ObservableObject
    {
        private readonly AuthService authService;

        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _password;
        

        public LoginViewModel(AuthService authService)
        {
            this.authService = authService;

            //try to get auth from local storage

            if (authService.IsAuthenticated())
                Shell.Current.GoToAsync("//MainView");
        }
        [RelayCommand]
        public async Task Login()
        {
            var userCred = await authService.Authonticate(Username, Password);

            if (userCred == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Login Error",
                    "Invalid username or password",
                    "OK");
            }
            else
            {
                authService.SaveToSecureStorage(userCred);
                await Shell.Current.GoToAsync("//MainView");

            }
        }
    }
}
