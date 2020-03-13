using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.ProductsVm
{
   public class ProductListVM
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public long? CategorieId { get; set; }
        public Boolean IsProductVariants { get; set; }
        public Boolean Taxable { get; set; }
        public Boolean IsActive { get; set; }
        public Nullable<long> TenantsId { get; set; }
        public Boolean Stockitem { get; set; }
        public string Images { get; set; }
        public double SellingPrice { get; set; }
        public double PurchasePrice { get; set; }
    }
}
