using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.RawMaterails
{
    public class RawMaterailsVm
    {
        public long RMId { get; set; }
        public string RMName { get; set; }
        public string AlternativeRMName { get; set; }
        public string Itemcode { get; set; }
        public Nullable<decimal> Purchase_Price { get; set; }
        public Nullable<decimal> Selling_Price { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> IStockOnHand { get; set; }
        public Nullable<decimal> ICostPrice { get; set; }
        public Nullable<decimal> ILandedCost { get; set; }
        public Nullable<long> Reorder_Quantity { get; set; }
        public Nullable<long> Minimumu_Order_Quantity { get; set; }




        public Nullable<long> ProductCategorieId { get; set; }
        public Nullable<long> BrandId { get; set; }
        public Nullable<long> WarehouseId { get; set; }
        public Nullable<long> TaxCodeId { get; set; }
        public Nullable<long> SupplierId { get; set; }
        public Nullable<long> UOMId { get; set; }
    }
}
