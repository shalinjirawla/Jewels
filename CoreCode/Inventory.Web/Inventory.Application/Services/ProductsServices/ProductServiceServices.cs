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
    public class ProductServiceServices : IProductService
    {
        private readonly ApplicationDbContext _DbContext;
        public ProductServiceServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<bool> DeleteService(long ServiceId)
        {
            try
            {
                if (ServiceId != 0)
                {
                    await Task.Run(() =>
                    {
                        var service = _DbContext.ProductService.FirstOrDefault(x => x.ServiceId == ServiceId);
                        if (service != null)
                        {
                            _DbContext.ProductService.Remove(service);
                            _DbContext.SaveChanges();
                        }
                    });
                }
                else
                    throw new Exception("Service Id not zero");
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<ProductServiceVM> GetService(long ServiceId)
        {
            ProductServiceVM output = new ProductServiceVM();
            try
            {
                if (ServiceId != 0)
                {
                    await Task.Run(() =>
                    {
                        var service = _DbContext.ProductService.FirstOrDefault(x => x.ServiceId == ServiceId);
                        if (service != null)
                        {
                            output.ServiceId = service.ServiceId;
                            output.Name = service.Name;
                            output.SKU = service.SKU;
                            output.Taxble = service.Taxble;
                            output.TaxId = service.TaxId;
                            output.PurchasePrice = service.PurchasePrice;
                            output.SellingPrice = service.SellingPrice;
                            output.MinmOrderQuantity = service.MinmOrderQuantity;
                            output.Description = service.Description;
                        }
                    });
                }
                else
                    throw new Exception("Service Id not zero");
            }
            catch (Exception e)
            {
                throw e;
            }
            return output;
        }

        public async Task<List<ProductServiceVM>> GetServiceList(long TenantId)
        {
            List<ProductServiceVM> list = new List<ProductServiceVM>();
            try
            {
                if (TenantId != 0)
                {
                    await Task.Run(() =>
                    {
                        var listofservice = _DbContext.ProductService.Where(x => x.TenantsId == TenantId).ToList();
                        if (listofservice != null && listofservice.Count() > 0)
                        {
                            foreach (var service in listofservice)
                            {
                                ProductServiceVM output = new ProductServiceVM();
                                output.ServiceId = service.ServiceId;
                                output.Name = service.Name;
                                output.SKU = service.SKU;
                                output.Taxble = service.Taxble;
                                output.TaxId = service.TaxId;
                                output.PurchasePrice = service.PurchasePrice;
                                output.SellingPrice = service.SellingPrice;
                                output.MinmOrderQuantity = service.MinmOrderQuantity;
                                output.Description = service.Description;
                                list.Add(output);
                            }
                        }
                    });
                }
                else
                    throw new Exception("Tenant id not zero");
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public async Task<bool> SaveService(ProductServiceVM model, string UserId, long TenantId)
        {
            try
            {
                if(model!=null)
                {
                    await Task.Run(() =>
                    {
                        ProductService productService = new ProductService()
                        {
                            ServiceId=model.ServiceId,
                            Name=model.Name,
                            SKU=model.SKU,
                            Taxble=model.Taxble,
                            TaxId=model.TaxId,
                            PurchasePrice=model.PurchasePrice,
                            SellingPrice=model.SellingPrice,
                            MinmOrderQuantity=model.MinmOrderQuantity,
                            Description=model.Description,
                            TenantsId = TenantId,
                        };
                        if(productService.ServiceId==0)
                        {
                            productService.CreationTime = DateTime.Now;
                            productService.CreatorUserId = UserId;
                            _DbContext.ProductService.Add(productService);
                            _DbContext.SaveChanges();
                        }
                        else
                        {
                            productService.LastModificationTime = DateTime.Now;
                            productService.LastModifierUserId = UserId;
                            _DbContext.ProductService.Update(productService);
                            _DbContext.SaveChanges();
                        }
                    });
                    
                }
            }
            catch (Exception e)
            {
                 throw e;
            }
            return true;
        }
    }
}
