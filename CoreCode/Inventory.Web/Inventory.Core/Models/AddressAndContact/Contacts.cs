using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Customer
{
    public class Contacts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContactId { get; set; }
        [ForeignKey("Customer")]
        public Nullable<long> CustomerId { get; set; }
        [ForeignKey("Supplier")]
        public Nullable<long> SupplierId { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public bool DefaultContact { get; set; }
        public Nullable<long> CountryId { get; set; }
        public string Fax { get; set; }
        public string Office { get; set; }
        [ForeignKey("Tenants")]
        public Nullable<long> TenantId { get; set; }
        public Customer Customer { get; set; }
        public Country country { get; set; }
        public Supplier.Supplier Supplier { get; set; }
        public Tenants.Tenants Tenants { get; set; }
    }
}
