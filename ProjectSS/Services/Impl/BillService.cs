using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services.Impl
{
    public class BillService:IBillService
    {

        public readonly MasterDbContext _context;

        public BillService(MasterDbContext context)
        {
            _context = context;
        }

        public List<BillResponse> GetListBill()
        {
            var bills = _context.Bills.Select(b => new Bill()
            {
                id = b.id,
                Cart = b.Cart,
                User = b.User,
                NameCustomerOrder = b.NameCustomerOrder,
                PhoneNumberCustomer = b.PhoneNumberCustomer,
                Address = b.Address,
                status = b.status
            }).ToList();
            var listBillResponse = new List<BillResponse>();
            foreach (var bill in bills)
            {
                var listOrderResponse = new List<ListOrderResponse>();
                foreach (var order in bill.Cart.OrderDetails)
                {
                    var orderResponse = new ListOrderResponse()
                    {
                        id = order.id,
                        ProductOrder = new ProductOrder()
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
                    };
                    listOrderResponse.Add(orderResponse);
                }

                var billResponse = new BillResponse()
                {
                    id = bill.id,
                    Cart = new CartResponse()
                    {
                        cartId = bill.Cart.id,
                        UserID = bill.Cart.UserId,
                        OrderDetails = listOrderResponse,
                        TotalCart = bill.Cart.TotalCart,
                    },
                    User = bill.User,
                    NameCustomerOrder = bill.NameCustomerOrder,
                    PhoneNumberCustomer = bill.PhoneNumberCustomer,
                    Address = bill.Address,
                    status = bill.status
                };
                listBillResponse.Add(billResponse);
            }

            return listBillResponse;
        }


        public BillResponse CreateBill(CreateBillRequest request)
        {

            var cart = _context.Carts.FirstOrDefault(c => c.id == request.CartID);
            var user = _context.Users.FirstOrDefault(u => u.Id == request.UserID);
            var newBill = new Bill()
            {
                id = Guid.NewGuid(),
                Cart = cart,
                User = user,
                NameCustomerOrder = request.NameCustomer,
                PhoneNumberCustomer = request.PhoneNumberCustomer,
                Address = request.Address,
                status = "xxx"
            };
            _context.Bills.Add(newBill);
            _context.SaveChanges();

            var ListOrder = new List<ListOrderResponse>();
            foreach (var order in newBill.Cart.OrderDetails)
            {
                var orderResponse = new ListOrderResponse()
                {
                    id = order.id,
                    ProductOrder = new ProductOrder()
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
                };
                ListOrder.Add(orderResponse);
            }
            
            var cartResponse = new CartResponse()
            {
                cartId = newBill.Cart.id,
                UserID = newBill.Cart.UserId,
                OrderDetails = ListOrder,
                TotalCart = newBill.Cart.TotalCart
            };

            return new BillResponse()
            {
                id = newBill.id,
                Cart = cartResponse,
                User = user,
                NameCustomerOrder = request.NameCustomer,
                PhoneNumberCustomer = request.PhoneNumberCustomer,
                Address = request.Address,
                status = "xxx",
            };
        }

        public Bill DeleteBill(Guid id)
        {
            var targetBill = _context.Bills.FirstOrDefault(b => b.id == id);
            _context.Remove(targetBill);
            _context.SaveChanges();
            return targetBill;
        }
    }
}