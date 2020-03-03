using Inventory.Application.ViewModel.ApplicationUser;
using Inventory.Application.ViewModel.Tenants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.Tenants
{
    public interface ITenants
    {
        Task<Boolean> SaveTenants(TenantsVm model);
        Task<string> RegisterAspnetUser(UserVm registerVm);
        Task<TenantsDetailsVm> GetRegisterDataAsync(long id, string UserId);
        Task<Boolean> RegisterTenantUpdate(UserVm model);
        Task<Boolean> RegisterTenantActived(UserVm model);
        Task<Boolean> ChechTenants(long TenantId);
        Task<Boolean> CheckUserId(string UserId);
        Task<List<TenantUserList>>GetTenantUserList(long TenantId);
    }
}
