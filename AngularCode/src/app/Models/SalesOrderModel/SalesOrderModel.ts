export class SalesOrdersVM {
    SalesOrdersId: Number;
    SalesOrderNumber: string;
    DateOrdered: Date;
    EstimatedDeliveryDate: Date;
    CustomerId: number;
    CustomerPurchesOrderNumber: any;
    Remarks: string;
    SalesOrderTypeId: number;
    SalesOrderRepId: number;
    CreditTermId: number;
    ShipmentMethodId: number;
    CurrencyId: number;
    PaymentStatus = 0;//PaymentStatus==0(InProgress),1(Partial Paid),2(Paid),3(Issued)
    ShipmentStatus = 0;//Shipment==0(InProgress),1(Delivered),2(Partial Delivered),3(Partially Issued),4(Issued)
}
export class SalesOrderDetailsVM {
    SalesOrderDetailsId: number;
    SalesOrdersId: number;
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
export class SalesOrderItemsVM {
    OrderItemsId: number;
    SalesOrdersId: number;
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
    SalesOrdersId: number;
    AdditionalChargeId: number;
    TaxId: number;
}
export class AdditionalChargeForProduct {
    AdditionalChargeForProductId: number;
    SalesOrdersId: number;
    ProductId: number;
    AdditionalChargeId: number;
    IsTaxble: boolean;
    TaxId: number;
}
export class SalesOrderMergeVM {
    SalesOrdersVM: SalesOrdersVM;
    SalesOrderDetailsVM: SalesOrderDetailsVM;
    SalesOrderItemsVM: SalesOrderItemsVM[] = [];
}