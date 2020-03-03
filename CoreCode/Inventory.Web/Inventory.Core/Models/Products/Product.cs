using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Products
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public long? CategorieId { get; set; }
        public long? BrandId { get; set; }
        public Boolean BatchItem { get; set; }
        public Boolean Stockitem { get; set; }
        public Boolean Taxable { get; set; }
        public Boolean SerialNumber { get; set; }
        public Boolean IsRawMaterail { get; set; }
        public string RawMaterial_points { get; set; }
        public Boolean IsProductVariants { get; set; }

        public Nullable<DateTime> CreationTime { get; set; }
        [ForeignKey("User")]
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        [ForeignKey("UserId")]
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        public Nullable<long> TenantsId { get; set; }

        public Tenants.Tenants Tenants { get; set; }
        public ApplicationUser.ApplicationUser User { get; set; }
        public ApplicationUser.ApplicationUser UserId { get; set; }
    }
}
