using Inventory.Core.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.PurchaseOrder
{
    public class PurchaseOrders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PurchaseOrdersId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime? DateOrdered { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        [ForeignKey("SupplierId")]
        public long SupplierId { get; set; }
        public string ReferenceNumber { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("CreditTermId")]
        public long? CreditTermId { get; set; }
        [ForeignKey("ShipmentMethodId")]
        public long? ShipmentMethodId { get; set; }
        [ForeignKey("CurrencyId")]
        public long? CurrencyId { get; set; }
        [ForeignKey("PaymentTermId")]
        public long? PaymentTermId { get; set; }
        public double? ExchangeRate { get; set; }
     
        public int PaymentStatus { get; set; } = 0;//PaymentStatus==0(InProgress),1(Partial Paid),2(Paid),3(Issued)
        public int ReceiveStatus { get; set; } = 0;//Shipment==0(InProgress),1(Delivered),2(Partial Delivered),3(Partially Issued),4(Issued)
        public Nullable<DateTime> CreationTime { get; set; }
        [ForeignKey("User")]
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        [ForeignKey("UserId")]
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        public Nullable<long> TenantsId { get; set; }

        public Tenants.Tenants Tenants { get; set; }
        public ApplicationUser.ApplicationUser User { get; set; }
        public ApplicationUser.ApplicationUser UserId { get; set; }
        public CreditTerms CreditTerms { get; set; }
        public ShipmentMethod ShipmentMethod { get; set; }
        public PaymentTerm PaymentTerm { get; set; }
        public Supplier.Supplier Supplier { get; set; }
        public Currency.Currency Currency { get; set; }
    }
}
