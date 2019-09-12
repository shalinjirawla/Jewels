﻿using Inventory.Application.ViewModel.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Inventory.Core.Models.ApplicationUser;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Inventory.Application.Interface.ApplicationUser;
using Inventory.EntityFrameworkCore.DbContext;
using System.Linq;
using Inventory.Core.Models.Tenants;
using System.Web.Providers.Entities;
using Microsoft.AspNetCore.Http;

namespace Inventory.Application.Services.ApplicationUserServices
{
    public class ApplicationUserServices : IApplicationUser
    {
        private IConfiguration _config;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _DbContext;
        public Boolean Status;
        IHttpContextAccessor _httpContextAccessor;
        public ApplicationUserServices(UserManager<ApplicationUser> UserManager, IConfiguration config,
            ApplicationDbContext DbContext,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _UserManager = UserManager;
            _config = config;
            _DbContext = DbContext;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

       

        public async Task<LoginVm> Login(LoginVm login)
        {
            var tokenString = "";
            try
            {
                await Task.Run(async () =>
                {
                    if (login != null)
                    {
                        Status = await SetCurrentLoginUserIdandTenantId(login.UserId, login.TenantId);
                        DateTime CurrentDateTime = DateTime.Now;
                        tokenString = GenerateJSONWebToken(login.UserName, login.EmailId, CurrentDateTime);
                    }
                    login.AccessToken = tokenString;
                    login.Password = null;
                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return login;
        }
        private string GenerateJSONWebToken(string Username, string EmailId, DateTime CurrentDateTime)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                  new Claim(JwtRegisteredClaimNames.Sub, Username),
                  new Claim(JwtRegisteredClaimNames.Email, EmailId),
                  new Claim("DateOfJoing",CurrentDateTime.ToString("yyyy-MM-dd")),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                 };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Boolean> SetCurrentLoginUserIdandTenantId(string UserId, long TenantId)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserId) && TenantId != 0)
                {
                    await Task.Run(() =>
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("UserId", UserId);
                        _httpContextAccessor.HttpContext.Session.SetString("TenantId", Convert.ToString(TenantId));
                    });
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return Status;
        }
        public async Task<string> GetUserId()
        {
            return await Task.Run(() =>
            {
                return _httpContextAccessor.HttpContext.Session.GetString("UserId");
            });
        }
        public async Task<long> GetTenantId()
        {
            return await Task.Run(() =>
            {
                return Convert.ToInt64(_httpContextAccessor.HttpContext.Session.GetString("TenantId"));

            });
        }
        public async Task<Boolean> Logout()
        {
            return await Task.Run(() =>
            {
                _httpContextAccessor.HttpContext.Session.Clear();
                Status = true;
                return Status;
            });
        }


    }
}
