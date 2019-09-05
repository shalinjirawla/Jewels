using Inventory.Application.ViewModel.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface.ApplicationUser
{
    public interface IApplicationUser
    {
        Task<string> Login(LoginModel login);
    }
}
