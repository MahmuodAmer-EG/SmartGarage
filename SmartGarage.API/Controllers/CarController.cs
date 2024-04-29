//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using SmartGarage.API.Models;
//using SmartGarage.Models;

//namespace SmartGarage.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class CarController : Controller
//    {
//        private readonly UserManager<ApplicationUser> userManager;
//        private readonly ApplicationDbContext context;

//        public CarController(UserManager<ApplicationUser> userManager,
//                                ApplicationDbContext context)
//        {



//            this.userManager = userManager;
//            this.context = context;
//        }
//        /// <summary>
//        /// Reserve the place for some minutes {{duration}}=> waiting the user to come=>unit = minute
//        /// 
//        /// 
//        /// This For the Mobile Application Client
//        /// </summary>
//        /// <returns></returns>

//        [HttpGet("ReservePlaceForCar")]
//        public async Task<IActionResult> ReservePlaceForCar(int duration)
//        {
//            /*
//             1 - Check if there units in the account.
//             2 - Check If there Places free.
//             3 - If the 2 coditions is true :
//                    -Reserve Nearst Place
//                    -             
//             */

//            // Get the user's ID from the current claims
//            var userId = HttpContext.User.FindFirst("uid").Value;


//            // Load the user from the database using UserManager
//            var user = await userManager.FindByIdAsync(userId);

//            //Check if save or request to store car before 
//            if (user.CarState != CarState.None)
//                return Ok(new CarReserveResponse
//                {
//                    Message = "There is already Action in progress for this user",
//                    Result = false,
//                    Floor = -1
//                }
//                );

//            if (user == null)
//                return NotFound();

//            var currentUnits = user.Units;

//            var neededUnits = duration;

//            var checkUnits = currentUnits >= neededUnits;
//            //TODO: check free place implementation
//            var firstFreePlace = context.CarPlaces.FirstOrDefault(c => c.Available == true);
//            bool checkFreePlace = false;

//            if (firstFreePlace == null)
//            {
//                var reserveResponse = new CarReserveResponse
//                {
//                    Message = "There is No Free Places in This time",
//                    Result = false,
//                    Floor = -1
//                };

//                return Ok(reserveResponse);
//            }
//            else
//            {
//                checkFreePlace = true;

//                if (checkUnits && checkFreePlace)
//                {
//                    firstFreePlace.UserId = userId;
//                    firstFreePlace.Available = false;


//                    user.Units -= duration;


//                    context.UnitTransactions.Add(new UnitTransaction { Id = Guid.NewGuid(), Quantity = -duration, UserId = user.Id });

//                    //change the car state in the user
//                    context.Users.First(u => u.Id == userId).CarState = CarState.WaitItToStore;


//                    await context.SaveChangesAsync();

//                    var reserveResponse = new CarReserveResponse
//                    {
//                        Message = $"We Reserved a place for your car for {duration} minutes",
//                        Result = true,
//                        Floor = firstFreePlace.Floor
//                    };

//                    return Ok(reserveResponse);
//                }
//                else
//                {
//                    var reserveResponse = new CarReserveResponse
//                    {
//                        Message = "There is No Units,pls Renew The Quote to complete in the process",
//                        Result = false,
//                        Floor = -1
//                    };
//                    return Ok(reserveResponse);
//                }
//            }



//        }

//    }
//}