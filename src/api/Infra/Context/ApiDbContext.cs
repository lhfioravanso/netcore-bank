using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infra.Mappings;

namespace Infra.Context {

    public class ApiDbContext: DbContext {

        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TransactionOperation> TransactionOperations { get; set; }
        public DbSet<IncomeProcessing> IncomeProcessings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>(new AccountMapping().Configure);
            modelBuilder.Entity<Transaction>(new TransactionMapping().Configure);
            modelBuilder.Entity<User>(new UserMapping().Configure);
            modelBuilder.Entity<TransactionOperation>(new TransactionOperationMapping().Configure);
            modelBuilder.Entity<IncomeProcessing>(new IncomeProcessingMapping().Configure);

            modelBuilder.Seed();
        }
    }
}