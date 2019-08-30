using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Customer
{
    public class CustomerAdderss
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerAddressId { get; set; }
        public string AddressType { get; set; }
        public string Address { get; set; }
        public bool DefaultAddress { get; set; }
        [ForeignKey("Customer")]
        public Nullable<long> CustomerId { get; set; }
        [ForeignKey("Country")]
        public Nullable<long> CountryId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; } 

        public Customer Customer { get; set; }
        public Country Country { get; set; }
    }
}
