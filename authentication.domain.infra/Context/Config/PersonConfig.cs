using authentication.domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace authentication.domain.Infra.Config
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            Console.WriteLine("PersonConfig.Configure()");

            builder.ToTable("Persons");

            #region Entity
            builder.HasKey(x => x.Id);

            #endregion Entity

            #region Person
            builder.HasIndex(x => x.Cpf).IsUnique();
            builder.OwnsOne(x => x.Password, pro =>
            {
                pro.Property(p => p.Salt).HasColumnName("Password_Salt").HasColumnType("blob(256)");
            });
            builder.OwnsMany(x => x.Addresses, p =>
            {
                p.ToTable("Persons_Addresses");

            });
            builder.OwnsMany(x => x.Emails, p =>
            {
                p.ToTable("Persons_Emails");
            });

            #endregion Person
        }
    }
}