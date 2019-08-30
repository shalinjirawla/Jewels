using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Products
{
  public  class ProductCategories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoriesId { get; set; }
        public string CategoriesName { get; set; }
        public string DisplayOrder { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> CreationTime { get; set; }
        public Nullable<long> CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        public Nullable<long> LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
    }
}
