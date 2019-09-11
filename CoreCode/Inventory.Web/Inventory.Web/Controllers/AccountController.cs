using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public AccountController(UserManager<ApplicationUser> UserManager,
            IApplicationUser applicationUser,
             SignInManager<ApplicationUser> signInManager)
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
                            SetCurrentLoginUserId(loginRequestUser.Id);
                        }
                    }
                    else
                    {
                        Status = false;
                        Message = "Invalid Username and Password.";
                        LoginVm = null;
                    }

                }
                else {
                    Status = false;
                    Message = LoginVm.UserName+ " Username is not Exist...";
                    LoginVm = null;
                }
                
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, LoginVm));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            if (ModelState.IsValid)
            {
                Status = await _applicationUser.RegisterTenant(model);
                if (Status)
                {
                    LoginVm LoginVm = new LoginVm
                    {
                        UserName = model.EmailId,
                        Password = model.Password,
                    };
                    Data = Login(LoginVm);
                    Message = "Regsiter Successfully Completed...";
                }
                else
                {
                    Message = model.EmailId + " Email Id Alredy Exist..";
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, Data));
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(GetAjaxResponse(true, "ok", null));
        }
        [HttpGet]
        public Boolean SetCurrentLoginUserId(string UserId)
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                HttpContext.Session.SetString("UserId", UserId);
            }
            return true;
        }
        [HttpGet]
        public string GetUserId()
        {
            string UserId = HttpContext.Session.GetString("UserId");
            return UserId;
        }
    }
}