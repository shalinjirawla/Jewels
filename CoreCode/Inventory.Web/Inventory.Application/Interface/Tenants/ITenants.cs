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
    }
}
