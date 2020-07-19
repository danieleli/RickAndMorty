namespace RickAndMorty.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RickAndMorty.Domain;
    using RickAndMorty.Utils;

    public sealed class GetCharacters : IGet<IEnumerable<Character>>
    {
        public async Task<IEnumerable<Character>> Get()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));

            var characters = await "CharacterMap.json".GetJsonFromManifestResource<IEnumerable<CharacterPoco>>(typeof(GetCharacters));

            if (characters != null)
            {
                return characters
                    .Where(IsValidCharacter)
                    .OrderBy(c => c.Id)
                    .Select(ToDomain);
            }

            return Array.Empty<Character>();
        }

        private static bool IsValidCharacter(CharacterPoco character)
        {
            return character.Id > 0
                   && !string.IsNullOrWhiteSpace(character.Name)
                   && !string.IsNullOrWhiteSpace(character.Catchphrase);
        }

        private static Character ToDomain(CharacterPoco character)
        {
            return new Character(
                character.Catchphrase,
                character.Id,
                character.Name);
        }

        private sealed class CharacterPoco
        {
            public string Catchphrase { get; set; } = string.Empty;

            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;
        }
    }
}