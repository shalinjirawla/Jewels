using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.ProductsVm
{
   public class ProductDetailVM
    {
        public long ProductDetailId { get; set; }
        public long ProductId { get; set; }
        public string Image { get; set; }
        public long? ReorderQuantity { get; set; }
        public double? PurchasePrice { get; set; }
        public double? SellingPrice { get; set; }
        public long? MinmOrderQuantity { get; set; }
        public string Desc { get; set; }
        public long? DefaultSupplierId { get; set; }
        public int? DefaultTaxId { get; set; }
        public long? DefaultWarehouseId { get; set; }
        public string UnitsOfMeasurement { get; set; }
        public double? InitialStockHand { get; set; }
        public double? InitialStockPrice { get; set; }
        public double? InitialHandCost { get; set; }
    }
}
