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
            Task<Boolean> SaveCurrency(CurrencyVm model);
            Task<List<CurrencyVm>> GetCurrencyList();
            Task<List<CurrencyVm>> GetActiveCurrencyList();
            Task<CurrencyVm> GetCurrency(long CurrencyId);
            Task<Boolean> UpdateCurrency(long CurrencyId, CurrencyVm currencyVm);
            Task<Boolean> DeleteCurrency(long Currency);
            Task<Boolean> CurrencyChange(long CurrencyId, Boolean Status);
        }
        public interface ITaxCode
        {
            Task<Boolean> SaveTaxCode(TaxCodeVm model);
            Task<List<TaxCodeVm>> GetTaxCodeList();
            Task<TaxCodeVm> GetTaxCode(long TaxId);
            Task<Boolean> UpdateTaxCode(long TaxId, TaxCodeVm currencyVm);
            Task<Boolean> DeleteTaxCode(long TaxId);
        }
        public interface ICreditTerms
        {
            Task<Boolean> SaveCreditTerms(CreditTermsVm model);
            Task<List<CreditTermsVm>> GetCreditTermsList();
            Task<CreditTermsVm> GetCreditTerms(long CreditTermId);
            Task<Boolean> UpdateCreditTerms(long CreditTermId, CreditTermsVm currencyVm);
            Task<Boolean> DeleteCreditTerms(long CreditTermId);
        }
    }
}
