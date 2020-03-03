using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
    public class GetSalesOrderVM
    {
        //sales Order start
        public long SalesOrdersId { get; set; }
        public string SalesOrderNumber { get; set; }
        public DateTime? DateOrdered { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public long CustomerId { get; set; }
        public string CustomerPurchesOrderNumber { get; set; }
        public string Remarks { get; set; }
        public long? SalesOrderTypeId { get; set; }
        public long? CreditTermId { get; set; }
        public long? ShipmentMethodId { get; set; }
        public long? CurrencyId { get; set; }
        public long? SalesOrderRepId { get; set; }

        public int PaymentStatus { get; set; } = 0;//PaymentStatus==0(InProgress),1(Partial Paid),2(Paid),3(Issued)
        public int ShipmentStatus { get; set; } = 0;//Shipment==0(InProgress),1(Delivered),2(Partial Delivered),3(Partially Issued),4(Issued)
        public long TenantId { get; set; }
        public Boolean IsActive { get; set; }
        //sales Order End
        //sales Order details start
        public long SalesOrderDetailsId { get; set; }
        public long? TotalQTY { get; set; }
        public double? Total { get; set; }
        public double? FinalTotal { get; set; }
        public Boolean? TaxInclude { get; set; }
        public double? FinalTaxTotal { get; set; }
        public double? AdditionalChargeAmount { get; set; }
        public Boolean? IsAdditionalChargeApply { get; set; }
        public string IsAdditionalChargeApplyType { get; set; }
        //sales order details end

        //sales order items start
        //public long OrderItemsId { get; set; }
        //public long? ProductId { get; set; }
        //public int? Unit { get; set; }
        //public double? UnitPrice { get; set; }
        //public int? QTY { get; set; }
        //public int? DiscountType { get; set; }
        //public double? Discount { get; set; }
        //public long? TaxId { get; set; }
        //public Boolean? IsTaxble { get; set; }
        //public double? TaxTotal { get; set; }
        //public double? ItemTotal { get; set; }
        //sales order items end
    }
}
