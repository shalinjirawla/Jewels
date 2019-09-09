using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.Core.Models.Tenants
{
   public class Tenants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TenantId { get; set; }//Company Id
        public string TenantName { get; set; }//Company Name
        public string BusinessRegisterNumber { get; set; }
        public string TagRegisterNumber { get; set; }
        public string EmailId { get; set; }
        public Nullable<DateTime> CreationTime { get; set; }
        public Nullable<long> CreatorUserId { get; set; }
        public Nullable<DateTime> LastModificationTime { get; set; }
        public Nullable<long> LastModifierUserId { get; set; }
        public Boolean IsActive { get; set; }
        public string Logo { get; set; }
        public Nullable<Boolean> IsInTrialPeriod { get; set; }
        public Nullable<DateTime> SubscriptionEndDateUtc { get; set; }
    }
}
