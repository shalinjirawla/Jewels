export class ReceiveNotesModel
{
    ReceiveNoteId: Number;
    ReceiveNoteNumber:string;
    ReceiveDate: Date;
    SupplierId: number;
    Remarks: string;
    IsActive:boolean;
    ProductList:any;
}
export class ReceiveNotesItemsModel{
    ReceiveNoteItemId: Number;
    ReceiveNoteId: Number;
    PurchaseOrdersId: Number;
    ProductId:number;
    WarehouseId:number;
    ProductQTY:any;
}