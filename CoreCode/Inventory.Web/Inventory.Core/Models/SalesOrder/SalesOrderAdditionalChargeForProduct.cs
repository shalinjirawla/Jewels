using Inventory.Core.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.SalesOrder
{
  public  class SalesOrderAdditionalChargeForProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AdditionalChargeForProductId { get; set; }
        [ForeignKey("SalesOrdersId")]
        public long SalesOrdersId { get; set; }
        [ForeignKey("ProductId")]
        public long? ProductId { get; set; }
        public Boolean? IsTaxble { get; set; }
        public long? AdditionalChargeId { get; set; }
        [ForeignKey("TaxId")]
        public long? TaxId { get; set; }
        public SalesOrders SalesOrders { get; set; }
        public Products.Product Product { get; set; }
        public AdditionalCharge AdditionalCharge { get; set; }
        public TaxCode TaxCode { get; set; }
    }
}
