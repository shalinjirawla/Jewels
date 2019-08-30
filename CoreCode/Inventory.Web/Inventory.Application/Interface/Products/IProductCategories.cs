using Inventory.Application.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.Products
{
    public interface IProductCategories
    {
        Task<Boolean> SaveProductCategories(ProductCategoriesVm productCategories);
        Task<List<ProductCategoriesVm>> GetCategoriesList();
        Task<ProductCategoriesVm> GetCategories(long CategorieId);
        Task<Boolean> UpdateProductCategories(long CategoriesId, ProductCategoriesVm productCategories);
        Task<Boolean> DeleteProductCategorie(long CategoriesId);
    }
}
