using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.CommonsVm
{
    public class WarehouseVm
    {
        public long WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public long Warehousecode { get; set; }
        public Boolean IsActive { get; set; }
    }
}
