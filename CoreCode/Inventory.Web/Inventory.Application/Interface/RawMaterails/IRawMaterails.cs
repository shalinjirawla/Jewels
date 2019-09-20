using Inventory.Application.ViewModel.RawMaterails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interface.RawMaterails
{
    public interface IRawMaterails
    {
        Boolean SaveRawMaterails(SaveRawMaterailsVm model, string GetUserId, long GetTenantId);
        List<SaveRawMaterailsVm> GetRawMaterailsList();
        SaveRawMaterailsVm GetRawMaterails(long rMId);
        Boolean UpdateRawMaterails(long RMId, SaveRawMaterailsVm model, string GetUserId, long GetTenantId);
        Boolean DeleteRawMaterails(long RMId);
    }
}
