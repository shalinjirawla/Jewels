using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Currency
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string Code { get; set; }
        public Nullable<DateTime> CreationTime { get; set; }
        [ForeignKey("User")]
        public string CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        [ForeignKey("UserId")]
        public string LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }

        public ApplicationUser.ApplicationUser User { get; set; }
        public ApplicationUser.ApplicationUser UserId { get; set; }
    }
}
