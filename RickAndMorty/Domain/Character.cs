namespace RickAndMorty.Domain
{
    public sealed class Character
    {
        public Character(
            string catchphrase,
            int id,
            string name)
        {
            this.Catchphrase = catchphrase;
            this.Id = id;
            this.Name = name;
        }

        public string Catchphrase { get; }

        public int Id { get; }

        public string Name { get; }
    }
}