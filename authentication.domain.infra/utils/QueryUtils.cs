using authentication.domain.entities;
using authentication.Domain.Enums;

namespace authentication.domain.Infra.Utils
{
    public static class QueryUtils
    {
        public static IQueryable<T> IsActive<T>(this IQueryable<T> source) where T : Entity =>
            source.Where(x => x.Status.HasFlag(EStatus.Active));

    }
}