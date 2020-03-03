using Inventory.Application.ViewModel.SalesOrder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.SalesOrder
{
   public interface ISalesOrder
    {
        Task<long> SaveSalesOrder(SalesOrderMergeVM input,string UserId,long TenantId);
        Task<List<SalesOrderListVM>> SalesOrderList(long TenantId);
        Task DeleteSalesOrder(long SalesOrderId);
        Task<SalesOrderMergeVM> GetSalesOrderDetails(long SalesOrderId);
    }
}
