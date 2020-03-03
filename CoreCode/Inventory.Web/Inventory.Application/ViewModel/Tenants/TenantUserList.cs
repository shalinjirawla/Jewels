using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.Tenants
{
  public  class TenantUserList
    {
        public long TenantId { get; set; }
        public string TenancyName { get; set; }
        public string  UserId { get; set; }
        public string UserName { get; set; }
    }
}
