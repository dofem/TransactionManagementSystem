using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessPurchase.Application.Contract;

namespace ProcessPurchase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) 
        { 
            _categoryService = categoryService;
        }


        
        [HttpPost]
        [Route("AddAProductCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Failure
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Exception
        public async Task<IActionResult> AddANewProductCategory(int id, string categoryName)
        {
            var response = await _categoryService.CreateACategory(id, categoryName);
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
                return StatusCode(500, "Category Addition Failed");
            }
        }
    }
}
