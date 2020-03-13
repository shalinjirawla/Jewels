using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
   public class PurchaseOrderMergeVM
    {
        public PurchaseOrdersVM PurchaseOrdersVM { get; set; }
        public PurchaseOrderDetailsVM PurchaseOrderDetailsVM { get; set; }
        public List<PurchaseOrderItemsVM> PurchaseOrderItemsVM { get; set; }
    }
}
