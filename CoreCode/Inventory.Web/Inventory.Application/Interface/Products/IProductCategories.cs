using Inventory.Application.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.Products
{
    public interface IProductCategories
    {
        Task<Boolean> SaveProductCategories(ProductCategoriesVm productCategories, string UserId, long TenantId);
        Task<List<ProductCategoriesVm>> GetCategoriesList();
        Task<ProductCategoriesVm> GetCategories(long CategorieId);
        Task<Boolean> UpdateProductCategories(long CategoriesId, ProductCategoriesVm productCategories, string UserId);
        Task<Boolean> DeleteProductCategorie(long CategoriesId);
    }
}
