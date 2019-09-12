using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface.Tenants;
using Inventory.Application.ViewModel.ApplicationUser;
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
        public async Task<IActionResult> GetRegisterData(long TenantId,string UserId)
        {
            if (TenantId != 0)
            {
                TenantsDetailsVm vm = new TenantsDetailsVm();
               Status =await _tenants.ChechTenants(TenantId);
                if (Status)
                {
                    Status =await _tenants.CheckUserId(UserId);
                    if (Status)
                    {
                        var data = _tenants.GetRegisterDataAsync(TenantId, UserId);
                        if (data.Result != null)
                        {
                            Data = data.Result;
                            UserVm model = new UserVm()
                            {
                                EmailId = data.Result.EmailId,
                                TenantId = data.Result.TenantId,
                            };
                            Status = await _tenants.RegisterTenantActived(model);
                            Message = "Your Account Succesfully Activated...!";
                            Status = true;
                        }
                        else
                        {
                            Message = "Could Not Find Any Company..";
                            Status = false;
                        }
                    }
                    else {
                        Message = "UserId could  Not Found..";
                    }
                }
                else {
                    Message = "Company Already Activated... Please Login..";
                }
                
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, Data));
        }
    }
}