using FluentValidation_Demo.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidation_Demo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(ProductDto product)
        {
            return Ok(new
            {
                Message = "Product created successfully.",
                Data = product
            });
        }

    }
}
