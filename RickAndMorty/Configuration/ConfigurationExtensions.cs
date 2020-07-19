namespace RickAndMorty.Configuration
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        public static IConfiguration CreateConfiguration(this IWebHostEnvironment environment)
        {
            return environment.ContentRootPath
                .CreateConfiguration(environment.EnvironmentName)
                .Build();
        }

        private static IConfigurationBuilder CreateConfiguration(
            this string applicationDirectory,
            string environmentName)
        {
            return new ConfigurationBuilder()
                .SetBasePath(applicationDirectory)
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentJson(environmentName)
                .AddEnvironmentVariables();
        }

        private static IConfigurationBuilder AddEnvironmentJson(
            this IConfigurationBuilder configurationBuilder,
            string environmentName)
        {
            IConfigurationBuilder result = configurationBuilder;

            environmentName = environmentName?.Trim() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(environmentName))
            {
                result = result.AddJsonFile($"appsettings.{environmentName}.json", true);
            }

            return result;
        }
    }
}