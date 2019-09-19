using Inventory.Application.ViewModel.RawMaterails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interface.RawMaterails
{
    public interface IRawMaterails
    {
        Boolean SaveRawMaterails(SaveRawMaterailsVm model, string GetUserId, long GetTenantId);
    }
}
