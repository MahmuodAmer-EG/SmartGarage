using Microsoft.Extensions.Logging;
using SmartGarage.MobileClient.Pages;
using SmartGarage.MobileClient.Repositories;
using SmartGarage.MobileClient.Services;
using SmartGarage.MobileClient.ViewModel;


namespace SmartGarage.MobileClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IPlatformHttpMessageHandler>(sp =>
            {
#if ANDROID
                return new AndroidHttpMessageHandler();
#else
                return null!;
#endif

            });


            builder.Services.AddHttpClient("custom-httpclient", httpClient =>
            {
                var baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7029" : "https://localhost:7029";
                httpClient.BaseAddress = new Uri(baseAddress);
                
            }).ConfigureHttpMessageHandlerBuilder(configBuilder =>
            {
                var platformMessageHandler = configBuilder.Services.GetRequiredService<IPlatformHttpMessageHandler>();
                configBuilder.PrimaryHandler = platformMessageHandler.GetHttpMessageHandler();
            });

            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddTransient<GarageRepository>();


            builder.Services.AddTransient<LoginView>(serviceProvider => new LoginView
            {
                BindingContext = serviceProvider.GetRequiredService<LoginViewModel>()
            }
);
            builder.Services.AddTransient<LoginViewModel>();


            builder.Services.AddTransient<MainView>(serviceProvider => new MainView(serviceProvider.GetService<SecureStorageUserRepository>())
            {
                BindingContext = serviceProvider.GetRequiredService<MainViewModel>()
            }
            );
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<GarageService>();


            builder.Services.AddTransient<CarStateView>(serviceProvider => new CarStateView
                {
                    BindingContext = serviceProvider.GetRequiredService<CarStateViewModel>()
                }
            );



            builder.Services.AddTransient<CarStateViewModel>();


            


            builder.Services.AddSingleton<SecureStorageUserRepository>();

            builder.Services.AddSingleton<ISecureStorage>(SecureStorage.Default);
            return builder.Build();
        }

        private static void AddView<TView, TViewModel>(this MauiAppBuilder builder)
            where TView : ContentPage, new()
            where TViewModel : class
        {
            builder.Services.AddTransient<TView>(
                serviceProvider => new TView()
                {
                    BindingContext = serviceProvider.GetService<TViewModel>()
                }
            );
        }
    }
}

public interface IAlertService
{
    // ----- async calls (use with "await" - MUST BE ON DISPATCHER THREAD) -----
    Task ShowAlertAsync(string title, string message, string cancel = "OK");
    Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No");

    // ----- "Fire and forget" calls -----
    void ShowAlert(string title, string message, string cancel = "OK");
    /// <param name="callback">Action to perform afterwards.</param>
    void ShowConfirmation(string title, string message, Action<bool> callback,
                          string accept = "Yes", string cancel = "No");
}

internal class AlertService : IAlertService
{
    // ----- async calls (use with "await" - MUST BE ON DISPATCHER THREAD) -----

    public Task ShowAlertAsync(string title, string message, string cancel = "OK")
    {
        return Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }

    public Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No")
    {
        return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }


    // ----- "Fire and forget" calls -----

    /// <summary>
    /// "Fire and forget". Method returns BEFORE showing alert.
    /// </summary>
    public void ShowAlert(string title, string message, string cancel = "OK")
    {
        Application.Current.MainPage.Dispatcher.Dispatch(async () =>
            await ShowAlertAsync(title, message, cancel)
        );
    }

    /// <summary>
    /// "Fire and forget". Method returns BEFORE showing alert.
    /// </summary>
    /// <param name="callback">Action to perform afterwards.</param>
    public void ShowConfirmation(string title, string message, Action<bool> callback,
                                 string accept = "Yes", string cancel = "No")
    {
        Application.Current.MainPage.Dispatcher.Dispatch(async () =>
        {
            bool answer = await ShowConfirmationAsync(title, message, accept, cancel);
            callback(answer);
        });
    }
}