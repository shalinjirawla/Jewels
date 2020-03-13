using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.ProductsVm
{
    public class ProductVariantVM
    {

        public long? ProductId { get; set; }
        public string VariantOptionsType { get; set; }
        public string VariantOptionLabel { get; set; }
        public List<ProductVariantListVM> ProductVariantListVMs { get; set; }
    }
    public class ProductVariantListVM
    {
        public long ProductVariantId { get; set; }
        public long? ProductId { get; set; }
        public string VariantOptionsType { get; set; }
        public string VariantOptionslabel { get; set; }
        public int? VariantslabelId { get; set; }
        public string Variantslabel { get; set; }
        public string Sku { get; set; }
        public long? ReorderQuantity { get; set; }
        public double? PurchasePrice { get; set; }
        public double? SellingPrice { get; set; }
        public Boolean Action { get; set; }
        public string Image { get; set; }
        public long? VariMinmOrderQuantity { get; set; }
        public string VariantDesc { get; set; }
        public long? DefaultSupplierId { get; set; }
        public int? DefaultTaxId { get; set; }
        public long? DefaultWarehouseId { get; set; }
        public string UnitsOfMeasurement { get; set; }
        public double? InitialStockHand { get; set; }
        public double? InitialStockPrice { get; set; }
        public double? InitialHandCost { get; set; }
    }
}
