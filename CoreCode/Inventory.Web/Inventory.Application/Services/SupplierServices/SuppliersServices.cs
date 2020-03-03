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
    public class SuppliersServices : ISupplier
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
                            supplier.DefaultCurrency =Model.DefaultCurrency!=0 ? Model.DefaultCurrency :null;
                            supplier.DefaultPaymentTerms = Model.DefaultPaymentTerms!=0 ? Model.DefaultPaymentTerms : null;
                            supplier.DefaultTaxCode = Model.DefaultTaxCode!=0?Model.DefaultTaxCode:null;
                            supplier.Shipmenmethod = Model.Shipmentmethod!=0?Model.Shipmentmethod:null;
                            supplier.Shipmenterms = Model.Shipmentterms!=0 ? Model.Shipmentterms:null;
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

                                foreach (var list in Model.AddressList.Address)
                                {
                                    Adderss Adderss = new Adderss();
                                    Adderss.AddressType = list.AddressType;
                                    Adderss.Address = list.Address;
                                    Adderss.DefaultAddress = list.DefaultAddress;
                                    Adderss.SupplierId = supplier.SupplierId;
                                    Adderss.CountryId = list.CountryId != null && list.CountryId != "" ? long.Parse(list.CountryId) : 0;
                                    if (Adderss.DefaultAddress)
                                    {
                                        CountryId = Adderss.CountryId;
                                    }
                                    Adderss.State = list.State;
                                    Adderss.City = list.City;
                                    Adderss.PostalCode = list.PostalCode;
                                    Adderss.TenantId = TenantId;
                                    _DbContext.Addersses.Add(Adderss);
                                    _DbContext.SaveChanges();

                                }

                            }
                            if (Model.ContactList != null)
                            {
                                foreach (var list in Model.ContactList.Contact)
                                {
                                    Contacts Contacts = new Contacts();

                                    Contacts.SupplierId = supplier.SupplierId;
                                    Contacts.Designation = list.Designation;
                                    Contacts.Email = list.Email;
                                    Contacts.FirstName = list.FirstName;
                                    Contacts.LastName = list.LastName;
                                    Contacts.DefaultContact = list.DefaultContact;
                                    Contacts.Mobile = list.Mobile;
                                    Contacts.CountryId = CountryId;
                                    Contacts.Fax = list.Fax;
                                    Contacts.Office = list.Office;
                                    Contacts.TenantId = TenantId;
                                    _DbContext.Contacts.Add(Contacts);
                                    _DbContext.SaveChanges();

                                }
                            }
                        }

                        else
                        {
                            var alreadySuppliers = _DbContext.Suppliers.FirstOrDefault(x => x.SupplierId == Model.SupplierId);
                            SupplierId = alreadySuppliers.SupplierId;
                            if (alreadySuppliers != null)
                            {
                                alreadySuppliers.CompanyName = Model.CompanyName;
                                alreadySuppliers.SupplierId = Model.SupplierId;

                                alreadySuppliers.Website = Model.Website;
                                alreadySuppliers.Remarks = Model.Remarks;
                                alreadySuppliers.DefaultCurrency = Model.DefaultCurrency;
                                alreadySuppliers.DefaultPaymentTerms = Model.DefaultPaymentTerms;
                                alreadySuppliers.DefaultTaxCode = Model.DefaultTaxCode;
                                alreadySuppliers.Shipmenmethod = Model.Shipmentmethod;
                                alreadySuppliers.Shipmenterms = Model.Shipmentterms;
                                alreadySuppliers.LastModifierUserId = UserId;
                                alreadySuppliers.LastModificationTime = DateTime.Now;
                                alreadySuppliers.IsActive = true;
                                alreadySuppliers.TenantsId = TenantId;
                                _DbContext.Update(alreadySuppliers);
                                _DbContext.SaveChanges();
                                if (Model.AddressList != null)
                                {
                                    var deleteaddresslist = _DbContext.Addersses.Where(x => x.SupplierId == alreadySuppliers.SupplierId).ToList();
                                    _DbContext.Addersses.RemoveRange(deleteaddresslist);
                                    _DbContext.SaveChanges();
                                    foreach (var list in Model.AddressList.Address)
                                    {
                                        Adderss Adderss = new Adderss();
                                        Adderss.AddressType = list.AddressType;
                                        Adderss.Address = list.Address;
                                        Adderss.DefaultAddress = list.DefaultAddress;
                                        Adderss.SupplierId = alreadySuppliers.SupplierId;
                                        Adderss.CountryId = list.CountryId != null && list.CountryId != "" ? long.Parse(list.CountryId) : 0;
                                        if (Adderss.DefaultAddress)
                                        {
                                            CountryId = Adderss.CountryId;
                                        }
                                        Adderss.State = list.State;
                                        Adderss.City = list.City;
                                        Adderss.PostalCode = list.PostalCode;
                                        Adderss.TenantId = TenantId;
                                        _DbContext.Addersses.Add(Adderss);
                                        _DbContext.SaveChanges();
                                    }

                                }
                                if (Model.ContactList != null)
                                {
                                    var deletecontactlist = _DbContext.Contacts.Where(x => x.SupplierId == alreadySuppliers.SupplierId).ToList();
                                    _DbContext.Contacts.RemoveRange(deletecontactlist);
                                    _DbContext.SaveChanges();
                                    foreach (var list in Model.ContactList.Contact)
                                    {

                                        if (list.contactId != null && list.contactId != "")
                                        {
                                            Contacts Contacts = new Contacts();
                                            Contacts.SupplierId = alreadySuppliers.SupplierId;
                                            Contacts.Designation = list.Designation;
                                            Contacts.Email = list.Email;
                                            Contacts.CountryId = CountryId;
                                            Contacts.FirstName = list.FirstName;
                                            Contacts.LastName = list.LastName;
                                            Contacts.DefaultContact = list.DefaultContact;
                                            Contacts.Mobile = list.Mobile;
                                            Contacts.Fax = list.Fax;
                                            Contacts.Office = list.Office;
                                            Contacts.TenantId = TenantId;
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

        public async Task<Boolean> DeleteSupplier(long Id)
        {
            try
            {
                Status = false;
                await Task.Run(() =>
                {
                    var SupplierAddress = _DbContext.Addersses.Where(x => x.SupplierId == Id).ToList();
                    var SupplierContact = _DbContext.Contacts.Where(x => x.SupplierId == Id).ToList();
                    var Supplier = _DbContext.Suppliers.Where(x => x.SupplierId == Id).FirstOrDefault();
                    _DbContext.Contacts.RemoveRange(SupplierContact);
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

        public async Task<List<DefaultSupplierList>> GetDefaultSupplierList()
        {
            List<DefaultSupplierList> list = new List<DefaultSupplierList>();
            try
            {
                await Task.Run(() =>
                {
                    var supplierlist = _DbContext.Suppliers.ToList();
                    if (supplierlist != null && supplierlist.Count > 0)
                    {
                        foreach (var item in supplierlist)
                        {
                            DefaultSupplierList dto = new DefaultSupplierList();
                            dto.SupplierId = item.SupplierId;
                            dto.SupplierCode = item.SupplierCode;
                            dto.CompanyName = item.CompanyName;
                            list.Add(dto);
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

        public async Task<SupplierVm> GetSupplierById(long Id)
        {
            SupplierVm SupplierVm = new SupplierVm();
            try
            {
                await Task.Run(() =>
                {
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
                        SupplierVm.Shipmentmethod = SupplierDetail.Shipmenmethod;
                        SupplierVm.Shipmentterms = SupplierDetail.Shipmenterms;
                    }
                    var SupplierAddressList = _DbContext.Addersses.Where(x => x.SupplierId == Id).ToList();
                    
                    List<SupplierAddressVm> SupplierAddressVmlist = new List<SupplierAddressVm>();
                    SuppliersAddressList SuppliersAddressList = new SuppliersAddressList();
                    foreach (var itemaddress in SupplierAddressList)
                    {
                        SupplierAddressVm address = new SupplierAddressVm();
                        address.addressId = Convert.ToString(itemaddress.AddressId);
                        address.Address = itemaddress.Address;
                        address.AddressType = itemaddress.AddressType;
                        address.City = itemaddress.City;
                        address.CountryId = Convert.ToString(itemaddress.CountryId);
                        address.DefaultAddress = itemaddress.DefaultAddress;
                        address.PostalCode = itemaddress.PostalCode;
                        address.State = itemaddress.State;
                        SupplierAddressVmlist.Add(address);
                    }
                    SuppliersAddressList.Address = SupplierAddressVmlist;
                    SupplierVm.AddressList = SuppliersAddressList;
                    var SupplierContactList = _DbContext.Contacts.Where(x => x.SupplierId == Id).ToList();
                    List<SupplierContactVm> SupplierContactVmlist = new List<SupplierContactVm>();
                    SuplliersContactList SuplliersContactList = new SuplliersContactList();
                    foreach (var contactitem in SupplierContactList)
                    {
                        SupplierContactVm contact = new SupplierContactVm();
                        contact.contactId = Convert.ToString(contactitem.ContactId);
                        contact.CountryId = contactitem.CountryId;
                        contact.DefaultContact = contactitem.DefaultContact;
                        contact.Designation = contactitem.Designation;
                        contact.Email = contactitem.Email;
                        contact.Fax = contactitem.Fax;
                        contact.FirstName = contactitem.FirstName;
                        contact.LastName = contactitem.LastName;
                        contact.Office = contactitem.Office;
                        contact.Mobile = contactitem.Mobile;
                        SupplierContactVmlist.Add(contact);
                    }
                    SuplliersContactList.Contact = SupplierContactVmlist;
                    SupplierVm.ContactList = SuplliersContactList;


                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return SupplierVm;
        }

        public async Task<List<SupplierVm>> GetSupplierList()
        {
            List<SupplierVm> supplierList = new List<SupplierVm>();
            try
            {
                await Task.Run(() =>
                {
                    var suppliersListdata = _DbContext.Suppliers.Where(x => x.IsActive == true).ToList();

                    if (suppliersListdata != null)
                    {
                        foreach (var item in suppliersListdata)
                        {
                            SupplierVm supplier = new SupplierVm();
                            supplier.SupplierId = item.SupplierId;
                            supplier.CompanyName = item.CompanyName;
                            supplier.SupplierCode = item.SupplierCode;
                            supplier.Website = item.Website;
                            supplier.Remarks = item.Remarks;
                            supplier.DefaultCurrency = item.DefaultCurrency;
                            supplier.DefaultPaymentTerms = item.DefaultPaymentTerms;
                            supplier.DefaultTaxCode = item.DefaultTaxCode;
                            supplier.Shipmentmethod = item.Shipmenmethod;
                            supplier.Shipmentterms = item.Shipmenterms;
                            var suppliersaddress = _DbContext.Addersses.Where(x => x.SupplierId == item.SupplierId).ToList();
                            if (suppliersaddress != null)
                            {
                              
                                List<SupplierAddressVm> SupplierAddressVmlist = new List<SupplierAddressVm>();
                                SuppliersAddressList SuppliersAddressList = new SuppliersAddressList();
                                foreach (var itemaddress in suppliersaddress)
                                {
                                    SupplierAddressVm address = new SupplierAddressVm();
                                    address.addressId = Convert.ToString(itemaddress.AddressId);
                                    address.Address = itemaddress.Address;
                                    address.AddressType = itemaddress.AddressType;
                                    address.City = itemaddress.City;
                                    address.CountryId = Convert.ToString(itemaddress.CountryId);
                                    address.DefaultAddress = itemaddress.DefaultAddress;
                                    address.PostalCode = itemaddress.PostalCode;
                                    address.State = itemaddress.State;
                                    SupplierAddressVmlist.Add(address);
                                }
                                SuppliersAddressList.Address = SupplierAddressVmlist;
                                supplier.AddressList = SuppliersAddressList;
                            }
                            
                            var supplierConatct = _DbContext.Contacts.Where(x => x.SupplierId == item.SupplierId).ToList();
                            if (supplierConatct != null)
                            {
                                List<SupplierContactVm> SupplierContactVmlist = new List<SupplierContactVm>();
                                SuplliersContactList SuplliersContactList = new SuplliersContactList();
                                foreach (var contactitem in supplierConatct)
                                {
                                    SupplierContactVm contact = new SupplierContactVm();
                                    contact.contactId = Convert.ToString(contactitem.ContactId);
                                    contact.CountryId = contactitem.CountryId;
                                    contact.DefaultContact = contactitem.DefaultContact;
                                    contact.Designation = contactitem.Designation;
                                    contact.Email = contactitem.Email;
                                    contact.Fax = contactitem.Fax;
                                    contact.FirstName = contactitem.FirstName;
                                    contact.LastName = contactitem.LastName;
                                    contact.Office = contactitem.Office;
                                    contact.Mobile = contactitem.Mobile;
                                    SupplierContactVmlist.Add(contact);
                                }
                                SuplliersContactList.Contact = SupplierContactVmlist;
                                supplier.ContactList=SuplliersContactList;
                            }
                            supplierList.Add(supplier);
                        }
                       
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return supplierList;
        }


    }
}
