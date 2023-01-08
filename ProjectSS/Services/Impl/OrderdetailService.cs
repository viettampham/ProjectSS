using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services.Impl
{
    public class OrderdetailService:IOrderDetailService
    {
        private readonly MasterDbContext _context;

        public OrderdetailService(MasterDbContext context)
        {
            _context = context;
        }
        public List<OrderDetail> Getlist()
        {
            /*var listOrder = _context.OrderDetails
                .Select(order => new ListOrderResponse()
            {
                id = order.id,
                ProductOrder = new ProductOrder
                {
                    id = order.Product.id,
                    title = order.Product.title,
                    description = order.Product.description,
                    image_url = order.Product.image_url,
                    price = order.Product.price,
                    size = order.Product.size,
                    Brand = order.Product.Brand
                },
                Quantity = order.Quantity,
                TotalMoney = order.Quantity * order.Product.price
            }).ToList();
            for (int i = 0; i < listOrder.Count; i++)
            {
                for (int j = 1; j < listOrder.Count; j++)
                {
                    if (listOrder[i].ProductOrder.id == listOrder[j].ProductOrder.id)
                    {
                        listOrder[i].Quantity = listOrder[i].Quantity + listOrder[j].Quantity;
                        listOrder.Remove(listOrder[j]);
                        _context.SaveChanges();
                    }
                }
            }
            var orders = _context.OrderDetails
                .Select(order => new ListOrderResponse()
                {
                    id = order.id,
                    ProductOrder = new ProductOrder
                    {
                        id = order.Product.id,
                        title = order.Product.title,
                        description = order.Product.description,
                        image_url = order.Product.image_url,
                        price = order.Product.price,
                        size = order.Product.size,
                        Brand = order.Product.Brand
                    },
                    Quantity = order.Quantity,
                    TotalMoney = order.Quantity * order.Product.price
                }).ToList();
                */
            var orderDetails = _context.OrderDetails.Select(order => new OrderDetail
            {
                id = order.id,
                Product = order.Product,
                Quantity = order.Quantity,
                TotalMoney = order.Quantity * order.Product.price
            }).ToList();
            for (var i = 0; i < orderDetails.Count; i++)
            {
                for (int j = i+1 ; j < orderDetails.Count; j++)
                {
                    if (orderDetails[i].Product.id == orderDetails[j].Product.id)
                    {
                        var targetOrder =
                            _context.OrderDetails.FirstOrDefault(o => o.id == orderDetails[i].id);
                        if (targetOrder==null)
                        {
                            throw new Exception("not found");
                        }
                        targetOrder.Quantity = targetOrder.Quantity + orderDetails[j].Quantity;
                        _context.Remove(orderDetails[j]);
                        _context.SaveChanges();
                    }
                }
            }
            var listOrder = _context.OrderDetails.Select(order => new OrderDetail
            {
                id = order.id,
                Product = order.Product,
                Quantity = order.Quantity,
                TotalMoney = order.Quantity * order.Product.price
            }).ToList();
            return listOrder;
        }

        public CreateOrderResponse CreateOrder(CreateOrderRequest request)
        {
            var newOrder = new OrderDetail();
            var product = new Product();
            
            var p = _context.Products.FirstOrDefault(x => x.id == request.ProductId);
            if (p==null)
            {
                throw new Exception("this product not exist");
            }
            else
            {
                product = p;
            }

            var productOrder = new ProductOrder
            {
                id = product.id,
                title = product.title,
                description = product.description,
                image_url = product.image_url,
                price = product.price,
                size = product.size,
                Brand = product.Brand
            };
            newOrder.id = Guid.NewGuid();
            newOrder.Product = product;
            newOrder.Quantity = request.Quantity;
            

            _context.Add(newOrder);
            _context.SaveChanges();
            return new CreateOrderResponse()
            {
                id = Guid.NewGuid(),
                Product = productOrder,
                Quantity = request.Quantity,
                TotalMoneyOrder = request.Quantity * productOrder.price
            };
        }

        public EditOrderResponse EditOrder(EditOrderRequest request)
        {
            var targetOrder = _context.OrderDetails.FirstOrDefault(o => o.id == request.id);
            if (targetOrder == null)
            {
                throw new Exception("this order not exist");
            }
            var product = new Product();
            var p = _context.Products.FirstOrDefault(x => x.id == request.ProductId);
            if (p==null)
            {
                throw new Exception("this product not exist");
            }
            else
            {
                product = p;
            }
            
            targetOrder.Product = product;
            targetOrder.Quantity = request.Quantity;
            
            var productOrder = new ProductOrder
            {
                title = product.title,
                description = product.description,
                image_url = product.image_url,
                price = product.price,
                size = product.size,
                Brand = product.Brand
            };
            
            
            _context.SaveChanges();
            return new EditOrderResponse
            {
                id = targetOrder.id,
                ProductOrder = productOrder,
                Quantity = targetOrder.Quantity,
                TotalMoney = targetOrder.Quantity * productOrder.price
            };
        }

        public OrderDetailResponse DeleteOrder(Guid id)
        {
            var targetOrder = _context.OrderDetails.FirstOrDefault(o => o.id == id);
            if (targetOrder == null)
            {
                throw new Exception("this order not exist");
            }

            _context.Remove(targetOrder);
            _context.SaveChanges();
            return new OrderDetailResponse
            {
                id = targetOrder.id,
                Product = targetOrder.Product,
                Quantity = targetOrder.Quantity,
                
            };
        }
    }
}