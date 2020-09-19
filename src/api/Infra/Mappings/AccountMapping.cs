using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Infra.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Bank)
                .IsRequired()
                .HasColumnName("Bank");

            builder.Property(c => c.Agency)
                .IsRequired()
                .HasColumnName("Agency");
            
            builder.Property(c => c.Number)
                .IsRequired()
                .HasColumnName("Number");
            
            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");

            builder.Property(c => c.Balance)
                .IsRequired()
                .HasColumnName("Balance");

            builder.Property(c => c.UserId)
                .IsRequired()
                .HasColumnName("UserId");

            builder.HasMany(c => c.Transactions)
                .WithOne(e => e.Account);
        }
    }
}