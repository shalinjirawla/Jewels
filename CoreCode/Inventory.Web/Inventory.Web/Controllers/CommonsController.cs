﻿using System;
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
        private readonly ITaxCode _taxCode;
        private readonly ICountry _icountry;
        private readonly ICreditTerms _icreditTerms;
        private readonly IWarehouse _warehouse;
        private readonly IShipmentTerm _shipmentTerm;
        private readonly IShipmentMethod _shipmentMethod;
        private readonly IPaymentTerm _ipaymentTerm;
        private readonly IUOM _IUOM;
        private readonly IMetric_Units _imetric_Units;
        private readonly ISalesOrderType _SalesOrderType;
        private readonly IAdditionalCharge _AdditionalCharge;
        public Boolean Status = false;
        public string Message = "";
        public object Data = null;
        public string GetUserId = "";
        public long GetTenantId = 0;
        private readonly SessionHanlderController _SessionHanlderController;
        public CommonsController(IDiscountType discountType,
            IGenerealsetup.ICurrency currency, ICreditTerms icreditTerms,
            ICountry country, IWarehouse warehouse,
            ITaxCode itaxCode, IShipmentTerm shipmentTerm,
            IShipmentMethod shipmentMethod, IPaymentTerm ipaymentTerm,
            IUOM IUOM, IMetric_Units imetric_Units,
            SessionHanlderController SessionHanlderController,
             ISalesOrderType SalesOrderType,
             IAdditionalCharge AdditionalCharge
            )
        {

            _discountType = discountType;
            _currency = currency;
            _icountry = country;
            _taxCode = itaxCode;
            _icreditTerms = icreditTerms;
            _warehouse = warehouse;
            _shipmentTerm = shipmentTerm;
            _shipmentMethod = shipmentMethod;
            _ipaymentTerm = ipaymentTerm;
            _IUOM = IUOM;
            _imetric_Units = imetric_Units;
            _SalesOrderType = SalesOrderType;
            _SessionHanlderController = SessionHanlderController;
            _AdditionalCharge = AdditionalCharge;

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
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
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
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
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



                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);

                Status = await _currency.SaveCurrency(model, GetUserId, GetTenantId);
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


                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                Status = await _currency.UpdateCurrency(CurrencyId, model, GetUserId);
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

            GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
            GetUserId = _SessionHanlderController.GetUserId(HttpContext);
            var CountryId = _icountry.AddCountryAsyc(model, GetUserId, GetTenantId);
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


                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                Status = await _icreditTerms.SaveCreditTerms(model, GetUserId, GetTenantId);
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



            GetUserId = _SessionHanlderController.GetUserId(HttpContext);
            Status = await _icreditTerms.UpdateCreditTerms(CreditTermId, model, GetUserId);
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
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);

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
                    GetUserId = _SessionHanlderController.GetUserId(HttpContext);
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


                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                Status = await _taxCode.SaveTaxCode(model, GetUserId, GetTenantId);
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

            GetUserId = _SessionHanlderController.GetUserId(HttpContext);
            Status = await _taxCode.UpdateTaxCode(TaxcodeId, model, GetUserId);
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
            var Taxcode = await _taxCode.GetTaxCodeList();
            return Ok(GetAjaxResponse(true, string.Empty, Taxcode));
        }

        [HttpGet]
        public async Task<IActionResult> GetTaxCodeById(long TaxId)
        {
            TaxCodeVm tax = new TaxCodeVm();
            if (TaxId != 0)
            {
                tax = await _taxCode.GetTaxCode(TaxId);
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(true, string.Empty, tax));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTaxCode(long TaxId)
        {
            Status = await _taxCode.DeleteTaxCode(TaxId);
            if (Status)
            {
                Message = "Tax Code is Deleted...!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        #endregion Tax Code APIs End

        #region Shipment Term APIs Start

        [HttpPost]
        public async Task<IActionResult> AddShipmentTerm(ShipmentTermVm model)
        {
            if (ModelState.IsValid && model != null)
            {
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                Status = await _shipmentTerm.SaveShipmentTerm(model, GetUserId, GetTenantId);
                if (Status)
                {
                    Message = "Shipment Term is Added....!";
                }
                else { Message = "Error Occurss..!"; }
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddShipmentTerm(long ShipmentTermId, ShipmentTermVm model)
        {
            GetUserId = _SessionHanlderController.GetUserId(HttpContext);
            Status = await _shipmentTerm.UpdateShipmentTerm(ShipmentTermId, model, GetUserId);
            if (Status)
            {
                Message = "Shipment Term is Updated....!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetShipmentTermList()
        {

            var ShipmentTerm = await _shipmentTerm.GetShipmentTermList();
            return Ok(GetAjaxResponse(true, string.Empty, ShipmentTerm));
        }

        [HttpGet]
        public async Task<IActionResult> GetShipmentTermById(long ShipmentTermId)
        {
            ShipmentTermVm ShipmentTerm = new ShipmentTermVm();
            if (ShipmentTermId != 0)
            {
                ShipmentTerm = await _shipmentTerm.GetShipmentTerm(ShipmentTermId);
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(true, string.Empty, ShipmentTerm));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAddShipmentTerm(long ShipmentTermId)
        {
            Status = await _shipmentTerm.DeleteShipmentTerm(ShipmentTermId);
            if (Status)
            {
                Message = "Shipment Term is Deleted...!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        #endregion Shipment Term APIs End

        #region Shipment Method APIs Start

        [HttpPost]
        public async Task<IActionResult> AddShipmentMethod(ShipmentMethodVm model)
        {
            if (ModelState.IsValid && model != null)
            {
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                Status = await _shipmentMethod.SaveShipmentMethod(model, GetUserId, GetTenantId);
                if (Status)
                {
                    Message = "Shipment Method is  Added....!";
                }
                else { Message = "Error Occurss..!"; }
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShipmentMethod(long ShipmentMethodId, ShipmentMethodVm model)
        {
            GetUserId = _SessionHanlderController.GetUserId(HttpContext);
            Status = await _shipmentMethod.UpdateShipmentMethod(ShipmentMethodId, model, GetUserId);
            if (Status)
            {
                Message = "Shipment Method is Updated....!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetShipmentMethodList()
        {

            var ShipmentTerm = await _shipmentMethod.GetShipmentMethodList();
            return Ok(GetAjaxResponse(true, string.Empty, ShipmentTerm));
        }

        [HttpGet]
        public async Task<IActionResult> GetShipmentMethodById(long ShipmentMethodId)
        {
            ShipmentMethodVm ShipmentMethod = new ShipmentMethodVm();
            if (ShipmentMethodId != 0)
            {
                ShipmentMethod = await _shipmentMethod.GetShipmentMethod(ShipmentMethodId);
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(true, string.Empty, ShipmentMethod));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteShipmentMethod(long ShipmentMethodId)
        {
            Status = await _shipmentMethod.DeleteShipmentMethod(ShipmentMethodId);
            if (Status)
            {
                Message = "Shipment Method is Deleted...!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        #endregion Shipment Method APIs End

        #region Payment Term APIs Start

        [HttpPost]
        public async Task<IActionResult> AddPaymentTerm(PaymentTermVm model)
        {
            if (ModelState.IsValid && model != null)
            {
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                Status = await _ipaymentTerm.SavePaymentTerm(model, GetUserId, GetTenantId);
                if (Status)
                {
                    Message = "Payment Term is  Added....!";
                }
                else { Message = "Error Occurss..!"; }
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePaymentTerm(long PaymentTermId, PaymentTermVm model)
        {
            GetUserId = _SessionHanlderController.GetUserId(HttpContext);
            Status = await _ipaymentTerm.UpdatePaymentTerm(PaymentTermId, model, GetUserId);
            if (Status)
            {
                Message = "Payment Term is Updated....!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentTermList()
        {

            var paymentTerm = await _ipaymentTerm.GetPaymentTermList();
            return Ok(GetAjaxResponse(true, string.Empty, paymentTerm));
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentTermById(long PaymentTermId)
        {
            PaymentTermVm PaymentTermVm = new PaymentTermVm();
            if (PaymentTermId != 0)
            {
                PaymentTermVm = await _ipaymentTerm.GetPaymentTerm(PaymentTermId);
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(true, string.Empty, PaymentTermVm));
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePaymentTerm(long PaymentTermId)
        {
            Status = await _ipaymentTerm.DeletePaymentTerm(PaymentTermId);
            if (Status)
            {
                Message = "PaymentTerm Term is Deleted...!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }

        #endregion Payment Term APIs End

        #region UOM APIs Start

        [HttpGet]
        public IActionResult GetUOMList()
        {

            var UOMList = _IUOM.GetUOMList();
            return Ok(GetAjaxResponse(true, string.Empty, UOMList));
        }

        #endregion UOM APIs End

        #region Metric_Units APIs Start

        [HttpGet]
        public IActionResult GetKgMetricUnitList()
        {

            var Metric_UnitsList = _imetric_Units.GetKgMetricUnitList();
            return Ok(GetAjaxResponse(true, string.Empty, Metric_UnitsList));
        }

        [HttpGet]
        public IActionResult GetFtMetricUnitList()
        {

            var Metric_UnitsList = _imetric_Units.GetFtMetricUnitList();
            return Ok(GetAjaxResponse(true, string.Empty, Metric_UnitsList));
        }

        #endregion Metric_Units APIs End
        #region Sales Order Type APIs Start

        [HttpPost]
        public async Task<IActionResult> AddSalesOrderType(SalesOrderTypeVM model)
        {
            if (ModelState.IsValid && model != null)
            {
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                var SaleOrderTypeId = await _SalesOrderType.SaveSalesOrderTypeAsync(model, GetUserId, GetTenantId);
                if (SaleOrderTypeId != 0)
                {
                    Status = true;
                    Message = "Sales Order Type is Saved....!";
                }
                else { Status = false; Message = "Error Occurss..!"; }
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesOrderTypeList()
        {
            GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
            var SalesOrderTypeList = await _SalesOrderType.GetSalesOrderTypeListAsync(GetTenantId);
            return Ok(GetAjaxResponse(true, string.Empty, SalesOrderTypeList));
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveSalesOrderTypeList()
        {

            GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
            var SalesOrderTypeList = await _SalesOrderType.GetActiveSalesOrderTypeListAsync(GetTenantId);
            return Ok(GetAjaxResponse(true, string.Empty, SalesOrderTypeList));
        }
        [HttpGet]
        public async Task<IActionResult> GetSalesOrderTypeById(long SalesOrderTypeId)
        {
            SalesOrderTypeVM SalesOrderType = new SalesOrderTypeVM();
            if (SalesOrderTypeId != 0)
            {
                SalesOrderType = await _SalesOrderType.GetSalesOrderTypeAsync(SalesOrderTypeId);
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(true, string.Empty, SalesOrderType));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSalesOrderType(long SalesOrderTypeId)
        {
            Status = await _SalesOrderType.DeleteSalesOrderTypeAsync(SalesOrderTypeId);
            if (Status)
            {
                Message = "Sales Order Type is Deleted...!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> SalesOrderTypeStatuschange(long SalesOrderTypeId, Boolean Statuschange)
        {
            if (SalesOrderTypeId != 0)
            {
                Status = await _SalesOrderType.SalesOrderTypeStatusChange(SalesOrderTypeId, Statuschange);
                if (Status)
                {
                    if (Statuschange)
                    {
                        Message = "Sales Order Type is Active..!";
                    }
                    else
                    {
                        Message = "Sales Order Type is Deactive...!";
                    }
                }
                else { }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        #endregion Sales Order Type APIs End
        #region Additional Charge APIs Start

        [HttpPost]
        public async Task<IActionResult> AddAdditionalCharge(AdditionalChargeVM model)
        {
            if (ModelState.IsValid && model != null)
            {
                GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
                GetUserId = _SessionHanlderController.GetUserId(HttpContext);
                var AdditionalChargeId = await _AdditionalCharge.SaveAdditionalChargeAsync(model, GetUserId, GetTenantId);
                if (AdditionalChargeId != 0)
                {
                    Status = true;
                    Message = "Additional Charge is Saved....!";
                }
                else { Status = false; Message = "Error Occurss..!"; }
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(Status, Message, null));
        }

        [HttpGet]
        public async Task<IActionResult> GetAdditionalChargeList()
        {
            GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
            var AdditionalChargeList = await _AdditionalCharge.GetAdditionalChargeListAsync(GetTenantId);
            return Ok(GetAjaxResponse(true, string.Empty, AdditionalChargeList));
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveAdditionalChargeList()
        {

            GetTenantId = _SessionHanlderController.GetTenantId(HttpContext);
            var AdditionalChargeList = await _AdditionalCharge.GetActiveAdditionalChargeAsync(GetTenantId);
            return Ok(GetAjaxResponse(true, string.Empty, AdditionalChargeList));
        }
        [HttpGet]
        public async Task<IActionResult> GetAdditionalChargeById(long AdditionalChargeId)
        {
            AdditionalChargeVM AdditionalCharge = new AdditionalChargeVM();
            if (AdditionalChargeId != 0)
            {
                AdditionalCharge = await _AdditionalCharge.GetAdditionalChargeAsync(AdditionalChargeId);
            }
            else { return BadRequest(); }

            return Ok(GetAjaxResponse(true, string.Empty, AdditionalCharge));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAdditionalCharge(long AdditionalChargeId)
        {
            Status = await _AdditionalCharge.DeleteAdditionalChargeAsync(AdditionalChargeId);
            if (Status)
            {
                Message = "Additional Charge is Deleted...!";
            }
            else { Message = "Error Occurss..!"; }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        [HttpGet]
        public async Task<IActionResult> AdditionalChargeStatuschange(long AdditionalChargeId, Boolean Statuschange)
        {
            if (AdditionalChargeId != 0)
            {
                Status = await _AdditionalCharge.AdditionalChargeStatusChange(AdditionalChargeId, Statuschange);
                if (Status)
                {
                    if (Statuschange)
                    {
                        Message = "Additional Charge is Active..!";
                    }
                    else
                    {
                        Message = "Additional Charge is Deactive...!";
                    }
                }
                else { }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, null));
        }
        #endregion Additional Charge APIs End
    }
}