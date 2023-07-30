using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessPurchase.Application.Contract;
using ProcessPurchase.Application.Implementation;
using ProcessPurchase.Application.Request;

namespace ProcessPurchase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseAProduct _purchaseProduct;

        public PurchaseController(IPurchaseAProduct purchaseProduct)
        {
            _purchaseProduct = purchaseProduct;
        }

        [HttpPost]
        [Route("OrderForAProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Failure
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Exception
        public async Task<IActionResult>MakeAPurchase([FromBody] PurchaseProductRequest request)
        {
            var response = await _purchaseProduct.PurchaseAProduct(request);
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
                return StatusCode(500, "Order creation attempt failed");
            }
        }
    }
}
