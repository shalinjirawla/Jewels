using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.PurchaseOrder
{
    public class GetPurchaseOrderListIdBySuppliersVM
    {

        public long SupplierId { get; set; }
        public long PurchaseOrderId { get; set; }
        public string Code { get; set; }
    }
    public class ProductListIdByPurchaseOrder
    {
        public long? PurchaseOrderId { get; set; }
        public long SupplierId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int? QTY { get; set; }
        public string UOM { get; set; }
        public double? Price { get; set; }
        public Boolean IsActive { get; set; }
        public long UniquIndex { get; set; }
    }
}
