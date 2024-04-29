using Microsoft.AspNetCore.Mvc;
using SmartGarage.Models;
using SmartGarage.Models.MobileClient.Responses;

namespace SmartGarage.API.Controllers
{
    public partial class MobileAppController : Controller
    {
        private async Task<CarState?> GetCurrentCarStateAsync(HttpContext httpContext)
        {
            // Get the user's ID from the current claims
            var userId = httpContext.User.FindFirst("uid").Value;


            // Load the user from the database using UserManager
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return user.CarState;
        }

        private async Task<StoreCarResponse> RequestPlaceToStoreCar(HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirst("uid").Value;

            var firstFreePlace = context.CarPlaces.FirstOrDefault(c => c.Available == true);

            if (firstFreePlace == null)
                return new StoreCarResponse
                {
                    Message = "There is No Free Places in This time",
                    Result = false,
                    QrCode = ""
                };
            else
            {
                firstFreePlace.UserId = userId;
                firstFreePlace.Available = false;

                //change the car state in the user
                context.Attach(firstFreePlace).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                var endDate = DateTimeOffset.Now.AddMinutes(10);
                var qrCode = Guid.NewGuid().ToString();

                context.User_QrCode.Add(new User_QrCode
                {
                    RequestType = RequestType.StoreCar,
                    QrCodeContent = qrCode,
                    EndDate = endDate,
                    UserId = userId
                });

                await context.SaveChangesAsync();

                return new StoreCarResponse { Message = "", Result = true, QrCode = qrCode };
            }
        }

        /// <summary>
        /// Try to create qr code and retrive the car 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private async Task<RetrieveCarResponse> TryToRetriveCar(HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirst("uid").Value;

            var userCarPlace = context.CarPlaces.FirstOrDefault(c => c.UserId == userId);

            if (userCarPlace == null)
            {
                var retriveCarResponse = new RetrieveCarResponse
                {
                    Message = "There is No Cars For You To Retrive Now!",
                    Result = false,
                    QrCode = ""
                };
                return retriveCarResponse;
            }
            else
            {
                var endDate = DateTimeOffset.Now.AddMinutes(10);
                var qrCode = Guid.NewGuid().ToString();

                context.User_QrCode.Add(new User_QrCode
                {
                    RequestType = RequestType.RetrieveCar,
                    QrCodeContent = qrCode,
                    EndDate = endDate,
                    UserId = userId
                });

                await context.SaveChangesAsync();

                var retriveCarResponse = new RetrieveCarResponse
                {
                    Message = "",
                    Result = true,
                    QrCode = qrCode
                };

                return retriveCarResponse;
            }
        }

    }
}