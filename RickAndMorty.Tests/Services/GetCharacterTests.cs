namespace RickAndMorty.Tests.Services
{
    using System.Threading.Tasks;
    using RickAndMorty.Domain;
    using RickAndMorty.Services;
    using Xunit;

    public sealed class GetCharacterTests : IClassFixture<SetupFixture>
    {
        public GetCharacterTests(SetupFixture setupFixture)
        {
            this.Service = setupFixture.Activate<GetCharacter>();
        }

        public GetCharacter Service { get; }

        [Fact]
        public async Task Get()
        {
            Character? found = await this.Service.Get(1);
            Assert.NotNull(found!);
            Assert.Equal("Rick Sanchez", found!.Name);
        }

        [Fact]
        public async Task GetTransientError()
        {
            await Assert.ThrowsAsync<TransientServiceException>(() => this.Service.Get(66));
        }
    }
}