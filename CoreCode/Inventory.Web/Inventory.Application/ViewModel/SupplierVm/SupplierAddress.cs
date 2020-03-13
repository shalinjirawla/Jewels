using Inventory.Application.ViewModel.CustomersVm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SupplierVm
{
   public class SupplierAddress
    {
        public long SupplierId { get; set; }
        public SupplierAddressVm BillingAddress { get; set; }
        public SupplierAddressVm ShippingAddress { get; set; }
    }
}
