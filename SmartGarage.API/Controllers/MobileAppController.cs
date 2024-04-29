using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartGarage.API.Models;
using SmartGarage.Models;
using SmartGarage.Models.MobileClient.Requests;
using SmartGarage.Models.MobileClient.Responses;

namespace SmartGarage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public partial class MobileAppController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext context;

        public MobileAppController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet("/TryToCreateQrCodeForStoreCar")]
        public async Task<IActionResult> TryToCreateQrCodeForStoreCar([FromQuery] StoreCarRequest storeCarRequest)
        {
            //TODO:try to insert the isCharge part

            try
            {
                var storeCarResponse = await RequestPlaceToStoreCar(HttpContext);
                return Ok(storeCarResponse);
            }
            catch (Exception)
            {
                return NotFound(new StoreCarResponse
                {
                    Result = false,
                    Message = "Error accuored while the proccess with working"
                });
            }
        }

        [HttpGet("/TryToCreateQrCodeForRetriveCar")]
        public async Task<IActionResult> TryToCreateQrCodeForRetriveCar()
        {
            //TODO: implement it 

            var carState = await GetCurrentCarStateAsync(HttpContext);
            
            //Check if the car inside the garage 
            if (carState != CarState.OutOfGarage)
            {

            }

            throw new NotImplementedException();
        }

        [HttpGet("/GetCurrentCarState")]
        public async Task<IActionResult> GetCurrentCarState()
        {
            var carState = GetCurrentCarStateAsync(HttpContext);
            return Ok(carState);
        }
    }
}
