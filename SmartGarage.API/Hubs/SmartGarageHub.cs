
using Microsoft.AspNetCore.SignalR;
using SmartGarage.API.ConstantsConfiguration;

namespace SmartGarage.API.Hubs
{
    public class SmartGarageHub:Hub
    {
        public void OpenGarageDoor()
        {
            //call the callback function For IOT Part From Mobile client
            Clients.All.SendAsync(StoreCarEvents.OnOpenDoorClicked);
        }

        public void StartStoringCar()
        {
            //call the callback function For IOT Part From Mobile client
            Clients.All.SendAsync(StoreCarEvents.OnStartStoringCarClicked);
        }
    }
}
