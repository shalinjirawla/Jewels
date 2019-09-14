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
    }
}
