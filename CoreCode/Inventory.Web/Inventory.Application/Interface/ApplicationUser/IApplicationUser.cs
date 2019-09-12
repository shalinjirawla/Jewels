using Inventory.Application.ViewModel.ApplicationUser;
using Inventory.Application.ViewModel.Tenants;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.ApplicationUser
{
    public interface IApplicationUser
    {
        Task<LoginVm> Login(LoginVm login);
        Task<Boolean> SetCurrentLoginUserIdandTenantId(string UserId,long TenantId);
        string GetUserId();
        string GetUserId1( IPrincipal user);
        long GetTenantId();
        Task<Boolean> Logout();
    }
}
