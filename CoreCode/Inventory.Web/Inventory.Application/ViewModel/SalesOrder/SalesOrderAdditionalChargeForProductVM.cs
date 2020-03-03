using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
   public class SalesOrderAdditionalChargeForProductVM
    {
        public long AdditionalChargeForProductId { get; set; }
        public long SalesOrdersId { get; set; }
        public long? ProductId { get; set; }
        public long? AdditionalChargeId { get; set; }
        public Boolean? IsTaxble { get; set; }
        public long? TaxId { get; set; }
    }
}
