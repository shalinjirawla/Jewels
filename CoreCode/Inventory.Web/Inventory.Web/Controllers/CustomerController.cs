using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Inventory.Web.share;
using Inventory.Application.Interface;
using Inventory.Application.ViewModel.CustomersVm;
using System.Threading.Tasks;
using Inventory.Application.Interface.Customer;
using Inventory.Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Inventory.Application.Interface.ApplicationUser;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[EnableCors("localhost")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _icustomer;
        private readonly IcustomerType _icustomerType;
        private readonly IApplicationUser _applicationUser;
        public bool Status = false;
        public string Message = "";
        public object Data = null;
        public string GetUserId = "";
        public long GetTenantId = 0;
        public CustomerController (ICustomer icustomer, IcustomerType icustomerType,
              IApplicationUser applicationUser
            )
        {
            _icustomer = icustomer;
            _icustomerType = icustomerType;
            _applicationUser = applicationUser;
            GetUserId = _applicationUser.GetUserId();
            GetTenantId = _applicationUser.GetTenantId();
            if (GetUserId == null && GetTenantId == 0)
            {
                _applicationUser.Logout();
            }
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddCustomerVm Model)
        {
            string a = "hello";
            string UserId =  _applicationUser.GetUserId();
            long TenantId =  _applicationUser.GetTenantId();
            var CustomerID =await _icustomer.AddCustomer(Model,UserId,TenantId);
            return Ok(GetAjaxResponse(true, string.Empty, a));
        }
        [HttpGet]
        public IActionResult GetCustomerList()
        {
            var customerlist =  _icustomer.GetCustomerListAsyn();
            return Ok(GetAjaxResponse(true, string.Empty, customerlist));
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int Id)
        {
            var customer = _icustomer.DeleteCustomerAsyc(Id);
            return Ok(GetAjaxResponse(true, string.Empty, customer));
        }

        [HttpGet]
        public IActionResult GetCustomerById(int Id)
        {
            var customer = _icustomer.GetCustomerByIdAsyc(Id);
            return Ok(GetAjaxResponse(true, string.Empty, customer));
        }

        [HttpGet]
        public IActionResult GetCustomerType()
        {
            var CustomerType = _icustomerType.GetCustomerTypeList();
            return Ok(GetAjaxResponse(true, string.Empty, CustomerType));
        }

        [HttpGet]
        public IActionResult GetCustomerTypeById(int Id)
        {
            var CustomerType = _icustomerType.GetCustomerTypeById(Id);
            return Ok(GetAjaxResponse(true, string.Empty, CustomerType));
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerType(CustomerTypeVm model)
        {
            await Task.Run(() =>
            {
                Data = _icustomerType.AddCustomerTypeAsyc(model, GetUserId, GetTenantId);
            });
         
            return Ok(GetAjaxResponse(true, string.Empty, Data));
        }

        [HttpDelete]
        public IActionResult DeleteCustomerType(int Id)
        {
            var CustomerType = _icustomerType.DeleteCustomerTypeAsyc(Id);
            return Ok(GetAjaxResponse(true, string.Empty, CustomerType));
        }


    }
}