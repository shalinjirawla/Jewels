using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Customer
{
    public class CustomerContacts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerContactId { get; set; }
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Office { get; set; }

        public Customer Customer { get; set; }
    }
}
