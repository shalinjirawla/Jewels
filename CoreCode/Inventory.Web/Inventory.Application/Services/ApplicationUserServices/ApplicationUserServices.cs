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

namespace Inventory.Application.Services.ApplicationUserServices
{
    public class ApplicationUserServices : IApplicationUser
    {
        private IConfiguration _config;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly ApplicationDbContext _DbContext;
        public Boolean Status;
        public ApplicationUserServices(UserManager<ApplicationUser> UserManager, IConfiguration config,
            ApplicationDbContext DbContext
            )
        {
            _UserManager = UserManager;
            _config = config;
            _DbContext = DbContext;
        }

        public async Task<Boolean> RegisterTenant(RegisterVm model)
        {
            try
            {
                Status = await IsTenantEmailIdExist(model.EmailId);
                if (!Status)
                {
                    await Task.Run(async () =>
                    {
                        Tenants tenants = new Tenants
                        {
                            TenantName = model.TenantName,
                            CreationTime = DateTime.Now,
                            CreatorUserId = null,
                            EmailId = model.EmailId,
                            BusinessRegisterNumber = model.BusinessRegisterNumber,
                            IsActive = true,
                            IsInTrialPeriod = true,
                            LastModificationTime = DateTime.Now,
                            LastModifierUserId = null,
                            Logo = string.Empty,
                            SubscriptionEndDateUtc = DateTime.UtcNow,
                            TagRegisterNumber = model.TagRegisterNumber
                        };
                        _DbContext.Tenants.Add(tenants);
                        _DbContext.SaveChanges();
                        var Tenant = _DbContext.Tenants.OrderByDescending(x => x.TenantId).FirstOrDefault();
                          
                        if (Tenant != null) {
                            model.TenantId = Tenant.TenantId;
                          Status=await RegisterAspnetUser(model);
                        }
                    });
                }
                else
                {
                    Status = false;
                }
            }
            catch (Exception e)
            {
                Status = false;
                throw e;

            }
            return Status;
        }
        public async Task<Boolean> RegisterAspnetUser(RegisterVm registerVm)
        {
            try
            {

                ApplicationUser user = new ApplicationUser()
                {
                    Email = registerVm.EmailId,
                    NormalizedEmail = registerVm.EmailId,
                    TenantId = registerVm.TenantId,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerVm.EmailId,

                };
                var result = await _UserManager.CreateAsync(user, registerVm.Password);

                if (result.Succeeded)
                {
                    Status = true;
                }
                else
                {
                    Status = false;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Status;
        }
        public async Task<Boolean> IsTenantEmailIdExist(string EmailId)
        {
            try
            {
                if (!string.IsNullOrEmpty(EmailId))
                {
                    await Task.Run(() =>
                    {
                        var check = _DbContext.Tenants.FirstOrDefault(x => x.EmailId == EmailId);
                        if (check != null)
                        {
                            Status = true;
                        }
                        else { Status = false; }
                    });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Status;
        }
        public async Task<LoginModel> Login(LoginModel login)
        {
            var tokenString = "";
            if (login != null)
            {
                await Task.Run(async () =>
                {
                    string EmailId = login.UserName;
                    DateTime Bod = DateTime.Now;
                    var user1= await _UserManager.FindByEmailAsync(login.UserName);

                    var userId = user1?.Id;
                    string mail = user1?.Email;
                    var user = AuthenticateUserAsync(login);
                    if (user != null)
                    {
                        var userid = user?.Id;
                        tokenString = GenerateJSONWebToken(login.UserName, EmailId, Bod);
                    }
                });
            }
            login.AccessToken = tokenString;
            login.Password = null;
            return login;
        }
        private string GenerateJSONWebToken(string Username, string EmailId, DateTime Bod)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                  new Claim(JwtRegisteredClaimNames.Sub, Username),
                  new Claim(JwtRegisteredClaimNames.Email, EmailId),
                  new Claim("DateOfJoing",Bod.ToString("yyyy-MM-dd")),
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
        private async Task<LoginModel> AuthenticateUserAsync(LoginModel loginModel)
        {
            LoginModel users = null;

            //Validate the User Credentials  
            //Demo Purpose, I have Passed HardCoded User Information  
            var user = await _UserManager.FindByNameAsync(loginModel.UserName);

            if (user != null && await (_UserManager.CheckPasswordAsync(user, loginModel.Password)))
            {
                users = new LoginModel { UserName = user.UserName, Password = user.PasswordHash };
            }
            return users;
        }
        

    }
}
