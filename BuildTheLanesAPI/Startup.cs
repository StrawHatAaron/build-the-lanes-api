using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using BuildTheLanesAPI.Helpers;
using BuildTheLanesAPI.Services;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

//Aaron Miller wrote this entire project

namespace BuildTheLanesAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebappContext>(o => o.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEngineerCertificationsService, EngineerCertificationsService>();
            services.AddScoped<IEngineeringDegreeService, EngineerDegreesService>();
            services.AddScoped<IResponsibilitiesService, ResponsibilitiesService>();
            services.AddScoped<IAdminsService, AdminsService>();
            services.AddScoped<IDonatorsService, DonatorsService>();
            services.AddScoped<IEngineersService, EngineersService>();
            services.AddScoped<IStaffsService, StaffsService>();
            services.AddScoped<IApplicableStandardsService, ApplicableStandardsService>();
            services.AddScoped<IDonatesService, DonatesService>();

            // services.AddCors();

            services.AddCors(options =>
            {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                        .WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure strongly typed settings objects
            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebappContext webappContext)
        {
            // migrate any database changes on startup (includes initial db creation)
            // webappContext.Database.Migrate();

            app.UseRouting();

            // global cors policy
            app.UseCors(MyAllowSpecificOrigins);
            // app.UseCors(x => x
            //     .AllowAnyOrigin()
            //     .AllowAnyMethod()
            //     .AllowAnyHeader());

            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
