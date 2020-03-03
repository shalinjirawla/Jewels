using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
   public class SalesOrderAdditionalChargeForAllVM
    {
        public long AdditionalForAllId { get; set; }
        public long SalesOrdersId { get; set; }
        public long? AdditionalChargeId { get; set; }
        public long? TaxId { get; set; }
    }
}
