using Infra.Repositories.Base;
using Infra.Context;
using Domain.Models;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class TransactionRepository: BaseRepository<Transaction>, ITransactionRepository {

        public TransactionRepository(ApiDbContext context): base(context) {

        }

    }

}