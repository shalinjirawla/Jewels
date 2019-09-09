using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Inventory.Core.Models.ApplicationUser;

namespace Inventory.Core.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
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
