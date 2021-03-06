using Domain.Interfaces.Services.Base;
using Domain.Models;
using Domain.Models.Enums;
using System.Collections.Generic;

namespace Domain.Interfaces.Services {
    public interface ITransactionService: IBaseService<Transaction> {
        Transaction CreateTransaction(Account account, Operation operation, decimal value);
        IList<Transaction> GetTransactionsByAccount(int accountId);
    }
}