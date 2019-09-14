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
    public class GeneralsetupServices : ICurrency, ITaxCode, ICreditTerms, IShipmentTerm, IShipmentMethod
    {
        private readonly ApplicationDbContext _DbContext;
        public GeneralsetupServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public Boolean Result = false;

        #region Currency Services Start
        public async Task<bool> SaveCurrency(CurrencyVm model, string UserId, long TenantId)
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
                                CreatorUserId = UserId,
                                LastModificationTime = DateTime.Now,
                                LastModifierUserId = UserId,
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

        public async Task<List<CurrencyVm>> GetActiveCurrencyList()
        {
            List<CurrencyVm> list = new List<CurrencyVm>();
            try
            {
                await Task.Run(() =>
                {
                    var data = _DbContext.Currencies.Where(x => x.IsActive == true).ToList();
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

        public async Task<bool> UpdateCurrency(long CurrencyId, CurrencyVm currencyVm, string UserId)
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
                            data.LastModifierUserId = UserId;
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
        public async Task<bool> SaveTaxCode(TaxCodeVm model, string UserId, long TenantId)
        {
            Boolean IsExist = false;
            try
            {

                if (model != null)
                {
                    IsExist = await IsTaxCodeExist(model.Code);
                    if (!IsExist)
                    {
                        TaxCode taxCode = new TaxCode
                        {
                            Code = model.Code,
                            Amount = model.Amount,
                            CreationTime = DateTime.Now,
                            CreatorUserId = UserId,
                            LastModificationTime = DateTime.Now,
                            LastModifierUserId = UserId,
                            IsActive = true,
                            TenantsId = TenantId,
                        };
                        _DbContext.TaxCode.Add(taxCode);
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

        public async Task<Boolean> IsTaxCodeExist(string Code)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = _DbContext.TaxCode.FirstOrDefault(x => x.Code == Code);
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

        public async Task<List<TaxCodeVm>> GetTaxCodeList()
        {
            List<TaxCodeVm> taxcodelist = new List<TaxCodeVm>();

            try
            {
                await Task.Run(() =>
                {
                    var taxcode = _DbContext.TaxCode.ToList();
                    foreach (var a in taxcode)
                    {
                        TaxCodeVm tax = new TaxCodeVm();
                        tax.TaxId = a.TaxId;
                        tax.Code = a.Code;
                        tax.Amount = a.Amount;

                        taxcodelist.Add(tax);
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return taxcodelist;
        }

        public async Task<TaxCodeVm> GetTaxCode(long TaxId)
        {
            TaxCodeVm tax = new TaxCodeVm();
            try
            {
                await Task.Run(() =>
                {
                    var taxcode = _DbContext.TaxCode.Where(x => x.TaxId == TaxId).FirstOrDefault();
                    if (taxcode != null)
                    {
                        tax.TaxId = taxcode.TaxId;
                        tax.Code = taxcode.Code;
                        tax.Amount = taxcode.Amount;
                    }

                });

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tax;
        }

        public async Task<bool> UpdateTaxCode(long TaxId, TaxCodeVm model, string UserId)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var taxcode = _DbContext.TaxCode.Where(x => x.TaxId == TaxId).FirstOrDefault();
                    if (taxcode != null && model != null)
                    {
                        taxcode.Code = model.Code;
                        taxcode.Amount = model.Amount;
                        taxcode.LastModifierUserId = UserId;
                        taxcode.LastModificationTime = DateTime.Now;
                        taxcode.IsActive = true;

                        _DbContext.TaxCode.Update(taxcode);
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

        public async Task<bool> DeleteTaxCode(long TaxId)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var taxcode = _DbContext.TaxCode.Where(x => x.TaxId == TaxId).FirstOrDefault();
                    if (taxcode != null)
                    {
                        _DbContext.TaxCode.Remove(taxcode);
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
        #endregion Tax Code Services End

        #region Credit Terms Services Start
        public async Task<bool> SaveCreditTerms(CreditTermsVm model, string UserId, long TenantId)
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
                            CreatorUserId = UserId,
                            LastModificationTime = DateTime.Now,
                            LastModifierUserId = UserId,
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

        public async Task<bool> UpdateCreditTerms(long CreditTermId, CreditTermsVm model, string UserId)
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
                        creditTerms.LastModificationTime = DateTime.Now;
                        creditTerms.LastModifierUserId = UserId;
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

        #region Shipment Terms Services Start

        public async Task<bool> SaveShipmentTerm(ShipmentTermVm model, string UserId, long TenantId)
        {
            Boolean IsExist = false;
            Boolean Result = false;
            try
            {

                if (model != null)
                {
                    IsExist = await IsShipmentTermExist(model.Code);
                    if (!IsExist)
                    {
                        ShipmentTerm ShipmentTerm = new ShipmentTerm
                        {
                            Code = model.Code,
                            Description = model.Description,
                            CreationTime = DateTime.Now,
                            CreatorUserId = UserId,
                            LastModificationTime = DateTime.Now,
                            LastModifierUserId = UserId,
                            IsActive = true,
                            TenantsId= TenantId
                        };
                        _DbContext.ShipmentTerms.Add(ShipmentTerm);
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
        public async Task<Boolean> IsShipmentTermExist(string Code)
        {

            try
            {
                await Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = _DbContext.ShipmentTerms.FirstOrDefault(x => x.Code == Code);
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

        public async Task<List<ShipmentTermVm>> GetShipmentTermList()
        {
            List<ShipmentTermVm> ShipmentTermList = new List<ShipmentTermVm>();

            try
            {
                await Task.Run(() =>
                {
                    var creditTerms = _DbContext.ShipmentTerms.ToList();
                    foreach (var a in creditTerms)
                    {
                        ShipmentTermVm ShipmentTerm = new ShipmentTermVm();
                        ShipmentTerm.ShipmentTermId = a.ShipmentTermId;
                        ShipmentTerm.Code = a.Code;
                        ShipmentTerm.Description = a.Description;

                        ShipmentTermList.Add(ShipmentTerm);
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return ShipmentTermList; 
        }

        public async Task<ShipmentTermVm> GetShipmentTerm(long ShipmentTermId)
        {
            ShipmentTermVm ShipmentTerm = new ShipmentTermVm();
            try
            {
                await Task.Run(() =>
                {
                    var term = _DbContext.ShipmentTerms.Where(x => x.ShipmentTermId == ShipmentTermId).FirstOrDefault();
                    if (term != null)
                    {
                        ShipmentTerm.ShipmentTermId = term.ShipmentTermId;
                        ShipmentTerm.Code = term.Code;
                        ShipmentTerm.Description = term.Description;
                    }

                });

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ShipmentTerm;
        }

        public async Task<bool> UpdateShipmentTerm(long ShipmentTermId, ShipmentTermVm model, string UserId)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var ShipmentTerms = _DbContext.ShipmentTerms.Where(x => x.ShipmentTermId == ShipmentTermId).FirstOrDefault();
                    if (ShipmentTerms != null && model != null)
                    {
                        ShipmentTerms.Code = model.Code;
                        ShipmentTerms.Description = model.Description;
                        ShipmentTerms.LastModificationTime = DateTime.Now;
                        ShipmentTerms.LastModifierUserId = UserId;
                        ShipmentTerms.IsActive = true;

                        _DbContext.ShipmentTerms.Update(ShipmentTerms);
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

        public async Task<bool> DeleteShipmentTerm(long ShipmentTermId)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var ShipmentTerm = _DbContext.ShipmentTerms.Where(x => x.ShipmentTermId == ShipmentTermId).FirstOrDefault();
                    if (ShipmentTerm != null)
                    {
                        _DbContext.ShipmentTerms.Remove(ShipmentTerm);
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

        #endregion Shipment Terms Services Start

        #region Shipment Method Services Start

        public async Task<bool> SaveShipmentMethod(ShipmentMethodVm model, string UserId, long TenantId)
        {
            Boolean IsExist = false;
            Boolean Result = false;
            try
            {

                if (model != null)
                {
                    IsExist = await IsShipmentMethodExist(model.Code);
                    if (!IsExist)
                    {
                        ShipmentMethod ShipmentMethod = new ShipmentMethod
                        {
                            Code = model.Code,
                            Description = model.Description,
                            CreationTime = DateTime.Now,
                            CreatorUserId = UserId,
                            LastModificationTime = DateTime.Now,
                            LastModifierUserId = UserId,
                            IsActive = true,
                            TenantsId=TenantId
                        };
                        _DbContext.ShipmentMethods.Add(ShipmentMethod);
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

        public async Task<Boolean> IsShipmentMethodExist(string Code)
        {

            try
            {
                await Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = _DbContext.ShipmentMethods.FirstOrDefault(x => x.Code == Code);
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

        public async Task<List<ShipmentMethodVm>> GetShipmentMethodList()
        {
            List<ShipmentMethodVm> ShipmentMethodList = new List<ShipmentMethodVm>();

            try
            {
                await Task.Run(() =>
                {
                    var ShipmentMethods = _DbContext.ShipmentMethods.ToList();
                    foreach (var a in ShipmentMethods)
                    {
                        ShipmentMethodVm ShipmentMethod = new ShipmentMethodVm();
                        ShipmentMethod.ShipmentMethodId = a.ShipmentMethodId;
                        ShipmentMethod.Code = a.Code;
                        ShipmentMethod.Description = a.Description;

                        ShipmentMethodList.Add(ShipmentMethod);
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return ShipmentMethodList; ;
        }

        public async Task<ShipmentMethodVm> GetShipmentMethod(long ShipmentMethodId)
        {
            ShipmentMethodVm ShipmentMethod = new ShipmentMethodVm();
            try
            {
                await Task.Run(() =>
                {
                    var term = _DbContext.ShipmentMethods.Where(x => x.ShipmentMethodId == ShipmentMethodId).FirstOrDefault();
                    if (term != null)
                    {
                        ShipmentMethod.ShipmentMethodId = term.ShipmentMethodId;
                        ShipmentMethod.Code = term.Code;
                        ShipmentMethod.Description = term.Description;
                    }

                });

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ShipmentMethod;
        }

        public async Task<bool> UpdateShipmentMethod(long ShipmentMethodId, ShipmentMethodVm model, string UserId)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var ShipmentMethod = _DbContext.ShipmentMethods.Where(x => x.ShipmentMethodId == ShipmentMethodId).FirstOrDefault();
                    if (ShipmentMethod != null && model != null)
                    {
                        ShipmentMethod.Code = model.Code;
                        ShipmentMethod.Description = model.Description;
                        ShipmentMethod.LastModificationTime = DateTime.Now;
                        ShipmentMethod.LastModifierUserId = UserId;
                        ShipmentMethod.IsActive = true;

                        _DbContext.ShipmentMethods.Update(ShipmentMethod);
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

        public async Task<bool> DeleteShipmentMethod(long ShipmentMethodId)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var ShipmentMethod = _DbContext.ShipmentMethods.Where(x => x.ShipmentMethodId == ShipmentMethodId).FirstOrDefault();
                    if (ShipmentMethod != null)
                    {
                        _DbContext.ShipmentMethods.Remove(ShipmentMethod);
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

        #endregion Shipment Method Services Start


    }
}
