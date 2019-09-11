using Inventory.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interface.Common
{
    public interface ICountry
    {
        List<CountryVm> GetCountryList();
        CountryVm GetCountryAsyc(int id);
        int AddCountryAsyc(CountryVm model, string UserId, long TenantId);
        int DeleteCountryAsyc(int id);
    }
}
