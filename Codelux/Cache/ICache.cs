namespace Codelux.Cache
{
    public interface ICache
    {
        void Flush();
        bool Remove(string key);
        bool TryGet<T>(string key, out T t);
        void Set<T>(string key, T t, ExpirationOptions expirationOptions);
        bool InsertIfNotExists<T>(string key, T value, ExpirationOptions expirationOptions);
        int NumberOfItems { get; }
        int EvictExpired();
    }
}
