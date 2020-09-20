using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Models.Enums;

namespace Infra.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionOperation>().HasData(
                new TransactionOperation { Id = 1, Operation = Operation.Deposit, Type = OperationType.Credit},
                new TransactionOperation { Id = 2, Operation = Operation.Withdraw, Type = OperationType.Debit},
                new TransactionOperation { Id = 3, Operation = Operation.Payment, Type = OperationType.Debit}
            );
        }
    }
}