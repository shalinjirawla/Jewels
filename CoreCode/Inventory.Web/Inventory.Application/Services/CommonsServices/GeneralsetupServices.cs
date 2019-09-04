using Inventory.Application.Interface.Common;
using Inventory.Application.ViewModel;
using Inventory.Core.Models.Currency;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Inventory.Application.Interface.Common.IGenerealsetup;

namespace Inventory.Application.Services.CommonsServices
{
    public class GeneralsetupServices : ICurrency
    {
        private readonly ApplicationDbContext _DbContext;
        public GeneralsetupServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public Boolean Result = false;

        #region Currency Services Start
        public async Task<bool> SaveCurrency(CurrencyVm model)
        {
            try
            {
                if (model != null)
                {
                    await Task.Run(async () =>
                    {
                        Result = await CurrencyIsExist(model.CurrencyName);
                        if (!Result)
                        {
                            Currency currency = new Currency
                            {
                                CurrencyName = model.CurrencyName,
                                Code = model.Code,
                                CreationTime = DateTime.Now,
                                CreatorUserId = 001,
                                LastModificationTime = DateTime.Now,
                                LastModifierUserId = 001,
                                IsActive = true,
                            };
                            _DbContext.Currencies.Add(currency);
                            _DbContext.SaveChanges();
                            Result = true;
                        }
                        else
                        {
                            Result = false;
                        }
                    });

                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        public async Task<bool> CurrencyIsExist(string CurrencyName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CurrencyName))
                {
                    await Task.Run(() =>
                    {
                        var data = _DbContext.Currencies.FirstOrDefault(x => x.CurrencyName == CurrencyName);
                        if (data != null)
                        {
                            Result = true;
                        }
                        else { Result = false; }
                    });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        public async Task<CurrencyVm> GetCurrency(long CurrencyId)
        {
            CurrencyVm currency = new CurrencyVm();
            try
            {
                await Task.Run(() =>
                {
                    if (CurrencyId != 0)
                    {
                        var data = _DbContext.Currencies.FirstOrDefault(x => x.CurrencyId == CurrencyId);
                        if (data != null)
                        {
                            currency.CurrencyId = data.CurrencyId;
                            currency.CurrencyName = data.CurrencyName;
                            currency.Code = data.Code;
                            currency.Status = data.IsActive;
                        }
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return currency;
        }

        public async Task<List<CurrencyVm>> GetCurrencyList()
        {
            List<CurrencyVm> list = new List<CurrencyVm>();
            try
            {
                await Task.Run(() =>
                {
                    var data = _DbContext.Currencies.ToList();
                    if (data != null && data.Count>0)
                    {
                        foreach (var item in data)
                        {
                            CurrencyVm Currency = new CurrencyVm
                            {
                                CurrencyId = item.CurrencyId,
                                CurrencyName = item.CurrencyName,
                                Code=item.Code,
                                Status=item.IsActive,
                            };
                            list.Add(Currency);
                        }
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return list;
        }

        public async Task<bool> UpdateCurrency(long CurrencyId, CurrencyVm currencyVm)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (CurrencyId != 0 && currencyVm != null)
                    {
                        var data = _DbContext.Currencies.FirstOrDefault(x => x.CurrencyId == CurrencyId);
                        if (data != null) {
                            data.CurrencyName = currencyVm.CurrencyName;
                            data.Code = currencyVm.Code;
                            data.LastModificationTime = DateTime.Now;
                            data.LastModifierUserId = 001;
                        }
                        _DbContext.Currencies.Update(data);
                        _DbContext.SaveChanges();
                        Result = true;
                    }
                    else { Result = false; }
                });
            }
            catch (Exception e)
            {

                throw;
            }
            return Result;
        }
        public async Task<bool> DeleteCurrency(long CurrencyId)
        {
            try
            {
                if (CurrencyId != 0) {
                    await Task.Run(() =>
                    {
                        var data = _DbContext.Currencies.FirstOrDefault(x => x.CurrencyId == CurrencyId);
                        if (data != null) {
                            _DbContext.Currencies.Remove(data);
                            _DbContext.SaveChanges();
                             Result = true;
                        }
                        else { Result = false; }
                    });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        #endregion Currency Services End

    }
}
