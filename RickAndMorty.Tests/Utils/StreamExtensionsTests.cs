namespace RickAndMorty.Tests.Utils
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using RickAndMorty.Utils;
    using Xunit;

    public class StreamExtensionsTests
    {
        [Fact]
        public void ReadString()
        {
            string expectedString = "any string";
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedString));

            string output = stream.ReadString();

            Assert.Equal(expectedString, output);
        }

        [Fact]
        public async Task ReadStringAsync()
        {
            string expectedString = "any string";
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedString));

            string output = await stream.ReadStringAsync();

            Assert.Equal(expectedString, output);
        }
    }
}