using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Commons
{
    public class Metric_Units
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Metric_UnitsId { get; set; }
        public string Metric_UnitsName { get; set; }
        public long Metric_UnitsType { get; set; }
    }
}
