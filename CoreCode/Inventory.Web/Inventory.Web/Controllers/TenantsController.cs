using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface.Tenants;
using Inventory.Application.ViewModel.Tenants;
using Inventory.Web.share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TenantsController : ControllerBase
    {
        private readonly ITenants _tenants;
        public Boolean Status = false;
        public string Message = "";
        public object Data;
        public TenantsController(ITenants tenants)
        {
            _tenants = tenants;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SaveTenant(TenantsVm vm)
        {
            if (vm != null) {
                Status =await _tenants.SaveTenants(vm);
                if (Status) {
                    Message = "Register Succesfully completed, Please Check Your Emailid for Confirm your account..";
                }
                else
                {
                    Message = vm.EmailId+" Is Already Exist...!";
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetRegisterData(long TenantId)
        {
            Data = null;
            if (TenantId != 0)
            {
                Data = _tenants.GetRegisterDataAsync(TenantId);
                if (Data != null)
                {
                    Message = "";
                    Status = true;
                }
                else {
                    Message = "Could Not Find Any Company..";
                    Status = false;
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, Data));
        }
    }
}