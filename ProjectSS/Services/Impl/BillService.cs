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
            var listOrder = new List<ListOrderResponse>();
            var listBillResponse = new List<BillResponse>();
            
            return listBillResponse;
        }


        public BillResponse CreateBill(CreateBillRequest request)
        {
            /*var targetCart = _context.Carts
                .Include(c=>c.OrderDetails)
                .FirstOrDefault(c => c.id == request.CartID);
            if (targetCart == null)
            {
                throw new Exception("not found cart");
            }

            var orders = new List<OrderDetail>();
            foreach (var order in targetCart.OrderDetails)
            {
                orders.Add(order);
            }*/
            var newBill = new BillResponse();
            return newBill;
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