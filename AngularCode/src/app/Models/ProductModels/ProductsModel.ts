export class CategoriesModel{
    public CategoriesId:undefined|any;
    public CategoriesName:undefined|string;
    public DisplayOrder:undefined|any;
    public Code:undefined|any;
    public Description:undefined|any;
}
export class BrandModel{
    public BrandId:undefined|any;
    public BrandName:undefined|string;
    public Description:undefined|any;
}
export class RawMaterialsModel{
    public RMId:undefined|any;
    public NameOrItem:undefined|any;
    public ItemCode:undefined|any;
    public CategorieId:undefined|any;
    public CategorieName:undefined|any;
    public BrandId:undefined|any;
    public BrandName:undefined|any;
    public StockItem:undefined|any;
    public Taxable:undefined|any;
    public Image:undefined|any;//PENDING
    public BarCode:undefined|any;
    public PurchasePrice:undefined|any;
    public SellingPrice:undefined|any;
    public Description:undefined|any;
    public DefaultSuppliers:undefined|any;
    public DefaultTax:undefined|any;
    public DefaultWhereHouselLocation:undefined|any;
}