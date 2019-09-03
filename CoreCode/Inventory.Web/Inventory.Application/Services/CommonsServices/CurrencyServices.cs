using Inventory.Application.Interface.Common;
using Inventory.Application.ViewModel;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory.Application.Services.CommonsServices
{
    public class CurrencyServices : ICurrency
    {
        private readonly ApplicationDbContext _DbContext;
        public CurrencyServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<CurrencyVm> GetCurrencyList()
        {
            List<CurrencyVm> CurrencyList = new List<CurrencyVm>();
            try
            {
                var a = _DbContext.Currencies.ToList();
                foreach (var c in a)
                {
                    CurrencyVm currency = new CurrencyVm();
                    currency.CurrencyId = c.CurrencyId;
                    currency.CurrencyName = c.CurrencyName;
                    CurrencyList.Add(currency);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CurrencyList;
        }
    }
}
