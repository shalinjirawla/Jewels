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

namespace Inventory.Application.Services.ApplicationUserServices
{
    public class ApplicationUserServices:IApplicationUser
    {
        private IConfiguration _config;
        private readonly UserManager<ApplicationUser> _UserManager;
        public ApplicationUserServices(UserManager<ApplicationUser> UserManager, IConfiguration config)
        {
            _UserManager = UserManager;
            _config = config;
        }

        public async Task<string> Login(LoginModel login)
        {
            var tokenString = "";
            if (login != null)
            {
                await Task.Run(() =>
                {
                    string EmailId = "hemant@gmmail.com";
                    DateTime Bod = DateTime.Now;
                    var user = AuthenticateUserAsync(login);
                    if (user != null)
                    {
                        tokenString = GenerateJSONWebToken(login.UserName, EmailId, Bod);
                    }
                });
            }

            return tokenString;
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
