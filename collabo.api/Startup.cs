﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collabo.API.Services;
using Collabo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using collabo.Common;

namespace collabo.api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info{ Title="Collabo API", Version="v1"});
            });


            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddSingleton<ICollaboDataService, CollaboDataService>();
            services.AddSingleton<IDataFileService<CollaboDB>>((c)=>{
                IConfigurationService config = c.GetService<IConfigurationService>();
                string dbName = config.GetDBConnectionString();
                return new JSONDataFileService<CollaboDB>(dbName);
            });
            services.AddSingleton<ICollaboRepository,JSONCollaboRepository>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
        
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
        
                        await context.Response.WriteAsync(new ErrorModel{StatusCode = 500,ErrorMessage = ex.Message}.ToString()); //ToString() is overridden to Serialize object
                    }
                });
            });
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI( c=>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Collabo API v1");
            });
        }
    }
}
