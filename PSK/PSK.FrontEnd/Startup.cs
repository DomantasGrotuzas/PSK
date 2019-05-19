using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSK.Persistence;
using PSK.FrontEnd.Filters;
using Autofac;
using PSK.DataAccess;
using PSK.Services;
using Autofac.Extensions.DependencyInjection;
using PSK.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PSK.FrontEnd.AutoMapper;

namespace PSK.FrontEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<Employee, UserRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            var connectionString = Configuration.GetValue<string>("PskConnectionString");

            services.AddDbContext<IDataContext, DataContext>(options => { options.UseSqlServer(connectionString); });

            services.AddSingleton(Configuration);

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile<MappingProfile>(); });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc(options =>
            {
                options.Filters.Add(new LogAttribute());
                options.Filters.Add(new ExceptionFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Register Dependencies
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterType<EmployeeDataAccess>().As<IEmployeeDataAccess>();
            containerBuilder.RegisterType<EmployeeService>().As<IEmployeeService>();
            containerBuilder.RegisterType<DataInitializer>().As<IDataInitializer>();
            containerBuilder.RegisterType<DataContext>().AsSelf();

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
                databaseInitializer.InitializeRoles();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
