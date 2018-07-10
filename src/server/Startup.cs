using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Server.Core;
using Server.Data;
using Server.Middlewares;
using Server.Repository;
using Server.Services;
using Server.ViewModels;

namespace Server
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
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IBusinessLogicService, BusinessLogicService>();
            services.AddScoped<UserService>();
            services.AddSingleton<IBankRepository, DbBankRepository>();
            
            services.AddDbContext<SQLContext>(options =>
                            options.UseSqlite(Configuration.GetSection("connectionStrings").
                                GetChildren().Where(x=>x.Key=="sqlite").FirstOrDefault().Value));

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
            app.UseMvc();
        }
    }
}