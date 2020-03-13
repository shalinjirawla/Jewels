
using Inventory.Application.ViewModel.PurchaseOrder;
using Inventory.Application.ViewModel.SalesOrder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.PurchaseOrder
{
   public interface IPurchaseOrder
    {
        Task<long> SavePurchaseOrder(PurchaseOrderMergeVM input, string UserId, long TenantId);
        Task<List<PurchaseOrderListVM>> PurchaseOrderList(long TenantId);
        Task DeletePurchaseOrder(long PurchaseOrderId);
        Task<PurchaseOrderMergeVM> GetPurchaseOrderDetails(long PurchaseOrderId);
        Task<List<GetPurchaseOrderListIdBySuppliersVM>> GetPucrahseOrderListIdBySupplier(long SupplierId);
        Task<List<ProductListIdByPurchaseOrder>> GetProductListIdByPurchasOrder(long PurchaseOrderId);
    }
}
