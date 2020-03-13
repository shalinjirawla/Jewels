using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
   public class PurchaseOrderAdditionalChargeForAllVM
    {
        public long AdditionalForAllId { get; set; }
        public long PurchaseOrdersId { get; set; }
        public long? AdditionalChargeId { get; set; }
        public long? TaxId { get; set; }
    }
}
