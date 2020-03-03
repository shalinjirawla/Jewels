using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.ProductsVm
{
    public class ProductServiceVM
    {
        public long ServiceId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public Boolean Taxble { get; set; }
        public long TaxId { get; set; }
        public double PurchasePrice { get; set; }
        public double SellingPrice { get; set; }
        public long MinmOrderQuantity { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        public Nullable<long> TenantsId { get; set; }
    }
}
