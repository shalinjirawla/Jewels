using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
   public class SalesOrderListVM
    {
        public long SalesOrdersId { get; set; }
        public string SalesOrderNumber { get; set; }
        public long? SalesOrderRepId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int PaymentStatus { get; set; } = 0;//PaymentStatus==0(InProgress),1(Partial Paid),2(Paid),3(Issued)
        public int ShipmentStatus { get; set; } = 0;//Shipment==0(InProgress),1(Delivered),2(Partial Delivered),3(Partially Issued),4(Issued)
        public DateTime? DateOrdered { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public double Total { get; set; }
        public long TenantId { get; set; }
        public Boolean IsActive { get; set; }
    }
}
