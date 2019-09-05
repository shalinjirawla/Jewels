using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface;
using Inventory.Application.Interface.Common;
using Inventory.Application.ViewModel;
using Inventory.Application.ViewModel.CommonsVm;
using Inventory.Web.share;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Inventory.Application.Interface.Common.IGenerealsetup;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("localhost")]
    public class CommonsController : ControllerBase
    {
        private readonly IDiscountType _discountType;
        private readonly IGenerealsetup.ICurrency _currency;
        private readonly ICountry _icountry;
        private readonly ICreditTerms _icreditTerms;
        public Boolean Status = false;
        public string Message = "";
        public CommonsController(IDiscountType discountType,
            IGenerealsetup.ICurrency currency, ICreditTerms icreditTerms,
            ICountry country
            )
        {
            _discountType = discountType;
            _currency = currency;
            _icountry = country;
            _icreditTerms = icreditTerms;
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
            else
            {
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
            else
            {
                return BadRequest();
            }
            return Ok(GetAjaxResponse(true, "Discount Type Succesfully Updated..", DiscounttypeId));
        }
        [HttpDelete]
        public async Task<IActionResult> DeteleDiscountType(long DiscountTypeId)
        {
            if (DiscountTypeId != 0)
            {
                DiscountTypeId = await _discountType.DeleteDiscountType(DiscountTypeId);

            }
            else
            {
                return BadRequest();
            }
            return Ok(GetAjaxResponse(true, "Discount Type Delete Successfully ....", DiscountTypeId));
        }
        #endregion Discount Type APIs End

        #region Currency Apis Start

        [HttpGet]
        public async Task<IActionResult> GetCurrencys()
        {
            List<CurrencyVm> list = new List<CurrencyVm>();
            list = await _currency.GetCurrencyList();
            return Ok(GetAjaxResponse(true, "List Of Currency", list));
        }
        [HttpPost]
        public async Task<IActionResult> SaveCurrency(CurrencyVm model)
        {
            if (ModelState.IsValid)
            {
                Status = await _currency.SaveCurrency(model);
                if (Status)
                {
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
        public async Task<IActionResult> GetCurrency(long CurrencyId)
        {
            CurrencyVm currencyVm = new CurrencyVm();
            if (CurrencyId != 0)
            {
                currencyVm = await _currency.GetCurrency(CurrencyId);
                if (currencyVm != null)
                {
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
            if (CurrencyId != 0 && ModelState.IsValid)
            {
                Status = await _currency.UpdateCurrency(CurrencyId, model);
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
        public async Task<IActionResult> DeleteCurrency(long CurrencyId)
        {
            if (CurrencyId != 0)
            {
                Status = await _currency.DeleteCurrency(CurrencyId);
                if (Status)
                {
                    Message = "Currency Delete Successfully";
                }
                else { Message = "Error Occurs"; }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> Currencychange(long CurrencyId, Boolean Statuschange)
        {
            if (CurrencyId != 0)
            {
                Status = await _currency.CurrencyChange(CurrencyId, Statuschange);
                if (Status)
                {
                    if (Statuschange)
                    {
                        Message = "Currency is Active..!";
                    }
                    else
                    {
                        Message = "Currency is Deactive...!";
                    }
                }
                else { }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        #endregion Currency Api End

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
            return Ok(GetAjaxResponse(true, msg, CountryId));
        }

        [HttpDelete]
        public IActionResult DeleteCountry(int Id)
        {
            var CountryId = _icountry.DeleteCountryAsyc(Id);
            return Ok(GetAjaxResponse(true, "Country Deleted Successfully", CountryId));
        }

        #endregion Country APIs End

        #region Credit Terms APIs Start

        [HttpGet]
        public async Task<IActionResult> GetCreditTermsList()
        {
            var credit = await _icreditTerms.GetCreditTermsList();
            return Ok(GetAjaxResponse(true, string.Empty, credit));
        }

        [HttpGet]
        public async Task<IActionResult> GetCreditTermsById(long CreditTermId)
        {
            var credit = await _icreditTerms.GetCreditTerms(CreditTermId);
            return Ok(GetAjaxResponse(true, string.Empty, credit));
        }

        [HttpPost]
        public async Task<IActionResult> AddCreditTerm(CreditTermsVm model)
        {
            Status = await _icreditTerms.SaveCreditTerms(model);
            if (Status)
            {
                Message = "Credit Terms Added....!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCreditTerm(long CreditTermId,CreditTermsVm model)
        {
            Status = await _icreditTerms.UpdateCreditTerms(CreditTermId,model);
            if (Status)
            {
                Message = "Credit Terms is Updated....!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCreditTerm(long CreditTermId)
        {
            Status = await _icreditTerms.DeleteCreditTerms(CreditTermId);
            if (Status)
            {
                Message = "Credit Terms is Deleted...!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        #endregion Credit Terms APIs End
    }
}