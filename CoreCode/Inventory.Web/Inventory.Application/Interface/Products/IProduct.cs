using Inventory.Application.ViewModel.ProductsVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.Products
{
   public interface IProduct
    {
        Task<Boolean> SaveProduct(ProductVariantMergeVM model, string UserId, long TenantId);
        Task<List<ProductVM>> GetProductList(long TenantId);
        Task<ProductVariantMergeVM> GetProduct(long ProductId);
        Task<Boolean> DeleteProduct(long ProductId);
    }
}
