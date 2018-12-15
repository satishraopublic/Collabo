using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collabo.API.Services;
using Collabo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

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
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI( c=>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Collabo API v1");
            });
        }
    }
}
