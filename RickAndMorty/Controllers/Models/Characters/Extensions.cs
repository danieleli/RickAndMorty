namespace RickAndMorty.Controllers.Models.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using RickAndMorty.Domain;

    public static class Extensions
    {
        public static GetAllResponse ToGetAllResponse(this IEnumerable<Character> characters)
        {
            return new GetAllResponse
            {
                Characters = characters.Select(ToCharacterSummaryResponse)
            };
        }

        public static CharacterDetailResponse ToCharacterDetailResponse(this Character character)
        {
            return new CharacterDetailResponse
            {
                Catchphrase = character.Catchphrase,
                Id = character.Id,
                Name = character.Name
            };
        }

        public static CharacterSummaryResponse ToCharacterSummaryResponse(this Character character)
        {
            return new CharacterSummaryResponse
            {
                Id = character.Id,
                Name = character.Name
            };
        }
    }
}