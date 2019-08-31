using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.CustomersVm
{
    public class CustomerAddressVm
    {
        public string addressId { get; set; }
        public string AddressType { get; set; }
        public string Address { get; set; }
        public bool DefaultAddress { get; set; }
        public string CountryId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
