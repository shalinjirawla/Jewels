using Inventory.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface
{
    public interface ICustomer
    {
        Task<long> AddCustomer(CustomerVm Model);
        List<CurrencyVm> GetCurrencyList();
        List<CustomerTypeVm> GetCustomerTypeList();
        List<CountryVm> GetCountryList();
    }
}
