using Inventory.Application.Interface;
using Inventory.Application.Interface.Common;
using Inventory.Application.Interface.Products;
using Inventory.Application.Services;
using Inventory.Application.Services.CommonsServices;
using Inventory.Application.Interface.Customer;
using Inventory.Application.Services.CustomersServices;
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
using Inventory.Core.Models.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Inventory.Application.Interface.ApplicationUser;
using Inventory.Application.Services.ApplicationUserServices;
using IdentityServer4.AccessTokenValidation;
using System.IO;
using Microsoft.AspNetCore.Hosting.Internal;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System;
using Inventory.Application.Interface.Tenants;
using Inventory.Application.Services.TenantsServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSession();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddMvc().AddControllersAsServices();
            //Password Strength Setting
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            //Setting the Account Login page
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/#/Login"; // If the LoginPath is not set here,
                                                      // ASP.NET Core will default to /#/Login
                options.LogoutPath = "/#/Logout"; // If the LogoutPath is not set here,
                                                        // ASP.NET Core will default to /#/Logout
                options.AccessDeniedPath = "/#/404"; // If the AccessDeniedPath is
                                                                    // not set here, ASP.NET Core 
                                                                    // will default to 
                                                                    // /#/404
                options.SlidingExpiration = true;
            });
           
            //adding autorization policy's
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                
            }).AddJwtBearer(option => {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
                option.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json; charset=utf-8";
                        var message ="An error occurred processing your authentication.";
                        var status = StatusCodes.Status401Unauthorized;
                        var body = "Unauthorized";
                        var result = JsonConvert.SerializeObject(new { message,status,body });
                        return context.Response.WriteAsync(result);
                    }
                };

            });
            
            services.AddScoped<ICustomer, CustomerService>();
            services.AddScoped<IDiscountType, DiscountTypeService>();
            services.AddScoped<IProductCategories, ProductCategoriesServices>();
            services.AddScoped<ICountry, CountryServices>();
            services.AddScoped<IProductBrand, ProductBrandServices>();
            services.AddScoped<IGenerealsetup.ICurrency, GeneralsetupServices>();
            services.AddScoped<IGenerealsetup.ITaxCode, GeneralsetupServices>();
            services.AddScoped<IGenerealsetup.ICreditTerms, GeneralsetupServices>();
            services.AddScoped<IGenerealsetup.IShipmentTerm, GeneralsetupServices>();
            services.AddScoped<IGenerealsetup.IShipmentMethod, GeneralsetupServices>();
            services.AddScoped<IcustomerType, CustomerTypeServices>();
            services.AddScoped<IApplicationUser, ApplicationUserServices>();
            services.AddScoped<IWarehouse, WarehouseService>();
            services.AddScoped<ITenants, TenantsServices>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Inventory API",
                    Description = "ASP.NET Core Web API With Angular 8"
                });

                c.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Obsolete]
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
            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
                {
                    // fallback when no content is provided in an api response
                    if (!context.HttpContext.Response.ContentLength.HasValue ||
                        context.HttpContext.Response.ContentLength == 0)
                    {
                        context.HttpContext.Response.ContentType = "text/plain";
                        var message = "Currently User Not Login..";
                        var status = context.HttpContext.Response.StatusCode;
                        var body = "";
                        var result = JsonConvert.SerializeObject(new { message, status, body });
                        await context.HttpContext.Response.WriteAsync(result);
                      //  await context.HttpContext.Response.WriteAsync("Currently User Not Login.." +
                           //   $"Status Code: {context.HttpContext.Response.StatusCode}");
                    }
                }
                else
                {
                    context.HttpContext.Response.Redirect($"/Error?code={context.HttpContext.Response.StatusCode}");
                }
            });
            app.UseDeveloperExceptionPage();
            app.UseFileServer();
            app.UseIdentity();
            app.UseSession();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API V1");
            });
           
            app.UseCors(DefaultCorsPolicyName);
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();
            app.UseAuthentication();
            SeedDatabase.initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);
        }
    }
}
