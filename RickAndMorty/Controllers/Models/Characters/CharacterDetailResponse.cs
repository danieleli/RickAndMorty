namespace RickAndMorty.Controllers.Models.Characters
{
    public sealed class CharacterDetailResponse
    {
        public string Catchphrase { get; set; } = string.Empty;

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}