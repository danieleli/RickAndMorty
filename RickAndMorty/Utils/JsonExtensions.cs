namespace RickAndMorty.Utils
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions DefaultSerializerOptionsValue = CreateDefaultJsonSerializerOptions();

        public static JsonSerializerOptions DefaultSerializerOptions => DefaultSerializerOptionsValue;

        public static T? FromJson<T>(this string value, JsonSerializerOptions? options = null, bool throwOnFailure = false)
            where T : class
        {
            T? result = default;

            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {
                    result = JsonSerializer.Deserialize<T>(value, options ?? DefaultSerializerOptions);
                }
                catch (Exception ex)
                {
                    if (throwOnFailure)
                    {
                        throw new JsonSerializationException($"Unable to deserialize item of type {typeof(T).Name} from provided value.", ex);
                    }
                }
            }

            return result;
        }

        public static object? FromJson(this string value, Type type, JsonSerializerOptions? options = null, bool throwOnFailure = false)
        {
            object? result = null;

            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {
                    result = JsonSerializer.Deserialize(value, type, options ?? DefaultSerializerOptions);
                }
                catch (Exception ex)
                {
                    if (throwOnFailure)
                    {
                        throw new JsonSerializationException($"Unable to deserialize item of type {type.Name} from provided value.", ex);
                    }
                }
            }

            return result;
        }

#pragma warning disable S4225 // Extension methods should not extend "object"
        public static string ToJson(this object item, JsonSerializerOptions? options = null)
#pragma warning restore S4225 // Extension methods should not extend "object"
        {
            string result = string.Empty;

            if (item != null)
            {
                result = JsonSerializer.Serialize(item, options ?? DefaultSerializerOptions);
            }

            return result;
        }

#pragma warning disable S4225 // Extension methods should not extend "object"
        public static string ToJson(this object item, Type type, JsonSerializerOptions? options = null)
#pragma warning restore S4225 // Extension methods should not extend "object"
        {
            string result = string.Empty;

            if (item != null)
            {
                result = JsonSerializer.Serialize(item, type, options ?? DefaultSerializerOptions);
            }

            return result;
        }

        private static JsonSerializerOptions CreateDefaultJsonSerializerOptions()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            options.Converters.Add(new JsonStringEnumConverter());

            return options;
        }
    }
}