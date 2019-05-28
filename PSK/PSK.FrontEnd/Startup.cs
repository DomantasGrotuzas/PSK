using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSK.DataAccess;
using PSK.DataAccess.Interfaces;
using PSK.Domain;
using PSK.Domain.Identity;
using PSK.FrontEnd.AutoMapper;
using PSK.FrontEnd.Filters;
using PSK.Persistence;
using PSK.Services;
using PSK.Services.Emails;
using PSK.Services.Interfaces;

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
                .AddDefaultTokenProviders();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            var connectionString = Configuration.GetValue<string>("PskConnectionString");

            services.AddDbContext<IDataContext, DataContext>(options => { options.UseSqlServer(connectionString); });

            services.AddSingleton(Configuration);

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile<MappingProfile>(); });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
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
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            //Register Dependencies
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterType<OfficeDataAccess>().As<IDataAccess<Office>>();
            containerBuilder.RegisterType<TripDataAccess>().As<IDataAccess<Trip>>();
            containerBuilder.RegisterType<AccommodationDataAccess>().As<IDataAccess<Accommodation>>();
            containerBuilder.RegisterType<TripEmployeeDataAccess>().As<IDataAccess<TripEmployee>>();
            containerBuilder.RegisterType<TripEmployeeDataAccess>().As<ITripEmployeeDataAccess>();

            containerBuilder.RegisterType<TripService>().As<ITripService>();
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
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}