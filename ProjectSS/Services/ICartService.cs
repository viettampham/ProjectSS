using System.Collections.Generic;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface ICartService
    {
        CartResponse CreateCart(CreateCartRequest request);
        CartResponse EditCart(EditCartRequest request);
        CartResponse DeleteCart(DeleteCartRequest request);
        List<CartResponse> GetList();
    }
}