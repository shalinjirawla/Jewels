using Inventory.Application.ViewModel.ApplicationUser;
using Inventory.Application.ViewModel.Tenants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.ApplicationUser
{
    public interface IApplicationUser
    {
        Task<LoginVm> Login(LoginVm login);
        Task<Boolean> SetCurrentLoginUserIdandTenantId(string UserId,long TenantId);
        Task<string> GetUserId();
        Task<long> GetTenantId();
        Task<Boolean> Logout();
    }
}
