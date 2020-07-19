namespace RickAndMorty.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using static System.FormattableString;

    internal static class DependencyExtensions
    {
        public static T Activate<T>(this SetupFixture setupFixture)
            where T : class
        {
            return setupFixture.Setup.ServiceProvider.Activate<T>();
        }

        public static T CreateService<T>(this SetupFixture setupFixture)
        {
            return setupFixture.Setup.ServiceProvider.CreateService<T>();
        }

        public static T CreateService<T>(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<T>();
        }

        private static T Activate<T>(this IServiceProvider serviceProvider)
            where T : class
        {
            Type type = typeof(T);
            IEnumerable<ConstructorInfo> constructors = type.GetConstructors().Where(ctor => ctor.IsPublic && !ctor.IsAbstract);

            if (!constructors.Any())
            {
                throw new InvalidOperationException(Invariant($"{type.Name} does not have any public or non-abstract constructors and must be constructed manually."));
            }
            else if (constructors.Count() > 1)
            {
                throw new InvalidOperationException(Invariant($"{type.Name} has multiple constructors and must be constructed manually."));
            }

            ConstructorInfo constructor = constructors.First(ctor => ctor.IsPublic);
            ParameterInfo[] parameters = constructor.GetParameters();

            return (T)constructor.Invoke(parameters.Select(p => serviceProvider.GetRequiredService(p.ParameterType)).ToArray());
        }
    }
}