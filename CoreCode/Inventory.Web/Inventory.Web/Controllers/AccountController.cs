using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using Inventory.Application.Interface.ApplicationUser;
using Inventory.Application.ViewModel.ApplicationUser;
using Inventory.Core.Models.ApplicationUser;
using Inventory.Web.share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        //private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUser _applicationUser;
        public Boolean Status = false;
        public string Message = "";
        public object Data = null;
        public string UserId = "";
        public IPrincipal user;
        public AccountController(UserManager<ApplicationUser> UserManager,
            IApplicationUser applicationUser,
             SignInManager<ApplicationUser> signInManager
             )
        {
            _UserManager = UserManager;
            _applicationUser = applicationUser;
            _signInManager = signInManager;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVm LoginVm)
        {
            if (ModelState.IsValid)
            {
               
                var loginRequestUser = _UserManager.Users.FirstOrDefault(x => x.UserName == LoginVm.UserName);
                if (loginRequestUser != null)
                {
                    var passwordValidationStatus = await _UserManager.CheckPasswordAsync(loginRequestUser, LoginVm.Password);
                    if (passwordValidationStatus)
                    {
                        Status = true;
                        Message = "Login Successfully...!";
                        if (loginRequestUser.Id != null)
                        {
                            LoginVm.UserId = loginRequestUser.Id;
                            LoginVm.EmailId = loginRequestUser.Email;
                            LoginVm.UserName = loginRequestUser.UserName;
                            LoginVm.TenantId = loginRequestUser.TenantId;
                            LoginVm = await _applicationUser.Login(LoginVm);
                        }
                    }
                    else
                    {
                        Status = false;
                        Message = "Invalid Username and Password.";
                        LoginVm = null;
                    }

                }
                else
                {
                    Status = false;
                    Message = LoginVm.UserName + " Username is not Exist...";
                    LoginVm = null;
                }

            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, LoginVm));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var re = Request;
            var headers = re.Headers;
            
            string id = _applicationUser.GetUserId();
            if (headers.ContainsKey("Custom"))
            {
                string token = headers.GetCommaSeparatedValues("Custom").First();
            }
            return Ok(GetAjaxResponse(true, "ok", null));
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult logout()
        {
            _applicationUser.Logout();
            Message = "Log Out Suceessfully.";
            Status = true;
            Data = null;
            return Ok(GetAjaxResponse(Status, Message, Data));
        }
    }
}