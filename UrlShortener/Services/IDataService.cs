namespace UrlShortener.Services
{
    public interface IDataService
    {
        void Add<T>(T content);
        T Get<T>();
    }
}