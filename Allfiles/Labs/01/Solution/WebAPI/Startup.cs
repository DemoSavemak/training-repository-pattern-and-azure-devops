using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Model;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebAPI.Repository;
using WebAPI.BAL;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        public Startup(IWebHostEnvironment env)
        {
            // Add appsetting.json and other json file such as appsetting for development :supakit
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
           //   var environment = Configuration["AppSettings:Environment"];
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //=================
            // Add Interface by Repo
            // services.AddScoped< IAgentStatusRepository , AgentStatusRepository>();
            // Add Interface by Generic Repo (onetime)

            // DB -------------

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IStoreRepository, StoreRepository>();

            // in Memory

            // BAL ===================
            services.AddScoped(typeof(IGenericBAL<>), typeof(GenericBAL<>));
            services.AddScoped<IStoreBAL, StoreBAL>();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Add appsetting.json and other json file such as appsetting for development
            services.Configure<Appsetting>(Configuration.GetSection("AppSettings"));

            // Add DB connection String to DBContext
            services.AddDbContext<FusionDBContext>(opts => opts.UseSqlServer(Configuration["AppSettings:ConnectionStrings"]));

            services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()

                    );

            });

            services.AddMvc();
           
            //  var connection = Configuration.GetConnectionString("Dev");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["AppSettings:Issuer"],
                    ValidAudience = Configuration["AppSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AppSettings:Secret"]))
                };
            });
            

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(0, 0);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Main API v0.0", Version = "v0.0" });

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });
            });

            //================Multipart File Upload supakit  https://codedocu.com/Details_mobile?d=2336&z=1&t=Set+upload+of+very+large+files+in+Asp.Net+MVC+Core+2.
            services.Configure<FormOptions>(options =>

            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });


            services.AddMvc().AddRazorRuntimeCompilation();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,  ILoggerFactory loggerFactory , IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();
            app.UseCors("MyAllowSpecificOrigins");
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Versioned API v1.0");
                c.DocumentTitle = "API Documentation";
                c.DocExpansion(DocExpansion.List);

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "api",
                pattern: "{controller}/{id?}");
            });
        }
    }
}
