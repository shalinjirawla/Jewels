using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel
{
    public class CustomerVm
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerTypeId { get; set; }
        public string CustomerCode { get; set; }
        public string Website { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string Remarks { get; set; }
        public Nullable<long> DefaultCreditTerms { get; set; }
        public string DefaultCreditLimit { get; set; }
        public string DiscountOption { get; set; }
        public string DiscountAmount { get; set; }
        public string DefaultCurrency { get; set; }
        
    }
}
