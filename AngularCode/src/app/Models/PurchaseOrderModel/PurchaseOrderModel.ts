export class PurchaseOrdersVM {
    PurchaseOrdersId: Number;
    PurchaseOrderNumber: string;
    DateOrdered: Date;
    EstimatedDeliveryDate: Date;
    SupplierId: number;
    ReferenceNumber: any;
    Remarks: string;
    PaymentTermId: number;
    CreditTermId: number;
    ShipmentMethodId: number;
    ExchangeRate: number;
    CurrencyId: number;
    PaymentStatus = 0;//PaymentStatus==0(InProgress),1(Partial Paid),2(Paid),3(Issued)
    ReceiveStatus = 0;//ReceiveStatus==0(InProgress),1(Delivered),2(Partial Delivered),3(Partially Issued),4(Issued)
}
export class PurchaseOrderDetailsVM {
    PurchaseOrderDetailsId: number;
    PurchaseOrdersId: number;
    AdditionalCharge: any;
    TotalQTY: any;
    Total: any;
    FinalTotal: any;
    TaxInclude: boolean;
    FinalTaxTotal: any;
    AdditionalChargeAmount: any;
    IsAdditionalChargeApply: boolean;
    IsAdditionalChargeApplyType: string;
    AdditionalChargeForAll:AdditionalChargeForAll[]=[];
    AdditionalChargeForProduct:AdditionalChargeForProduct[]=[];
}
export class PurchaseOrderItemsVM {
    OrderItemsId: number;
    PurchaseOrdersId: number;
    ProductId: number;
    UnitPrice: any;
    Unit: any;
    QTY: any;
    DiscountType: any;
    Discount: any;
    TaxId: number;
    IsTaxble: boolean;
    TaxTotal: any;
    Total: any;
}
export class AdditionalChargeForAll {
    AdditionalForAllId: number;
    PurchaseOrdersId: number;
    AdditionalChargeId: number;
    TaxId: number;
}
export class AdditionalChargeForProduct {
    AdditionalChargeForProductId: number;
    PurchaseOrdersId: number;
    ProductId: number;
    AdditionalChargeId: number;
    IsTaxble: boolean;
    TaxId: number;
}
export class PurchaseOrderMergeVM {
    PurchaseOrdersVM: PurchaseOrdersVM;
    PurchaseOrderDetailsVM: PurchaseOrderDetailsVM;
    PurchaseOrderItemsVM: PurchaseOrderItemsVM[] = [];
}