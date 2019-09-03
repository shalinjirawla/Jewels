using Inventory.Application.Interface;
using Inventory.Application.ViewModel;
using Inventory.Application.ViewModel.CustomersVm;
using Inventory.Core.Models.Customer;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<long> AddCustomer(AddCustomerVm Model)
        {
            long CustomerId = 0;
            try
            {
                Customer customer = new Customer();
                await Task.Run(() =>
                {

                    if (Model != null)
                    {
                        if (Model.CustomerId == 0 && Model.CustomerName != null && Model.CustomerName != "")
                        {
                            customer.CustomerName = Model.CustomerName;
                            customer.CusromerCode = Model.CustomerCode;
                            int typeid;
                            typeid = Model.CustomerTypeId != null && Model.CustomerTypeId != "" ? int.Parse(Model.CustomerTypeId) : 0;
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
                            if (customer.DiscountOption != 0)
                            {
                                customer.DiscountAmount = Model.DiscountAmount != null && Model.DiscountAmount != "" ? double.Parse(Model.DiscountAmount) : 0;
                            }
                            else
                            {
                                customer.DiscountAmount = 0;
                            }
                            customer.DefaultCurrency = Model.DefaultCurrency != null && Model.DefaultCurrency != "" ? long.Parse(Model.DefaultCurrency) : 0;
                            customer.CreatorUserId = 1;
                            customer.LastModifierUserId = 1;
                            DateTime date = new DateTime();
                            customer.LastModificationTime = date;
                            customer.IsActive = true;


                            _DbContext.Customers.Add(customer);
                            _DbContext.SaveChanges();
                            if (Model.AddressList != null)
                            {
                                
                                foreach (var list in Model.AddressList.Address)
                                {
                                    CustomerAdderss customerAdderss = new CustomerAdderss();
                                    customerAdderss.AddressType = list.AddressType;
                                    customerAdderss.Address = list.Address;
                                    customerAdderss.DefaultAddress = list.DefaultAddress;
                                    customerAdderss.CustomerId = customer.CustomerId;
                                    customerAdderss.CountryId = list.CountryId != null && list.CountryId != "" ? long.Parse(list.CountryId) : 0;
                                    customerAdderss.State = list.State;
                                    customerAdderss.City = list.City;
                                    customerAdderss.PostalCode = list.PostalCode;

                                    _DbContext.customerAddersses.Add(customerAdderss);
                                    _DbContext.SaveChanges();

                                }

                            }
                            if (Model.ContactList != null)
                            {
                                foreach(var list in Model.ContactList.Contact)
                                {
                                    CustomerContacts customerContacts = new CustomerContacts();

                                    customerContacts.CustomerId = customer.CustomerId;
                                    customerContacts.Designation = list.Designation;
                                    customerContacts.Email = list.Email;
                                    customerContacts.FirstName = list.FirstName;
                                    customerContacts.LastName = list.LastName;
                                    customerContacts.Mobile = list.Mobile;
                                    customerContacts.Fax = list.Fax;
                                    customerContacts.Office = list.Office;

                                    _DbContext.customerContacts.Add(customerContacts);
                                    _DbContext.SaveChanges();

                                }
                            }
                        }



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

        public List<CustomerTypeVm> GetCustomerTypeList()
        {
            List<CustomerTypeVm> customerTypeList = new List<CustomerTypeVm>();
            try
            {
                var a = _DbContext.CustomerTypes.ToList();
                foreach (var h in a)
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
