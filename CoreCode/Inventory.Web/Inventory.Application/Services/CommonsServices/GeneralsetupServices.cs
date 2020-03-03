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
    public class GeneralsetupServices : ICurrency, ITaxCode, ICreditTerms, IShipmentTerm, IShipmentMethod,
            IPaymentTerm, IWarehouse, IUOM, IMetric_Units, ISalesOrderType, IAdditionalCharge
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
                            TenantsId = TenantId
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
                            TenantsId = TenantId
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

        #region Paymnet Term Services Start

        public async Task<bool> SavePaymentTerm(PaymentTermVm model, string UserId, long TenantId)
        {
            Boolean IsExist = false;
            Boolean Result = false;
            try
            {

                if (model != null)
                {
                    IsExist = await IsPaymentTermExist(model.Code);
                    if (!IsExist)
                    {
                        PaymentTerm PaymentTerm = new PaymentTerm
                        {
                            Code = model.Code,
                            Duration = model.Duration,
                            Description = model.Description,
                            CreationTime = DateTime.Now,
                            CreatorUserId = UserId,
                            LastModificationTime = DateTime.Now,
                            LastModifierUserId = UserId,
                            IsActive = true,
                            TenantsId = TenantId
                        };
                        _DbContext.PaymentTerms.Add(PaymentTerm);
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

        public async Task<Boolean> IsPaymentTermExist(string Code)
        {

            try
            {
                await Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        var data = _DbContext.PaymentTerms.FirstOrDefault(x => x.Code == Code);
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

        public async Task<List<PaymentTermVm>> GetPaymentTermList()
        {
            List<PaymentTermVm> PaymentTermList = new List<PaymentTermVm>();

            try
            {
                await Task.Run(() =>
                {
                    var PaymentTerm = _DbContext.PaymentTerms.ToList();
                    foreach (var a in PaymentTerm)
                    {
                        PaymentTermVm PaymentTermVm = new PaymentTermVm();
                        PaymentTermVm.PaymentTermId = a.PaymentTermId;
                        PaymentTermVm.Code = a.Code;
                        PaymentTermVm.Duration = a.Duration;
                        PaymentTermVm.Description = a.Description;

                        PaymentTermList.Add(PaymentTermVm);
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return PaymentTermList; ;
        }

        public async Task<PaymentTermVm> GetPaymentTerm(long PaymentTermId)
        {
            PaymentTermVm PaymentTerm = new PaymentTermVm();
            try
            {
                await Task.Run(() =>
                {
                    var term = _DbContext.PaymentTerms.Where(x => x.PaymentTermId == PaymentTermId).FirstOrDefault();
                    if (term != null)
                    {
                        PaymentTerm.PaymentTermId = term.PaymentTermId;
                        PaymentTerm.Code = term.Code;
                        PaymentTerm.Description = term.Description;
                        PaymentTerm.Duration = term.Duration;
                    }

                });

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return PaymentTerm;
        }

        public async Task<bool> UpdatePaymentTerm(long PaymentTermId, PaymentTermVm model, string UserId)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var PaymentTerm = _DbContext.PaymentTerms.Where(x => x.PaymentTermId == PaymentTermId).FirstOrDefault();
                    if (PaymentTerm != null && model != null)
                    {
                        PaymentTerm.Code = model.Code;
                        PaymentTerm.Description = model.Description;
                        PaymentTerm.Duration = model.Duration;
                        PaymentTerm.LastModificationTime = DateTime.Now;
                        PaymentTerm.LastModifierUserId = UserId;
                        PaymentTerm.IsActive = true;

                        _DbContext.PaymentTerms.Update(PaymentTerm);
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

        public async Task<bool> DeletePaymentTerm(long PaymentTermId)
        {
            try
            {
                await Task.Run(() =>
                {
                    Result = false;
                    var PaymentTerm = _DbContext.PaymentTerms.Where(x => x.PaymentTermId == PaymentTermId).FirstOrDefault();
                    if (PaymentTerm != null)
                    {
                        _DbContext.PaymentTerms.Remove(PaymentTerm);
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

        #endregion Paymnet Term Services Start

        #region Warehouse Services Start
        public bool DeleteWarehouseAsync(long id)
        {
            var warehouse = false;
            try
            {
                var ware = _DbContext.Warehouses.FirstOrDefault(x => x.WarehouseId == id);
                if (ware != null)
                {
                    _DbContext.Warehouses.Remove(ware);
                    _DbContext.SaveChanges();
                    warehouse = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return warehouse;
        }

        public List<WarehouseVm> GetActiveWarehouseListAsync()
        {
            List<WarehouseVm> warehouseList = new List<WarehouseVm>();
            try
            {
                var list = _DbContext.Warehouses.Where(x => x.IsActive == true).ToList();
                foreach (var a in list)
                {
                    WarehouseVm warehouse = new WarehouseVm();
                    warehouse.WarehouseId = a.WarehouseId;
                    warehouse.WarehouseName = a.Name;
                    warehouse.Warehousecode = a.code;
                    warehouse.IsActive = a.IsActive;

                    warehouseList.Add(warehouse);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return warehouseList;
        }

        public WarehouseVm GetWarehouseAsync(long id)
        {
            WarehouseVm warehouse = new WarehouseVm();
            try
            {
                var a = _DbContext.Warehouses.FirstOrDefault(x => x.WarehouseId == id);
                if (a != null)
                {
                    warehouse.WarehouseId = a.WarehouseId;
                    warehouse.WarehouseName = a.Name;
                    warehouse.Warehousecode = a.code;
                    warehouse.IsActive = a.IsActive;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return warehouse;
        }

        public List<WarehouseVm> GetWarehouseListAsync()
        {
            List<WarehouseVm> warehouseList = new List<WarehouseVm>();
            try
            {
                var list = _DbContext.Warehouses.ToList();
                foreach (var a in list)
                {
                    WarehouseVm warehouse = new WarehouseVm();
                    warehouse.WarehouseId = a.WarehouseId;
                    warehouse.WarehouseName = a.Name;
                    warehouse.Warehousecode = a.code;
                    warehouse.IsActive = a.IsActive;

                    warehouseList.Add(warehouse);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return warehouseList;
        }

        public string SaveWarehouseListAsync(WarehouseVm model, string UserId, long TenantId)
        {
            string WarehouseId = "";
            try
            {
                if (model.WarehouseId == 0)
                {
                    Warehouse warehouse = new Warehouse();
                    warehouse.Name = model.WarehouseName;
                    warehouse.code = model.Warehousecode;
                    warehouse.CreationTime = DateTime.Now;
                    warehouse.LastModificationTime = DateTime.Now;
                    warehouse.CreatorUserId = UserId;
                    warehouse.LastModifierUserId = UserId;
                    warehouse.IsActive = true;
                    warehouse.TenantsId = TenantId;

                    _DbContext.Warehouses.Add(warehouse);
                    _DbContext.SaveChanges();
                    WarehouseId = "Warehouse is Added Successfully";

                }
                else
                {
                    Warehouse warehouse = new Warehouse();
                    warehouse = _DbContext.Warehouses.FirstOrDefault(x => x.WarehouseId == model.WarehouseId);
                    if (warehouse != null)
                    {
                        warehouse.Name = model.WarehouseName;
                        warehouse.code = model.Warehousecode;
                        warehouse.LastModificationTime = DateTime.Now;
                        warehouse.LastModifierUserId = UserId;

                        _DbContext.Update(warehouse);
                        _DbContext.SaveChanges();
                        WarehouseId = "Warehouse is Updates Successfully";
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return WarehouseId;
        }

        public bool UpdateWarehouseStatusAsync(long id, bool status, string UserId)
        {
            Boolean Status = false;
            try
            {
                Warehouse warehouse = new Warehouse();
                warehouse = _DbContext.Warehouses.FirstOrDefault(x => x.WarehouseId == id);
                if (warehouse != null)
                {
                    warehouse.IsActive = status;
                    warehouse.LastModificationTime = DateTime.Now;
                    warehouse.LastModifierUserId = UserId;

                    _DbContext.Update(warehouse);
                    _DbContext.SaveChanges();
                    Status = true;
                }
                else { Status = false; }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Status;
        }



        #endregion Warehouse Services Start

        #region UOM Services Start
        public List<UOMVm> GetUOMList()
        {
            List<UOMVm> UOMList = new List<UOMVm>();
            try
            {
                var data = _DbContext.UOMs.ToList();
                foreach (var a in data)
                {
                    UOMVm uom = new UOMVm();
                    uom.UOMId = a.UOMId;
                    uom.UOMName = a.UOMName;
                    UOMList.Add(uom);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return UOMList;
        }


        #endregion UOM Services Start

        #region UOM Services Start

        public List<Metric_UnitsVm> GetKgMetricUnitList()
        {
            List<Metric_UnitsVm> MetricUnitList = new List<Metric_UnitsVm>();
            try
            {
                var data = _DbContext.Metric_Units.Where(x => x.Metric_UnitsType == 0).ToList();
                foreach (var a in data)
                {
                    Metric_UnitsVm metric_Units = new Metric_UnitsVm();
                    metric_Units.Metric_UnitsId = a.Metric_UnitsId;
                    metric_Units.Metric_UnitsName = a.Metric_UnitsName;
                    metric_Units.Metric_UnitsType = a.Metric_UnitsType;
                    MetricUnitList.Add(metric_Units);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return MetricUnitList;
        }

        public List<Metric_UnitsVm> GetFtMetricUnitList()
        {
            List<Metric_UnitsVm> MetricUnitList = new List<Metric_UnitsVm>();
            try
            {
                var data = _DbContext.Metric_Units.Where(x => x.Metric_UnitsType == 1).ToList();
                foreach (var a in data)
                {
                    Metric_UnitsVm metric_Units = new Metric_UnitsVm();
                    metric_Units.Metric_UnitsId = a.Metric_UnitsId;
                    metric_Units.Metric_UnitsName = a.Metric_UnitsName;
                    metric_Units.Metric_UnitsType = a.Metric_UnitsType;
                    MetricUnitList.Add(metric_Units);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return MetricUnitList;
        }
        #endregion UOM Services Start
        #region Sales Order Type Services Start
        public async Task<List<SalesOrderTypeVM>> GetSalesOrderTypeListAsync(long TenantId)
        {
            List<SalesOrderTypeVM> List = new List<SalesOrderTypeVM>();
            try
            {
                await Task.Run(() =>
                {
                    var SalesList = _DbContext.SalesOrderType.Where(x => x.TenantsId == TenantId).ToList();
                    if (SalesList != null && SalesList.Count() > 0)
                    {
                        foreach (var item in SalesList)
                        {
                            SalesOrderTypeVM saleitem = new SalesOrderTypeVM()
                            {
                                SalesOrderTypeId = item.SalesOrderTypeId,
                                TypeName = item.TypeName,
                                Description = item.Description,
                                IsActive = item.IsActive
                            };
                            List.Add(saleitem);
                        }
                    }

                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return List;
        }

        public async Task<SalesOrderTypeVM> GetSalesOrderTypeAsync(long id)
        {
            SalesOrderTypeVM output = new SalesOrderTypeVM();
            try
            {
                if (id != 0)
                {
                    await Task.Run(() =>
                    {
                        var Sales = _DbContext.SalesOrderType.FirstOrDefault(x => x.SalesOrderTypeId == id);
                        if (Sales != null)
                        {
                            output.SalesOrderTypeId = Sales.SalesOrderTypeId;
                            output.TypeName = Sales.TypeName;
                            output.Description = Sales.Description;
                            output.IsActive = Sales.IsActive;
                        }
                    });
                }
                else
                    throw new Exception("Sales Order id not zero");
            }
            catch (Exception e)
            {
                throw e;
            }
            return output;
        }

        public async Task<long> SaveSalesOrderTypeAsync(SalesOrderTypeVM model, string UserId, long TenantId)
        {
            try
            {
                if (model != null)
                {
                    await Task.Run(() =>
                    {
                        SalesOrderType input = new SalesOrderType()
                        {
                            SalesOrderTypeId = model.SalesOrderTypeId,
                            TypeName = model.TypeName,
                            Description = model.Description,
                            IsActive = model.IsActive
                        };
                        if (model.SalesOrderTypeId == 0)
                        {
                            input.CreationTime = DateTime.Now;
                            input.CreatorUserId = UserId;
                            input.TenantsId = TenantId;
                            _DbContext.SalesOrderType.Add(input);
                            _DbContext.SaveChanges();
                            model.SalesOrderTypeId = input.SalesOrderTypeId;
                        }
                        else
                        {
                            var Salesorder = _DbContext.SalesOrderType.FirstOrDefault(x => x.SalesOrderTypeId == input.SalesOrderTypeId);
                            if (Salesorder != null)
                            {
                                Salesorder.SalesOrderTypeId = model.SalesOrderTypeId;
                                Salesorder.TypeName = model.TypeName;
                                Salesorder.Description = model.Description;
                                Salesorder.IsActive = model.IsActive;
                                Salesorder.LastModificationTime = DateTime.Now;
                                Salesorder.LastModifierUserId = UserId;
                                _DbContext.SalesOrderType.Update(Salesorder);
                                _DbContext.SaveChanges();
                            }


                        }
                    });
                }
                else
                    throw new Exception("input not allow null");
            }
            catch (Exception)
            {

                throw;
            }
            return model.SalesOrderTypeId;
        }

        public async Task<Boolean> DeleteSalesOrderTypeAsync(long id)
        {
            try
            {
                if (id != 0)
                {
                    await Task.Run(() =>
                    {
                        var SaleOrderType = _DbContext.SalesOrderType.FirstOrDefault(x => x.SalesOrderTypeId == id);
                        if (SaleOrderType != null)
                        {
                            _DbContext.SalesOrderType.Remove(SaleOrderType);
                            _DbContext.SaveChanges();
                        }
                    });
                }
                else
                    throw new Exception("id not zero allow");
            }
            catch (Exception e)
            {

                throw;
            }
            return true;
        }

        public async Task<List<SalesOrderTypeVM>> GetActiveSalesOrderTypeListAsync(long TenantId)
        {
            List<SalesOrderTypeVM> List = new List<SalesOrderTypeVM>();
            try
            {
                await Task.Run(() =>
                {
                    var SalesList = _DbContext.SalesOrderType.Where(x => x.TenantsId == TenantId && x.IsActive == true).ToList();
                    if (SalesList != null && SalesList.Count() > 0)
                    {
                        foreach (var item in SalesList)
                        {
                            SalesOrderTypeVM saleitem = new SalesOrderTypeVM()
                            {
                                SalesOrderTypeId = item.SalesOrderTypeId,
                                TypeName = item.TypeName,
                                Description = item.Description,
                                IsActive = item.IsActive
                            };
                            List.Add(saleitem);
                        }
                    }

                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return List;
        }

        public async Task<bool> SalesOrderTypeStatusChange(long SalesOrderTypeId, bool Statuschange)
        {
            Boolean Status = false;
            try
            {
                await Task.Run(() =>
                {
                    var Sales = _DbContext.SalesOrderType.FirstOrDefault(x => x.SalesOrderTypeId == SalesOrderTypeId);
                    if (Sales != null)
                    {
                        Sales.IsActive = Statuschange;
                        Sales.LastModificationTime = DateTime.Now;
                        _DbContext.Update(Sales);
                        _DbContext.SaveChanges();
                        Status = true;
                    }
                    else { Status = false; }
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Status;
        }



        #endregion Sales Order Type Services Start
        #region Additional Charge Start
        public async Task<List<AdditionalChargeVM>> GetAdditionalChargeListAsync(long TenantId)
        {
            List<AdditionalChargeVM> List = new List<AdditionalChargeVM>();
            try
            {
                await Task.Run(() =>
                {
                    var ChargeList = _DbContext.AdditionalCharge.Where(x => x.TenantsId == TenantId).ToList();
                    if (ChargeList != null && ChargeList.Count() > 0)
                    {
                        foreach (var item in ChargeList)
                        {
                            AdditionalChargeVM charge = new AdditionalChargeVM()
                            {
                                AdditionalChargeId = item.AdditionalChargeId,
                                Name = item.Name,
                                UnitPriceType = item.UnitPriceType,
                                UnitPrice = item.UnitPrice,
                                Description = item.Description,
                                IsActive = item.IsActive
                            };
                            List.Add(charge);
                        }
                    }

                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return List;
        }

        public async Task<AdditionalChargeVM> GetAdditionalChargeAsync(long AdditionalCharge)
        {
            AdditionalChargeVM output = new AdditionalChargeVM();
            try
            {
                if (AdditionalCharge != 0)
                {
                    await Task.Run(() =>
                    {
                        var Charge = _DbContext.AdditionalCharge.FirstOrDefault(x => x.AdditionalChargeId == AdditionalCharge);
                        if (Charge != null)
                        {
                            output.AdditionalChargeId = Charge.AdditionalChargeId;
                            output.Name = Charge.Name;
                            output.UnitPriceType = Charge.UnitPriceType;
                            output.UnitPrice = Charge.UnitPrice;
                            output.Description = Charge.Description;
                            output.IsActive = Charge.IsActive;
                        }
                    });
                }
                else
                    throw new Exception("Additional Charge not zero");
            }
            catch (Exception e)
            {
                throw e;
            }
            return output;
        }

        public async Task<long> SaveAdditionalChargeAsync(AdditionalChargeVM model, string UserId, long TenantId)
        {
            try
            {
                if (model != null)
                {
                    await Task.Run(() =>
                    {
                        AdditionalCharge input = new AdditionalCharge()
                        {
                            AdditionalChargeId = model.AdditionalChargeId,
                            Name = model.Name,
                            UnitPriceType = model.UnitPriceType,
                            UnitPrice = model.UnitPrice,
                            Description = model.Description,
                            IsActive = model.IsActive
                        };
                        if (model.AdditionalChargeId == 0)
                        {
                            input.CreationTime = DateTime.Now;
                            input.CreatorUserId = UserId;
                            input.TenantsId = TenantId;
                            _DbContext.AdditionalCharge.Add(input);
                            _DbContext.SaveChanges();
                            model.AdditionalChargeId = input.AdditionalChargeId;
                        }
                        else
                        {
                            var AdditionalCharge = _DbContext.AdditionalCharge.FirstOrDefault(x => x.AdditionalChargeId == input.AdditionalChargeId);
                            if (AdditionalCharge != null)
                            {
                                AdditionalCharge.AdditionalChargeId = model.AdditionalChargeId;
                                AdditionalCharge.Name = model.Name;
                                AdditionalCharge.UnitPriceType = model.UnitPriceType;
                                AdditionalCharge.UnitPrice = model.UnitPrice;
                                AdditionalCharge.Description = model.Description;
                                AdditionalCharge.IsActive = model.IsActive;
                                AdditionalCharge.LastModificationTime = DateTime.Now;
                                AdditionalCharge.LastModifierUserId = UserId;
                                _DbContext.AdditionalCharge.Update(AdditionalCharge);
                                _DbContext.SaveChanges();
                            }


                        }
                    });
                }
                else
                    throw new Exception("input not allow null");
            }
            catch (Exception)
            {

                throw;
            }
            return model.AdditionalChargeId;
        }

        public async Task<bool> DeleteAdditionalChargeAsync(long id)
        {
            try
            {
                if (id != 0)
                {
                    await Task.Run(() =>
                    {
                        var AdditionalCharge = _DbContext.AdditionalCharge.FirstOrDefault(x => x.AdditionalChargeId == id);
                        if (AdditionalCharge != null)
                        {
                            _DbContext.AdditionalCharge.Remove(AdditionalCharge);
                            _DbContext.SaveChanges();
                        }
                    });
                }
                else
                    throw new Exception("id not zero allow");
            }
            catch (Exception e)
            {

                throw;
            }
            return true;
        }

        public async Task<bool> AdditionalChargeStatusChange(long AdditionalChargeId, bool Statuschange)
        {
            try
            {
                if (AdditionalChargeId != 0)
                {
                    await Task.Run(() =>
                    {
                        var charge = _DbContext.AdditionalCharge.FirstOrDefault(x => x.AdditionalChargeId == AdditionalChargeId);
                        if (charge != null)
                        {
                            charge.IsActive = Statuschange;
                            _DbContext.AdditionalCharge.Update(charge);
                            _DbContext.SaveChanges();
                        }
                    });
                }
                else
                    throw new Exception("id not zero allow");
            }
            catch (Exception e)
            {

                throw;
            }
            return true;
        }

        public async Task<List<AdditionalChargeVM>> GetActiveAdditionalChargeAsync(long TenantId)
        {
            List<AdditionalChargeVM> List = new List<AdditionalChargeVM>();
            try
            {
                await Task.Run(() =>
                {
                    var AdditionalChargeList = _DbContext.AdditionalCharge.Where(x => x.TenantsId == TenantId && x.IsActive == true).ToList();
                    if (AdditionalChargeList != null && AdditionalChargeList.Count() > 0)
                    {
                        foreach (var item in AdditionalChargeList)
                        {
                            AdditionalChargeVM model = new AdditionalChargeVM()
                            {
                                AdditionalChargeId = item.AdditionalChargeId,
                                Name = item.Name,
                                UnitPriceType = item.UnitPriceType,
                                UnitPrice = item.UnitPrice,
                                Description = item.Description,
                                IsActive = item.IsActive
                            };
                            List.Add(model);
                        }
                    }

                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return List;
        }
        #endregion Additional Charge Services end

    }
}
