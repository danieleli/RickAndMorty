namespace RickAndMorty.Utils
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public static class StreamExtensions
    {
        public static string ReadString(this Stream stream)
        {
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 4096);
            return reader.ReadToEnd();
        }

        public static Task<string> ReadStringAsync(this Stream stream)
        {
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 4096);
            return reader.ReadToEndAsync();
        }
    }
}