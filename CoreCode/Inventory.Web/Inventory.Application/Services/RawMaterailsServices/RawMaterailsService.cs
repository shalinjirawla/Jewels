using Inventory.Application.Interface.RawMaterails;
using Inventory.Application.ViewModel.RawMaterails;
using Inventory.Application.ViewModel.UploadImageVm;
using Inventory.Core.Models;
using Inventory.Core.Models.UploadImage;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Services
{
    public class RawMaterailsService : IRawMaterails
    {
        private readonly ApplicationDbContext _DbContext;

        public RawMaterailsService(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public bool SaveRawMaterails(SaveRawMaterailsVm model, string GetUserId, long GetTenantId)
        {

            try
            {
                if (model != null)
                {
                    RawMaterails RawMaterails = new RawMaterails
                    {
                        RMName = model.RMName,
                        AlternativeRMName = model.AlternativeRMName,
                        Itemcode = model.Itemcode,
                        Purchase_Price = model.Purchase_Price!=null&& model.Purchase_Price!=""?decimal.Parse(model.Purchase_Price):0,
                        Selling_Price = model.Selling_Price!=null&& model.Selling_Price!=""?decimal.Parse(model.Selling_Price):0,
                        Description = model.Description,
                        StockItem = model.StockItem,
                        Taxable = model.Taxable,
                        IStockOnHand = model.IStockOnHand!=null&& model.IStockOnHand!=""?decimal.Parse(model.IStockOnHand):0,
                        ICostPrice = model.ICostPrice!=null&& model.ICostPrice!=""?decimal.Parse(model.ICostPrice):0,
                        ILandedCost = model.ILandedCost!=null&& model.ILandedCost!=""?decimal.Parse(model.ILandedCost):0,
                        Reorder_Quantity = model.Reorder_Quantity!=null&& model.Reorder_Quantity!=""?long.Parse(model.Reorder_Quantity):0,
                        Minimumu_Order_Quantity = model.Minimumu_Order_Quantity!=null&& model.Minimumu_Order_Quantity!=""?long.Parse(model.Minimumu_Order_Quantity):0,

                        UOMId = model.UOMId != 0 ? model.UOMId : null,
                        Outer_Weight = model.Outer_Weight != null && model.Outer_Weight != "" ? decimal.Parse(model.Outer_Weight) : 0,
                        Outer_Weight_metric_Units = model.Outer_Weight_metric_Units != 0 ? model.Outer_Weight_metric_Units : null,
                        Inner_Weight = model.Inner_Weight != null && model.Inner_Weight != "" ? decimal.Parse(model.Inner_Weight) : 0,
                        Inner_Weight_metric_Units = model.Inner_Weight_metric_Units != 0 ? model.Inner_Weight_metric_Units : null,
                        OD_Width = model.OD_Width != null && model.OD_Width != "" ? decimal.Parse(model.OD_Width) : 0,
                        OD_Height = model.OD_Height != null && model.OD_Height != "" ? decimal.Parse(model.OD_Height) : 0,
                        OD_length = model.OD_length != null && model.OD_length != "" ? decimal.Parse(model.OD_length) : 0,
                        OD_metric_Units = model.OD_metric_Units != 0 ? model.OD_metric_Units : null,
                        OD_CBM = model.OD_CBM != null && model.OD_CBM != "" ? decimal.Parse(model.OD_CBM) : 0,
                        ID_Width = model.ID_Width != null && model.ID_Width != "" ? decimal.Parse(model.ID_Width) : 0,
                        ID_Height = model.ID_Height != null && model.ID_Height != "" ? decimal.Parse(model.ID_Height) : 0,
                        ID_length = model.ID_length != null && model.ID_length != "" ? decimal.Parse(model.ID_length) : 0,
                        ID_metric_Units = model.ID_metric_Units != 0 ? model.ID_metric_Units : null,
                        ID_CBM = model.ID_CBM != null && model.ID_CBM != "" ? decimal.Parse(model.ID_CBM) : 0,

                        ProductCategorieId = model.ProductCategorieId != 0 ? model.ProductCategorieId : null,
                        BrandId = model.BrandId != 0 ? model.BrandId : null,
                        WarehouseId = model.WarehouseId != 0 ? model.WarehouseId : null,
                        TaxCodeId = model.TaxCodeId != 0 ? model.TaxCodeId : null,
                        SupplierId = model.SupplierId != 0 ? model.SupplierId : null,

                        CreationTime = DateTime.Now,
                        CreatorUserId = GetUserId,
                        LastModificationTime = DateTime.Now,
                        LastModifierUserId = GetUserId,
                        IsActive = true,
                        TenantsId = GetTenantId,
                    };
                    // _DbContext.RawMaterails.Add(RawMaterails);
                    // _DbContext.SaveChanges();
                    if (model.PictureList.Picture.Count != 0)
                    {
                        foreach (var image in model.PictureList.Picture)
                        {
                            UploadImage uploadImage = new UploadImage
                            {
                                UploadImageString = image.PictuterString,
                                DefaultImage = image.DefaultPicture,
                                RawMaterailId = RawMaterails.RMId,
                                ProductId = null,

                                CreationTime = DateTime.Now,
                                CreatorUserId = GetUserId,
                                LastModificationTime = DateTime.Now,
                                LastModifierUserId = GetUserId,
                                IsActive = true,
                                TenantsId = GetTenantId,
                            };
                            // _DbContext.UploadImages.Add(uploadImage);
                            // _DbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            throw new NotImplementedException();
        }

    }
}
