using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using dotNetMVC.Data;
using dotNetMVC.Services;

namespace dotNetMVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Alterar para MySQL no lugar de SqlServer
            services.AddDbContext<dotNetMVCContext>(options =>  //Contexto relacionado ao entity framework que herda a classe DBContext
            options.UseMySql(Configuration.GetConnectionString("dotNetMVCContext"), builder =>
                                                        //Nome do assembly
                           builder.MigrationsAssembly("dotNetMVC")));
            //options.UseSqlServer(Configuration.GetConnectionString("dotNetMVCContext")));

            /* É necessário ter o provider do MySQLL para o entity framework, para que ele reconheça
             * qual a linguagem do banco de dados que estamos a trabalhar, nas dependencias do nuget,
             * incluir a dependência do Pomelo.EntityFramework.MySQL, ter MUITA ATENÇÃO nas versões do .NET
             * que estamos a trabalhar e nas versoes SDK/Entity framework, para todas serem compatíveis.*/

            //Registra o serviço no sistema de injeção de dependência da aplicação para os serviços serem injetados em outras classes
            services.AddScoped<SeedingService>();
            services.AddScoped<SellerService>();
            services.AddScoped<DepartmentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        {
            //definindo o locale da aplicação com formatato dos USA
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed(); // Popula a base de dados
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
