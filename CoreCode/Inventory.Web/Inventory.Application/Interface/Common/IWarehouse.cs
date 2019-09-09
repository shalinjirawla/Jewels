using Inventory.Application.ViewModel.CommonsVm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Interface.Common
{
    public interface IWarehouse
    {
        List<WarehouseVm> GetWarehouseListAsync();
        WarehouseVm GetWarehouseAsync(long id);
        string SaveWarehouseListAsync(WarehouseVm model);
        Boolean DeleteWarehouseAsync(long id);
        List<WarehouseVm> GetActiveWarehouseListAsync();
        Boolean UpdateWarehouseStatusAsync(long id, bool status);
    }
}
