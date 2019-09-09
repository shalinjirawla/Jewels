using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventory.Application.ViewModel.ApplicationUser
{
   public class RegisterVm
    {
        public long TenantId { get; set; }//Company Id
        public string TenantName { get; set; }//Company Name
        public string BusinessRegisterNumber { get; set; }
        public string TagRegisterNumber { get; set; }
        [Required, MaxLength(256)]
        public string EmailId { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string Logo { get; set; }
        public Nullable<Boolean> IsActive { get; set; }
        public Nullable<long> IsInTrialPeriod { get; set; }
        public Nullable<DateTime> SubscriptionEndDateUtc { get; set; }
    }
}
