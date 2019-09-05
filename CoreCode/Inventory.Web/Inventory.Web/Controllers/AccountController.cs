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
                Message =await _applicationUser.Login(loginModel);
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
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "hemant@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "hemant",
                };
                var result=await _UserManager.CreateAsync(user,model.Password);

                if (result.Succeeded)
                {
                  //  await _UserManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else { return BadRequest(); }
            return Ok(true);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(GetAjaxResponse(true, "ok", null));
        }
    }
}