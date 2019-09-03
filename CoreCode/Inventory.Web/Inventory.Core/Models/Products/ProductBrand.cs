using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Products
{
   public class ProductBrand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BrandId { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> CreationTime { get; set; }
        public Nullable<long> CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        public Nullable<long> LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
    }
}
