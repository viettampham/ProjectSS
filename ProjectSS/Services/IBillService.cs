using System;
using System.Collections.Generic;
using ProjectSS.Models;
using ProjectSS.Models.RequestModels;
using ProjectSS.Models.ViewModels;

namespace ProjectSS.Services
{
    public interface IBillService
    {
        List<BillResponse> GetListBill();
        BillResponse CreateBill(CreateBillRequest request);
        Bill DeleteBill(Guid id);
    }
}