using authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace authentication.domain.Infra.Config
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120)
                .IsRequired(true);
        }
    }
}