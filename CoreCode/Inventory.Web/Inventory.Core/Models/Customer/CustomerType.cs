using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Customer
{
    public class CustomerType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerTypeId { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
 



    }
}
