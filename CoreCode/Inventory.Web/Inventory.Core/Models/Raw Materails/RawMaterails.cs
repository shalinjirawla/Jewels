using Inventory.Core.Models.Commons;
using Inventory.Core.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models
{
    public class RawMaterails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RMId { get; set; }
        public string RMName { get; set; }
        public string AlternativeRMName { get; set; }
        public string Itemcode { get; set; }
        public Nullable<decimal> Purchase_Price { get; set; }
        public Nullable<decimal> Selling_Price { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> IStockOnHand { get; set; }
        public Nullable<decimal> ICostPrice { get; set; }
        public Nullable<decimal> ILandedCost { get; set; }
        public Nullable<long> Reorder_Quantity { get; set; }
        public Nullable<long> Minimumu_Order_Quantity { get; set; }



        [ForeignKey("productCategories")]
        public Nullable<long> ProductCategorieId { get; set; }
        [ForeignKey("ProductBrand")]
        public Nullable<long> BrandId { get; set; }
        [ForeignKey("warehouse")]
        public Nullable<long> WarehouseId { get; set; }
        [ForeignKey("taxCode")]
        public Nullable<long> TaxCodeId { get; set; }
        [ForeignKey("supplier")]
        public Nullable<long> SupplierId { get; set; }
        [ForeignKey("UOM")]
        public Nullable<long> UOMId { get; set; }



        public Nullable<DateTime> CreationTime { get; set; }
        [ForeignKey("User")]
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        [ForeignKey("UserId")]
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        public Nullable<long> TenantsId { get; set; }


        public UOM UOM { get; set; }
        public Supplier.Supplier supplier { get; set; }
        public TaxCode taxCode { get; set; }
        public Warehouse warehouse { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public ProductCategories productCategories { get; set; }
        public Tenants.Tenants Tenants { get; set; }
        public ApplicationUser.ApplicationUser User { get; set; }
        public ApplicationUser.ApplicationUser UserId { get; set; }
    }
}
