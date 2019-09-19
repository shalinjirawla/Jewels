using Inventory.Application.ViewModel.UploadImageVm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.RawMaterails
{
    public class SaveRawMaterailsVm
    {
        public long RMId { get; set; }
        public string RMName { get; set; }
        public string AlternativeRMName { get; set; }
        public string Itemcode { get; set; }
        public string Purchase_Price { get; set; }
        public string Selling_Price { get; set; }
        public string Description { get; set; }
        public Boolean StockItem { get; set; }
        public Boolean Taxable { get; set; }
        public string IStockOnHand { get; set; }
        public string ICostPrice { get; set; }
        public string ILandedCost { get; set; }
        public string Reorder_Quantity { get; set; }
        public string Minimumu_Order_Quantity { get; set; }

        public Nullable<long> UOMId { get; set; }

        public string Outer_Weight { get; set; }
        public Nullable<long> Outer_Weight_metric_Units { get; set; }
        public string Inner_Weight { get; set; }
        public Nullable<long> Inner_Weight_metric_Units { get; set; }

        public string OD_Width { get; set; }
        public string OD_Height { get; set; }
        public string OD_length { get; set; }
        public Nullable<long> OD_metric_Units { get; set; }
        public string OD_CBM { get; set; }

        public string ID_Width { get; set; }
        public string ID_Height { get; set; }
        public string ID_length { get; set; }
        public Nullable<long> ID_metric_Units { get; set; }
        public string ID_CBM { get; set; }

        public Nullable<long> ProductCategorieId { get; set; }
        public Nullable<long> BrandId { get; set; }
        public Nullable<long> WarehouseId { get; set; }
        public Nullable<long> TaxCodeId { get; set; }
        public Nullable<long> SupplierId { get; set; }

       public ImageListVm PictureList { get; set; }
    }
}
