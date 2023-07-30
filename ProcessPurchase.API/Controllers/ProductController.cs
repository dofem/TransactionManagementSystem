using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessPurchase.Application.Contract;
using ProcessPurchase.Application.Implementation;
using ProcessPurchase.Application.Request;

namespace ProcessPurchase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("AddAProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Failure
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Exception
        public async Task<IActionResult> AddANewProduct([FromBody] AddProduct addProduct)
        {
            var response = await _productService.AddAProduct(addProduct);
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
                return StatusCode(500, "Product Addition Failed");
            }
        }



        [HttpPost]
        [Route("AddProductQuantity")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Failure
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Exception
        public async Task<IActionResult> AddNumberOfProductQuantity(int quantity, int id)
        {
            var response = await _productService.AddProductQuantity(quantity,id);
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
                return StatusCode(500, "Addition of Quantity Failed");
            }
        }

    }
}



    



