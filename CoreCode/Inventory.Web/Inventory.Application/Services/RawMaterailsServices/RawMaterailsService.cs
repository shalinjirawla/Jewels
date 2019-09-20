using Inventory.Application.Interface.RawMaterails;
using Inventory.Application.ViewModel.RawMaterails;
using Inventory.Application.ViewModel.UploadImageVm;
using Inventory.Core.Models;
using Inventory.Core.Models.UploadImage;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<SaveRawMaterailsVm> GetRawMaterailsList()
        {
            List<SaveRawMaterailsVm> RawMaterailsList = new List<SaveRawMaterailsVm>();
            try
            {
                var dataList = _DbContext.RawMaterails.ToList();
                foreach (var data in dataList)
                {
                    SaveRawMaterailsVm RawMaterails = new SaveRawMaterailsVm();
                    RawMaterails.RMId = data.RMId;
                    RawMaterails.RMName = data.RMName;
                    RawMaterails.Itemcode = data.Itemcode;
                    RawMaterails.IStockOnHand = data.IStockOnHand.ToString();
                    RawMaterails.Purchase_Price = data.Purchase_Price.ToString();

                    List<UploadImageVm> uploadImageList = new List<UploadImageVm>();
                    ImageListVm imageListVm = new ImageListVm();

                    var imagedata = _DbContext.UploadImages.Where(x => x.RawMaterailId == data.RMId && x.DefaultImage == true).ToList();
                    foreach (var image in imagedata)
                    {
                        UploadImageVm uploadImage = new UploadImageVm();
                        uploadImage.PictuterId = image.UploadImageId.ToString();
                        uploadImage.PictuterString = image.UploadImageString;
                        uploadImage.DefaultPicture = image.DefaultImage;
                        uploadImage.RawMaterailId = image.RawMaterailId;

                        uploadImageList.Add(uploadImage);
                    }
                    imageListVm.Picture = uploadImageList;
                    RawMaterails.PictureList = imageListVm;

                    RawMaterailsList.Add(RawMaterails);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return RawMaterailsList;
        }

        public bool SaveRawMaterails(SaveRawMaterailsVm model, string GetUserId, long GetTenantId)
        {
            Boolean result = false;

            try
            {
                if (model != null)
                {
                    RawMaterails RawMaterails = new RawMaterails
                    {
                        RMName = model.RMName,
                        AlternativeRMName = model.AlternativeRMName,
                        Itemcode = model.Itemcode,
                        Purchase_Price = model.Purchase_Price != null && model.Purchase_Price != "" ? decimal.Parse(model.Purchase_Price) : 0,
                        Selling_Price = model.Selling_Price != null && model.Selling_Price != "" ? decimal.Parse(model.Selling_Price) : 0,
                        Description = model.Description,
                        StockItem = model.StockItem,
                        Taxable = model.Taxable,
                        IStockOnHand = model.IStockOnHand != null && model.IStockOnHand != "" ? decimal.Parse(model.IStockOnHand) : 0,
                        ICostPrice = model.ICostPrice != null && model.ICostPrice != "" ? decimal.Parse(model.ICostPrice) : 0,
                        ILandedCost = model.ILandedCost != null && model.ILandedCost != "" ? decimal.Parse(model.ILandedCost) : 0,
                        Reorder_Quantity = model.Reorder_Quantity != null && model.Reorder_Quantity != "" ? long.Parse(model.Reorder_Quantity) : 0,
                        Minimumu_Order_Quantity = model.Minimumu_Order_Quantity != null && model.Minimumu_Order_Quantity != "" ? long.Parse(model.Minimumu_Order_Quantity) : 0,

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
                    _DbContext.RawMaterails.Add(RawMaterails);
                    _DbContext.SaveChanges();
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
                            _DbContext.UploadImages.Add(uploadImage);
                            _DbContext.SaveChanges();
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public SaveRawMaterailsVm GetRawMaterails(long rMId)
        {
            SaveRawMaterailsVm RawMaterail = new SaveRawMaterailsVm();

            try
            {
                var data = _DbContext.RawMaterails.FirstOrDefault(x => x.RMId == rMId);
                if (data != null)
                {
                    RawMaterail.RMId = data.RMId;
                    RawMaterail.RMName = data.RMName;
                    RawMaterail.AlternativeRMName = data.AlternativeRMName;
                    RawMaterail.Itemcode = data.Itemcode;
                    RawMaterail.Purchase_Price = data.Purchase_Price.ToString();
                    RawMaterail.Selling_Price = data.Selling_Price.ToString();
                    RawMaterail.Description = data.Description;
                    RawMaterail.StockItem = data.StockItem;
                    RawMaterail.Taxable = data.Taxable;
                    RawMaterail.IStockOnHand = data.IStockOnHand.ToString();
                    RawMaterail.ICostPrice = data.ICostPrice.ToString();
                    RawMaterail.ILandedCost = data.ILandedCost.ToString();
                    RawMaterail.Reorder_Quantity = data.Reorder_Quantity.ToString();
                    RawMaterail.Minimumu_Order_Quantity = data.Minimumu_Order_Quantity.ToString();
                    RawMaterail.UOMId = data.UOMId;
                    RawMaterail.Outer_Weight = data.Outer_Weight.ToString();
                    RawMaterail.Outer_Weight_metric_Units = data.Outer_Weight_metric_Units;
                    RawMaterail.Inner_Weight = data.Inner_Weight.ToString();
                    RawMaterail.Inner_Weight_metric_Units = data.Inner_Weight_metric_Units;
                    RawMaterail.OD_Width = data.OD_Width.ToString();
                    RawMaterail.OD_Height = data.OD_Height.ToString();
                    RawMaterail.OD_length = data.OD_length.ToString();
                    RawMaterail.OD_metric_Units = data.OD_metric_Units;
                    RawMaterail.OD_CBM = data.OD_CBM.ToString();
                    RawMaterail.ID_Width = data.ID_Width.ToString();
                    RawMaterail.ID_Height = data.ID_Height.ToString();
                    RawMaterail.ID_length = data.ID_length.ToString();
                    RawMaterail.ID_metric_Units = data.ID_metric_Units;
                    RawMaterail.ID_CBM = data.ID_CBM.ToString();
                    RawMaterail.ProductCategorieId = data.ProductCategorieId;
                    RawMaterail.BrandId = data.BrandId;
                    RawMaterail.WarehouseId = data.WarehouseId;
                    RawMaterail.TaxCodeId = data.TaxCodeId;
                    RawMaterail.SupplierId = data.SupplierId;

                    List<UploadImageVm> uploadImageList = new List<UploadImageVm>();
                    ImageListVm imageListVm = new ImageListVm();

                    var imagedata = _DbContext.UploadImages.Where(x => x.RawMaterailId == data.RMId).ToList();
                    foreach (var image in imagedata)
                    {
                        UploadImageVm uploadImage = new UploadImageVm();
                        uploadImage.PictuterId = image.UploadImageId.ToString();
                        uploadImage.PictuterString = image.UploadImageString;
                        uploadImage.DefaultPicture = image.DefaultImage;
                        uploadImage.RawMaterailId = image.RawMaterailId;

                        uploadImageList.Add(uploadImage);
                    }
                    imageListVm.Picture = uploadImageList;
                    RawMaterail.PictureList = imageListVm;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return RawMaterail;
        }

        public Boolean UpdateRawMaterails(long RMId, SaveRawMaterailsVm model, string GetUserId, long GetTenantId)
        {
            Boolean result = false;
            try
            {
                if (RMId != 0)
                {
                    if (model != null)
                    {
                        RawMaterails RawMaterails = new RawMaterails();
                        RawMaterails = _DbContext.RawMaterails.FirstOrDefault(x => x.RMId == RMId);

                        RawMaterails.RMName = model.RMName;
                        RawMaterails.AlternativeRMName = model.AlternativeRMName;
                        RawMaterails.Itemcode = model.Itemcode;
                        RawMaterails.Purchase_Price = model.Purchase_Price != null && model.Purchase_Price != "" ? decimal.Parse(model.Purchase_Price) : 0;
                        RawMaterails.Selling_Price = model.Selling_Price != null && model.Selling_Price != "" ? decimal.Parse(model.Selling_Price) : 0;
                        RawMaterails.Description = model.Description;
                        RawMaterails.StockItem = model.StockItem;
                        RawMaterails.Taxable = model.Taxable;
                        RawMaterails.IStockOnHand = model.IStockOnHand != null && model.IStockOnHand != "" ? decimal.Parse(model.IStockOnHand) : 0;
                        RawMaterails.ICostPrice = model.ICostPrice != null && model.ICostPrice != "" ? decimal.Parse(model.ICostPrice) : 0;
                        RawMaterails.ILandedCost = model.ILandedCost != null && model.ILandedCost != "" ? decimal.Parse(model.ILandedCost) : 0;
                        RawMaterails.Reorder_Quantity = model.Reorder_Quantity != null && model.Reorder_Quantity != "" ? long.Parse(model.Reorder_Quantity) : 0;
                        RawMaterails.Minimumu_Order_Quantity = model.Minimumu_Order_Quantity != null && model.Minimumu_Order_Quantity != "" ? long.Parse(model.Minimumu_Order_Quantity) : 0;

                        RawMaterails.UOMId = model.UOMId != 0 ? model.UOMId : null;
                        RawMaterails.Outer_Weight = model.Outer_Weight != null && model.Outer_Weight != "" ? decimal.Parse(model.Outer_Weight) : 0;
                        RawMaterails.Outer_Weight_metric_Units = model.Outer_Weight_metric_Units != 0 ? model.Outer_Weight_metric_Units : null;
                        RawMaterails.Inner_Weight = model.Inner_Weight != null && model.Inner_Weight != "" ? decimal.Parse(model.Inner_Weight) : 0;
                        RawMaterails.Inner_Weight_metric_Units = model.Inner_Weight_metric_Units != 0 ? model.Inner_Weight_metric_Units : null;
                        RawMaterails.OD_Width = model.OD_Width != null && model.OD_Width != "" ? decimal.Parse(model.OD_Width) : 0;
                        RawMaterails.OD_Height = model.OD_Height != null && model.OD_Height != "" ? decimal.Parse(model.OD_Height) : 0;
                        RawMaterails.OD_length = model.OD_length != null && model.OD_length != "" ? decimal.Parse(model.OD_length) : 0;
                        RawMaterails.OD_metric_Units = model.OD_metric_Units != 0 ? model.OD_metric_Units : null;
                        RawMaterails.OD_CBM = model.OD_CBM != null && model.OD_CBM != "" ? decimal.Parse(model.OD_CBM) : 0;
                        RawMaterails.ID_Width = model.ID_Width != null && model.ID_Width != "" ? decimal.Parse(model.ID_Width) : 0;
                        RawMaterails.ID_Height = model.ID_Height != null && model.ID_Height != "" ? decimal.Parse(model.ID_Height) : 0;
                        RawMaterails.ID_length = model.ID_length != null && model.ID_length != "" ? decimal.Parse(model.ID_length) : 0;
                        RawMaterails.ID_metric_Units = model.ID_metric_Units != 0 ? model.ID_metric_Units : null;
                        RawMaterails.ID_CBM = model.ID_CBM != null && model.ID_CBM != "" ? decimal.Parse(model.ID_CBM) : 0;

                        RawMaterails.ProductCategorieId = model.ProductCategorieId != 0 ? model.ProductCategorieId : null;
                        RawMaterails.BrandId = model.BrandId != 0 ? model.BrandId : null;
                        RawMaterails.WarehouseId = model.WarehouseId != 0 ? model.WarehouseId : null;
                        RawMaterails.TaxCodeId = model.TaxCodeId != 0 ? model.TaxCodeId : null;
                        RawMaterails.SupplierId = model.SupplierId != 0 ? model.SupplierId : null;

                        RawMaterails.LastModificationTime = DateTime.Now;
                        RawMaterails.LastModifierUserId = GetUserId;
                        RawMaterails.IsActive = true;

                        _DbContext.RawMaterails.Update(RawMaterails);
                        _DbContext.SaveChanges();
                        var data = _DbContext.UploadImages.Where(x => x.RawMaterailId == RawMaterails.RMId).ToList();
                        _DbContext.UploadImages.RemoveRange(data);
                        _DbContext.SaveChanges();
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
                                _DbContext.UploadImages.Add(uploadImage);
                                _DbContext.SaveChanges();
                            }
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public bool DeleteRawMaterails(long RMId)
        {
            Boolean result = false;

            try
            {
                if (RMId != 0 && RMId >= 0)
                {
                    var data = _DbContext.RawMaterails.FirstOrDefault(x => x.RMId == RMId);
                    if (data != null)
                    {
                        var image = _DbContext.UploadImages.Where(x => x.RawMaterailId == data.RMId).ToList();
                        if (image != null)
                        {
                            _DbContext.UploadImages.RemoveRange(image);

                        }
                        _DbContext.RawMaterails.Remove(data);
                        _DbContext.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
    }
}
