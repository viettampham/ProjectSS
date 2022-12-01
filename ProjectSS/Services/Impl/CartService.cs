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
            /*var orderResponse = new List<ListOrderResponse>();
            var orders = new List<OrderDetail>();
            foreach (var id in request.OrderDetailsId)
            {
                var targetOrder = _context.OrderDetails
                    .Include(o=>o.Product)
                    .FirstOrDefault(o => o.id == id);
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
            _context.Carts.Add(newCart);
            _context.SaveChanges();
            return new CartResponse
            {
                cartId = newCart.id,
                OrderDetails = orderResponse
            };*/

            
            
            var orderDetails = new List<OrderDetail>();
            foreach (var id in request.OrderDetailsId)
            {
                var order = _context.OrderDetails
                    .Include(o=>o.Product)
                    .FirstOrDefault(o => o.id == id);
                if (order == null)
                {
                    throw new Exception("this order not exist");
                }
                orderDetails.Add(order);
            }

            var orderResponses = new List<ListOrderResponse>();
            foreach (var order in orderDetails)
            {
                var orderResponse = new ListOrderResponse
                {
                    id = order.id,
                    ProductOrder = new ProductOrder
                    {
                        title = order.Product.title,
                        description = order.Product.description,
                        image_url = order.Product.image_url,
                        price = order.Product.price,
                        size = order.Product.size,
                        Brand = order.Product.Brand
                    },
                    Quantity = order.Quantity,
                    TotalMoney = order.Quantity * order.Product.price
                };
                orderResponses.Add(orderResponse);
            }
            var newCart = new Cart
            {
                id = Guid.NewGuid(), 
                OrderDetails = orderDetails
            };
            _context.Carts.Add(newCart);
            _context.SaveChanges();
            return new CartResponse
            {
                cartId = newCart.id,
                OrderDetails = orderResponses
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
            var listCart = _context.Carts
                .Include(c=>c.OrderDetails)
                .Select(c => c).ToList();
            
            var cartResponses = new List<CartResponse>();
            foreach (var cart in listCart)
            {
                
                var orderResponses = new List<ListOrderResponse>();
                foreach (var order in cart.OrderDetails)
                {
                    var x = _context.OrderDetails
                        .Include(o=>o.Product)
                        .FirstOrDefault(o => o.id == order.id);
                    if (x == null)
                    {
                        throw new Exception("this order nor exist");
                    }
                    var orderResponse = new ListOrderResponse
                    {
                        id = x.id,
                        ProductOrder = new ProductOrder
                        {
                            title = x.Product.title,
                            description = x.Product.description,
                            image_url = x.Product.image_url,
                            price = x.Product.price,
                            size = x.Product.size,
                            Brand = x.Product.Brand
                        },
                        Quantity = x.Quantity,
                        TotalMoney = x.Quantity * x.Product.price
                    };
                    orderResponses.Add(orderResponse);
                    var cartResponse = new CartResponse
                    {
                        cartId = cart.id,
                        OrderDetails = orderResponses
                    };
                    cartResponses.Add(cartResponse);
                }
            }
            return cartResponses;
        }

    }
}