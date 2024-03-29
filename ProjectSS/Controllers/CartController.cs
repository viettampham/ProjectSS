﻿using System;
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
        public IActionResult CreateCart(Guid id)
        {
            var newCart = _cartService.CreateCart(id);
            return Ok(newCart);
        }
        
        [HttpPost("edit-cart")]
        public IActionResult EditCart(EditCartRequest request)
        {
            var targetCart = _cartService.EditCart(request);
            return Ok(targetCart);
        }
        
        [HttpDelete("delete-cart/{id}")]
        public IActionResult DeleteCart(Guid id)
        {
            var targetCart = _cartService.DeleteCart(id);
            return Ok(targetCart);
        }

        [HttpGet("get-cart-by-id/{id}")]
        public IActionResult GetUserCart(Guid id)
        {
            var targetCart = _cartService.GetCartByUser(id);
            return Ok(targetCart);
        }
    }
}