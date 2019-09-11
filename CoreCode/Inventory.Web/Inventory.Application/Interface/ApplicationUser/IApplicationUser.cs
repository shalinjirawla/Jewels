using Inventory.Application.ViewModel.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.ApplicationUser
{
    public interface IApplicationUser
    {
        Task<Boolean> RegisterTenant(RegisterVm model);
        Task<LoginVm> Login(LoginVm login);
        
    }
}
