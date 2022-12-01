using Microsoft.AspNetCore.Mvc;
using ProjectSS.Models.RequestModels;
using ProjectSS.Services;

namespace ProjectSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController:ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("get-list-cart")]
        public IActionResult GetListCart()
        {
            var listCart = _cartService.GetList();
            return Ok(listCart);
        }

        [HttpPost("create-cart")]
        public IActionResult CreateCart(CreateCartRequest request)
        {
            var newCart = _cartService.CreateCart(request);
            return Ok(newCart);
        }
        
        [HttpPost("edit-cart")]
        public IActionResult EditCart(EditCartRequest request)
        {
            var targetCart = _cartService.EditCart(request);
            return Ok(targetCart);
        }
        
        [HttpDelete("delete-cart")]
        public IActionResult DeleteCart(DeleteCartRequest request)
        {
            var targetCart = _cartService.DeleteCart(request);
            return Ok(targetCart);
        }
        
    }
}