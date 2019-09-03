using Inventory.Application.ViewModel.ProductsVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.Products
{
  public  interface IProductBrand
    {
        Task<Boolean> SaveProductCategories(ProductBrandVm model);
        Task<List<ProductBrandVm>> GetCategoriesList();
        Task<ProductBrandVm> GetCategories(long BrandId);
        Task<Boolean> UpdateProductCategories(long BrandId, ProductBrandVm model);
        Task<Boolean> DeleteProductCategorie(long BrandId);
    }
}
