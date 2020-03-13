using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
   public class PurchaseOrderAdditionalChargeForProductVM
    {
        public long AdditionalChargeForProductId { get; set; }
        public long PurchaseOrdersId { get; set; }
        public long? ProductId { get; set; }
        public long? AdditionalChargeId { get; set; }
        public Boolean? IsTaxble { get; set; }
        public long? TaxId { get; set; }
    }
}
