using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface.PurchaseOrder;
using Inventory.Application.Interface.SalesOrder;
using Inventory.Application.ViewModel.PurchaseOrder;
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
    public class PurchaseOrderController : ControllerBase
    {
        private readonly SessionHanlderController _SessionHanlderController;
        private readonly IPurchaseOrder _IPurchaseOrder;
        private readonly IReceiveNotes _IReceiveNotes;
        public Boolean Status = false;
        public string Message = "";
        public object Data = null;
        public string UserId = "";
        public long TenantId = 0;
        public PurchaseOrderController(SessionHanlderController SessionHanlderController,
            IPurchaseOrder IPurchaseOrder, IReceiveNotes IReceiveNotes)
        {
            _SessionHanlderController = SessionHanlderController;
            _IPurchaseOrder = IPurchaseOrder;
            _IReceiveNotes = IReceiveNotes;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        [HttpPost]
        public async Task<IActionResult> SavePurchaseOrder(PurchaseOrderMergeVM input)
        {
            if (input != null)
            {
                TenantId = _SessionHanlderController.GetTenantId(HttpContext);
                UserId = _SessionHanlderController.GetUserId(HttpContext);
                var Sid = await _IPurchaseOrder.SavePurchaseOrder(input, UserId, TenantId);
                if (Sid != 0)
                {
                    Status = true;
                    Message = "Purchase Order Successfully Saved..";
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
        public async Task<IActionResult> GetPurchaseOrderList()
        {
            TenantId = _SessionHanlderController.GetTenantId(HttpContext);
            Data = await _IPurchaseOrder.PurchaseOrderList(TenantId);
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePurchaseOrder(long PurchaseOrderId)
        {
            if (PurchaseOrderId != 0)
            {
                await _IPurchaseOrder.DeletePurchaseOrder(PurchaseOrderId);
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(true, "Purchase Order Delete Successfull..", null));
        }
        [HttpGet]
        public async Task<IActionResult> GetPurchaseOrderDetails(long PurchaseOrderId)
        {
            if (PurchaseOrderId != 0)
            {
                Data = await _IPurchaseOrder.GetPurchaseOrderDetails(PurchaseOrderId);
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
        [HttpGet]
        public async Task<IActionResult> GetPurchaseOrderDetailsIdBySupplier(long SupplierId)
        {
            if (SupplierId != 0)
            {
                Data = await _IPurchaseOrder.GetPucrahseOrderListIdBySupplier(SupplierId);
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
        [HttpGet]
        public async Task<IActionResult> GetProductIdByPurchaseOrder(long PurchaseOrderId)
        {
            if (PurchaseOrderId != 0)
            {
                Data = await _IPurchaseOrder.GetProductListIdByPurchasOrder(PurchaseOrderId);
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
        [HttpPost]
        public async Task<IActionResult> SaveReceiveNotes(ReceiveNotesVM input)
        {
            if (input != null)
            {
                TenantId = _SessionHanlderController.GetTenantId(HttpContext);
                UserId = _SessionHanlderController.GetUserId(HttpContext);
                var Sid = await _IReceiveNotes.SaveReceiveNotes(input, UserId, TenantId);
                if (Sid != 0)
                {
                    Status = true;
                    Message = "Receive Notes Successfully Saved..";
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
        public async Task<IActionResult> GetReceiveNotesList()
        {
            TenantId = _SessionHanlderController.GetTenantId(HttpContext);
            Data = await _IReceiveNotes.ReceiveNotesList(TenantId);
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReceiveNotes(long ReceiveNotesId)
        {
            if (ReceiveNotesId != 0)
            {
                await _IReceiveNotes.DeleteReceiveNotes(ReceiveNotesId);
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(true, "Receive Notes Delete Successfull..", null));
        }
        [HttpGet]
        public async Task<IActionResult> GetReceiveNotesDetails(long ReceiveNotesId)
        {
            if (ReceiveNotesId != 0)
            {
                Data = await _IReceiveNotes.GetReceiveNotesDetails(ReceiveNotesId);
            }
            else
                return BadRequest();
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }
    }
}