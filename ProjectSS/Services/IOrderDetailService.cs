using System.Collections.Generic;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface IOrderDetailService
    {
        List<ListOrderResponse> Getlist();
        CreateOrderResponse CreateOrder(CreateOrderRequest request);
        EditOrderResponse EditOrder(EditOrderRequest request);
        OrderDetailResponse DeleteOrder(DeleteOrderRequest request);

    }
}