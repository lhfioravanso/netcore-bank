using Domain.Interfaces.Repositories.Base;
using Domain.Models;
using Domain.Models.Enums;
using System.Collections.Generic;

namespace Domain.Interfaces.Repositories
{
    public interface ITransactionOperationRepository: IBaseRepository<TransactionOperation> {
        TransactionOperation GetByOperation(Operation operation);
        IList<TransactionOperation> GetAllByOperationType(OperationType operationType);
    }
}