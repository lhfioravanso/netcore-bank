using Domain.Interfaces.Repositories.Base;
using Domain.Models;
using System.Collections.Generic;

namespace Domain.Interfaces.Repositories
{
    public interface ITransactionRepository: IBaseRepository<Transaction> {
        IList<Transaction> GetTransactionsByAccount(int accountId);
    }
}