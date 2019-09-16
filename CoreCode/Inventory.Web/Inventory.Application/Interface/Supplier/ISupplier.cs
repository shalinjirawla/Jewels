using Inventory.Application.ViewModel.SupplierVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.Supplier
{
    public interface ISupplier
    {
        Task<long> AddUpadteSupplier(SupplierVm Model, string UserId, long TenantId);
        Task<List<SupplierVm>> GetSupplierListAsyn();
        Task<SupplierVm> GetSupplierByIdAsyc(long Id);
        Task<Boolean> DeleteSupplierAsyc(long Id);
    }
}
