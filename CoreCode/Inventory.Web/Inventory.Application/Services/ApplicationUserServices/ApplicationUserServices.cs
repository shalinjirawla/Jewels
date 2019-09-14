using Inventory.Application.ViewModel.ApplicationUser;
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
using System.Security.Principal;

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
                        DateTime CurrentDateTime = DateTime.Now;
                        tokenString = GenerateJSONWebToken(login.UserName, login.EmailId, CurrentDateTime, login.UserId,login.TenantId);
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
        private string GenerateJSONWebToken(string Username, string EmailId, DateTime CurrentDateTime,string UserId,long TenantId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                  new Claim(JwtRegisteredClaimNames.Sub, Username),
                  new Claim(JwtRegisteredClaimNames.Email, EmailId),
                  new Claim("DateOfJoing",CurrentDateTime.ToString("yyyy-MM-dd")),
                    new Claim("UserId", UserId),
                    new Claim("TenantId",Convert.ToString(TenantId)),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                 };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       


    }
}
