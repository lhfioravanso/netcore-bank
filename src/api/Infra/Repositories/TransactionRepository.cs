using Infra.Repositories.Base;
using Infra.Context;
using Domain.Models;
using Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class TransactionRepository: BaseRepository<Transaction>, ITransactionRepository {

        public TransactionRepository(ApiDbContext context): base(context) {

        }

        public virtual IList<Transaction> GetTransactionsByAccount(int accountId) {
            return this._context.Transactions.Where(transaction => transaction.AccountId == accountId).ToList();
        }

    }

}