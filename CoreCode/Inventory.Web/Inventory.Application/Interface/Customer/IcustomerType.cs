using Inventory.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interface.Customer
{
    public interface IcustomerType
    {
        List<CustomerTypeVm> GetCustomerTypeList();
        CustomerTypeVm GetCustomerTypeById(int id);
        int AddCustomerTypeAsyc(CustomerTypeVm model, string UserId, long TenantId);
        int DeleteCustomerTypeAsyc(int Id);
    }
}
