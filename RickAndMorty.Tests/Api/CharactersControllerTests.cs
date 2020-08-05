namespace RickAndMorty.Tests.Api
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using RickAndMorty.Configuration;
    using RickAndMorty.Utils;
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

        [Fact]
        public async Task GetTransientError()
        {
            var response = await this.Send(
                HttpMethod.Get,
                $"{GetAllPath}/66");

            var responseBody      = await response.Content.ReadAsStringAsync();
            var exceptionResponse = responseBody.FromJson<ExceptionHandlerExtensions.ExceptionResponse>();

            Assert.NotNull(exceptionResponse);
            Assert.Equal(HttpStatusCode.InternalServerError.ToInt(), exceptionResponse.StatusCode);
            Assert.Contains("transient", exceptionResponse.Message);
        }
    }
}