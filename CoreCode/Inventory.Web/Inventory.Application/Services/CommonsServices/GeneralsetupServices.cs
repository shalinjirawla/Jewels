using Inventory.Application.Interface.Common;
using Inventory.Application.ViewModel;
using Inventory.Application.ViewModel.CommonsVm;
using Inventory.Core.Models.Commons;
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
    public class GeneralsetupServices : ICurrency, ITaxCode, ICreditTerms
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
                    if (data != null && data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            CurrencyVm Currency = new CurrencyVm
                            {
                                CurrencyId = item.CurrencyId,
                                CurrencyName = item.CurrencyName,
                                Code = item.Code,
                                Status = item.IsActive,
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
                        if (data != null)
                        {
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
                if (CurrencyId != 0)
                {
                    await Task.Run(() =>
                    {
                        var data = _DbContext.Currencies.FirstOrDefault(x => x.CurrencyId == CurrencyId);
                        if (data != null)
                        {
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
        public async Task<Boolean> CurrencyChange(long CurrencyId, Boolean Status)
        {
            try
            {
                if (CurrencyId != 0)
                {
                    await Task.Run(() =>
                    {

                        var data = _DbContext.Currencies.FirstOrDefault(x => x.CurrencyId == CurrencyId);
                        if (data != null)
                        {
                            data.IsActive = Status;
                            _DbContext.Currencies.Update(data);
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
        #endregion Currency Services End
        #region Tax Code Serives start
        public Task<bool> SaveTaxCode(TaxCodeVm model)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaxCodeVm>> GetTaxCodeList()
        {
            throw new NotImplementedException();
        }

        public Task<TaxCodeVm> GetTaxCode(long TaxId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTaxCode(long TaxId, TaxCodeVm currencyVm)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTaxCode(long TaxId)
        {
            throw new NotImplementedException();
        }
        #endregion Tax Code Services End
        #region Credit Terms Services Start
        public async Task<bool> SaveCreditTerms(CreditTermsVm model)
        {
            Boolean IsExist = false;
            try
            {
                if (model != null)
                {
                    IsExist = await IsCreditTermsExist(model.Code);
                    if (!IsExist)
                    {
                        CreditTerms creditTerms = new CreditTerms
                        {
                            Code = model.Code,
                            Duration = model.Duration,
                            Description = model.Description,
                            CreationTime = DateTime.Now,
                            CreatorUserId = 001,
                            LastModificationTime = DateTime.Now,
                            LastModifierUserId = 001,
                            IsActive = true,
                        };
                        _DbContext.CreditTerms.Add(creditTerms);
                        _DbContext.SaveChanges();
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        public async Task<Boolean> IsCreditTermsExist(string Code)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = _DbContext.CreditTerms.FirstOrDefault(x => x.Code == Code);
                        if (data != null)
                        {
                            Result = true;
                        }
                        else { Result = false; }
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        public async Task<List<CreditTermsVm>> GetCreditTermsList()
        {
            List<CreditTermsVm> creditTermsLlist = new List<CreditTermsVm>();

            try
            {
                await Task.Run(() =>
                {
                    var creditTerms = _DbContext.CreditTerms.ToList();
                    foreach (var a in creditTerms)
                    {
                        CreditTermsVm credit = new CreditTermsVm();
                        credit.CreditTermId = a.CreditTermId;
                        credit.Code = a.Code;
                        credit.Duration = a.Duration;
                        credit.Description = a.Description;

                        creditTermsLlist.Add(credit);
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return creditTermsLlist; ;

        }

        public async Task<CreditTermsVm> GetCreditTerms(long CreditTermId)
        {
            CreditTermsVm creditTerms = new CreditTermsVm();
            try
            {
                await Task.Run(() =>
                {
                    var credit = _DbContext.CreditTerms.Where(x => x.CreditTermId == CreditTermId).FirstOrDefault();
                    if (credit != null)
                    {
                        creditTerms.CreditTermId = credit.CreditTermId;
                        creditTerms.Code = credit.Code;
                        creditTerms.Duration = credit.Duration;
                        creditTerms.Description = credit.Description;
                    }

                });

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return creditTerms;
        }

        public async Task<bool> UpdateCreditTerms(long CreditTermId, CreditTermsVm model)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var creditTerms = _DbContext.CreditTerms.Where(x => x.CreditTermId == CreditTermId).FirstOrDefault();
                    if (creditTerms != null && model != null)
                    {
                        creditTerms.Code = model.Code;
                        creditTerms.Duration = model.Duration;
                        creditTerms.Description = model.Description;
                        creditTerms.CreationTime = DateTime.Now;
                        creditTerms.CreatorUserId = 001;
                        creditTerms.LastModificationTime = DateTime.Now;
                        creditTerms.LastModifierUserId = 001;
                        creditTerms.IsActive = true;

                        _DbContext.CreditTerms.Update(creditTerms);
                        _DbContext.SaveChanges();
                        Result = true;

                    }
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Result;
        }

        public async Task<bool> DeleteCreditTerms(long CreditTermId)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var Credit = _DbContext.CreditTerms.Where(x => x.CreditTermId == CreditTermId).FirstOrDefault();
                    if (Credit != null)
                    {
                        _DbContext.CreditTerms.Remove(Credit);
                        _DbContext.SaveChanges();
                        Result = true;

                    }
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Result;
        }
        #endregion Credit Terms Services end
    }
}
