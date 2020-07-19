namespace RickAndMorty
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static void Main()
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) =>
                {
                });

            builder.Build().Run();
        }
    }
}