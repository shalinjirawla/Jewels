using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Application.Interface;
using Inventory.Application.Interface.ApplicationUser;
using Inventory.Application.Interface.Common;
using Inventory.Application.ViewModel;
using Inventory.Application.ViewModel.CommonsVm;
using Inventory.Web.share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Inventory.Application.Interface.Common.IGenerealsetup;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("localhost")]
    [Authorize]
    public class CommonsController : ControllerBase
    {
        private readonly IDiscountType _discountType;
        private readonly IGenerealsetup.ICurrency _currency;
        private readonly ITaxCode _itaxCode;
        private readonly ICountry _icountry;
        private readonly ICreditTerms _icreditTerms;
        private readonly IWarehouse _warehouse;
        private readonly IApplicationUser _applicationUser;
        public Boolean Status = false;
        public string Message = "";
        public object Data = null;
        public string GetUserId = "";
        public long GetTenantId = 0;
        public CommonsController(IDiscountType discountType,
            IGenerealsetup.ICurrency currency, ICreditTerms icreditTerms,
            ICountry country, IWarehouse warehouse,
            IApplicationUser applicationUser,
            ITaxCode itaxCode
            )
        {

            _discountType = discountType;
            _currency = currency;
            _icountry = country;
            _icreditTerms = icreditTerms;
            _warehouse = warehouse;
            _applicationUser = applicationUser;
            GetUserId = _applicationUser.GetUserId();
            GetTenantId = _applicationUser.GetTenantId();
            if (GetUserId == null && GetTenantId == 0)
            _itaxCode = itaxCode;
            if (_applicationUser.GetUserId() == null)
            {
                _applicationUser.Logout();
            }
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
                await _discountType.SaveDiscountType(discountTypeVm, GetUserId, GetTenantId);
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
                DiscounttypeId = await _discountType.UpdateDiscountType(DiscounttypeId, discountTypeVm, GetUserId);
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
        [HttpGet]
        public async Task<IActionResult> GetActiveCurrencys()
        {
            List<CurrencyVm> list = new List<CurrencyVm>();
            list = await _currency.GetActiveCurrencyList();
            return Ok(GetAjaxResponse(true, "List Of Currency", list));
        }

        [HttpPost]
        public async Task<IActionResult> SaveCurrency(CurrencyVm model)
        {
            if (ModelState.IsValid)
            {
                string UserId = await _applicationUser.GetUserId();
                long TenantId = await _applicationUser.GetTenantId();
                Status = await _currency.SaveCurrency(model,UserId,TenantId);
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
                string UserId = await _applicationUser.GetUserId();
                Status = await _currency.UpdateCurrency(CurrencyId, model, UserId);
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
            if (Id != 0)
            {
                var CountryList = _icountry.GetCountryAsyc(Id);
                return Ok(GetAjaxResponse(true, string.Empty, CountryList));
            }
            else
            {
                return Ok(GetAjaxResponse(false, string.Empty, null));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateCountry(CountryVm model)
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
            string UserId = GetUserId;
            long TenantId = GetTenantId;
            var CountryId = _icountry.AddCountryAsyc(model, UserId, TenantId);
            return Ok(GetAjaxResponse(true, msg, CountryId));
        }

        [HttpDelete]
        public IActionResult DeleteCountry(int Id)
        {
            if (Id != 0)
            {
                var CountryId = _icountry.DeleteCountryAsyc(Id);
                return Ok(GetAjaxResponse(true, "Country Deleted Successfully", CountryId));
            }
            else
            {
                return Ok(GetAjaxResponse(false, string.Empty, null));
            }

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
            CreditTermsVm vm = new CreditTermsVm();
            if (CreditTermId != 0)
            {
                vm = await _icreditTerms.GetCreditTerms(CreditTermId);
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(true, string.Empty, vm));
        }

        [HttpPost]
        public async Task<IActionResult> AddCreditTerm(CreditTermsVm model)
        {
            if (ModelState.IsValid && model != null)
            {
                string UserId = await _applicationUser.GetUserId();
                long TenantId = await _applicationUser.GetTenantId();
                Status = await _icreditTerms.SaveCreditTerms(model,UserId,TenantId);
                if (Status)
                {
                    Message = "Credit Terms Added....!";
                }
                else { Message = "Error Occurss..!"; }
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCreditTerm(long CreditTermId, CreditTermsVm model)
        {

            Status = await _icreditTerms.UpdateCreditTerms(CreditTermId, model);
            string UserId = await _applicationUser.GetUserId();
            Status = await _icreditTerms.UpdateCreditTerms(CreditTermId,model,UserId);
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

        #region Warehouse APIs Start

        [HttpGet]
        public IActionResult GetWarehouseList()
        {
            var warehouse = _warehouse.GetWarehouseListAsync();
            return Ok(GetAjaxResponse(true, string.Empty, warehouse));

        }
        [HttpGet]
        public IActionResult GetWarehouse(long Id)
        {
            if (Id != 0)
            {
                var warehouse = _warehouse.GetWarehouseAsync(Id);
                return Ok(GetAjaxResponse(true, string.Empty, warehouse));
            }
            else
            {
                return Ok(GetAjaxResponse(false, "your WareHouse Id not found", null));
            }


        }
        [HttpPost]
        public async Task<IActionResult> SaveWarehouseList(WarehouseVm model)
        {
            await Task.Run(() =>
            {
                Data = _warehouse.SaveWarehouseListAsync(model, GetUserId, GetTenantId);
            });
            return Ok(GetAjaxResponse(true, string.Empty, Data));

        }
        [HttpDelete]
        public IActionResult DeleteWarehouse(long Id)
        {

            if (Id != 0)
            {
                var a = _warehouse.DeleteWarehouseAsync(Id);
                Message = "Warehouse deleted successfully";
                Status = true;
            }
            else
            {
                Message = "Your Warehouse Id is not found";
                Status = false;
            }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpGet]
        public IActionResult GetActiveWarehouseList()
        {
            var warehouse = _warehouse.GetActiveWarehouseListAsync();
            return Ok(GetAjaxResponse(true, string.Empty, warehouse));

        }

        [HttpGet]
        public async Task<IActionResult> UpdateWarehouseStatus(long Id, Boolean status)
        {
            if (Id != 0)
            {
                await Task.Run(() =>
                {
                    Data = _warehouse.UpdateWarehouseStatusAsync(Id, status, GetUserId);
                });
                if (status)
                {
                    Message = "Ware House Actived..";
                    Status = true;
                }
                else
                {
                    Message = "Ware House Deactived..";
                    Status = true;
                }
                return Ok(GetAjaxResponse(true, Message, Data));
            }
            else
            {
                return Ok(GetAjaxResponse(false, "Your warehouse is not Found", null));
            }
        }

        #endregion Warehouse APIs End

        #region Tax Code APIs Start

        [HttpPost]
        public async Task<IActionResult> AddTextCode(TaxCodeVm model)
        {
            if (ModelState.IsValid && model != null)
            {
                string UserId = await _applicationUser.GetUserId();
                long TenantId = await _applicationUser.GetTenantId();
                Status = await _itaxCode.SaveTaxCode(model, UserId, TenantId);
                if (Status)
                {
                    Message = "Tax Code Added....!";
                }
                else { Message = "Error Occurss..!"; }
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTaxCode(long TaxcodeId, TaxCodeVm model)
        {
            string UserId = await _applicationUser.GetUserId();
            Status = await _itaxCode.UpdateTaxCode(TaxcodeId, model, UserId);
            if (Status)
            {
                Message = "Tax Code is Updated....!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetTaxCodeList()
        {
            var Taxcode = await _itaxCode.GetTaxCodeList();
            return Ok(GetAjaxResponse(true, string.Empty, Taxcode));
        }

        [HttpGet]
        public async Task<IActionResult> GetTaxCodeById(long TaxId)
        {
            TaxCodeVm tax = new TaxCodeVm();
            if (TaxId != 0)
            {
                tax = await _itaxCode.GetTaxCode(TaxId);
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(true, string.Empty, tax));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTaxCode(long TaxId)
        {
            Status = await _itaxCode.DeleteTaxCode(TaxId);
            if (Status)
            {
                Message = "Tax Code is Deleted...!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        #endregion Tax Code APIs End
    }
}