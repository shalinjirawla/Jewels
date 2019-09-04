using Inventory.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interface.Common
{
    public interface ICurrency
    {
        List<CurrencyVm> GetCurrencyList();
        CurrencyVm GetCurrencyByIDAsyc(int id);
    }
}
