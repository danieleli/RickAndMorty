namespace RickAndMorty.Controllers.Models.Characters
{
    using System;
    using System.Collections.Generic;

    public sealed class GetAllResponse
    {
        public IEnumerable<CharacterSummaryResponse> Characters { get; set; } = Array.Empty<CharacterSummaryResponse>();
    }
}