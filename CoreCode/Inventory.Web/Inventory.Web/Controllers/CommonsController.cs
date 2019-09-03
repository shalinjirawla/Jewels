using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface;
using Inventory.Application.Interface.Common;
using Inventory.Application.ViewModel;
using Inventory.Web.share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonsController : ControllerBase
    {
        private readonly IDiscountType _discountType;
        private readonly ICountry _icountry;
        private readonly ICurrency _icurrency;
        public CommonsController(IDiscountType discountType, ICountry icountry, ICurrency icurrency)
        {
            _discountType = discountType;
            _icountry = icountry;
            _icurrency = icurrency;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        #region Discount Type APIs Start
        [HttpGet]
        public async Task<IActionResult> GetDiscountTypeLists()
        {
            var DiscounytTypeList = await _discountType.GetDiscounytTypeList();
            return Ok(GetAjaxResponse(true, "List Of Discount Type", DiscounytTypeList));
        }
        [HttpPost]
        public async Task<IActionResult> SaveDiscountType(DiscountTypeVm discountTypeVm)
        {
            if (ModelState.IsValid)
            {
                await _discountType.SaveDiscountType(discountTypeVm);
            }
            else
            {
                return BadRequest();
            }
            return Ok(GetAjaxResponse(true, "Discount Type Susscesfully Save...!", null));
        }
        [HttpGet]
        public async Task<IActionResult> GetDiscountType(long DiscountTypeId)
        {
            DiscountTypeVm discountTypeVm = new DiscountTypeVm();
            if (DiscountTypeId != 0)
            {
                discountTypeVm = await _discountType.GetDiscountType(DiscountTypeId);
            }
            else {
                return BadRequest();
            }
            return Ok(GetAjaxResponse(true, string.Empty, discountTypeVm));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDiscountType(long DiscounttypeId, DiscountTypeVm discountTypeVm)
        {
            if (DiscounttypeId != 0 && ModelState.IsValid)
            {
                DiscounttypeId = await _discountType.UpdateDiscountType(DiscounttypeId, discountTypeVm);
            }
            else {
                return BadRequest();
            }
            return Ok(GetAjaxResponse(true, "Discount Type Succesfully Updated..", DiscounttypeId));
        }
        [HttpDelete]
        public async Task<IActionResult> DeteleDiscountType(long DiscountTypeId)
        {
            if (DiscountTypeId != 0) {
                DiscountTypeId = await _discountType.DeleteDiscountType(DiscountTypeId);
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(true, "Discount Type Delete Successfully ....", DiscountTypeId));
        }
        #endregion Discount Type APIs End

        #region Country APIs Start

        [HttpGet]
        public IActionResult GetCountryList()
        {
            var CountryList = _icountry.GetCountryList();
            return Ok(GetAjaxResponse(true, string.Empty, CountryList));
        }
        
        [HttpGet]
        public IActionResult GetCountry(int Id)
        {
            var CountryList = _icountry.GetCountryAsyc(Id);
            return Ok(GetAjaxResponse(true, string.Empty, CountryList));
        }

        [HttpPost]
        public IActionResult AddUpdateCountry(CountryVm model)
        {
            var msg = "";
            if (model.CountryId == 0)
            {
                msg = "Country Added Successfully";
            }
            else
            {
                msg = "Country Updated Successfully";
            }
            var CountryId = _icountry.AddCountryAsyc(model);
            return Ok(GetAjaxResponse(true, msg , CountryId));
        }

        [HttpDelete]
        public IActionResult DeleteCountry(int Id)
        {
            var CountryId = _icountry.DeleteCountryAsyc(Id);
            return Ok(GetAjaxResponse(true, "Country Deleted Successfully", CountryId));
        }

        #endregion Country APIs End

        #region Currency APIs Start

        [HttpGet]
        public IActionResult GetCurrency()
        {
            var Currency = _icurrency.GetCurrencyList();
            return Ok(GetAjaxResponse(true, string.Empty, Currency));
        }

        #endregion Currency APIs End
    }
}