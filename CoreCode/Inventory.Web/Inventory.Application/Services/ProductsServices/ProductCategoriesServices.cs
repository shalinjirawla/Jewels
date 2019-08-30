using Inventory.Application.Interface.Products;
using Inventory.Application.ViewModel.Products;
using Inventory.Core.Models.Products;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services.ProductsServices
{
    public class ProductCategoriesServices : IProductCategories
    {
        private readonly ApplicationDbContext _DbContext;
        public ProductCategoriesServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<Boolean> SaveProductCategories(ProductCategoriesVm model)
        {
            Boolean Result = false;
            try
            {
                await Task.Run(async () =>
                {
                    if (model != null)
                    {
                        Boolean IsCategoriesExist = await CheckProductCategoriesExistOrNot(model.CategoriesName);
                        if (!IsCategoriesExist)
                        {
                            Result = true;
                            ProductCategories data = new ProductCategories
                            {
                                CategoriesName = model.CategoriesName,
                                Code = model.Code,
                                DisplayOrder = model.DisplayOrder,
                                Description = model.Description,
                                CreationTime = DateTime.Now,
                                CreatorUserId = 001,
                                LastModificationTime = DateTime.Now,
                                LastModifierUserId = 001,
                                IsActive = true,
                            };
                            _DbContext.ProductCategories.Add(data);
                            _DbContext.SaveChanges();
                        }
                        else
                        {
                            Result = false;
                        }

                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        public async Task<Boolean> CheckProductCategoriesExistOrNot(string CategoriesName)
        {
            Boolean Exist = false;
            try
            {
                await Task.Run(() =>
                {
                    if (CategoriesName != null)
                    {
                        var data = _DbContext.ProductCategories.FirstOrDefault(x => x.CategoriesName==CategoriesName);
                        if (data != null)
                        {
                            Exist = true;
                        }
                    }
                });
                return Exist;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<List<ProductCategoriesVm>> GetCategoriesList()
        {
            List<ProductCategoriesVm> list = new List<ProductCategoriesVm>();
            try
            {
                await Task.Run(() =>
                {
                    var categories = _DbContext.ProductCategories.Where(x => x.IsActive == true).ToList();
                    if (categories != null)
                    {
                        foreach (var item in categories)
                        {
                            ProductCategoriesVm model = new ProductCategoriesVm();
                            model.CategoriesId = item.CategoriesId;
                            model.CategoriesName = item.CategoriesName;
                            model.DisplayOrder = item.DisplayOrder;
                            model.Code = item.Code;
                            model.Description = item.Description;
                            list.Add(model);
                        }

                    }
                });
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        public async Task<ProductCategoriesVm> GetCategories(long CategorieId)
        {
            ProductCategoriesVm model = new ProductCategoriesVm();
            try
            {
                await Task.Run(() =>
                {
                    var data = _DbContext.ProductCategories.FirstOrDefault(x => x.CategoriesId == CategorieId);
                    if (data != null)
                    {
                        model.CategoriesId = data.CategoriesId;
                        model.CategoriesName = data.CategoriesName;
                        model.DisplayOrder = data.DisplayOrder;
                        model.Code = data.Code;
                        model.Description = data.Description;
                    };

                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return model;
        }
        public async Task<Boolean> UpdateProductCategories(long CategoriesId, ProductCategoriesVm model)
        {
            Boolean Result = false;
            try
            {
                await Task.Run(async () =>
                {
                    if (model != null)
                    {
                        Boolean IsCategoriesExist = await CheckProductCategoriesExistOrNot(model.CategoriesName);
                        if (!IsCategoriesExist)
                        {
                            var check = _DbContext.ProductCategories.FirstOrDefault(x => x.CategoriesId == CategoriesId);
                            if (check != null)
                            {
                                Result = true;

                                check.CategoriesName = model.CategoriesName;
                                check.Code = model.Code;
                                check.DisplayOrder = model.DisplayOrder;
                                check.Description = model.Description;
                                check.LastModificationTime = DateTime.Now;
                                check.LastModifierUserId = 001;
                                
                                _DbContext.ProductCategories.Update(check);
                                _DbContext.SaveChanges();
                            }
                        }
                        else
                        {
                            Result = false;
                        }

                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        public async Task<Boolean> DeleteProductCategorie(long CategoriesId)
        {
            Boolean Result = false;
            try
            {
                await Task.Run(() =>
                {
                    var data = _DbContext.ProductCategories.FirstOrDefault(x => x.CategoriesId == CategoriesId);
                    if (data != null)
                    {
                        _DbContext.ProductCategories.Remove(data);
                        _DbContext.SaveChanges();
                        Result = true;
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
    }
}
