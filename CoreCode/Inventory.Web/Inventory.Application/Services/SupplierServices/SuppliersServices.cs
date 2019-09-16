using Inventory.Application.Interface.Supplier;
using Inventory.Application.ViewModel.CustomersVm;
using Inventory.Application.ViewModel.SupplierVm;
using Inventory.Core.Models.Customer;
using Inventory.Core.Models.Supplier;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services.SupplierServices
{
    class SuppliersServices : ISupplier
    {
        private readonly ApplicationDbContext _DbContext;
        public Boolean Status = false;
        public string Message = "";
        public SuppliersServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<long> AddUpadteSupplier(SupplierVm Model, string UserId, long TenantId)
        {
            long SupplierId = 0;
            try
            {
                Supplier supplier = new Supplier();
                await Task.Run(() =>
                {

                    if (Model != null)
                    {
                        Nullable<long> CountryId = 0;
                        if (Model.SupplierId == 0 && Model.CompanyName != null && Model.CompanyName != "")
                        {
                            supplier.CompanyName = Model.CompanyName;
                            supplier.SupplierCode = Model.SupplierCode;
                            supplier.Website = Model.Website;
                            supplier.Remarks = Model.Remarks;
                            supplier.DefaultCurrency = Model.DefaultCurrency;
                            supplier.DefaultPaymentTerms = Model.DefaultPaymentTerms;
                            supplier.DefaultTaxCode = Model.DefaultTaxCode;
                            supplier.Shipmenmethod = Model.Shipmenmethod;
                            supplier.Shipmenterms = Model.Shipmenterms;
                            supplier.CreatorUserId = UserId;
                            supplier.CreationTime = DateTime.Now;
                            supplier.LastModifierUserId = UserId;
                            supplier.LastModificationTime = DateTime.Now;
                            supplier.IsActive = true;
                            supplier.TenantsId = TenantId;


                            _DbContext.Suppliers.Add(supplier);
                            _DbContext.SaveChanges();
                            SupplierId = supplier.SupplierId;
                            if (Model.AddressList != null)
                            {

                                foreach (var list in Model.AddressList.AddressList)
                                {
                                    Adderss Adderss = new Adderss();
                                    Adderss.AddressType = list.AddressType;
                                    Adderss.Address = list.Address;
                                    Adderss.DefaultAddress = list.DefaultAddress;
                                    Adderss.CustomerId = supplier.SupplierId;
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
                                foreach (var list in Model.ContactList.ContactList)
                                {
                                    Contacts Contacts = new Contacts();

                                    Contacts.CustomerId = supplier.SupplierId;
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
                            var alreadySuppliers = _DbContext.Suppliers.FirstOrDefault(x => x.SupplierId == Model.SupplierId);
                            if (alreadySuppliers != null)
                            {
                                alreadySuppliers.CompanyName = Model.CompanyName;
                                alreadySuppliers.SupplierId = Model.SupplierId;

                                alreadySuppliers.Website = Model.Website;
                                alreadySuppliers.Remarks = Model.Remarks;
                                alreadySuppliers.DefaultCurrency = Model.DefaultCurrency;
                                alreadySuppliers.DefaultPaymentTerms = Model.DefaultPaymentTerms;
                                alreadySuppliers.DefaultTaxCode = Model.DefaultTaxCode;
                                alreadySuppliers.Shipmenmethod = Model.Shipmenmethod;
                                alreadySuppliers.Shipmenterms = Model.Shipmenterms;
                                alreadySuppliers.LastModifierUserId = UserId;
                                alreadySuppliers.LastModificationTime = DateTime.Now;
                                alreadySuppliers.IsActive = true;

                                _DbContext.Update(alreadySuppliers);
                                _DbContext.SaveChanges();
                                if (Model.AddressList != null)
                                {
                                    var deleteaddresslist = _DbContext.Addersses.Where(x => x.CustomerId == alreadySuppliers.SupplierId).ToList();
                                    _DbContext.Addersses.RemoveRange(deleteaddresslist);
                                    foreach (var list in Model.AddressList.AddressList)
                                    {
                                        Adderss Adderss = new Adderss();
                                        Adderss.AddressType = list.AddressType;
                                        Adderss.Address = list.Address;
                                        Adderss.DefaultAddress = list.DefaultAddress;
                                        Adderss.CustomerId = alreadySuppliers.SupplierId;
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
                                    var deletecontactlist = _DbContext.Contacts.Where(x => x.CustomerId == alreadySuppliers.SupplierId).ToList();
                                    _DbContext.Contacts.RemoveRange(deletecontactlist);
                                    foreach (var list in Model.ContactList.ContactList)
                                    {

                                        if (list.contactId != null && list.contactId != "")
                                        {
                                            Contacts Contacts = new Contacts();
                                            Contacts.CustomerId = alreadySuppliers.SupplierId;
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
            return SupplierId;
        }

        public async Task<bool> DeleteSupplierAsyc(long Id)
        {
            try
            {
                Status = false;
                await Task.Run(() =>
                {
                    var SupplierAddress = _DbContext.Addersses.Where(x => x.SupplierId == Id).ToList();
                    var Supplier = _DbContext.Suppliers.Where(x => x.SupplierId == Id).FirstOrDefault();
                    _DbContext.Addersses.RemoveRange(SupplierAddress);
                    _DbContext.Suppliers.Remove(Supplier);
                    _DbContext.SaveChanges();
                    Status = true;
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return Status;
        }

        public async Task<SupplierVm> GetSupplierByIdAsyc(long Id)
        {
            SupplierVm SupplierVm = new SupplierVm();
            try
            {
                await Task.Run(() =>
                {
                    var SupplierAddressList = _DbContext.Addersses.Where(x => x.SupplierId == Id).ToList();
                    List<SuppliersAddressList> Address = new List<SuppliersAddressList>();
                    SuppliersAddressList AddressList = new SuppliersAddressList();
                    foreach (var address in SupplierAddressList)
                    {
                        SupplierAddressVm Supplieraddress = new SupplierAddressVm();
                        Supplieraddress.addressId = address.AddressId.ToString();
                        Supplieraddress.AddressType = address.AddressType;
                        Supplieraddress.Address = address.Address;
                        Supplieraddress.DefaultAddress = address.DefaultAddress;
                        Supplieraddress.CountryId = address.CountryId.ToString();
                        Supplieraddress.State = address.State;
                        Supplieraddress.City = address.City;
                        Supplieraddress.PostalCode = address.PostalCode;
                        AddressList.AddressList.Add(Supplieraddress);
                    }
                    SupplierVm.AddressList = AddressList;

                    var SupplierContactList = _DbContext.Contacts.Where(x => x.SupplierId == Id).ToList();
                    List<SupplierContactVm> supplierContactList = new List<SupplierContactVm>();
                    SuplliersContactList ContactList = new SuplliersContactList();
                    foreach (var contact in SupplierContactList)
                    {
                        SupplierContactVm SupplierContactVm = new SupplierContactVm();
                        SupplierContactVm.contactId = contact.ContactId.ToString();
                        SupplierContactVm.Designation = contact.Designation;
                        SupplierContactVm.Email = contact.Email;
                        SupplierContactVm.FirstName = contact.FirstName;
                        SupplierContactVm.LastName = contact.LastName;
                        SupplierContactVm.DefaultContact = contact.DefaultContact;
                        SupplierContactVm.Mobile = contact.Mobile;
                        SupplierContactVm.Fax = contact.Fax;
                        SupplierContactVm.Office = contact.Office;
                        SupplierContactVm.CountryId = contact.CountryId;
                        ContactList.ContactList.Add(SupplierContactVm);

                    }
                    SupplierVm.ContactList = ContactList;

                    var SupplierDetail = _DbContext.Suppliers.Where(x => x.SupplierId == Id).FirstOrDefault();
                    if (SupplierDetail != null)
                    {
                        SupplierVm.SupplierId = SupplierDetail.SupplierId;
                        SupplierVm.CompanyName = SupplierDetail.CompanyName;
                        SupplierVm.SupplierCode = SupplierDetail.SupplierCode;
                        SupplierVm.Website = SupplierDetail.Website;
                        SupplierVm.Remarks = SupplierDetail.Remarks;
                        SupplierVm.DefaultCurrency = SupplierDetail.DefaultCurrency;
                        SupplierVm.DefaultTaxCode = SupplierDetail.DefaultTaxCode;
                        SupplierVm.DefaultPaymentTerms = SupplierDetail.DefaultPaymentTerms;
                        SupplierVm.Shipmenmethod = SupplierDetail.Shipmenmethod;
                        SupplierVm.Shipmenterms = SupplierDetail.Shipmenterms;
                    }
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return SupplierVm;
        }

        public Task<List<SupplierVm>> GetSupplierListAsyn()
        {
            throw new NotImplementedException();
        }


    }
}
