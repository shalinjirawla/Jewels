using Inventory.Core.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.PurchaseOrder
{
    public class PurchaseOrderItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderItemsId { get; set; }
        [ForeignKey("PurchaseOrdersId")]
        public long? PurchaseOrdersId { get; set; }
        [ForeignKey("ProductId")]
        public long? ProductId { get; set; }
        public int? Unit { get; set; }
        public double? UnitPrice { get; set; }
        public int? QTY { get; set; }
        public int? DiscountType { get; set; }
        public double? Discount { get; set; }
        [ForeignKey("TaxId")]
        public long? TaxId { get; set; }
        public Boolean? IsTaxble { get; set; }
        public double? TaxTotal { get; set; }
        public double? Total { get; set; }
        public PurchaseOrders PurchaseOrders { get; set; }
        public Products.Product Product { get; set; }
        public TaxCode TaxCode { get; set; }

    }
}
