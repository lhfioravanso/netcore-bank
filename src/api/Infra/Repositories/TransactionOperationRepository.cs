using Infra.Repositories.Base;
using Infra.Context;
using Domain.Models;
using Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;
using Domain.Models.Enums;

namespace Infra.Repositories
{
    public class TransactionOperationRepository: BaseRepository<TransactionOperation>, ITransactionOperationRepository {

        public TransactionOperationRepository(ApiDbContext context): base(context) {

        }

        public virtual TransactionOperation GetByOperation(Operation operation) {
            return this._context.TransactionOperations.SingleOrDefault(t => t.Operation == operation);
        }
        public virtual IList<TransactionOperation> GetAllByOperationType(OperationType operationType) {
            return this._context.TransactionOperations.Where(t => t.Type == operationType).ToList();
        }

    }

}