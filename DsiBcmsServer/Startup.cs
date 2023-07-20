using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSI.BcmsServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DsiBcmsServer {
    public class Startup {

        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string DefaultCorsPolicy = "_defaultCorsPolicy";
        private string[] AllowedOrigins = { 
            "http://127.0.0.1", "https://127.0.0.1", // jQuery
            "http://127.0.0.1:4200", "https://127.0.0.1:4200", // Angular
            "http://127.0.0.1:5500", "https://127.0.0.1:5500", // Live Server
            "http://localhost", "https://localhost", // jQuery
            "http://localhost:4200", "https://localhost:4200", // Angular
            "http://localhost:5500", "https://localhost:5500", // Live Server
            "http://doudsystems.com", "https://doudsystems.com"
        };
        private string[] AllowedMethods = { "GET", "POST", "PUT", "DELETE" };

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var connStrKey = "ProdDb";
#if DEBUG
            connStrKey = "DevDb";
#endif
#if DEVPRODBKUP
            // this is a recent copy of the production database
            // it can be used to diagnose bugs in the production code
            connStrKey = "DevProdDb";
#endif
#if AZURE
            connStrKey = "AzureDb";
#endif
#if RIPPER
            connStrKey = "RipperDb";
#endif
            services.AddDbContext<DsiBcmsContext>(x => {
                x.UseLazyLoadingProxies();
                x.UseSqlServer(Configuration.GetConnectionString(connStrKey));
            });
            services.AddCors(x =>
                x.AddPolicy(DefaultCorsPolicy, x =>
                    x.WithOrigins(AllowedOrigins).WithMethods(AllowedMethods).AllowAnyHeader()
                )
            );
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseCors(DefaultCorsPolicy);

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                scope.ServiceProvider.GetService<DsiBcmsContext>().Database.Migrate();
            }

            logger.Info("Starting Boot Camp Management System (BCMS) {0}", DSI.BcmsServer.Models.System.Version);

        }
    }
}
