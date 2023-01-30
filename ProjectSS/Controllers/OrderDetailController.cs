using System;
using Microsoft.AspNetCore.Mvc;
using ProjectSS.Models.RequestModels;
using ProjectSS.Services;

namespace ProjectSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderDetailController:ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("get-list-order")]
        public IActionResult Getlist()
        {
            var listOrder = _orderDetailService.Getlist();
            return Ok(listOrder);
        }

        [HttpPost("create-order")]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            var newOrder = _orderDetailService.CreateOrder(request);
            return Ok(newOrder);
        }
        
        [HttpPost("edit-order")]
        public IActionResult EditOrder(EditOrderRequest request)
        {
            var targetOrer = _orderDetailService.EditOrder(request);
            return Ok(targetOrer);
        }

        [HttpDelete("delete-order/{id}")]
        public IActionResult DeleteOrder(Guid id)
        {
            var targetOrder = _orderDetailService.DeleteOrder(id);
            return Ok(targetOrder);
        }

        [HttpGet("get-order-by-userid/{id}")]
        public IActionResult GetOrderByUserID(Guid id)
        {
            var orderReponses = _orderDetailService.GetOrderByUserID(id);
            return Ok(orderReponses);
        }
    }
}