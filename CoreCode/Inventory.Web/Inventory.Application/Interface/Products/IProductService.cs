using Inventory.Application.ViewModel.ProductsVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.Products
{
   public interface IProductService
    {
        Task<Boolean> SaveService(ProductServiceVM model, string UserId, long TenantId);
        Task<List<ProductServiceVM>> GetServiceList(long TenantId);
        Task<ProductServiceVM> GetService(long ServiceId);
        Task<Boolean> DeleteService(long ServiceId);
    }
}
