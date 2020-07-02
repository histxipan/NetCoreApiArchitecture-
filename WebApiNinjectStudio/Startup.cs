using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApiNinjectStudio.Domain.Abstract;
using WebApiNinjectStudio.Domain.Concrete;
using WebApiNinjectStudio.Domain.Entities;
using WebApiNinjectStudio.Services;

namespace WebApiNinjectStudio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region Injection Service

            //Account handle
            services.AddTransient<AccountService>();
            //AES Security handle
            services.AddTransient<AESSecurity>();
            //Pbkdf2 Security handle, especially used in password
            services.AddTransient<Pbkdf2Security>();

            #endregion

            #region Injection Repository

            services.AddScoped<IProductRepository, EFProductRepository>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IRoleRepository, EFRoleRepository>();

            #endregion

            #region JWT Token and Authorization

            //JWT Token, Authorization permission efter user role i database
            //Create Authentication requirement after permission 
            var permissionRequirement = new AuthPolicyRequirement();
            //Get signing secret key to JWT token
            string secureKeyOfToken = Configuration["TokenSettings:SecretKey"];
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy => policy.Requirements.Add(permissionRequirement));
            })
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secureKeyOfToken)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Injection AuthorizationHandler
            //Handlers that use Entity Framework shouldn't be registered as singletons.
            services.AddSingleton(permissionRequirement);
            services.AddSingleton<IAuthorizationHandler, AuthPolicyHandler>();

            #endregion

            //Database connetion string
            services.AddDbContext<EFDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBContext")));

            //Newtonsoft json package
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //Global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
