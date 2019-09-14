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

        public async Task<long> AddCustomer(AddCustomerVm Model, string UserId, long TenantId)
        {
            long CustomerId = 0;
            try
            {
                Customer customer = new Customer();
                await Task.Run(() =>
                {

                    if (Model != null)
                    {
                        Nullable<long> CountryId = 0;
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
                            customer.CreditTermId = Model.DefaultCreditTerms;
                            customer.DefaultCreditLimit = Model.DefaultCreditLimit != null && Model.DefaultCreditLimit != "" ? long.Parse(Model.DefaultCreditLimit) : 0;
                            if (Model.DiscountOption != null && Model.DiscountOption != "" && Model.DiscountOption != "0")
                            {
                                customer.DiscountOption = long.Parse(Model.DiscountOption);

                            }
                            if (customer.DiscountOption != 0 && customer.DiscountOption != null)
                            {
                                customer.DiscountAmount = Model.DiscountAmount != null && Model.DiscountAmount != "" ? double.Parse(Model.DiscountAmount) : 0;
                            }
                            else
                            {
                                customer.DiscountAmount = 0;
                            }
                            if (Model.DefaultCurrency != null && Model.DefaultCurrency != "" && Model.DefaultCurrency != "0")
                            {
                                customer.DefaultCurrency = long.Parse(Model.DefaultCurrency);

                            }
                            customer.CreatorUserId = UserId;
                            customer.CreationTime = new DateTime();
                            customer.LastModifierUserId = UserId;
                            DateTime date = new DateTime();
                            customer.LastModificationTime = date;
                            customer.IsActive = true;
                            customer.TenantsId = TenantId;


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
                                    if (customerAdderss.DefaultAddress)
                                    {
                                        CountryId = customerAdderss.CountryId;
                                    }
                                    customerAdderss.State = list.State;
                                    customerAdderss.City = list.City;
                                    customerAdderss.PostalCode = list.PostalCode;

                                    _DbContext.customerAddersses.Add(customerAdderss);
                                    _DbContext.SaveChanges();

                                }

                            }
                            if (Model.ContactList != null)
                            {
                                foreach (var list in Model.ContactList.Contact)
                                {
                                    CustomerContacts customerContacts = new CustomerContacts();

                                    customerContacts.CustomerId = customer.CustomerId;
                                    customerContacts.Designation = list.Designation;
                                    customerContacts.Email = list.Email;
                                    customerContacts.FirstName = list.FirstName;
                                    customerContacts.LastName = list.LastName;
                                    customerContacts.DefaultContact = list.DefaultContact;
                                    customerContacts.Mobile = list.Mobile;
                                    customerContacts.CountryId = CountryId;
                                    customerContacts.Fax = list.Fax;
                                    customerContacts.Office = list.Office;

                                    _DbContext.customerContacts.Add(customerContacts);
                                    _DbContext.SaveChanges();

                                }
                            }
                        }

                        else
                        {
                            var alreadycustomer = _DbContext.Customers.FirstOrDefault(x => x.CustomerId == Model.CustomerId);
                            if (alreadycustomer != null)
                            {
                                customer.CustomerId = Model.CustomerId;
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
                                customer.CreditTermId = Model.DefaultCreditTerms;
                                customer.DefaultCreditLimit = Model.DefaultCreditLimit != null && Model.DefaultCreditLimit != "" ? long.Parse(Model.DefaultCreditLimit) : 0;
                                if (Model.DiscountOption != null && Model.DiscountOption != "" && Model.DiscountOption != "0")
                                {
                                    customer.DiscountOption = long.Parse(Model.DiscountOption);

                                }
                                if (customer.DiscountOption != 0 && customer.DiscountOption != null)
                                {
                                    customer.DiscountAmount = Model.DiscountAmount != null && Model.DiscountAmount != "" ? double.Parse(Model.DiscountAmount) : 0;
                                }
                                else
                                {
                                    customer.DiscountAmount = 0;
                                }
                                if (Model.DefaultCurrency != null && Model.DefaultCurrency != "" && Model.DefaultCurrency != "0")
                                {
                                    customer.DefaultCurrency = long.Parse(Model.DefaultCurrency);

                                }
                                customer.CreatorUserId = alreadycustomer.CreatorUserId;
                                customer.CreationTime = alreadycustomer.CreationTime;
                                customer.LastModifierUserId = "1";
                                DateTime date = new DateTime();
                                customer.LastModificationTime = date;
                                customer.IsActive = true;

                                _DbContext.Update(customer);
                                _DbContext.SaveChanges();
                                if (Model.AddressList != null)
                                {
                                    var deleteaddresslist = _DbContext.customerAddersses.Where(x => x.CustomerId == customer.CustomerId).ToList();
                                    _DbContext.customerAddersses.RemoveRange(deleteaddresslist);
                                    foreach (var list in Model.AddressList.Address)
                                    {
                                        CustomerAdderss customerAdderss = new CustomerAdderss();
                                        customerAdderss.AddressType = list.AddressType;
                                        customerAdderss.Address = list.Address;
                                        customerAdderss.DefaultAddress = list.DefaultAddress;
                                        customerAdderss.CustomerId = customer.CustomerId;
                                        customerAdderss.CountryId = list.CountryId != null && list.CountryId != "" ? long.Parse(list.CountryId) : 0;
                                        if (customerAdderss.DefaultAddress)
                                        {
                                            CountryId = customerAdderss.CountryId;
                                        }
                                        customerAdderss.State = list.State;
                                        customerAdderss.City = list.City;
                                        customerAdderss.PostalCode = list.PostalCode;

                                        _DbContext.customerAddersses.Add(customerAdderss);
                                        _DbContext.SaveChanges();
                                    }

                                }
                                if (Model.ContactList != null)
                                {
                                    var deletecontactlist = _DbContext.customerContacts.Where(x => x.CustomerId == customer.CustomerId).ToList();
                                    _DbContext.customerContacts.RemoveRange(deletecontactlist);
                                    foreach (var list in Model.ContactList.Contact)
                                    {

                                        if (list.contactId != null && list.contactId != "")
                                        {
                                            CustomerContacts customerContacts = new CustomerContacts();
                                            customerContacts.CustomerId = customer.CustomerId;
                                            customerContacts.Designation = list.Designation;
                                            customerContacts.Email = list.Email;
                                            customerContacts.CountryId = CountryId;
                                            customerContacts.FirstName = list.FirstName;
                                            customerContacts.LastName = list.LastName;
                                            customerContacts.DefaultContact = list.DefaultContact;
                                            customerContacts.Mobile = list.Mobile;
                                            customerContacts.Fax = list.Fax;
                                            customerContacts.Office = list.Office;
                                            _DbContext.customerContacts.Add(customerContacts);
                                            _DbContext.SaveChanges();

                                        }

                                    }
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

        public List<CustomerVm> GetCustomerListAsyn()
        {
            try
            {
                List<CustomerVm> customerList = new List<CustomerVm>();
                var list = _DbContext.Customers.ToList();
                foreach (var a in list)
                {
                    CustomerVm customer = new CustomerVm();
                    customer.CustomerId = a.CustomerId;
                    customer.CustomerName = a.CustomerName;
                    customer.CustomerTypeId = a.CustomerTypeId.ToString();
                    customer.CustomerCode = a.CusromerCode;
                    customer.Website = a.Website;
                    customer.TaxRegistrationNumber = a.TaxRegistrationNumber;
                    customer.Remarks = a.Remarks;
                    customer.DefaultCreditTerms = a.CreditTermId;
                    customer.DefaultCurrency = a.DefaultCurrency.ToString();
                    customer.DiscountOption = a.DiscountOption.ToString();
                    customer.DiscountAmount = a.DiscountAmount.ToString();
                    customerList.Add(customer);

                }

                return customerList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AddCustomerVm GetCustomerByIdAsyc(int Id)
        {
            AddCustomerVm customer = new AddCustomerVm();
            try
            {
                var customerAddressList = _DbContext.customerAddersses.Where(x => x.CustomerId == Id).ToList();
                List<CustomerAddressVm> Address = new List<CustomerAddressVm>();
                AddressList AddressList = new AddressList();
                foreach (var address in customerAddressList)
                {
                    CustomerAddressVm customeraddress = new CustomerAddressVm();
                    customeraddress.addressId = address.CustomerAddressId.ToString();
                    customeraddress.AddressType = address.AddressType;
                    customeraddress.Address = address.Address;
                    customeraddress.DefaultAddress = address.DefaultAddress;
                    customeraddress.CountryId = address.CountryId.ToString();
                    customeraddress.State = address.State;
                    customeraddress.City = address.City;
                    customeraddress.PostalCode = address.PostalCode;
                    Address.Add(customeraddress);
                }
                AddressList.Address = Address;
                customer.AddressList = AddressList;

                var customerContactList = _DbContext.customerContacts.Where(x => x.CustomerId == Id).ToList();
                List<CustomerContactVm> Contact = new List<CustomerContactVm>();
                ContactList ContactList = new ContactList();
                foreach (var contact in customerContactList)
                {
                    CustomerContactVm customerContact = new CustomerContactVm();
                    customerContact.contactId = contact.CustomerContactId.ToString();
                    customerContact.Designation = contact.Designation;
                    customerContact.Email = contact.Email;
                    customerContact.FirstName = contact.FirstName;
                    customerContact.LastName = contact.LastName;
                    customerContact.DefaultContact = contact.DefaultContact;
                    customerContact.Mobile = contact.Mobile;
                    customerContact.Fax = contact.Fax;
                    customerContact.Office = contact.Office;
                    customerContact.CountryId = contact.CountryId;
                    Contact.Add(customerContact);

                }
                ContactList.Contact = Contact;
                customer.ContactList = ContactList;

                var customerDetail = _DbContext.Customers.Where(x => x.CustomerId == Id).FirstOrDefault();
                if (customerDetail != null)
                {
                    customer.CustomerId = customerDetail.CustomerId;
                    customer.CustomerName = customerDetail.CustomerName;
                    customer.CustomerTypeId = customerDetail.CustomerTypeId.ToString();
                    customer.CustomerCode = customerDetail.CusromerCode;
                    customer.Website = customerDetail.Website;
                    customer.TaxRegistrationNumber = customerDetail.TaxRegistrationNumber;
                    customer.Remarks = customerDetail.Remarks;
                    customer.DefaultCreditTerms = customerDetail.CreditTermId;
                    customer.DefaultCreditLimit = customerDetail.DefaultCreditLimit.ToString();
                    customer.DiscountOption = customerDetail.DiscountOption.ToString();
                    customer.DiscountAmount = customerDetail.DiscountAmount.ToString();
                    customer.DefaultCurrency = customerDetail.DefaultCurrency.ToString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return customer;
        }

        public int DeleteCustomerAsyc(int Id)
        {
            int cId = 0;

            try
            {
                var customerAddress = _DbContext.customerAddersses.Where(x => x.CustomerId == Id).ToList();
                var customerContact = _DbContext.customerContacts.Where(x => x.CustomerId == Id).ToList();
                var customer = _DbContext.Customers.Where(x => x.CustomerId == Id).FirstOrDefault();
                _DbContext.customerAddersses.RemoveRange(customerAddress);
                _DbContext.customerContacts.RemoveRange(customerContact);
                _DbContext.Customers.Remove(customer);
                _DbContext.SaveChanges();
                cId = Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return cId;
        }
    }
}
