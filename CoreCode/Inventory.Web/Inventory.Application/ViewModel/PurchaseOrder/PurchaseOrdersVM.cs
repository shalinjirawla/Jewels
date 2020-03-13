using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
   public class PurchaseOrdersVM
    {
        public long PurchaseOrdersId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime? DateOrdered { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public long SupplierId { get; set; }
        public string ReferenceNumber { get; set; }
        public string Remarks { get; set; }
        public long? PaymentTermId { get; set; }
        public long? CreditTermId { get; set; }
        public long? ShipmentMethodId { get; set; }
        public long? CurrencyId { get; set; }
        public double? ExchangeRate { get; set; }

        public int PaymentStatus { get; set; } = 0;//PaymentStatus==0(InProgress),1(Partial Paid),2(Paid),3(Issued)
        public int RecieveStatus { get; set; } = 0;//RecieveStatus==0(InProgress),1(Delivered),2(Partial Delivered),3(Partially Issued),4(Issued)

    }
}
