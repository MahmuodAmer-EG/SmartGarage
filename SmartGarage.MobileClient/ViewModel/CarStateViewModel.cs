using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QRCoder;
using SmartGarage.MobileClient.Services;
using SmartGarage.MobileClient.Models;

namespace SmartGarage.MobileClient.ViewModel
{
    public partial class CarStateViewModel : ObservableObject
    {
        private readonly GarageService garageService;
        private readonly IAlertService alertService;

        public CarStateViewModel(GarageService garageService, IAlertService alertService)
        {
            
            this.garageService = garageService;
            this.alertService = alertService;
        }

        [ObservableProperty]
        private bool _isDoorOpen;

        [ObservableProperty]
        private ImageSource _qrCodeImage ;

        [ObservableProperty]
        private bool _ShowStartStoringButton;

        [RelayCommand]
        public async Task OpenGarageDoor()
        {
            //TODO: hit the api and get the token
            var reserveResponse = await garageService.RequestToStoreCarAsync();
            
            
            if(reserveResponse != null && reserveResponse.Result == true) 
            {
                ChangeQRCodeImage(reserveResponse.QrCode);
                IsDoorOpen = true;
            }
            else
            {
                ChangeQRCodeImage("Testing Qr Code for Smart Garage System");

                System.Threading.Thread.Sleep(9000);

                IsDoorOpen = !IsDoorOpen;
            }

                //alertService.ShowAlert("Alert", reserveResponse.Message, "OK");
            
        }

        [RelayCommand]
        public async Task StartStoring()
        {

        }

        private void ChangeQRCodeImage(string qrcodeContent)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrcodeContent, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);
            QrCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));

        }
    }
}
