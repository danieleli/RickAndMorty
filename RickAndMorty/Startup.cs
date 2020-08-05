using Microsoft.AspNetCore.Mvc;

namespace RickAndMorty
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RickAndMorty.Configuration;
    using RickAndMorty.Utils;

    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            this.Configuration = environment.CreateConfiguration();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.RegisterRickAndMortyService(this.Configuration);
            services.AddApiVersioning();
            services.ConfigureSwagger("v1", "RickAndMorty");
            services.AddControllers(o =>
            {
                o.CacheProfiles.Add(
                    Constants.DEFAULT_API_CACHE,
                    new CacheProfile()
                    {
                        Duration = 30
                    });
            });
            services.AddResponseCaching(o => { });
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.UseSwagger(
                routePrefix: string.Empty,
                serviceName: "RickAndMorty",
                schemeAndDomain: "http://localhost:8962");
            application.UseRouting();
            application.UseResponseCaching();                         // application.UseCors must be called before the call to application.UseResponseCaching()
            application.UseEndpoints(c => { c.MapControllers(); });
        }
    }
}