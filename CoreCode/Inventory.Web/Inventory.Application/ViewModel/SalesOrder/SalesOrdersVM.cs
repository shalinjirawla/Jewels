using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
   public class SalesOrdersVM
    {
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

    }
}
