using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services.Impl
{
    public class CartService:ICartService
    {
        private readonly MasterDbContext _context;

        public CartService(MasterDbContext context)
        {
            _context = context;
        }
        
        public CartResponse CreateCart(CreateCartRequest request)
        {
            var orderResponse = new List<ListOrderResponse>();
            var orders = new List<OrderDetail>();
            foreach (var id in request.OrderDetailsId)
            {
                var targetOrder = _context.OrderDetails.FirstOrDefault(o => o.id == id);
                if (targetOrder == null)
                {
                    throw new Exception("this order not exist");
                }

                var order = new OrderDetail();
                var orderTempo = new ListOrderResponse
                {
                    id = targetOrder.id,
                    ProductOrder = new ProductOrder
                    {
                        title = targetOrder.Product.title,
                        description = targetOrder.Product.description,
                        image_url = targetOrder.Product.image_url,
                        price = targetOrder.Product.price,
                        size = targetOrder.Product.size,
                        Brand = targetOrder.Product.Brand
                    },
                    Quantity = targetOrder.Quantity,
                    TotalMoney = targetOrder.Product.price * targetOrder.Quantity
                };
                orders.Add(order);
                orderResponse.Add(orderTempo);
            }
            var newCart = new Cart
            {
                id = Guid.NewGuid(),
                OrderDetails = orders
            };
            _context.Add(newCart);
            return new CartResponse
            {
                cartId = newCart.id,
                OrderDetails = orderResponse
            };
        }

        public CartResponse EditCart(EditCartRequest request)
        {
            var targetCart = _context.Carts
                .Include(cart=> cart.OrderDetails)
                .FirstOrDefault(c => c.id == request.cartId);
            if (targetCart == null)
            {
                throw new Exception("this cart not exist");
            }
            targetCart.OrderDetails.Clear();
            var Order = new List<OrderDetail>();
            var Orders = new List<ListOrderResponse>();
            foreach (var orderId in request.OrderDetailsId)
            {
                var OrderTempo = _context.OrderDetails
                    .Include(c=>c.Product)
                    .FirstOrDefault(o => o.id == orderId);
                if (OrderTempo == null)
                {
                    throw new Exception("this order not exist");
                }

                var orderTg = new ListOrderResponse
                {
                    id = OrderTempo.id,
                    ProductOrder = new ProductOrder
                    {
                        title = OrderTempo.Product.title,
                        description = OrderTempo.Product.description,
                        image_url = OrderTempo.Product.image_url,
                        price = OrderTempo.Product.price,
                        size = OrderTempo.Product.size,
                        Brand = OrderTempo.Product.Brand
                    },
                    Quantity = OrderTempo.Quantity,
                    TotalMoney = OrderTempo.Product.price * OrderTempo.Quantity
                };
                Order.Add(OrderTempo);
                Orders.Add(orderTg);
            }

            _context.SaveChanges();

            return new CartResponse
            {
                cartId = targetCart.id,
                OrderDetails = Orders
            };
        }

        public CartResponse DeleteCart(DeleteCartRequest request)
        {
            var targetCart = _context.Carts.FirstOrDefault(c => c.id == request.CartId);
            if (targetCart == null)
            {
                throw new Exception("this cart not exist");
            }

            var listOrdertmp = new List<ListOrderResponse>();
            foreach (var orderDb in targetCart.OrderDetails)
            {
                var order = new ListOrderResponse
                {
                    id = orderDb.id,
                    ProductOrder = new ProductOrder
                    {
                        title = orderDb.Product.title,
                        description = orderDb.Product.description,
                        image_url = orderDb.Product.image_url,
                        price = orderDb.Product.price,
                        size = orderDb.Product.size,
                        Brand = orderDb.Product.Brand
                    },
                    Quantity = orderDb.Quantity,
                    TotalMoney = orderDb.Product.price * orderDb.Quantity,
                };
                listOrdertmp.Add(order);
            }

            _context.Remove(targetCart);
            _context.SaveChanges();
            
            return new CartResponse
            {
                cartId = targetCart.id,
                OrderDetails = listOrdertmp
            };
        }

        public List<CartResponse> GetList()
        {
            var listCart = _context.Carts.Select(cart => new Cart
            {
                id = cart.id,
                OrderDetails = cart.OrderDetails
            }).ToList();
            var listCartResponse = new List<CartResponse>();
            var lisOrderResponse = new List<ListOrderResponse>();
            foreach (var cart in listCart)
            {
                foreach (var orderTmp in cart.OrderDetails)
                {
                    var orderDetail = new ListOrderResponse
                    {
                        id = orderTmp.id,
                        ProductOrder = new ProductOrder
                        {
                            title = orderTmp.Product.title,
                            description = orderTmp.Product.description,
                            image_url = orderTmp.Product.image_url,
                            price = orderTmp.Product.price,
                            size = orderTmp.Product.size,
                            Brand = orderTmp.Product.Brand
                        },
                        Quantity = orderTmp.Quantity,
                        TotalMoney = orderTmp.Product.price * orderTmp.Quantity
                    };
                    lisOrderResponse.Add(orderDetail);
                }

                var cartResponse = new CartResponse
                {
                    cartId = cart.id,
                    OrderDetails = lisOrderResponse
                };
                listCartResponse.Add(cartResponse);
            }
            return listCartResponse;
        }
    }
}