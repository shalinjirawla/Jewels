using Inventory.Core.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Products
{
  public  class ProductDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductDetailId { get; set; }
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }
        public string Image { get; set; }
        public long? ReorderQuantity { get; set; }
        public double? PurchasePrice { get; set; }
        public double? SellingPrice { get; set; }
        public long? MinmOrderQuantity { get; set; }
        public string Desc { get; set; }
        [ForeignKey("SupplierId")]
        public long? DefaultSupplierId { get; set; }
        public Supplier.Supplier Supplier { get; set; }
        [ForeignKey("TaxId")]
        public int? DefaultTaxId { get; set; }
        [ForeignKey("WarehouseId")]
        public long? DefaultWarehouseId { get; set; }
        public TaxCode TaxCode { get; set; }
        public Warehouse Warehouse { get; set; }
        public Product Product { get; set; }
        public string UnitsOfMeasurement { get; set; }
        public double? InitialStockHand { get; set; }
        public double? InitialStockPrice { get; set; }
        public double? InitialHandCost { get; set; }
    }
}
