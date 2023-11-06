using authentication.domain.entities;
using authentication.domain.Infra.Contexts;
using authentication.domain.Infra.Utils;
using authentication.Domain.Repositories;

namespace authentication.domain.Infra.Repositories
{
    public class PersonRepository<T> : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(DataContext context) : base(context)
        {

        }

        public async Task<Person?> GetByCpf(string cpf)
        {
            var cpfExists = await Task.Run(() => _context.Persons!.IsActive().Where(x => x.Cpf == cpf).FirstOrDefault());
            Console.WriteLine(cpfExists);
            return cpfExists;
        }

        public async Task<Person?> GetByEmail(string Email)
        {
            return await Task.Run(() => _context.Persons!.IsActive().Where(x => x.Email.Any(y => y.Address == Email)).FirstOrDefault());
        }
    }
}