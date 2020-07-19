namespace RickAndMorty.Controllers.Models.Ping
{
    using System;

    public sealed class GetResponse
    {
        public string ApplicationName { get; set; } = string.Empty;

        public long Timestamp { get; set; }
    }
}