using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Infra.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Username)
                .IsRequired()
                .HasColumnName("Username");

            builder.Property(c => c.Password)
                .IsRequired()
                .HasColumnName("Password");
            
            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnName("Name");
            
            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");
        }
    }
}