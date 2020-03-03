using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface.Supplier;
using Inventory.Application.ViewModel.SupplierVm;
using Inventory.Web.share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("localhost")]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplier _supplier;
        private readonly SessionHanlderController _SessionHanlderController;
        public Boolean Status = false;
        public string Message = "";
        public object Data = null;
        public string GetUserId = "";
        public long GetTenantId = 0;
        public SupplierController(ISupplier supplier, SessionHanlderController SessionHanlderController)
        {
            _supplier = supplier;
            _SessionHanlderController = SessionHanlderController;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateSuppliers(SupplierVm supplier)
        {
            if (supplier != null)
            {
                long Suppliersid = await _supplier.AddUpadteSupplier(supplier, _SessionHanlderController.GetUserId(HttpContext), _SessionHanlderController.GetTenantId(HttpContext));
                if (Suppliersid != 0)
                {
                    if (supplier.SupplierId != 0)
                    {
                        Message = "Supplier Successfully Updated..";
                    }
                    else
                    {
                        Message = "Supplier Successfully Addedd..";
                    }
                    Status = true;
                }
                else
                {
                    Status = false;
                    Message = "Supplier Not saved..";
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, Data));
        }
        [HttpGet]
        public async Task<IActionResult> GetsupplierList()
        {
            List<SupplierVm> supplierVmsList = new List<SupplierVm>();
            supplierVmsList = await _supplier.GetSupplierList();
            return Ok(GetAjaxResponse(true, "Suppliers List", supplierVmsList));
        }
        [HttpGet]
        public async Task<IActionResult> GetSupplier(long SupplierId)
        {
            if (SupplierId != 0)
            {
                var data = await _supplier.GetSupplierById(SupplierId);
                await Task.Run(() =>
                {
                    if (data != null)
                    {
                        Data = data;
                        Status = true;
                        Message = string.Empty;
                    }
                    else
                    {
                        Status = false;
                        Message = "Could Not Fond Any data..";
                    }
                });
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, Data));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSuppliers(long SuppliersId)
        {
            if (SuppliersId != 0)
            {
                Status = await _supplier.DeleteSupplier(SuppliersId);
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> GetDafaultSuppliers()
        {
            var data = await _supplier.GetDefaultSupplierList();
            return Ok(GetAjaxResponse(true, string.Empty, data));
        }
    }
}