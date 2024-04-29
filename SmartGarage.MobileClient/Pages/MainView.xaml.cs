using SmartGarage.MobileClient.Repositories;

namespace SmartGarage.MobileClient.Pages;

public partial class MainView : ContentPage
{
    private readonly SecureStorageUserRepository secureStorageUser;
    

    public MainView(SecureStorageUserRepository secureStorageUser)
	{
		InitializeComponent();
        this.secureStorageUser = secureStorageUser;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        circularProgressBar.Progress = 50;
    }
}