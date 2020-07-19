namespace RickAndMorty.Services
{
    using System.Threading.Tasks;

    public interface IGet<in TKey, TValue>
        where TValue : class
    {
        Task<TValue?> Get(TKey key);
    }
}