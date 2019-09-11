using Inventory.Application.Interface.Tenants;
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
                          //  Result = SendMail(model.EmailId);
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
        public Boolean SendMail(string ToEmailId)
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

            }
            catch (Exception e)
            {

                throw;
            }
            return true;
        }
    }
}
