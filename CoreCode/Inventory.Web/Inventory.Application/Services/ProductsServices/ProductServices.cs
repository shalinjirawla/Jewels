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
    public class ProductServices : IProduct
    {
        private readonly ApplicationDbContext _DbContext;
        public ProductServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<bool> DeleteProduct(long ProductId)
        {
            try
            {
                await Task.Run(() =>
                {
                    var product = _DbContext.Product.FirstOrDefault(x => x.ProductId == ProductId);
                    if (product != null)
                    {
                        product.IsActive = false;
                        _DbContext.Product.Update(product);
                        _DbContext.SaveChanges();
                    }
                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<ProductVariantMergeVM> GetProduct(long ProductId)
        {
            ProductVariantMergeVM output = new ProductVariantMergeVM();
            ProductVariantVM ProductVarinatData = new ProductVariantVM();
            ProductDetailVM productDetailVM = new ProductDetailVM();
            try
            {
                if (ProductId != 0)
                {
                    await Task.Run(() =>
                    {
                        var product = _DbContext.Product.FirstOrDefault(x => x.ProductId == ProductId);
                        if (product != null)
                        {
                            ProductVM ProductData = new ProductVM
                            {
                                ProductId = product.ProductId,
                                Name = product.Name,
                                Sku = product.Sku,
                                CategorieId = product.CategorieId,
                                BrandId = product.BrandId,
                                BatchItem = product.BatchItem,
                                Stockitem = product.Stockitem,
                                Taxable = product.Taxable,
                                SerialNumber = product.SerialNumber,
                                IsRawMaterail = product.IsRawMaterail,
                                RawMaterial_points = product.RawMaterial_points,
                                IsProductVariants = product.IsProductVariants,
                            };
                            output.ProductData = ProductData;
                            if (ProductData.IsProductVariants)
                            {

                                List<ProductVariantListVM> productVariantListVMs = new List<ProductVariantListVM>();
                                var productvariant = _DbContext.ProductVariant.Where(x => x.ProductId == ProductId).ToList();
                                if (productvariant != null && productvariant.Count() > 0)
                                {
                                    foreach (var item in productvariant)
                                    {
                                        ProductVariantListVM variant = new ProductVariantListVM()
                                        {

                                            ProductVariantId = item.ProductVariantId,
                                            ProductId = item.ProductId,
                                            VariantOptionsType = item.VariantOptionsType,
                                            VariantOptionslabel = item.VariantOptionslabel,
                                            VariantslabelId = item.VariantslabelId,
                                            Variantslabel = item.Variantslabel,
                                            Sku = item.Sku,
                                            ReorderQuantity = item.ReorderQuantity,
                                            PurchasePrice = item.PurchasePrice,
                                            SellingPrice = item.SellingPrice,
                                            Image = item.Image,
                                            VariMinmOrderQuantity = item.VariMinmOrderQuantity,
                                            VariantDesc = item.VariantDesc,
                                            DefaultSupplierId = item.DefaultSupplierId,
                                            DefaultTaxId = item.DefaultTaxId,
                                            DefaultWarehouseId = item.DefaultWarehouseId,
                                            UnitsOfMeasurement = item.UnitsOfMeasurement,
                                            InitialStockHand = item.InitialStockHand,
                                            InitialStockPrice = item.InitialStockPrice,
                                            InitialHandCost = item.InitialHandCost,
                                        };
                                        ProductVarinatData.ProductId = item.ProductId;
                                        ProductVarinatData.VariantOptionsType = item.VariantslabelId.ToString();
                                        ProductVarinatData.VariantOptionLabel = item.Variantslabel;
                                        productVariantListVMs.Add(variant);

                                    }
                                    ProductVarinatData.ProductVariantListVMs = productVariantListVMs;
                                    output.ProductVarinatData = ProductVarinatData;
                                }
                            }
                            else
                            {
                                var productdetails = _DbContext.ProductDetail.FirstOrDefault(x => x.ProductId == ProductId);
                                if (productdetails != null)
                                {
                                    productDetailVM.ProductId = productdetails.ProductId;
                                    productDetailVM.Image = productdetails.Image;
                                    productDetailVM.ReorderQuantity = productdetails.ReorderQuantity;
                                    productDetailVM.PurchasePrice = productdetails.PurchasePrice;
                                    productDetailVM.SellingPrice = productdetails.SellingPrice;
                                    productDetailVM.MinmOrderQuantity = productdetails.MinmOrderQuantity;
                                    productDetailVM.Desc = productdetails.Desc;
                                    productDetailVM.DefaultSupplierId = productdetails.DefaultSupplierId;
                                    productDetailVM.DefaultTaxId = productdetails.DefaultTaxId;
                                    productDetailVM.DefaultWarehouseId = productdetails.DefaultWarehouseId;
                                    productDetailVM.UnitsOfMeasurement = productdetails.UnitsOfMeasurement;
                                    productDetailVM.InitialStockHand = productdetails.InitialStockHand;
                                    productDetailVM.InitialStockPrice = productdetails.InitialStockPrice;
                                    productDetailVM.InitialHandCost = productdetails.InitialHandCost;
                                    output.ProductDetailData = productDetailVM;
                                }
                            }
                        }
                    });
                }
                else
                    throw new Exception("Product Id zero not allow");
            }
            catch (Exception e)
            {
                throw e;
            }
            return output;
        }

        public async Task<List<ProductListVM>> GetProductList(long TenantId)
        {
            List<ProductListVM> List = new List<ProductListVM>();
            try
            {
                await Task.Run(() =>
                {
                    var ProductList = _DbContext.Product.Where(x => x.TenantsId == TenantId && x.IsActive == true).ToList();
                    if (ProductList != null && ProductList.Count > 0)
                    {
                        foreach (var item in ProductList)
                        {
                            ProductListVM dto = new ProductListVM()
                            {
                                ProductId = item.ProductId,
                                Name = item.Name,
                                Sku = item.Sku,
                                IsActive = item.IsActive,
                                Stockitem = item.Stockitem,
                                Taxable = item.Taxable,
                            };
                            if(item.IsProductVariants)
                            {
                                var variant = _DbContext.ProductVariant.Where(x => x.Image != null).FirstOrDefault(x => x.ProductId == item.ProductId);
                                if(variant!=null)
                                {
                                    dto.Images = variant.Image;
                                    dto.SellingPrice = variant.SellingPrice??0.00;
                                    dto.PurchasePrice = variant.PurchasePrice??0.00;
                                }
                            }
                            else
                            {
                                var productdetails = _DbContext.ProductDetail.FirstOrDefault(x => x.ProductId ==item.ProductId);
                                if(productdetails!=null)
                                {
                                    dto.Images = productdetails.Image;
                                    dto.SellingPrice = productdetails.SellingPrice ?? 0.00;
                                    dto.PurchasePrice = productdetails.PurchasePrice ?? 0.00;
                                }
                            }
                            List.Add(dto);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                throw;
            }
            return List;
        }

        public async Task<Boolean> SaveProduct(ProductVariantMergeVM model, string UserId, long TenantId)
        {
            try
            {
                if (model != null)
                {
                    await Task.Run(async () =>
                    {
                        var input = model.ProductData;
                        var inputProductdetails = model.ProductDetailData;
                        var inputvariant = model.ProductVarinatData.ProductVariantListVMs;
                        Product product = new Product
                        {
                            ProductId = input.ProductId,
                            Name = input.Name,
                            Sku = input.Sku,
                            CategorieId = input.CategorieId,
                            BrandId = input.BrandId,
                            BatchItem = input.BatchItem,
                            Stockitem = input.Stockitem,
                            Taxable = input.Taxable,
                            SerialNumber = input.SerialNumber,
                            IsRawMaterail = input.IsRawMaterail,
                            RawMaterial_points = input.RawMaterial_points,
                            IsProductVariants = input.IsProductVariants,
                            TenantsId = TenantId,
                            IsActive = true,
                        };


                        if (product != null && product.ProductId == 0)
                        {
                            product.CreationTime = DateTime.Now;
                            product.CreatorUserId = UserId;
                            product.IsActive = true;
                            _DbContext.Add(product);
                            _DbContext.SaveChanges();
                            if (input.IsProductVariants)
                            {
                                if (inputvariant != null && inputvariant.Count > 0)
                                    await SaveProductVariant(inputvariant, product.ProductId, model.ProductVarinatData.VariantOptionsType, model.ProductVarinatData.VariantOptionLabel);
                            }
                            else
                            {
                                ProductDetail productDetail = new ProductDetail()
                                {
                                    ProductId = product.ProductId,
                                    Image = inputProductdetails.Image,
                                    MinmOrderQuantity = inputProductdetails.MinmOrderQuantity,
                                    Desc = inputProductdetails.Desc,
                                    DefaultSupplierId = inputProductdetails.DefaultSupplierId,
                                    DefaultTaxId = inputProductdetails.DefaultTaxId,
                                    DefaultWarehouseId = inputProductdetails.DefaultWarehouseId,
                                    UnitsOfMeasurement = inputProductdetails.UnitsOfMeasurement,
                                    InitialStockHand = inputProductdetails.InitialStockHand,
                                    InitialStockPrice = inputProductdetails.InitialStockPrice,
                                    InitialHandCost = inputProductdetails.InitialHandCost,
                                };
                                _DbContext.ProductDetail.Add(productDetail);
                                _DbContext.SaveChanges();
                            }
                        }
                        else
                        {
                            product.LastModificationTime = DateTime.Now;
                            product.LastModifierUserId = UserId;
                            _DbContext.Product.Update(product);
                            _DbContext.SaveChanges();
                            if (input.IsProductVariants)
                            {
                                if (inputvariant != null && inputvariant.Count > 0)
                                    await SaveProductVariant(inputvariant, input.ProductId, model.ProductVarinatData.VariantOptionsType, model.ProductVarinatData.VariantOptionLabel);
                            }
                            else
                            {
                                var productdetails = _DbContext.ProductDetail.FirstOrDefault(x => x.ProductId == input.ProductId);
                                if (productdetails != null)
                                {
                                    productdetails.Image = inputProductdetails.Image;
                                    productdetails.MinmOrderQuantity = inputProductdetails.MinmOrderQuantity;
                                    productdetails.ReorderQuantity = inputProductdetails.ReorderQuantity;
                                    productdetails.PurchasePrice = inputProductdetails.PurchasePrice;
                                    productdetails.SellingPrice = inputProductdetails.SellingPrice;
                                    productdetails.Desc = inputProductdetails.Desc;
                                    productdetails.DefaultSupplierId = inputProductdetails.DefaultSupplierId;
                                    productdetails.DefaultTaxId = inputProductdetails.DefaultTaxId;
                                    productdetails.DefaultWarehouseId = inputProductdetails.DefaultWarehouseId;
                                    productdetails.UnitsOfMeasurement = inputProductdetails.UnitsOfMeasurement;
                                    productdetails.InitialStockHand = inputProductdetails.InitialStockHand;
                                    productdetails.InitialStockPrice = inputProductdetails.InitialStockPrice;
                                    productdetails.InitialHandCost = inputProductdetails.InitialHandCost;
                                    _DbContext.ProductDetail.Update(productdetails);
                                    _DbContext.SaveChanges();
                                }
                                else
                                {
                                    ProductDetail productDetail = new ProductDetail()
                                    {
                                        ProductId = product.ProductId,
                                        Image = inputProductdetails.Image,
                                        ReorderQuantity = inputProductdetails.ReorderQuantity,
                                        PurchasePrice = inputProductdetails.PurchasePrice,
                                        SellingPrice = inputProductdetails.SellingPrice,
                                        MinmOrderQuantity = inputProductdetails.MinmOrderQuantity,
                                        Desc = inputProductdetails.Desc,
                                        DefaultSupplierId = inputProductdetails.DefaultSupplierId,
                                        DefaultTaxId = inputProductdetails.DefaultTaxId,
                                        DefaultWarehouseId = inputProductdetails.DefaultWarehouseId,
                                        UnitsOfMeasurement = inputProductdetails.UnitsOfMeasurement,
                                        InitialStockHand = inputProductdetails.InitialStockHand,
                                        InitialStockPrice = inputProductdetails.InitialStockPrice,
                                        InitialHandCost = inputProductdetails.InitialHandCost,
                                    };
                                    _DbContext.ProductDetail.Add(productDetail);
                                    _DbContext.SaveChanges();
                                }

                            }
                        }
                    });
                }
                else
                    throw new Exception("inpu null not allow");
            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
        }
        public async Task SaveProductVariant(List<ProductVariantListVM> inputlist, long ProductId, string VariantOptionsType, string VariantOptionslabel)
        {
            try
            {
                if (inputlist != null && inputlist.Count() > 0)
                {
                    await Task.Run(() =>
                    {
                        foreach (var input in inputlist)
                        {
                            ProductVariant varinat = new ProductVariant
                            {
                                ProductVariantId = input.ProductVariantId,
                                ProductId = ProductId,
                                VariantOptionsType = VariantOptionsType,
                                VariantOptionslabel = VariantOptionslabel,
                                VariantslabelId = input.VariantslabelId,
                                Variantslabel = input.Variantslabel,
                                Sku = input.Sku,
                                ReorderQuantity = input.ReorderQuantity,
                                PurchasePrice = input.PurchasePrice,
                                SellingPrice = input.SellingPrice,
                                Image = input.Image,
                                VariMinmOrderQuantity = input.VariMinmOrderQuantity,
                                VariantDesc = input.VariantDesc,
                                DefaultSupplierId = input.DefaultSupplierId,
                                DefaultTaxId = input.DefaultTaxId,
                                DefaultWarehouseId = input.DefaultWarehouseId,
                                UnitsOfMeasurement = input.UnitsOfMeasurement,
                                InitialStockHand = input.InitialStockHand,
                                InitialStockPrice = input.InitialStockPrice,
                                InitialHandCost = input.InitialHandCost,
                            };
                            if (varinat != null && varinat.ProductVariantId == 0)
                            {
                                _DbContext.ProductVariant.Add(varinat);
                                _DbContext.SaveChanges();
                            }
                            else
                            {
                                var getproductvarinat = _DbContext.ProductVariant.Where(x => x.ProductId == input.ProductId).ToList();
                                if (getproductvarinat != null && getproductvarinat.Count > 0)
                                {
                                    foreach (var item in getproductvarinat)
                                    {
                                        if (item.ProductVariantId == input.ProductVariantId)
                                        {
                                            item.VariantOptionsType = VariantOptionsType;
                                            item.VariantOptionslabel = VariantOptionslabel;
                                            item.VariantslabelId = input.VariantslabelId;
                                            item.Variantslabel = input.Variantslabel;
                                            item.Sku = input.Sku;
                                            item.ReorderQuantity = input.ReorderQuantity;
                                            item.PurchasePrice = input.PurchasePrice;
                                            item.SellingPrice = input.SellingPrice;
                                            item.Image = input.Image;
                                            item.VariMinmOrderQuantity = input.VariMinmOrderQuantity;
                                            item.VariantDesc = input.VariantDesc;
                                            item.DefaultSupplierId = input.DefaultSupplierId;
                                            item.DefaultTaxId = input.DefaultTaxId;
                                            item.DefaultWarehouseId = input.DefaultWarehouseId;
                                            item.UnitsOfMeasurement = input.UnitsOfMeasurement;
                                            item.InitialStockHand = input.InitialStockHand;
                                            item.InitialStockPrice = input.InitialStockPrice;
                                            item.InitialHandCost = input.InitialHandCost;
                                            _DbContext.ProductVariant.Update(item);
                                            _DbContext.SaveChanges();
                                            break;
                                        }
                                        else
                                        {
                                            var islist = inputlist.FirstOrDefault(x => x.ProductVariantId == item.ProductVariantId);
                                            if (islist == null)
                                            {
                                                _DbContext.ProductVariant.Remove(item);
                                                _DbContext.SaveChanges();
                                                break;
                                            }
                                        }

                                    }
                                }

                            }
                        }
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
