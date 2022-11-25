using Microsoft.AspNetCore.Mvc;
using ProjectSS.Models.RequestModels;
using ProjectSS.Services;

namespace ProjectSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("Create-Product")]
        public IActionResult CreateProduct([FromBody]CreateProductRequest request)
        {
            var newProduct = _productService.CreateProduct(request);
            return Ok(newProduct);
        }

        [HttpPost("Edit-product")]
        public IActionResult EditProduct(EditProductRequest request)
        {
            var targetProduct = _productService.EditProduct(request);
            return Ok(targetProduct);
        }

        [HttpDelete("Delete-product/{id}")]
        public IActionResult DeleteProduct(DeleteProductRequest request)
        {
            var targetProduct = _productService.DeleteProduct(request);
            return Ok(targetProduct);
        }

        [HttpGet("Get-list-product")]
        public IActionResult GetList()
        {
            var listProduct = _productService.GetListProduct();
            return Ok(listProduct);
        }

    }
}