namespace RickAndMorty.Tests
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;

    public sealed class Setup
    {
        private static readonly object SyncRoot = new object();
        private static bool initialized;
        private static IServiceProvider? serviceProvider;
        private static TestServer? testServerValue;
        private string? binDirectory;
        private string? testDataDirectory;

        public IServiceProvider ServiceProvider => Setup.serviceProvider ?? throw new InvalidOperationException("Test service provider has not been initialized");

        public string BinDirectory
        {
            get
            {
                if (this.binDirectory == null)
                {
                    this.binDirectory = Path.GetDirectoryName(
                        Uri.UnescapeDataString(
                            new UriBuilder(
                                new Uri(this.GetType().Assembly.CodeBase ?? throw new InvalidOperationException("Cannot load assembly."))).Path));
                }

                return this.binDirectory ?? throw new InvalidOperationException("Could not resolve bin directory.");
            }
        }

        public string TestDataDirectory
        {
            get
            {
                if (this.testDataDirectory == null)
                {
                    this.testDataDirectory = Path.GetFullPath(
                        Path.Combine(
                            Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(this.BinDirectory))) ?? throw new InvalidOperationException("Could not resolve bin directory"),
                            "TestData"));
                }

                return this.testDataDirectory ?? throw new InvalidOperationException("Could not resolve test data directory.");
            }
        }

        public TestServer TestServer => Setup.testServerValue ?? throw new InvalidOperationException("Test server has not been initialized");

        public void Initialize()
        {
            if (!Setup.initialized)
            {
                lock (Setup.SyncRoot)
                {
                    if (!Setup.initialized)
                    {
                        if (!Directory.Exists(this.TestDataDirectory))
                        {
                            Directory.CreateDirectory(this.TestDataDirectory);
                        }

                        Setup.testServerValue = new TestServer(this.CreateWebHostBuilder()) { AllowSynchronousIO = true };
                        Setup.serviceProvider = Setup.testServerValue.Host.Services;
                        Setup.initialized = true;
                    }
                }
            }
        }

        private IWebHostBuilder CreateWebHostBuilder()
        {
            WebHostBuilder webHostBuilder = new WebHostBuilder();
            webHostBuilder.UseEnvironment("Development").UseStartup<Startup>();
            return webHostBuilder;
        }
    }
}
