using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Infra.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.AccountId)
                .IsRequired()
                .HasColumnName("AccountId");

            builder.Property(c => c.Value)
                .IsRequired()
                .HasColumnName("Value");
            
            builder.Property(c => c.TransactionOperationId)
                .IsRequired()
                .HasColumnName("TransactionOperationId");
            
            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");

            builder.HasOne(c => c.Account)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ACCOUNT_TRANSACTION");
        }
    }
}