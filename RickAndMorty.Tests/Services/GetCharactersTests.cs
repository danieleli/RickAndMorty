namespace RickAndMorty.Tests.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using RickAndMorty.Services;
    using Xunit;

    public sealed class GetCharactersTests : IClassFixture<SetupFixture>
    {
        public GetCharactersTests(SetupFixture setupFixture)
        {
            this.Service = setupFixture.Activate<GetCharacters>();
        }

        public GetCharacters Service { get; }

        [Fact]
        public async Task Get()
        {
            var characters = (await this.Service.Get()).ToArray();
            Assert.NotEmpty(characters);
            Assert.Equal(5, characters.Length);
        }
    }
}