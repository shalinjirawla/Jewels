using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
    public class SalesOrderDetailsVM
    {
        public long SalesOrderDetailsId { get; set; }
        public long SalesOrdersId { get; set; }
       // public string AdditionalChargeType { get; set; }
        public long? TotalQTY { get; set; }
        public double? Total { get; set; }
        public double? FinalTotal { get; set; }
        public Boolean? TaxInclude { get; set; }
        public double? FinalTaxTotal { get; set; }
        public double? AdditionalChargeAmount { get; set; }
        public Boolean? IsAdditionalChargeApply { get; set; }
        public string IsAdditionalChargeApplyType { get; set; }
        public List<SalesOrderAdditionalChargeForAllVM> AdditionalChargeForAll { get; set; }
        public List<SalesOrderAdditionalChargeForProductVM> AdditionalChargeForProduct { get; set; }
    }
}
