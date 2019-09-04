using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel
{
    public class CurrencyVm
    {
        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string Code { get; set; }
        public Boolean Status { get; set; }
    }
}
