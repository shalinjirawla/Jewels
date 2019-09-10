using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventory.Application.ViewModel.ApplicationUser
{
    public class LoginModel
    {
        public string UserId { get; set; }
        public string EmailId { get; set; }
        [Required(ErrorMessage = "User Name Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        public Boolean RememberMe { get; set; }
        public string AccessToken { get; set; }
        public string ReturnUrl { get; set; }
        public long TenantId { get; set; }
    }
}
