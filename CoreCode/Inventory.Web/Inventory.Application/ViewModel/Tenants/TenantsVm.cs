    using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.Tenants
{
  public  class TenantsVm
    {
        public long TenantId { get; set; }//Company Id
        public string TenantName { get; set; }//Company Name
        public string EmailId { get; set; }//Email Id With Login a user
        public string Password { get; set; }//Passowrd With Login a user
    }
}

