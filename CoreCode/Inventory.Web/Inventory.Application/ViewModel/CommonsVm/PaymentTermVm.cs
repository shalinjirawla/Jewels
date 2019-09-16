using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.CommonsVm
{
    public class PaymentTermVm
    {
        public long PaymentTermId { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
    }
}
