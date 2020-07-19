namespace RickAndMorty.Tests.Api
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class PingControllerTests : ControllerTests
    {
        private const string GetPath = "v1/ping";

        public PingControllerTests(SetupFixture setupFixture)
            : base(setupFixture)
        {
        }

        [Fact]
        public async Task Get()
        {
            await this.SendAndAssertSuccessAsync(
                HttpMethod.Get,
                GetPath);
        }
    }
}