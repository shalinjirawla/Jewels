using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventory.Application.ViewModel.ApplicationUser
{
   public class UserVm
    {
        public long TenantId { get; set; }//Company Id
       
        [Required, MaxLength(256)]
        public string EmailId { get; set; }
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
       
    }
}
