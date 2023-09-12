using authentication.domain.entities;

namespace authentication.Domain.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<bool> Health();

        Task Create(IEnumerable<T> entities);
        Task Delete(IEnumerable<T> entities, bool permanent = false);
        Task Update(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetById(IEnumerable<Guid>? ids = null, bool track = true, bool active = true);
        async Task Create(T entity) => await Create(new[] { entity });
        async Task Delete(T entity, bool permanent = false) => await Delete(new[] { entity }, permanent);
        async Task Update(T entity) => await Update(new[] { entity });
        async Task<T?> GetById(Guid id, bool track = true, bool active = true) => (await GetById(new[] { id }, track, active)).FirstOrDefault();
    }
}