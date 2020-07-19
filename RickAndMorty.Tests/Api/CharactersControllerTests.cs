namespace RickAndMorty.Tests.Api
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class CharactersControllerTests : ControllerTests
    {
        private const string GetAllPath = "v1/characters";

        public CharactersControllerTests(SetupFixture setupFixture)
            : base(setupFixture)
        {
        }

        [Fact]
        public async Task GetAll()
        {
            await this.SendAndAssertSuccessAsync(
                HttpMethod.Get,
                GetAllPath);
        }

        [Fact]
        public async Task GetOne()
        {
            await this.SendAndAssertSuccessAsync(
                HttpMethod.Get,
                $"{GetAllPath}/1");
        }

        [Fact]
        public async Task GetOneNotFound()
        {
            await this.SendAndAssertFailureAsync(
                HttpMethod.Get,
                $"{GetAllPath}/666",
                HttpStatusCode.NotFound);
        }
    }
}