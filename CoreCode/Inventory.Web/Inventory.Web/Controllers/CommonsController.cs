using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface;
using Inventory.Application.Interface.Common;
using Inventory.Application.ViewModel;
using Inventory.Web.share;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("localhost")]
    public class CommonsController : ControllerBase
    {
        private readonly IDiscountType _discountType;
        private readonly IGenerealsetup.ICurrency _currency;
        public Boolean Status = false;
        public string Message="";
        public CommonsController(IDiscountType discountType,
            IGenerealsetup.ICurrency currency
            )
        {
            _discountType = discountType;
            _currency = currency;
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
                discountTypeVm =await _discountType.GetDiscountType(DiscountTypeId);
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
                DiscounttypeId= await _discountType.UpdateDiscountType(DiscounttypeId, discountTypeVm);
            }
            else {
                return BadRequest();
            }
            return Ok(GetAjaxResponse(true,"Discount Type Succesfully Updated..", DiscounttypeId));
        }
        [HttpDelete]
        public async Task<IActionResult> DeteleDiscountType(long DiscountTypeId)
        {
            if (DiscountTypeId != 0) {
                DiscountTypeId =await _discountType.DeleteDiscountType(DiscountTypeId);
            }   
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(true, "Discount Type Delete Successfully ....", DiscountTypeId));
        }
        #endregion Discount Type APIs End

        #region Currency Apis Start

        [HttpGet]
        public async Task<IActionResult> GetCurrencys()
        {
            List<CurrencyVm> list = new List<CurrencyVm>();
            list =await _currency.GetCurrencyList();
            return Ok(GetAjaxResponse(true, "List Of Currency", list));
        }
        [HttpPost]
        public async Task<IActionResult> SaveCurrency(CurrencyVm model) {
            if (ModelState.IsValid)
            {
                Status = await _currency.SaveCurrency(model);
                if (Status) {
                    Message = "Currency Succesfully Saved..!";
                }
                else
                {
                    Message = "Error Occurs..";
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrency(long CurrencyId) {
            CurrencyVm currencyVm = new CurrencyVm();
            if (CurrencyId != 0) {
                currencyVm =await _currency.GetCurrency(CurrencyId);
                if (currencyVm != null) {
                    Status = true;
                }
                else { Status = false; }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, string.Empty, currencyVm));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCurrency(long CurrencyId, CurrencyVm model)
        {
            if (CurrencyId != 0 && ModelState.IsValid) {
                Status =await _currency.UpdateCurrency(CurrencyId, model);
                if (Status)
                {
                    Message = "Currency Successfulyy Upated....!";
                }
                else { Message = "Error Occurss..!"; }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCurrency(long CurrencyId) {
            if (CurrencyId != 0) {
                Status = await _currency.DeleteCurrency(CurrencyId);
                if (Status)
                {
                    Message = "Currency Delete Successfully";
                }
                else { Message = "Error Occurs"; }
            }else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        public async Task<IActionResult> Currencychange(long CurrencyId, Boolean Status) {
            if (CurrencyId != 0) {

            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        #endregion Currency Api End

    }
}