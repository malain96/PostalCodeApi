using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PostalCodeApi.Domain.Repositories;
using PostalCodeApi.Domain.Services;
using PostalCodeApi.Persistence.Contexts;
using PostalCodeApi.Persistence.Repositories;
using PostalCodeApi.Services;

namespace PostalCodeApi
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
            services.AddControllers();
            
            // Database
            services.AddDbContext<PostalCodeDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PostalCodeDb")));
            
            services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPostalCodeCityRepository, PostalCodeCityRepository>();
            services.AddScoped<IPostalCodeService, PostalCodeService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPostalCodeCityService, PostalCodeCityService>();
            services.AddScoped<IGeoNamesService, GeoNamesService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // Add Swagger doc
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Postal Code API", Version = "v1" });
            });

            services.AddSingleton<IConfiguration>(Configuration);
            
            // Add auto mapper 
            services.AddAutoMapper(typeof(Startup));
            
            // Remove auto model state validation 
            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions => {
                apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Postal Code API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
