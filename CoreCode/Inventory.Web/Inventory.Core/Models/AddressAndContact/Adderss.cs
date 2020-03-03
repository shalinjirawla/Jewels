using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Customer
{
    public class Adderss
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AddressId { get; set; }
        public string AddressType { get; set; }//billing address ==1  && shipping address ==2
        public string Address { get; set; }
        public bool DefaultAddress { get; set; }
        [ForeignKey("Customer")]
        public Nullable<long> CustomerId { get; set; }
        [ForeignKey("Supplier")]
        public Nullable<long> SupplierId { get; set; }
        [ForeignKey("Country")]
        public Nullable<long> CountryId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; } 
        [ForeignKey("Tenants")]
        public Nullable<long> TenantId { get; set; }
        public Supplier.Supplier Supplier { get; set; }
        public Customer Customer { get; set; }
        public Country Country { get; set; }
        public Tenants.Tenants Tenants { get; set; }
    }
}
