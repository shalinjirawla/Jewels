using System;
using System.Collections.Generic;
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
        private readonly IApplicationUser _applicationUser;
        public Boolean Status = false;
        public string Message = "";
        public object Data=null;
        public AccountController(UserManager<ApplicationUser> UserManager, IApplicationUser applicationUser)
        {
            _UserManager = UserManager;
            _applicationUser = applicationUser;
        }
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginProcess(LoginModel loginModel)
        {
            if (ModelState.IsValid) {
                loginModel = await _applicationUser.Login(loginModel);
                if (loginModel != null) {
                    Status = true;
                    Message = "Login Successfully...!";
                    var user = await _UserManager.FindByEmailAsync(loginModel.UserName);
                    if(user!=null)
                    {
                           SetCurrentLoginUserId(user?.Id);
                    }
                }
                else { Status = false; Message = "Error Occurs..!"; }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status, Message, loginModel));
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            if (ModelState.IsValid)
            {
               Status=await _applicationUser.RegisterTenant(model);
                if (Status) {
                    LoginModel loginModel = new LoginModel
                    {
                        UserName = model.EmailId,
                        Password = model.Password,
                    };
                    Data = LoginProcess(loginModel);
                    Message = "Regsiter Successfully Completed...";
                }
                else
                {
                    Message = model.EmailId+ " Email Id Alredy Exist..";
                }
            }
            else { return BadRequest(); }
            return Ok(GetAjaxResponse(Status,Message, Data));
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(GetAjaxResponse(true, "ok", null));
        }
        [HttpGet]
        public  Boolean SetCurrentLoginUserId(string UserId)
        {
            if (!string.IsNullOrEmpty(UserId)) {
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