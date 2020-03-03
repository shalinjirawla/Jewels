using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.ProductsVm
{
    public class ProductVM
    {
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
       
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
       
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        public Nullable<long> TenantsId { get; set; }
    }
}
