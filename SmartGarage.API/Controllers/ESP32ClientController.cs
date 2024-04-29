using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartGarage.API.Models;

namespace SmartGarage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ESP32ClientController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public ESP32ClientController(UserManager<ApplicationUser> userManager,
                                ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }
        /// <summary>
        /// The User will come and scan qr-code in 2 cases :
        /// 1 - Need to open the garage and store car
        /// 2 - If he come to take the car back
        /// 
        /// 
        /// This For The Embedded Client => This only give the embedded system guide that what to make but not update any value 
        /// </summary>
        /// <param name="QrCodeContent">Qr code that the ESP32 sends in the request</param>
        /// <returns></returns>
        [HttpGet("/ReadQrCode")]
        public async Task<IActionResult> ReadQrCode(string QrCodeContent)
        {
            var response = CreateQrcodeResponse(QrCodeContent);

            if (response.Result == false)
                return BadRequest(response);

            //get the floor from the CarPlaces
            var userId = HttpContext.User.FindFirst("uid").Value;
            var carPlace = context.CarPlaces.FirstOrDefault(cp => cp.UserId == userId);


            if (carPlace == null)
                return BadRequest(response);
            else
            {
                response.Floor = carPlace.Floor;
                return Ok(response);
            }



            return Ok(response);
        }

        private ReadQrCodeResponse CreateQrcodeResponse(string QrCodeContent)
        {
            var qrCode = context.User_QrCode.FirstOrDefault(q => q.QrCodeContent == QrCodeContent);

            if (qrCode == null)
                return new ReadQrCodeResponse { Message = "This Qr code not in the database.", Result = false };

            //check if still valid
            var now = DateTime.Now;

            var endDate = qrCode.EndDate.ToLocalTime();
            ReadQrCodeResponse message;
            if (now < endDate)
                message = new ReadQrCodeResponse { Result = false, Message = "Qr Code timeout, pls regenarate another one" };
            else
                message = new ReadQrCodeResponse
                {
                    Result = true,
                    Request = qrCode.RequestType == 0 ? "OpenGarageDoor" : "RetrieveCar",
                    Message = "",
                    Floor = -1,
                    //TODO: get charge from any point
                    IsCharge = true
                };

            return message;
        }

    }
}
