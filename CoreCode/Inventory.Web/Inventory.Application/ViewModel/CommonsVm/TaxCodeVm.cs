using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.CommonsVm
{
   public class TaxCodeVm
    {
        public long TaxId { get; set; }
        public string Code { get; set; }
        public double Amount { get; set; }
    }
}
