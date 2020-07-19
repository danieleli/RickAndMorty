namespace RickAndMorty.Utils
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RickAndMorty.Domain;
    using RickAndMorty.Services;

    public static class DependencyExtensions
    {
        public static IServiceProvider RegisterRickAndMortyService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IGet<int, Character>, GetCharacter>();
            services.AddSingleton<IGet<IEnumerable<Character>>, GetCharacters>();
            return services.BuildServiceProvider();
        }
    }
}