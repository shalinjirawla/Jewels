using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
   public class SalesOrderMergeVM
    {
        public SalesOrdersVM SalesOrdersVM { get; set; }
        public SalesOrderDetailsVM SalesOrderDetailsVM { get; set; }
        public List<SalesOrderItemsVM> SalesOrderItemsList { get; set; }
    }
}
