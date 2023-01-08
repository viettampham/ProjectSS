using System;
using System.Collections.Generic;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface IOrderDetailService
    {
        List<OrderDetail> Getlist();
        CreateOrderResponse CreateOrder(CreateOrderRequest request);
        EditOrderResponse EditOrder(EditOrderRequest request);
        OrderDetailResponse DeleteOrder(Guid id);

    }
}