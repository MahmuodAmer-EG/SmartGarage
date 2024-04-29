using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGarage.MobileClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartGarage.MobileClient.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel(GarageRepository garageRepository)
        {
            this.garageRepository = garageRepository;
        }
        [ObservableProperty]
        private int _duration;

        [ObservableProperty]
        private int _usage;
        private readonly GarageRepository garageRepository;

        [RelayCommand]
        public async void ReservePlace()
        {
            
        }
        [RelayCommand]
        public async void StoreCarNow()
        {
        }
        [RelayCommand]
        public async void RenewNow()
        {
        }
    }
}
