namespace RickAndMorty.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RickAndMorty.Domain;

    public sealed class GetCharacter : IGet<int, Character>
    {
        public GetCharacter(IGet<IEnumerable<Character>> getAll)
        {
            this.GetAll = getAll;
        }

        public IGet<IEnumerable<Character>> GetAll { get; }

        public async Task<Character?> Get(int key)
        {
            if (key == 66)
            {
                throw new TransientServiceException("This error represents some transient external error that might recover if tried again.");
            }

            var all = (await this.GetAll.Get()).ToArray();

            return all.FirstOrDefault(c => c.Id == key);
        }
    }
}