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
export class ProductModel{
    public ProductId:undefined|any;
    public Name:undefined|string;
    public CategorieId:undefined|number;
    public BrandId:undefined|number;
    public BatchItem:undefined|boolean;
    public Stockitem:undefined|boolean;
    public Taxable:undefined|boolean;
    public SerialNumber:undefined|boolean;
    public IsRawMaterail:undefined|boolean;
    public IsProductVariants:undefined|boolean;
    public PurchasePrice:undefined|any;
    public SellingPrice:undefined|any;
    public ReorderOuantity:undefined|any
    public MinimumorderQuatity:undefined|any;
    public Desc:undefined|any;
    public DefaultSupplierId:undefined|number;
    public DefaultWarehouseLonctionId:undefined|number;
    public InitialStockHand:undefined|any;
    public InitialStockPrice:undefined|any;
    public InitialLandedCost:undefined|any;
    public InitialStockBatch:undefined|any;
    public BaseUms:undefined|any;
    public OuterWeight:undefined|any;
    public OuterWeighttype:undefined|any;
    public OuterDemensionsWidth:undefined|any;
    public OuterDemensionsHeight:undefined|any;
    public OuterDemensionsLenght:undefined|any
    public OuterDemensionsType:undefined|any;
    public OuterDemensionsCBM:undefined|any;
    public InnerWeight:undefined|any;
    public InnerWeightType:undefined|any;
    public InnerDemensionsWidth:undefined|any;
    public InnerDemensionsHeight:undefined|any;
    public InnerDemensionsLenght:undefined|any
    public InnerDemensionsType:undefined|any;
    public InnerDemensionsCBM:undefined|any;
    public DoesRawMaterial:HasProductRawMaterial[];
}
export class HasProductRawMaterial
{
   public RawMaterialId: undefined|any;
   public UOM:undefined|any;
   public QTY:undefined|any;    
}
export class ProductVariant{
    
}