export class SalesOrderDetailsModel {
    public SalesOrderId: undefined | string;
    public FormCompany: undefined | string;
    public DateOrdered: undefined | Date;
    public EstimatedDeliveryDate: undefined | Date;
    public CustomerId: undefined | any;
    public CustomerPurchesOrderNumber: undefined | any;
    public Remarks: undefined | string;
}
export class SalesOrderTypeModel {
    SalesOrderType: undefined | any;
    Creditterms: undefined | any;
    ShippingMethod: undefined | any;
    SalesRep: undefined | any;
    Currency: undefined | any;
}
export class SalesOrderItemsModel {
    public SalesOrderType: undefined | any;
    public CreaditTerms: undefined | any;
    public ShipmentMethod: undefined | any;
    public SalesRep: undefined | any;

}