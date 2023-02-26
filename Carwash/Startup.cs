using Carwash.Model;
using Carwash.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Carwash
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
            services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CarwashCS")));

            services.AddControllers();
            services.AddTransient<IUser, ImpUser>();
            services.AddTransient<IPackage, ImpPackage>();
            services.AddTransient<ICardetails, ImpCardetails>(); 
            services.AddTransient<IAdmin, ImpAdmin>();

            services.AddCors(option => option.AddPolicy("MyPolicy", ApplicationBuilder =>
               {
                   ApplicationBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
               })
            );

           
            services.AddAuthentication(X =>
            {
                X.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                X.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; x.SaveToken = true; x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret.....")),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Carwash", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter the token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                                   Reference = new OpenApiReference
                                   {
                                      Type= ReferenceType.SecurityScheme,
                                      Id= "Bearer"
                                   }
                          },
                           new string[] {}
                    }
                });

            });
        
           

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carwash v1"));
            }


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
