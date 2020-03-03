using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface.SalesOrder;
using Inventory.Application.ViewModel.SalesOrder;
using Inventory.Web.share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SalesOrderController : ControllerBase
    {
        private readonly SessionHanlderController _SessionHanlderController;
        private readonly ISalesOrder _ISalesOrder;
        public Boolean Status = false;
        public string Message = "";
        public object Data = null;
        public string UserId = "";
        public long TenantId = 0;
        public SalesOrderController(SessionHanlderController SessionHanlderController, ISalesOrder ISalesOrder)
        {
            _SessionHanlderController = SessionHanlderController;
            _ISalesOrder = ISalesOrder;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        [HttpPost]
        public async Task<IActionResult> SaveSalesOrder(SalesOrderMergeVM input)
        {
            if (input != null)
            {
                TenantId = _SessionHanlderController.GetTenantId(HttpContext);
                UserId = _SessionHanlderController.GetUserId(HttpContext);
                var Sid = await _ISalesOrder.SaveSalesOrder(input, UserId, TenantId);
                if (Sid != 0)
                {
                    Status = true;
                    Message = "Sales Order Successfully Saved..";
                }
                else
                {
                    Status = false;
                    Message = "Error";
                }
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> GetSalesOrderList()
        {
            TenantId = _SessionHanlderController.GetTenantId(HttpContext);
            Data = await _ISalesOrder.SalesOrderList(TenantId);
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSalesOrder(long SalesOrderId)
        {
            if (SalesOrderId != 0)
            {
               await _ISalesOrder.DeleteSalesOrder(SalesOrderId);
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(true, "Sales Order Delete Successfull..", null));
        }
        [HttpGet]
        public async Task<IActionResult> GetSalesOrderDetails(long SalesOrderId)
        {
            if (SalesOrderId != 0)
            {
               Data= await _ISalesOrder.GetSalesOrderDetails(SalesOrderId);
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
    }
}