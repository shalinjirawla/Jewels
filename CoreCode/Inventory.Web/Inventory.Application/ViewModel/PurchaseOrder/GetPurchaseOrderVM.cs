﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.SalesOrder
{
    public class GetPurchaseOrderVM
    {
        //sales Order start
        public long PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? DateOrdered { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public long SupplierId { get; set; }
        public string Remarks { get; set; }
        public long? CreditTermId { get; set; }
        public long? PaymentTermId { get; set; }
        public long? ShipmentMethodId { get; set; }
        public long? CurrencyId { get; set; }
        public double? ExchangeRate { get; set; }
        public int PaymentStatus { get; set; } = 0;//PaymentStatus==0(InProgress),1(Partial Paid),2(Paid),3(Issued)
        public int ReceiveStatus { get; set; } = 0;//ReceiveStatus==0(InProgress),1(Delivered),2(Partial Delivered),3(Partially Issued),4(Issued)
        public long TenantId { get; set; }
        public Boolean IsActive { get; set; }
        //sales Order End
        //sales Order details start
        public long PurchaseOrderDetailsId { get; set; }
        public long? TotalQTY { get; set; }
        public double? Total { get; set; }
        public double? FinalTotal { get; set; }
        public Boolean? TaxInclude { get; set; }
        public double? FinalTaxTotal { get; set; }
        public double? AdditionalChargeAmount { get; set; }
        public Boolean? IsAdditionalChargeApply { get; set; }
        public string IsAdditionalChargeApplyType { get; set; }
       
    }
}
