//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using SmartGarage.API.Models;

//namespace SmartGarage.API.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//[Authorize]
//public partial class GarageController : Controller
//{

//    private readonly UserManager<ApplicationUser> userManager;
//    private readonly ApplicationDbContext context;

//    public GarageController(UserManager<ApplicationUser> userManager,ApplicationDbContext context)
//    {
//        this.userManager = userManager;
//        this.context = context;
//    }

//    [HttpGet]
//    [Route("/RequestToStoreCar")]
//    public async Task<IActionResult> RequestToStoreCar()
//    {
//        var carState = await GetCurrentCarState(httpContext:HttpContext);
        
//        //This in Case of the User not found for any reason
//        if (carState == null)
//            return NotFound("Sorry this User not in the database ,pls Call Admin of the system for more details");

//        //Check if save or request to store car before 
//        if (carState != CarState.None)
//            return Ok(new CarReserveResponse
//            {
//                Message = "There is already Action in progress for this user",
//                Result = false,
//                Floor = -1
//            });

//        //Check for free place for the car
//        var response = await RequestPlaceForCar(httpContext: HttpContext);

//        return Ok(response);
//    }


//}