using Inventory.Core.Models.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Products
{
    public class ProductService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ServiceId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public Boolean Taxble { get; set; }
        [ForeignKey("TaxId")]
        public long TaxId { get; set; }
        public double PurchasePrice { get; set; }
        public double SellingPrice { get; set; }
        public long MinmOrderQuantity { get; set; }
        public string Description { get; set; }
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
        public TaxCode TaxCode { get; set; }
    }
}
