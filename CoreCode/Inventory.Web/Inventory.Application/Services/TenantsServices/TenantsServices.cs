using Inventory.Application.Interface.Tenants;
using Inventory.Application.ViewModel.ApplicationUser;
using Inventory.Application.ViewModel.Tenants;
using Inventory.Core.Models.ApplicationUser;
using Inventory.Core.Models.Tenants;
using Inventory.EntityFrameworkCore.DbContext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services.TenantsServices
{
    public class TenantsServices : ITenants
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly UserManager<ApplicationUser> _UserManager;
        public TenantsServices(ApplicationDbContext DbContext, UserManager<ApplicationUser> UserManager)
        {
            _DbContext = DbContext;
            _UserManager = UserManager;
        }
        public Boolean Result = false;
        public Boolean Status = false;
        public async Task<Boolean> SaveTenants(TenantsVm model)
        {
            try
            {
                if (model != null)
                {
                    Result = IsEmailExist(model.EmailId);
                    if (!Result)
                    {
                        Tenants tenants = new Tenants()
                        {
                            TenantName = model.TenantName,
                            IsActive = false,
                            EmailId = model.EmailId,
                            CreationTime = DateTime.Now,
                        };
                        _DbContext.Tenants.Add(tenants);
                        _DbContext.SaveChanges();
                        UserVm userVm = new UserVm()
                        {
                            UserName = tenants.EmailId,
                            Password = model.Password,
                            EmailId = tenants.EmailId,
                            TenantId = tenants.TenantId
                        };
                        string UserId = await RegisterAspnetUser(userVm);
                        Result = SendMail(model.EmailId, tenants.TenantId, tenants.TenantName, UserId);
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        public Boolean IsEmailExist(string EmailId)
        {

            try
            {
                if (!string.IsNullOrEmpty(EmailId))
                {
                    var checkAspnetUser = _UserManager.FindByEmailAsync(EmailId);
                    var checkTenants = _DbContext.Tenants.Where(x => x.EmailId == EmailId).FirstOrDefault();
                    if (checkAspnetUser.Result != null && checkTenants != null)
                    {
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                }

            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        public Boolean SendMail(string ToEmailId, long TenantId, string TenantName, string UserId)
        {
            try
            {
                string to = ToEmailId; //To address    
                string from = "ravi.k@ncoresoft.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "Your TenantId is " + TenantId;
                string ActivationUrl = "http://localhost:4200/#/register?TenantId=" + TenantId + "&UserId=" + UserId;
                string mailbody2 = "Hi " + TenantName + " !\n" +
                  "Thanks for showing interest and registring in " +
                  " Please <a href='" + ActivationUrl + "'>click here to activate</a>  your account and enjoy our services. \nThanks!";
                message.Subject = "Sending Email Using Asp.Net & C#";
                message.Body = mailbody2;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.zoho.com", 587);
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("ravi.k@ncoresoft.com", "Ncore@123");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                client.Timeout = 2000000;

                client.Send(message);

            }
            catch (Exception e)
            {

                throw e;
            }
            return true;
        }
        public async Task<string> RegisterAspnetUser(UserVm registerVm)
        {
            string UserId = "";
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
                UserId = user.Id;
                if (result.Succeeded)
                {
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
            return UserId;
        }

        public async Task<TenantsDetailsVm> GetRegisterDataAsync(long id, string UserId)
        {
            TenantsDetailsVm registerdata = new TenantsDetailsVm();
            try
            {
                var CheckTenants = _DbContext.Tenants.FirstOrDefault(x => x.TenantId == id && x.IsActive == false);
                var CheckUser = _UserManager.Users.FirstOrDefault(x => x.Id == UserId);
                await Task.Run(() =>
                {

                    if (CheckTenants != null && CheckUser != null)
                    {
                        registerdata.TenantId = CheckTenants.TenantId;
                        registerdata.TenantName = CheckTenants.TenantName;
                        registerdata.EmailId = CheckTenants.EmailId;
                    }
                    else
                    {
                        registerdata = null;
                    }
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return registerdata;
        }
        public async Task<Boolean> RegisterTenantActived(UserVm model)
        {
            try
            {
                await Task.Run(async () =>
                {
                    var CheckTenants = _DbContext.Tenants.FirstOrDefault(x => x.TenantId == model.TenantId && x.EmailId == model.EmailId && x.IsActive == false);
                    if (CheckTenants != null)
                    {
                        CheckTenants.IsActive = true;
                        CheckTenants.IsInTrialPeriod = true;
                        CheckTenants.LastModificationTime = DateTime.Now;
                        CheckTenants.SubscriptionEndDateUtc = DateTime.Now.AddDays(15);
                        //SubscriptionEndDateUtc for train version after 15 day this account is Deactived..
                        _DbContext.Tenants.Update(CheckTenants);
                        _DbContext.SaveChanges();
                        model.TenantId = CheckTenants.TenantId;
                        Status = await RegisterTenantUpdate(model);
                    }
                    else
                    {
                        Status = false;
                    }
                });
            }
            catch (Exception e)
            {
                Status = false;
                throw e;

            }
            return Status;
        }
        public async Task<Boolean> RegisterTenantUpdate(UserVm registerVm)
        {
            try
            {

                ApplicationUser user = new ApplicationUser()
                {
                    EmailConfirmed = true,
                    TenantId = registerVm.TenantId,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                var result = await _UserManager.UpdateAsync(user);

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

        public async Task<bool> ChechTenants(long TenantId)
        {
            try
            {
                await Task.Run(() =>
                {
                    var CheckTenants = _DbContext.Tenants.FirstOrDefault(x => x.TenantId == TenantId && x.IsActive == false);
                    if (CheckTenants != null)
                    {
                        Status = true;
                    }
                    else
                    {
                        Status = false;
                    }
                });
            }
            catch (Exception e)
            {

                throw e;
            }
            return Status;
        }

        public async Task<Boolean> CheckUserId(string UserId)
        {
            try
            {
                if (UserId != "")
                {
                    await Task.Run(() =>
                    {
                        var user = _UserManager.Users.FirstOrDefault(x => x.Id == UserId);
                        if (user != null)
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

        public async Task<List<TenantUserList>> GetTenantUserList(long TenantId)
        {
            List<TenantUserList> list = new List<TenantUserList>();
            try
            {
                if (TenantId != 0)
                {
                    await Task.Run(() =>
                    {
                        var gettenants = (from tetant in _DbContext.Tenants.ToList()
                                          join user in _DbContext.ApplicationUser.ToList()
                                          on tetant.TenantId equals user.TenantId
                                          select new TenantUserList
                                          {
                                              TenantId = tetant.TenantId,
                                              TenancyName = tetant.TenantName,
                                              UserId = user.Id,
                                              UserName = user.UserName
                                          });
                        if (gettenants != null && gettenants.Count() > 0)
                        {
                            foreach (var item in gettenants.ToList())
                            {
                                TenantUserList dto = new TenantUserList()
                                {
                                    TenantId=item.TenantId,
                                    TenancyName=item.TenancyName,
                                    UserId=item.UserId,
                                    UserName=item.UserName,
                                };
                                list.Add(dto);
                            }
                        }
                    });
                }
                else
                    throw new Exception("TenantId not zero");
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
    }
}
