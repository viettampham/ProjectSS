using System;
using Microsoft.AspNetCore.Mvc;
using ProjectSS.Models.RequestModels;
using ProjectSS.Services;

namespace ProjectSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController:ControllerBase
    {
        public readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpPost("create-bill")]
        public IActionResult CreateBill(CreateBillRequest request)
        {
            var newBill = _billService.CreateBill(request);
            return Ok(newBill);
        }

        [HttpGet("get-list-bill")]
        public IActionResult GetBill()
        {
            var bills = _billService.GetListBill();
            return Ok(bills);
        }
        
        [HttpDelete("delete-bill")]
        public IActionResult DeleteBill(Guid id)
        {
            var targetBill = _billService.DeleteBill(id);
            return Ok(targetBill);
        }
    }
}