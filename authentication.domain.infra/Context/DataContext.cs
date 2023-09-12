using authentication.domain.entities;
using authentication.domain.Infra.Config;
using Microsoft.EntityFrameworkCore;

namespace authentication.domain.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Person>? Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Flunt.Notifications.Notification>();
            modelBuilder.Ignore<IReadOnlyCollection<Flunt.Notifications.Notification>>();
            modelBuilder.ApplyConfiguration<Person>(new PersonConfig());
        }
    }
}