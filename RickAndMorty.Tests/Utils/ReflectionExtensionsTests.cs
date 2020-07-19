namespace RickAndMorty.Tests.Utils
{
    using System.Threading.Tasks;
    using RickAndMorty.Utils;
    using Xunit;

    public class ReflectionExtensionsTests
    {
        [Fact]
        public async Task GetJsonFromManifestResource()
        {
            var character = await "TestPayload.json".GetJsonFromManifestResource<NamedCharacter>(this.GetType());
            Assert.NotNull(character);
            Assert.Equal("Krobmbopulous Michael", character!.Name);
        }

        [Fact]
        public async Task GetManifestResourceString()
        {
            string value = await this.GetType().GetManifestResourceString("TestPayload.json");
            Assert.False(string.IsNullOrWhiteSpace(value));
        }

        private sealed class NamedCharacter
        {
            public string Name { get; set; } = string.Empty;
        }
    }
}