using Inventory.Application.Interface.Products;
using Inventory.Application.ViewModel.ProductsVm;
using Inventory.Core.Models.Products;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services.ProductsServices
{
    public class ProductBrandServices : IProductBrand
    {
        private readonly ApplicationDbContext _DbContext;
        public ProductBrandServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public Boolean Result = false;
        public async Task<List<ProductBrandVm>> GetCategoriesList()
        {
            List<ProductBrandVm> List = new List<ProductBrandVm>();
            try
            {
                await Task.Run(() =>
                {
                    var data = _DbContext.ProductBrands.Where(x => x.IsActive == true).ToList();
                    if (data != null)
                    {
                        foreach (var item in data)
                        {
                            ProductBrandVm vm = new ProductBrandVm();
                            vm.BrandId = item.BrandId;
                            vm.BrandName = item.BrandName;
                            vm.Description = item.Description;
                            List.Add(vm);
                        }
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return List;
        }

        public async Task<bool> SaveProductCategories(ProductBrandVm model)
        {
            Boolean IsExist = false;
            try
            {
                if (model != null)
                {
                    await Task.Run(async () =>
                    {
                        IsExist = await BrandIsExist(model.BrandName);
                        if (!IsExist)
                        {
                            ProductBrand brand = new ProductBrand
                            {
                                BrandName = model.BrandName,
                                Description = model.Description,
                                CreationTime = DateTime.Now,
                                CreatorUserId = 001,
                                LastModificationTime = DateTime.Now,
                                LastModifierUserId = 001,
                                IsActive = true,
                            };
                            _DbContext.ProductBrands.Add(brand);
                            _DbContext.SaveChanges();
                            IsExist = true;
                        }
                        else
                        {
                            IsExist = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return IsExist;
        }
        public async Task<Boolean> BrandIsExist(string BrandName)
        {
            Boolean IsExist = false;
            try
            {
                await Task.Run(() =>
                {
                    var data = _DbContext.ProductBrands.Where(x => x.BrandName == BrandName).FirstOrDefault();
                    if (data != null)
                    {
                        IsExist = true;
                    }
                });
            }
            catch (Exception e)
            {

                throw;
            }
            return IsExist;
        }
        public async Task<ProductBrandVm> GetCategories(long BrandId)
        {
            ProductBrandVm vm = new ProductBrandVm();
            try
            {
                if (BrandId != 0)
                {
                    await Task.Run(() =>
                    {
                        var data = _DbContext.ProductBrands.FirstOrDefault(x => x.BrandId == BrandId);
                        if (data != null)
                        {
                            vm.BrandId = data.BrandId;
                            vm.BrandName = data.BrandName;
                            vm.Description = data.Description;
                        }
                    });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return vm;
        }
        public async Task<bool> UpdateProductCategories(long BrandId, ProductBrandVm model)
        {
            try
            {
                if (BrandId != 0 && model != null)
                {
                    await Task.Run(() =>
                    {
                        var data = _DbContext.ProductBrands.FirstOrDefault(x => x.BrandId == BrandId);
                        if (data != null)
                        {
                            data.BrandName = model.BrandName;
                            data.Description = model.Description;
                            data.LastModificationTime = DateTime.Now;
                            data.LastModifierUserId = 001;
                            _DbContext.ProductBrands.Update(data);
                            _DbContext.SaveChanges();
                            Result = true;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                Result = false;
                throw e;
            }
            return Result;
        }
        public async Task<bool> DeleteProductCategorie(long BrandId)
        {
            try
            {
                if (BrandId != 0)
                {
                    await Task.Run(() =>
                    {
                        var data = _DbContext.ProductBrands.FirstOrDefault(x => x.BrandId == BrandId);
                        if (data != null) {
                            _DbContext.ProductBrands.Remove(data);
                            _DbContext.SaveChanges();
                            Result = true;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                Result = false;
                throw e;
            }
            return Result;
        }

    }
}
