using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;
using Domain.Models.Enums;
using System;

namespace Infra.Mappings
{
    public class TransactionOperationMapping : IEntityTypeConfiguration<TransactionOperation>
    {
        public void Configure(EntityTypeBuilder<TransactionOperation> builder)
        {
            builder.ToTable("TransactionOperation");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).
                ValueGeneratedNever();

            builder.Property(c => c.Operation)
                .IsRequired()
                .HasColumnName("Operation");

            builder.Property(c => c.Type)
                .IsRequired()
                .HasColumnName("Type");

            builder.HasMany(c => c.Transactions)
                .WithOne(e => e.TransactionOperation);
        }
    }
}