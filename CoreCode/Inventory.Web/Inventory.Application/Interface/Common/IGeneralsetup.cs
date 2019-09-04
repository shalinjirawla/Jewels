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
        public interface ICreditTerms
        {
            Task<Boolean> SaveCreditTerms(CreditTermsVm model);
        }
        public interface ICurrency
        {
            Task<Boolean> SaveCurrency(CurrencyVm model);
            Task<List<CurrencyVm>> GetCurrencyList();
            Task<CurrencyVm> GetCurrency(long CurrencyId);
            Task<Boolean> UpdateCurrency(long CurrencyId, CurrencyVm currencyVm);
            Task<Boolean> DeleteCurrency(long Currency);
        }
    }
}
