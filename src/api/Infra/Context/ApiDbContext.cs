using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Models;
using Infra.Mappings;


namespace Infra.Context {

    public class ApiDbContext: DbContext {

        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>(new AccountMapping().Configure);
            modelBuilder.Entity<Transaction>(new TransactionMapping().Configure);
        }
    }
}