using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STARK_Project.CryptoAPIService;
using STARK_Project.Data;
using STARK_Project.DatabaseModel;
using STARK_Project.DBServices;
using STARK_Project.EmailServices;
using STARK_Project.NotificationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STARK_Project.Calculator;

namespace STARK_Project
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
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();

            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"))
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings());
            //services.AddHangfireServer();

            services.AddScoped<IDbService, ApplicationDbService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IEmailService, SMTPService>();


            services.AddScoped<INotificationService, HangFireNotificationService>();
            services.AddHttpContextAccessor();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            //calculator
            services.AddScoped<ICalculator, CalculatorConverter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();

            Hangfire.Storage.IMonitoringApi monitoringApi = JobStorage.Current.GetMonitoringApi();
            JobStorage.Current.GetConnection().RemoveTimedOutServers(new TimeSpan(0, 0, 15));

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1,
                Queues = new[] { "jobqueue" },
                ServerName = "HangfireJobServer",
            });


            //var monitor = Hangfire.JobStorage.Current.GetConnection().;
            //if (monitor.ProcessingCount() > 0)
            //{
            //    foreach (var job in monitor.ProcessingJobs(0, (int)monitor.ProcessingCount()))
            //    {
            //        BackgroundJob.Requeue(job.Key);
            //    }
            //}

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Summary}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
