using Inventory.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interface
{
    public interface IDiscountType
    {
        Task<long> SaveDiscountType(DiscountTypeVm discountTypeVm);
        Task<DiscountTypeVm> GetDiscountType(long DiscountTypeId);
        Task<List<DiscountTypeVm>> GetDiscounytTypeList();
        Task<long> UpdateDiscountType(long DiscountTypeId, DiscountTypeVm discountTypeVm);
        Task<long> DeleteDiscountType(long DiscountTypeId);

    }
}
