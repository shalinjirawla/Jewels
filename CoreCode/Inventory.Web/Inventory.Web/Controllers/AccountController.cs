using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
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
        private readonly SessionHanlderController _SessionHanlderController;
        public Boolean Status = false;
        public string Message = "";
        public object Data = null;
        public string UserId = "";
        public AccountController(UserManager<ApplicationUser> UserManager,
            IApplicationUser applicationUser,
             SignInManager<ApplicationUser> signInManager,
             SessionHanlderController SessionHanlderController
             )
        {
            _UserManager = UserManager;
            _applicationUser = applicationUser;
            _signInManager = signInManager;
            _SessionHanlderController = SessionHanlderController;
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
            try
            {


                if (ModelState.IsValid)
                {

                    var loginRequestUser = _UserManager.Users.FirstOrDefault(x => x.UserName == LoginVm.UserName);
                    if (loginRequestUser != null)
                    {
                        var passwordValidationStatus = await _UserManager.CheckPasswordAsync(loginRequestUser, LoginVm.Password);
                        if (passwordValidationStatus)
                        {
                            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(loginRequestUser);
                            await _signInManager.RefreshSignInAsync(loginRequestUser);
                            Status = true;
                            Message = "Login Successfully...!";
                            if (loginRequestUser.Id != null)
                            {
                                _SessionHanlderController.SetUserId(HttpContext, loginRequestUser.Id, loginRequestUser.TenantId);

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
            }
            catch (Exception e)
            {

                throw e;
            }
            return Ok(GetAjaxResponse(Status, Message, LoginVm));
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult logout()
        {
            _SessionHanlderController.LogOut(HttpContext);
            Message = "Log Out Suceessfully.";
            Status = true;
            Data = null;
            return Ok(GetAjaxResponse(Status, Message, Data));
        }
        

    }
}