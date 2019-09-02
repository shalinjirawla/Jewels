using Inventory.Application.Interface;
using Inventory.Application.Interface.Products;
using Inventory.Application.Services;
using Inventory.Application.Services.ProductsServices;
using Inventory.EntityFrameworkCore.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Inventory.Web
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "localhost";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(DefaultCorsPolicyName));
            });
            services.AddScoped<ICustomer,CustomerService>();
            services.AddScoped<IDiscountType, DiscountTypeService>();
            services.AddScoped<IProductCategories, ProductCategoriesServices>();
            //Configure CORS for angular2 UI
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    //App:CorsOrigins in appsettings.json can contain more than one address with splitted by comma.
                    builder
                        //.WithOrigins(_appConfiguration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.RemovePostFix("/")).ToArray())
                        .AllowAnyOrigin() //TODO: Will be replaced by above when Microsoft releases microsoft.aspnetcore.cors 2.0 - https://github.com/aspnet/CORS/pull/94
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Inventory API",
                    Description = "ASP.NET Core Web API With Angular 8"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(DefaultCorsPolicyName);
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API V1");
            });

        }
    }
}
