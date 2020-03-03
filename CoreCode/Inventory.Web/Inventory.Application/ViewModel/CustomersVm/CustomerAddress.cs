using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.CustomersVm
{
  public  class CustomerAddress
    {
        public long CustomerId { get; set; }
        public CustomerAddressVm BillingAddress { get; set; }
        public CustomerAddressVm ShippingAddress { get; set; }
    }
}
