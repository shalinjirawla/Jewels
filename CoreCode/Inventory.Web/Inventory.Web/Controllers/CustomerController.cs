using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Inventory.Web.share;
using Inventory.Application.Interface;
using Inventory.Application.ViewModel;


namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[EnableCors("localhost")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _icustomer;

        public CustomerController (ICustomer icustomer)
        {
            _icustomer = icustomer;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        [HttpPost]
        public IActionResult AddCustomer(CustomerVm Model)
        {
            string a = "hello";
            var CustomerID = _icustomer.AddCustomer(Model);
            return Ok(GetAjaxResponse(true, string.Empty, a));
        }
        [HttpGet]
        public IActionResult GetCurrency()
        {
            var Currency = _icustomer.GetCurrencyList();
            return Ok(GetAjaxResponse(true, string.Empty, Currency));
        }
        [HttpGet]
        public IActionResult GetCustomerType()
        {
            var CustomerType = _icustomer.GetCustomerTypeList();
            return Ok(GetAjaxResponse(true, string.Empty, CustomerType));
        }
        [HttpGet]
        public IActionResult GetCountryList()
        {
            var CountryList = _icustomer.GetCountryList();
            return Ok(GetAjaxResponse(true, string.Empty, CountryList));
        }
    }
}