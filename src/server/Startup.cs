using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Server.Models;
using Server.Repository;
using Server.Services;

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
            services.AddDbContext<SQLContext>(options =>
                            options.UseSqlite(Configuration.GetSection("connectionStrings").
                                GetChildren().Where(x => x.Key == "sqlite").FirstOrDefault().Value));
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IBusinessLogicService, BusinessLogicService>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();
            services.AddScoped<IBankRepository, BankRepository>();


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
<<<<<<< HEAD
            
=======

>>>>>>> 016c4fa60f2f201a69d747c766956d5b1d7404fb
            app.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
            app.UseMvc();
        }
    }
}