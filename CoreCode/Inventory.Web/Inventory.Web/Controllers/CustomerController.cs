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
        public CustomerController (ICustomer icustomer, IcustomerType icustomerType,
              IApplicationUser applicationUser
            )
        {
            _icustomer = icustomer;
            _icustomerType = icustomerType;
            _applicationUser = applicationUser;
            if (_applicationUser.GetUserId() == null && _applicationUser.GetTenantId() == null)
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
            var CustomerID =await _icustomer.AddCustomer(Model);
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
        public IActionResult AddCustomerType(CustomerTypeVm model)
        {
            var CustomerType = _icustomerType.AddCustomerTypeAsyc(model);
            return Ok(GetAjaxResponse(true, string.Empty, CustomerType));
        }

        [HttpDelete]
        public IActionResult DeleteCustomerType(int Id)
        {
            var CustomerType = _icustomerType.DeleteCustomerTypeAsyc(Id);
            return Ok(GetAjaxResponse(true, string.Empty, CustomerType));
        }


    }
}