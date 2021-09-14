using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Formatters;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Datas;
using PetShop.Datas.Repositories;
using PetShop.Domain.IRepositories;
using PetShop.Domain.Services;

namespace PetShop.RestAPI
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
            /*services.AddDbContext<PetShopContext>(
                opt => opt.UseInMemoryDatabase("PetDB")
            );*/

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            services.AddDbContext<PetShopContext>(
                opt =>
                {
                    opt.UseLoggerFactory(loggerFactory)
                        .UseSqlite("Data Source=petShop.db");
                }, ServiceLifetime.Transient
            );
            
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPetTypeService, PetTypeService>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetTypeRepository, PetTypeRepository>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();

            services.AddControllers().AddNewtonsoftJson(options=>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            var serviceProvider = services.BuildServiceProvider();
            var setUp = serviceProvider.GetRequiredService<IPetRepository>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PetShop.RestAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PetShop.RestAPI v1"));
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<PetShopContext>();
                    DbInitialize.InitData(context);
                }
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}