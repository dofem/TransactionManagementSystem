using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessPurchase.Application.Contract;
using ProcessPurchase.Application.Implementation;
using ProcessPurchase.Application.Request;

namespace ProcessPurchase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("OnBoardACustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Failure
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Exception
        public async Task<IActionResult> OnBoardCustomer([FromBody] CreateUser createUser)
        {
            var response = await _userService.AddNewCustomer(createUser);
            if (response != null)
            {
                if (response.status)
                {
                    return StatusCode(201, response.message);
                }
                else
                {
                    return BadRequest(response.message);
                }
            }
            else
            {
                return StatusCode(500, "Customer Onboarding attempt failed");
            }
        }


        [HttpPost]
        [Route("TopUpWalletAccount")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Failure
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Exception
        public async Task<IActionResult> OnBoardCustomer([FromBody] TopUp topUp)
        {
            var response = await _userService.TopUpWallet(topUp);
            if(response != null) 
            { 
                if(response.status)
                {
                    return StatusCode(201, response.message);
                }
                else
                {
                    return BadRequest(response.message);
                }
            }
            else
            {
                return StatusCode(500, "Customer Wallet TopUp Failed");
            }
        }

    }
}
