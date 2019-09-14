export class Supplier{
    
    public SupplierId:undefined|any;
    public CompanyName:undefined|any;
    public SupplierCode:undefined|any;
    public Website:undefined|any;
    public Remarks:undefined|any;
    public DefaultCurrency:undefined|any;
    public DefaultPaymentTerms:undefined|any;
    public DefaultTax:undefined|any;
    public ShipmentTerms:undefined|any;
    public Shipmentmethod:undefined|any;
}
export class SuppliersAddressModel{
    AddressId:number;
    AddressType : string;
    Address : string;
    Country : string;
    State : string;
    City : string;
    PostalCode : string;  
    DefaultAddress:boolean;
}

export class SuppliersContactModel{
    Designation:string;
    Email:string;
    FirstName:string;
    LastName:string;
    Mobile:string;
    CountryId:number;
    Fax:string;
    Office:string;
}