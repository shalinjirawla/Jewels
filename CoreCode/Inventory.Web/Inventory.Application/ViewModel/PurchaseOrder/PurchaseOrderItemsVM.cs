using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
    public class PurchaseOrderItemsVM
    {
        public long OrderItemsId { get; set; }
        public long? PurchaseOrdersId { get; set; }
        public long? ProductId { get; set; }
        public int? Unit { get; set; }
        public double? UnitPrice { get; set; }
        public int? QTY { get; set; }
        public int? DiscountType { get; set; }
        public double? Discount { get; set; }
        public long? TaxId { get; set; }
        public Boolean? IsTaxble { get; set; }
        public double? TaxTotal { get; set; }
        public double? Total { get; set; }
    }
}
