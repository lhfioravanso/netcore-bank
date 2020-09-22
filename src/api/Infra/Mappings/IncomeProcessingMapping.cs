using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace Infra.Mappings
{
    public class IncomeProcessingMapping : IEntityTypeConfiguration<IncomeProcessing>
    {
        public void Configure(EntityTypeBuilder<IncomeProcessing> builder)
        {
            builder.ToTable("IncomeProcessing");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ProcessedDate)
                .IsRequired()
                .HasColumnName("ProcessedDate");

        }
    }
}