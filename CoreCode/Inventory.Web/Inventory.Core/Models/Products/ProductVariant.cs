using Inventory.Core.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Products
{
  public  class ProductVariant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductVariantId { get; set; }
        [ForeignKey("ProductId")]
        public long? ProductId { get; set; }
        public Product Product { get; set; }
        public string VariantOptionsType { get; set; }
        public string VariantOptionslabel { get; set; }
        public int? VariantslabelId { get; set; }

        public string Variantslabel { get; set; }
        public string Sku { get; set; }
        public long? ReorderQuantity { get; set; }
        public double? PurchasePrice { get; set; }
        public double? SellingPrice { get; set; }
        public string Image { get; set; }
        public long? VariMinmOrderQuantity { get; set; }
        public string VariantDesc { get; set; }
        [ForeignKey("SupplierId")]
        public long? DefaultSupplierId { get; set; }
        public Supplier.Supplier Supplier { get; set; }
        public int? DefaultTaxId { get; set; }
        [ForeignKey("WarehouseId")]
        public long? DefaultWarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public double? UnitsOfMeasurement { get; set; }
        public double? InitialStockHand { get; set; }
        public double? InitialStockPrice { get; set; }
        public double? InitialHandCost { get; set; }
    }
}
