using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModel.CustomersVm
{
    public class CustomerContactVm
    {
        public string contactId { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool DefaultContact { get; set; }
        public string Mobile { get; set; }
        public Nullable<long> CountryId { get; set; }
        public string Fax { get; set; }
        public string Office { get; set; }
    }
}
