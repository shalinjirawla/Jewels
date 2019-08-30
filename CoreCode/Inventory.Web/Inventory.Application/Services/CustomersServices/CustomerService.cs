using Inventory.Application.Interface;
using Inventory.Application.ViewModel;
using Inventory.Core.Models.Customer;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class CustomerService : ICustomer
    {
        private readonly ApplicationDbContext _DbContext;

        public CustomerService(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<long> AddCustomer(CustomerVm Model)
        {
            long CustomerId = 0;
            try
            {
                await Task.Run(() => { 
                Customer customer = new Customer();
                if (Model != null)
                {
                    if (Model.CustomerId == 0 && Model.CustomerName != null && Model.CustomerName != "")
                    {
                        customer.CustomerName = Model.CustomerName;
                        customer.CusromerCode = Model.CustomerCode;
                        int typeid;
                        typeid = int.Parse(Model.CustomerTypeId);
                        if (typeid != 0 && typeid > 0)
                        {
                            customer.CustomerTypeId = typeid;
                        }
                        customer.Website = Model.Website;
                        customer.TaxRegistrationNumber = Model.TaxRegistrationNumber;
                        customer.Remarks = Model.Remarks;
                        customer.DefaultCreditTerms = Model.DefaultCreditTerms;
                        customer.DefaultCreditLimit = Model.DefaultCreditLimit != null && Model.DefaultCreditLimit != "" ? long.Parse(Model.DefaultCreditLimit) : 0;
                        customer.DiscountOption = Model.DiscountOption != null && Model.DiscountOption != "" ? long.Parse(Model.DiscountOption) : 0;
                        if (customer.DiscountOption == 1)
                        {
                            customer.DiscountPercentage = Model.DiscountPercentage != null ? double.Parse(Model.DiscountPercentage) : 0;
                            customer.DiscountAmount = Model.DiscountAmount != null && Model.DiscountAmount != "" ? double.Parse(Model.DiscountAmount) : 0;
                        }
                        else if (customer.DiscountOption == 2)
                        {
                            customer.DiscountPercentage = Model.DiscountPercentage != null && Model.DiscountPercentage != "" ? double.Parse(Model.DiscountPercentage) : 0;
                            customer.DiscountAmount = Model.DiscountAmount != null && Model.DiscountAmount != "" ? double.Parse(Model.DiscountAmount) : 0;
                        }
                        else
                        {
                            customer.DiscountPercentage = 0;
                            customer.DiscountAmount = 0;
                        }
                        customer.DefaultCurrency = Model.DefaultCurrency != null && Model.DefaultCurrency != "" ? long .Parse(Model.DefaultCurrency) : 0; 
                    }
                    _DbContext.Customers.Add(customer);
                    _DbContext.SaveChanges();
                    CustomerId = customer.CustomerId;
                }
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return CustomerId;
        }

        public List<CurrencyVm> GetCurrencyList()
        {
            List<CurrencyVm> CurrencyList = new List<CurrencyVm>();
            try
            {
                
                var a =_DbContext.Currencies.ToList();
                foreach(var c in a)
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

        public List<CustomerTypeVm> GetCustomerTypeList()
        {
            List<CustomerTypeVm> customerTypeList = new List<CustomerTypeVm>();
            try
            {
                var a = _DbContext.CustomerTypes.ToList();
                foreach(var h in a)
                {
                    CustomerTypeVm customerType = new CustomerTypeVm();
                    customerType.CustomerTypeId = h.CustomerTypeId;
                    customerType.CustomerTypeName = h.CustomerTypeName;
                    customerTypeList.Add(customerType);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return customerTypeList;
        }
        public List<CountryVm> GetCountryList()
        {
            List<CountryVm> countryList = new List<CountryVm>();
            try
            {
                var countries = _DbContext.country.ToList();
                foreach (var a in countries)
                {
                    CountryVm country = new CountryVm();
                    country.CountryId = a.CountryId;
                    country.CountryName = a.CountryName;
                    countryList.Add(country);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return countryList;
        }
    }
}
