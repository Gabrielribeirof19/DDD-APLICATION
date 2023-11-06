using authentication.domain.entities;
using authentication.Domain.Entities;
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
                pro.Property(p => p.Salt).HasColumnName("Password_Salt").HasColumnType("NVARCHAR").HasMaxLength(120).IsRequired(true);
            });
            builder.OwnsMany(x => x.Addresses, p =>
            {
                p.ToTable("Persons_Addresses");

            });
            builder.OwnsMany(x => x.Email, p =>
            {
                p.ToTable("Persons_Emails");
                p.Ignore(x => x.IsValid);
                p.Ignore(x => x.Notifications);
            });
            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user
                    .HasOne<Person>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade));

            #endregion Person
        }
    }
}