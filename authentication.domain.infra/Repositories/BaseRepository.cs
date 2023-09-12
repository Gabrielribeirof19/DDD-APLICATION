using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using authentication.domain.entities;
using authentication.domain.Infra.Contexts;
using authentication.Domain.Enums;
using authentication.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace authentication.domain.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context) => _context = context;

        public async Task<bool> Health() => await _context.Database.CanConnectAsync();

        public async Task Create(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                entity.SetModified();
            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(IEnumerable<T> entities, bool permanent = false)
        {
            if (permanent)
            {
                var dbSet = _context.Set<T>();
                if (dbSet is null)
                    throw new NotImplementedException($"BaseRepository<{typeof(T).Name}>");
                dbSet.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
            else
            {
                foreach (var entity in entities)
                    entity.SetStatus(EStatus.Inactive);
                await Update(entities);
            }
        }

        public async Task<IEnumerable<T>> GetById(IEnumerable<Guid>? ids = null, bool track = true, bool active = true)
        {
            if (ids == null || !ids.Any()) return new List<T>();
            var dbSet = _context.Set<T>();
            if (dbSet == null) throw new NotImplementedException($"BaseRepository<{typeof(T).Name}>");
            Expression<Func<T, bool>> predicate = active ? x => x.Status == EStatus.Active && ids.Contains(x.Id) : x => ids.Contains(x.Id);
            var entities = track ? dbSet : dbSet.AsNoTracking();
            return await entities.Where(predicate).OrderByDescending(x => x.Created).ToListAsync();
        }
    }
}