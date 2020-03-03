using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.CommonsVm
{
   public class AdditionalChargeVM
    {
        public long AdditionalChargeId { get; set; }
        public string Name { get; set; }
        public string UnitPriceType { get; set; }
        public string UnitPrice { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        public Nullable<long> TenantsId { get; set; }
    }
}
