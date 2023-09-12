using authentication.domain.entities;

namespace authentication.Domain.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person?> GetByCpf(string cpf);
    }
}