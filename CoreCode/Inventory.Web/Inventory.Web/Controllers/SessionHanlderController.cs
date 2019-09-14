using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Inventory.Core.Models.ApplicationUser;
using System.Security.Principal;
using Inventory.Application.Interface.ApplicationUser;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SessionHanlderController : ControllerBase
    {
        public string Message = "";
        private string UserEmail;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Boolean Status = false;
        public string UserId = "";
        public long TenantId = 0;
        public SessionHanlderController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserEmail = _httpContextAccessor.HttpContext.User.Claims
                        .FirstOrDefault(c => c.Type == "preferred_username")?.Value;

        }
        [HttpGet]
        public Boolean SetUserId(HttpContext httpContext, string UserId,long TenantId)
        {
            Boolean Result = false;
            try
            {
                if (httpContext != null && !string.IsNullOrEmpty(UserId) && TenantId!=0)
                {
                    httpContext.Session.SetString("UserId", UserId);
                    string stringTenantId = Convert.ToString(TenantId);
                    httpContext.Session.SetString("TenantId", stringTenantId);
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
           

            return Result;
        }
        [HttpGet]
        public string GetUserId(HttpContext httpContext)
        {
           
            try
            {
                if (httpContext != null)
                {
                    UserId = httpContext.Session.GetString("UserId");
                    if (string.IsNullOrEmpty(UserId))
                    {
                        UserId = "User Id Not login";
                        if (httpContext.User != null && httpContext.User.Claims!=null) {
                            UserId = httpContext.User.Claims.ToArray()[3].Value;
                        }
                    }
                   
                }
                else
                {
                    UserId = "httpContext Not Fond";
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            
            return UserId;
        }
        [HttpGet]
        public long GetTenantId(HttpContext httpContext)
        {
            try
            {
                if (httpContext != null)
                {
                    string stringTenatId = httpContext.Session.GetString("TenantId");
                    if (!string.IsNullOrEmpty(stringTenatId)) {
                        TenantId = Convert.ToInt64(stringTenatId);
                    }
                    else
                    {
                        if (httpContext.User != null && httpContext.User.Claims != null)
                        {
                            TenantId =Convert.ToInt64(httpContext.User.Claims.ToArray()[4].Value);
                        }
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return TenantId;
        }
        [HttpGet]
        public Boolean LogOut(HttpContext httpContext)
        {
            try
            {
                if (httpContext != null)
                {
                    httpContext.Session.Clear();
                }
                else
                {
                    UserId = "httpContext Not Fond";
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Status;
        }
    }
}