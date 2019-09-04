using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.CommonsVm
{
   public class CreditTermsVm
    {
        public long CreditTermId { get; set; }
        public string Code { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
    }
}
