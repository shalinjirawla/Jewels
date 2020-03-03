using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.CommonsVm
{
   public class SalesOrderTypeVM
    {
        public long SalesOrderTypeId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public Boolean Active { get; set; }
        public Nullable<DateTime> CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        public Nullable<long> TenantsId { get; set; }
    }
}
