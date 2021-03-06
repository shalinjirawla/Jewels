﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.PurchaseOrder
{

   public class PurchaseOrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PurchaseOrderDetailsId { get; set; }
        [ForeignKey("PurchaseOrdersId")]
        public long? PurchaseOrdersId { get; set; }
        public string AdditionalChargeType { get; set; }
        public long? TotalQTY { get; set; }
        public double? Total { get; set; }
        public double? FinalTotal { get; set; }
        public Boolean? TaxInclude { get; set; }
        public double? FinalTaxTotal { get; set; }
        public double? AdditionalChargeAmount { get; set; }
        public Boolean? IsAdditionalChargeApply { get; set; }
        public string IsAdditionalChargeApplyType { get; set; }
        public PurchaseOrders PurchaseOrders { get; set; }
    }
}
