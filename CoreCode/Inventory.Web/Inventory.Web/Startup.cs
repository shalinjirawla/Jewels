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
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
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
            services.AddDistributedMemoryCache();
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

            });
            
            services.AddScoped<ICustomer, CustomerService>();
            services.AddScoped<IDiscountType, DiscountTypeService>();
            services.AddScoped<IProductCategories, ProductCategoriesServices>();
            services.AddScoped<ICountry, CountryServices>();
            services.AddScoped<IProductBrand, ProductBrandServices>();
            services.AddScoped<IGenerealsetup.ICurrency, GeneralsetupServices>();
            services.AddScoped<IGenerealsetup.ICreditTerms, GeneralsetupServices>();
            services.AddScoped<IcustomerType, CustomerTypeServices>();
            services.AddScoped<IApplicationUser, ApplicationUserServices>();
            services.AddScoped<IWarehouse, WarehouseService>();
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

            app.UseDeveloperExceptionPage();
            app.UseFileServer();
            app.UseIdentity();
            app.UseSession();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API V1");
            });
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;

                    //when authorization has failed, should retrun a json message to client
                    if (error != null && error.Error is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Unauthorized",
                            Msg = "token expired"
                        }));
                    }
                    //when orther error, retrun a error message json to client
                    else if (error != null && error.Error != null)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            State = "Internal Server Error",
                            Msg = error.Error.Message
                        }));
                    }
                    //when no error, do next.
                    else await next();
                });
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
