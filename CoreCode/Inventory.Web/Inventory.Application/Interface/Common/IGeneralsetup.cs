using Inventory.Application.ViewModel;
using Inventory.Application.ViewModel.CommonsVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.Common
{
    public class IGenerealsetup
    {

        public interface ICurrency
        {
            Task<Boolean> SaveCurrency(CurrencyVm model, string UserId, long TenantId);
            Task<List<CurrencyVm>> GetCurrencyList();
            Task<List<CurrencyVm>> GetActiveCurrencyList();
            Task<CurrencyVm> GetCurrency(long CurrencyId);
            Task<Boolean> UpdateCurrency(long CurrencyId, CurrencyVm currencyVm, string UserId);
            Task<Boolean> DeleteCurrency(long Currency);
            Task<Boolean> CurrencyChange(long CurrencyId, Boolean Status);
        }
        public interface ITaxCode
        {
            Task<Boolean> SaveTaxCode(TaxCodeVm model, string UserId, long TenantId);
            Task<List<TaxCodeVm>> GetTaxCodeList();
            Task<TaxCodeVm> GetTaxCode(long TaxId);
            Task<Boolean> UpdateTaxCode(long TaxId, TaxCodeVm currencyVm, string UserId);
            Task<Boolean> DeleteTaxCode(long TaxId);
        }
        public interface ICreditTerms
        {
            Task<Boolean> SaveCreditTerms(CreditTermsVm model, string UserId, long TenantId);
            Task<List<CreditTermsVm>> GetCreditTermsList();
            Task<CreditTermsVm> GetCreditTerms(long CreditTermId);
            Task<Boolean> UpdateCreditTerms(long CreditTermId, CreditTermsVm currencyVm, string UserId);
            Task<Boolean> DeleteCreditTerms(long CreditTermId);
        }

        public interface IShipmentTerm
        {
            Task<Boolean> SaveShipmentTerm(ShipmentTermVm model, string UserId, long TenantId);
            Task<List<ShipmentTermVm>> GetShipmentTermList();
            Task<ShipmentTermVm> GetShipmentTerm(long ShipmentTermId);
            Task<Boolean> UpdateShipmentTerm(long ShipmentTermId, ShipmentTermVm currencyVm, string UserId);
            Task<Boolean> DeleteShipmentTerm(long ShipmentTermId);
        }

        public interface IShipmentMethod
        {
            Task<Boolean> SaveShipmentMethod(ShipmentMethodVm model, string UserId, long TenantId);
            Task<List<ShipmentMethodVm>> GetShipmentMethodList();
            Task<ShipmentMethodVm> GetShipmentMethod(long ShipmentMethodId);
            Task<Boolean> UpdateShipmentMethod(long ShipmentMethodId, ShipmentMethodVm model, string UserId);
            Task<Boolean> DeleteShipmentMethod(long ShipmentMethodId);
        }

        public interface IPaymentTerm
        {
            Task<Boolean> SavePaymentTerm(PaymentTermVm model, string UserId, long TenantId);
            Task<List<PaymentTermVm>> GetPaymentTermList();
            Task<PaymentTermVm> GetPaymentTerm(long PaymentTermId);
            Task<Boolean> UpdatePaymentTerm(long PaymentTermId, PaymentTermVm model, string UserId);
            Task<Boolean> DeletePaymentTerm(long PaymentTermId);
        }
        public interface IWarehouse
        {
            List<WarehouseVm> GetWarehouseListAsync();
            WarehouseVm GetWarehouseAsync(long id);
            string SaveWarehouseListAsync(WarehouseVm model, string UserId, long TenantId);
            Boolean DeleteWarehouseAsync(long id);
            List<WarehouseVm> GetActiveWarehouseListAsync();
            Boolean UpdateWarehouseStatusAsync(long id, bool status, string UserId);
        }

        public interface IUOM
        {
            List<UOMVm> GetUOMList();
        }

        public interface IMetric_Units
        {
            List<Metric_UnitsVm> GetKgMetricUnitList();
            List<Metric_UnitsVm> GetFtMetricUnitList();
        }
    }
}   
