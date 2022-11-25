using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<ListOrderResponse> Getlist()
        {
            var listOrder = _context.OrderDetails.Select(o => new OrderDetailResponse
            {
                id = o.id,
                Product = o.Product,
                Quantity = o.Quantity,
                
            }).ToList();

            var listOrderResponse = new List<ListOrderResponse>();
            foreach (var o in listOrder)
            {
                var newOrderTemporariry = new ListOrderResponse
                {
                    id = o.id,
                    ProductOrder = new ProductOrder
                    {
                        title = o.Product.title,
                        description = o.Product.description,
                        image_url = o.Product.image_url,
                        price = o.Product.price,
                        size = o.Product.size,
                        Brand = o.Product.Brand
                    },
                    Quantity = o.Quantity,
                    TotalMoney = o.Quantity * o.Product.price
                };
                listOrderResponse.Add(newOrderTemporariry);
            }

           
            
            return listOrderResponse;
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

        public OrderDetailResponse DeleteOrder(DeleteOrderRequest request)
        {
            var targetOrder = _context.OrderDetails.FirstOrDefault(o => o.id == request.Id);
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