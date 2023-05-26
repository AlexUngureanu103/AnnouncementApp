namespace AnnouncementsAPI.Services.Abstractions
{
    public interface ICollectionService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<bool> Create(T model);

        Task<bool> Update(Guid id, T model);

        Task<bool> Delete(Guid id);
    }
}
