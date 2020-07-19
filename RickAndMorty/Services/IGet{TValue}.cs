namespace RickAndMorty.Services
{
    using System.Threading.Tasks;

    public interface IGet<TValue>
    {
        Task<TValue> Get();
    }
}