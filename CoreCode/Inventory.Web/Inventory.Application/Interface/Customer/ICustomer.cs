using Inventory.Application.ViewModel;
using Inventory.Application.ViewModel.CustomersVm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface
{
    public interface ICustomer
    {
        Task<long> AddCustomer(AddCustomerVm Model,string UserId, long TenantId);
        List<CustomerVm> GetCustomerListAsyn();
        AddCustomerVm GetCustomerByIdAsyc(int Id);
        int DeleteCustomerAsyc(int Id);
        CustomerAddress GetCustomerAddress(int CustomerId);
    }
}
