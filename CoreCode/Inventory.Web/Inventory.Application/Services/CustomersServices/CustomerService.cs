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
                                    Adderss Adderss = new Adderss();
                                    Adderss.AddressType = list.AddressType;
                                    Adderss.Address = list.Address;
                                    Adderss.DefaultAddress = list.DefaultAddress;
                                    Adderss.CustomerId = customer.CustomerId;
                                    Adderss.CountryId = list.CountryId != null && list.CountryId != "" ? long.Parse(list.CountryId) : 0;
                                    if (Adderss.DefaultAddress)
                                    {
                                        CountryId = Adderss.CountryId;
                                    }
                                    Adderss.State = list.State;
                                    Adderss.City = list.City;
                                    Adderss.PostalCode = list.PostalCode;

                                    _DbContext.Addersses.Add(Adderss);
                                    _DbContext.SaveChanges();

                                }

                            }
                            if (Model.ContactList != null)
                            {
                                foreach (var list in Model.ContactList.Contact)
                                {
                                    Contacts Contacts = new Contacts();

                                    Contacts.CustomerId = customer.CustomerId;
                                    Contacts.Designation = list.Designation;
                                    Contacts.Email = list.Email;
                                    Contacts.FirstName = list.FirstName;
                                    Contacts.LastName = list.LastName;
                                    Contacts.DefaultContact = list.DefaultContact;
                                    Contacts.Mobile = list.Mobile;
                                    Contacts.CountryId = CountryId;
                                    Contacts.Fax = list.Fax;
                                    Contacts.Office = list.Office;

                                    _DbContext.Contacts.Add(Contacts);
                                    _DbContext.SaveChanges();

                                }
                            }
                        }

                        else
                        {
                            var alreadycustomer = _DbContext.Customers.FirstOrDefault(x => x.CustomerId == Model.CustomerId);
                            if (alreadycustomer != null)
                            {
                                alreadycustomer.CustomerName = Model.CustomerName;
                                alreadycustomer.CusromerCode = Model.CustomerCode;
                                int typeid;
                                typeid = Model.CustomerTypeId != null && Model.CustomerTypeId != "" ? int.Parse(Model.CustomerTypeId) : 0;
                                if (typeid != 0 && typeid > 0)
                                {
                                    alreadycustomer.CustomerTypeId = typeid;
                                }
                                alreadycustomer.Website = Model.Website;
                                alreadycustomer.TaxRegistrationNumber = Model.TaxRegistrationNumber;
                                alreadycustomer.Remarks = Model.Remarks;
                                alreadycustomer.CreditTermId = Model.DefaultCreditTerms;
                                alreadycustomer.DefaultCreditLimit = Model.DefaultCreditLimit != null && Model.DefaultCreditLimit != "" ? long.Parse(Model.DefaultCreditLimit) : 0;
                                if (Model.DiscountOption != null && Model.DiscountOption != "" && Model.DiscountOption != "0")
                                {
                                    alreadycustomer.DiscountOption = long.Parse(Model.DiscountOption);

                                }
                                if (alreadycustomer.DiscountOption != 0 && alreadycustomer.DiscountOption != null)
                                {
                                    alreadycustomer.DiscountAmount = Model.DiscountAmount != null && Model.DiscountAmount != "" ? double.Parse(Model.DiscountAmount) : 0;
                                }
                                else
                                {
                                    alreadycustomer.DiscountAmount = 0;
                                }
                                if (Model.DefaultCurrency != null && Model.DefaultCurrency != "" && Model.DefaultCurrency != "0")
                                {
                                    alreadycustomer.DefaultCurrency = long.Parse(Model.DefaultCurrency);

                                }
                                alreadycustomer.LastModifierUserId = UserId;
                                DateTime date = new DateTime();
                                alreadycustomer.LastModificationTime = date;
                                alreadycustomer.IsActive = true;

                                _DbContext.Update(alreadycustomer);
                                _DbContext.SaveChanges();
                                if (Model.AddressList != null)
                                {
                                    var deleteaddresslist = _DbContext.Addersses.Where(x => x.CustomerId == alreadycustomer.CustomerId).ToList();
                                    _DbContext.Addersses.RemoveRange(deleteaddresslist);
                                    foreach (var list in Model.AddressList.Address)
                                    {
                                        Adderss Adderss = new Adderss();
                                        Adderss.AddressType = list.AddressType;
                                        Adderss.Address = list.Address;
                                        Adderss.DefaultAddress = list.DefaultAddress;
                                        Adderss.CustomerId = alreadycustomer.CustomerId;
                                        Adderss.CountryId = list.CountryId != null && list.CountryId != "" ? long.Parse(list.CountryId) : 0;
                                        if (Adderss.DefaultAddress)
                                        {
                                            CountryId = Adderss.CountryId;
                                        }
                                        Adderss.State = list.State;
                                        Adderss.City = list.City;
                                        Adderss.PostalCode = list.PostalCode;

                                        _DbContext.Addersses.Add(Adderss);
                                        _DbContext.SaveChanges();
                                    }

                                }
                                if (Model.ContactList != null)
                                {
                                    var deletecontactlist = _DbContext.Contacts.Where(x => x.CustomerId == alreadycustomer.CustomerId).ToList();
                                    _DbContext.Contacts.RemoveRange(deletecontactlist);
                                    foreach (var list in Model.ContactList.Contact)
                                    {

                                        if (list.contactId != null && list.contactId != "")
                                        {
                                            Contacts Contacts = new Contacts();
                                            Contacts.CustomerId = alreadycustomer.CustomerId;
                                            Contacts.Designation = list.Designation;
                                            Contacts.Email = list.Email;
                                            Contacts.CountryId = CountryId;
                                            Contacts.FirstName = list.FirstName;
                                            Contacts.LastName = list.LastName;
                                            Contacts.DefaultContact = list.DefaultContact;
                                            Contacts.Mobile = list.Mobile;
                                            Contacts.Fax = list.Fax;
                                            Contacts.Office = list.Office;
                                            _DbContext.Contacts.Add(Contacts);
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
                var customerAddressList = _DbContext.Addersses.Where(x => x.CustomerId == Id).ToList();
                List<CustomerAddressVm> Address = new List<CustomerAddressVm>();
                AddressList AddressList = new AddressList();
                foreach (var address in customerAddressList)
                {
                    CustomerAddressVm customeraddress = new CustomerAddressVm();
                    customeraddress.addressId = address.AddressId.ToString();
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

                var customerContactList = _DbContext.Contacts.Where(x => x.CustomerId == Id).ToList();
                List<CustomerContactVm> Contact = new List<CustomerContactVm>();
                ContactList ContactList = new ContactList();
                foreach (var contact in customerContactList)
                {
                    CustomerContactVm customerContact = new CustomerContactVm();
                    customerContact.contactId = contact.ContactId.ToString();
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
                var customerAddress = _DbContext.Addersses.Where(x => x.CustomerId == Id).ToList();
                var customerContact = _DbContext.Contacts.Where(x => x.CustomerId == Id).ToList();
                var customer = _DbContext.Customers.Where(x => x.CustomerId == Id).FirstOrDefault();
                _DbContext.Addersses.RemoveRange(customerAddress);
                _DbContext.Contacts.RemoveRange(customerContact);
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
