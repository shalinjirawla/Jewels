using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.Products
{
    public class ProductCategoriesVm
    {
        public long CategoriesId { get; set; }
        public string CategoriesName { get; set; }
        public string DisplayOrder { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
