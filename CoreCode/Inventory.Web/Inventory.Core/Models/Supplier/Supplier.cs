using Inventory.Core.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Supplier
{
   public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string SupplierCode { get; set; }
        public string Website { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("Currency")]
        public Nullable<long> DefaultCurrency { get; set; }
        [ForeignKey("paymentTerm")]
        public Nullable<long> DefaultPaymentTerms { get; set; }//foreignkey from PaymentTerms

        [ForeignKey("TaxCode")]
        public Nullable<long> DefaultTaxCode { get; set; }
        [ForeignKey("ShipmentTerm")]
        public Nullable<long> Shipmenterms { get; set; }
        [ForeignKey("ShipmentMethod")]
        public Nullable<long> Shipmenmethod { get; set; }
        [ForeignKey("User")]
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> CreationTime { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        [ForeignKey("UserId")]
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        [ForeignKey("Tenants")]
        public Nullable<long> TenantsId { get; set; }

        public Tenants.Tenants Tenants { get; set; }
        public Currency.Currency Currency { get; set; }
        public PaymentTerm paymentTerm { get; set; }
        public TaxCode TaxCode { get; set; }
        public ShipmentTerm ShipmentTerm { get; set; }
        public ShipmentMethod ShipmentMethod { get; set; }
        public ApplicationUser.ApplicationUser User { get; set; }
        public ApplicationUser.ApplicationUser UserId { get; set; }
    }
}
