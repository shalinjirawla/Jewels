using Inventory.Application.ViewModel.CustomersVm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SupplierVm
{
  public  class SupplierVm
    {
        public long SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string SupplierCode { get; set; }
        public string Website { get; set; }
        public string Remarks { get; set; }
        public Nullable<long> DefaultCurrency { get; set; }
        public Nullable<long> DefaultPaymentTerms { get; set; }
        public Nullable<long> DefaultTaxCode { get; set; }
        public Nullable<long> Shipmenterms { get; set; }
        public Nullable<long> Shipmenmethod { get; set; }
        public SuppliersAddressList AddressList { get; set; }
        public SuplliersContactList ContactList { get; set; }
    }
    public class SuplliersContactList
    {
        public List<SupplierContactVm> ContactList { get; set; }
    }
    public class SuppliersAddressList
    {
        public List<SupplierAddressVm>  AddressList { get; set; }
    }
}
