using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface.Supplier;
using Inventory.Application.ViewModel.SupplierVm;
using Inventory.Web.share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplier _supplier;
        private readonly SessionHanlderController _SessionHanlderController;
        public bool Status = false;
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
            if (supplier != null) {
                long Suppliersid = await _supplier.AddUpadteSupplier(supplier, _SessionHanlderController.GetUserId(HttpContext), _SessionHanlderController.GetTenantId(HttpContext));
                if (Suppliersid != 0) {
                    Status = true;
                    Message = "Supplier Successfully Addedd..";
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
            supplierVmsList = await _supplier.GetSupplierListAsyn();
            return Ok(GetAjaxResponse(true, "Suppliers List", supplierVmsList));
        }
    }
}