using Inventory.Application.Interface.Tenants;
using Inventory.Application.ViewModel.ApplicationUser;
using Inventory.Application.ViewModel.Tenants;
using Inventory.Core.Models.Tenants;
using Inventory.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services.TenantsServices
{
   public class TenantsServices:ITenants
    {
        private readonly ApplicationDbContext _DbContext;
        public TenantsServices(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public Boolean Result = false;

        public async Task<Boolean> SaveTenants(TenantsVm model)
        {
            try
            {
                if (model!=null){
                    await Task.Run(() =>
                    {

                    Result = IsEmailEixst(model.EmailId);
                    if (!Result)
                    {
                            Tenants tenants = new Tenants()
                            {
                                TenantName = model.TenantName,
                                IsActive = false,
                                EmailId = model.EmailId,
                                CreationTime=DateTime.Now,
                            };
                            _DbContext.Tenants.Add(tenants);
                            _DbContext.SaveChanges();
                            
                            Result = SendMail(model.EmailId, tenants.TenantId,tenants.TenantName);
                        Result = true;
                    }
                    else {
                        Result = false;
                    }
                    });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Result;
        }
        public Boolean IsEmailEixst(string EmailId) {

            try
            {
                if (!string.IsNullOrEmpty(EmailId)) {
                  
                        var check = _DbContext.Tenants.Where(x => x.EmailId == EmailId).FirstOrDefault();
                        if (check != null)
                        {
                            Result = true;
                        }
                        else {
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
        public Boolean SendMail(string ToEmailId, long TenantId,string TenantName)
        {
            try
            {
                //System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress("hemant@ncoresoft.com", "From Test");
                //System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(ToEmailId, ToEmailId);
                //System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);
                //message.Subject = "Congratulations! Receipt No: ";
                //message.Body = "test";

                //System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential("hemant@ncoresoft.com", "Ncoresoft@123");

                //System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("smtp.zoho.com", 465);
                //mailClient.EnableSsl = true;
                //mailClient.UseDefaultCredentials = false;
                //mailClient.Credentials = mailAuthentication;

                //message.IsBodyHtml = true;
                //mailClient.Send(message);

                string to = ToEmailId; //To address    
                string from = "ravi.k@ncoresoft.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "Your TenantId is " + TenantId;
                string ActivationUrl = "http://localhost:4200/#/register?TenantId="+ TenantId;
                string mailbody2 =  "Hi " + TenantName + " !\n" +
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

        public RegisterVm GetRegisterDataAsync(long id)
        {
            RegisterVm registerdata = new RegisterVm();
            try
            {
                var data = _DbContext.Tenants.FirstOrDefault(x => x.TenantId == id && x.IsActive == false && x.EmailId!=null);
                if (data != null)
                {
                    registerdata.TenantId = data.TenantId;
                    registerdata.TenantName = data.TenantName;
                    registerdata.EmailId = data.EmailId;
                }
                else
                {
                    registerdata = null;
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return registerdata;
        }
    }
}
