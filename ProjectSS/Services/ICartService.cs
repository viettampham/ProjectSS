using System;
using System.Collections.Generic;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface ICartService
    {
        CartResponse CreateCart(Guid id);
        CartResponse EditCart(EditCartRequest request);
        Cart DeleteCart(Guid id);
        List<Cart> GetList();
        CartResponse GetCartByUser(Guid id);
    }
}