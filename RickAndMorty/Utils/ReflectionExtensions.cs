namespace RickAndMorty.Utils
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class ReflectionExtensions
    {
        public static async Task<T?> GetJsonFromManifestResource<T>(this string resourceName, Type anyTypeFromResourceAssembly)
            where T : class
        {
            var json = await anyTypeFromResourceAssembly.GetManifestResourceString(resourceName);
            return json.FromJson<T>(throwOnFailure: true);
        }

        public static async Task<string> GetManifestResourceString(this Type type, string name)
        {
            await using Stream? stream = type.Assembly.GetManifestResourceStream(type, name);

            if (stream != null)
            {
                return await stream.ReadStringAsync() ?? string.Empty;
            }

            return string.Empty;
        }
    }
}